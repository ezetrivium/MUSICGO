using BE.Entities;
using DAL.Mappers;
using ManagedBass;
using Newtonsoft.Json;
using SL;
using Spectrogram;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utilities;
using Utilities.Exceptions;
using ViewModels;
using ViewModels.ViewModels;

namespace BLL.BLLs
{
    public class SongBLL : BaseBLL<SongBE, SongViewModel>
    {
        public SongBLL()
        {
            this.Dal = new SongDAL();
        }
        private readonly string[] formats = { ".wav" };
        public override Guid Add(SongViewModel viewModel)
        {
            try
            {

                if (this.IsValid(viewModel))
                {
                    SongBE entity;
                    entity = Mapper.Map<SongViewModel, SongBE>(viewModel);
                    BinnacleSL binnacleSL = new BinnacleSL();
                    if (!ValidFile(viewModel))
                    {
                        throw new BusinessException(Messages.InvalidFileFormat);
                        
                    }

                    var guid = Guid.NewGuid().ToString();
                    string path = FileUtils.GetRepoMusicPath(guid + Path.GetExtension(viewModel.File.FileName));
                    viewModel.File.SaveAs(path);

                    entity.SongKey = guid + Path.GetExtension(viewModel.File.FileName);

                
                    Guid result = this.Dal.Add(entity);

                    if(result != Guid.Empty)
                    {
                        var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                        string userObj = identityClaims.FindFirst("userObject").Value;
                        var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);

                        binnacleSL.AddBinnacle(new BinnacleBE()
                        {
                            User = userLogged,
                            Description = "Add Song",
                        });
                        return result;
                    }
                    throw new BusinessException(Messages.ErrorAddSong);
                }
                else
                {
                    throw new Exception(Messages.Generic_Error);
                }
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public List<float> GetHash(Bitmap bmpSource)
        {
            List<float> lResult = new List<float>();
            //create new image with 16x16 pixel
            Bitmap bmpMin = new Bitmap(bmpSource,new Size(600,200));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true / false                
                    //lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness());
                }
            }
            return lResult;
        }


        public IList<SongViewModel> GetReport()
        {
            try
            {
                SongDAL songDAL = new SongDAL();
                var svm = Mapper.Map<SongBE,SongViewModel>(songDAL.GetSongsReport());

                return svm;
            }
            catch(Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }

        public SuccessViewModel CalculateSuccess(SongViewModel song)
        {
            try
            {
                SongBE entity;
                entity = Mapper.Map<SongViewModel, SongBE>(song);
                SongDAL songDAL = new SongDAL();
                SuccessViewModel successViewModel = new SuccessViewModel();


                var listSongs = songDAL.GetSongsToCalculate(entity.Category);


                (int sampleRate, double[] audio) = WavFile.ReadMono(FileUtils.GetRepoMusicPath(song.SongKey));


                var spec = new Spectrogram.Spectrogram(sampleRate / 2, fftSize: (16384 / 8), stepSize: (2500 * 5), maxFreq: 2200);
                spec.Add(audio);
                var tempPath = Path.GetTempPath();
                spec.SaveImage(tempPath + "/" + song.SongKey + ".jpg", intensity: 5, dB: true);

                var file = FileUtils.GetImageBytes(tempPath + "/" + song.SongKey + ".jpg");
                successViewModel.ImageBase64 = "data:image/jpg;base64," + Convert.ToBase64String(file);

                var bmHash = this.GetHash(spec.GetBitmap());





                List<Spectrogram.Spectrogram> spectrograms = new List<Spectrogram.Spectrogram>();

                foreach (var son in listSongs)
                {


                    (int sampleRateSong, double[] audioSong) = WavFile.ReadMono(FileUtils.GetRepoMusicPath(son.SongKey));
                    var specSong = new Spectrogram.Spectrogram(sampleRateSong / 2, fftSize: (16384 / 8), stepSize: (2500 * 5), maxFreq: 2200);
                    specSong.Add(audioSong);
                    spectrograms.Add(specSong);
                }

                int equalElements = 0;

                foreach (var sp in spectrograms)
                {

                    equalElements += bmHash.Zip(this.GetHash(sp.GetBitmap()), (i, j) => i == j).Count(eq => eq);
                }

                var con = Convert.ToInt32(equalElements / spectrograms.Count);
                successViewModel.Percentage = Convert.ToInt32((con * 100) / bmHash.Count);
                return successViewModel;
            }
            catch(Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
            
        }


        public IList<SongViewModel> GetSongsToPlay(UserViewModel user)
        {
            try
            {
                var userbe = Mapper.Map<UserViewModel, UserBE>(user);
                VoteBLL voteBLL = new VoteBLL();

                var votes = voteBLL.GetUserVotes(user);

                var songs = new List<SongBE>();

                if (votes.Count > 10)
                {
                    var votesCategories = from v in votes
                                          group v by v.Song.Category.Id into g
                                          select new
                                          {
                                              Category = g.Key,
                                              songsCount = g.Count(),
                                              votes = g.ToList()
                                          };


                    var maxCategory = votesCategories.OrderByDescending(vc => vc.songsCount).Take(2);


                    var categoriesSongs = new List<SongBE>();

                    foreach (var song in maxCategory)
                    {

                        categoriesSongs.AddRange(this.GetCategorySongs(new CategoryBE() { Id = song.Category },userbe));
                    }

                    categoriesSongs.AddRange(this.GetSimilarVotesSongs(userbe));

                    categoriesSongs = categoriesSongs.GroupBy(x => x.Id)
                        .Where(x => x.Count() >= 1)
                        .Select(x => x.First()).ToList();


                   

                    if(categoriesSongs.Count > 50)
                    {
                        songs.AddRange(categoriesSongs.OrderByDescending(cs => cs.Playbacks).Take(20));

                        var csongs = categoriesSongs.OrderByDescending(cs => cs.Playbacks).ToList();

                        csongs.RemoveRange(0,2);


                        System.Random rnd = new System.Random();

                        for (int i = 1; i <= 30; i++)
                        {
                            
                            int index = rnd.Next(csongs.Count);
                            if (!songs.Contains(csongs[index]))
                            {
                                songs.Add(csongs[index]);
                            }
                                
                        }
                    }
                    else
                    {
                        songs = categoriesSongs;
                    }


                    if (songs.Count <= 10)
                    {
                        songs = this.GetRandomSongs(userbe).ToList();
                    }

      


                }
                else
                {
                    songs = this.GetRandomSongs(userbe).ToList();

                    
                }

                var songsvm = Mapper.Map<SongBE, SongViewModel>(songs);

                foreach (var song in songsvm)
                {
                    var file = FileUtils.GetImageBytes(FileUtils.GetRepoImagePath(song.Album.ImgKey));
                    song.Album.ImageBase64 = "data:image/jpg;base64," + Convert.ToBase64String(file);
                    
                }
                

                return songsvm;
            }
            catch(Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
            



            


        }


        private IList<SongBE> GetCategorySongs(CategoryBE category,UserBE user)
        {
            SongDAL songDAL = new SongDAL();
            return songDAL.GetCategorySongs(category, user);
        }

        private IList<SongBE> GetRandomSongs(UserBE user)
        {
            SongDAL songDAL = new SongDAL();
            return songDAL.GetRandomSongs(user);
        }


        public IList<SongViewModel> GetSongsVoted(UserViewModel user)
        {
            SongDAL songDAL = new SongDAL();
            var userbe = Mapper.Map<UserViewModel, UserBE>(user);

            var svm = Mapper.Map<SongBE, SongViewModel>(songDAL.GetSongsVoted(userbe));

            foreach (var song in svm)
            {
                var file = FileUtils.GetImageBytes(FileUtils.GetRepoImagePath(song.Album.ImgKey));
                song.Album.ImageBase64 = "data:image/jpg;base64," + Convert.ToBase64String(file);

            }
            return svm;
        }



        private IList<SongBE> GetSimilarVotesSongs(UserBE user)
        {
            SongDAL songDAL = new SongDAL();
            return songDAL.GetSimilarVotesSongs(user);
        }


        public override bool Update(SongViewModel viewModel)
        {
            try
            {

                if (this.IsValid(viewModel))
                {
                    SongBE entity;
                    entity = Mapper.Map<SongViewModel, SongBE>(viewModel);
                    BinnacleSL binnacleSL = new BinnacleSL();



                    bool result = this.Dal.Update(entity);

                    if (result)
                    {
                        var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                        string userObj = identityClaims.FindFirst("userObject").Value;
                        var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);

                        binnacleSL.AddBinnacle(new BinnacleBE()
                        {
                            User = userLogged,
                            Description = "Update Song",
                        });
                        return result;
                    }
                    throw new BusinessException(Messages.ErrorUpdateSong);
                }
                else
                {
                    throw new Exception(Messages.Generic_Error);
                }

            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }

     
        public void CountPlayback(SongViewModel song)
        {
            SongDAL songDAL = new SongDAL();
            var songBE= songDAL.GetById(song.Id);

            var count = CacheManager.GetWithTimeout("songvoted" + songBE.Id + "-" + songBE.User.Id);

            if (count == null || Convert.ToInt32(count) < 5)
            {
                if(count == null)
                {
                    CacheManager.SetWithTimeout("songvoted" + songBE.Id + "-" + songBE.User.Id, 0, TimeSpan.FromDays(7));
                }
                else
                {
                    CacheManager.Set("songvoted" + songBE.Id + "-" + songBE.User.Id, Convert.ToInt32(CacheManager.Get("songvoted" + songBE.Id + "-" + songBE.User.Id)) + 1);
                }

                if (songDAL.CountPlayback(songBE))
                {
                    DVVerifier dVVerifier = new DVVerifier();
                    UserDAL userdal = new UserDAL();
                    var userDVH = userdal.GetDVHEntity(songBE.User.Id);
                    userDVH.DVH = dVVerifier.DVHCalculate(userDVH);
                    userdal.SetDVH(userDVH);
                    dVVerifier.DVCalculate("UserDAL");
                }
            }
            
        }

        public override bool Delete(Guid id)
        {
            try
            {
                bool result;
                result = this.Dal.Delete(id);

                if (result)
                {
                    return result;
                }
                throw new BusinessException(Messages.ErrorDeleteSong);
                
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }

        }




        public UserViewModel GetUserSongs(UserViewModel user)
        {
            try
            {
                SongDAL songDAL = new SongDAL();
                IList<SongBE> entities;
                UserBE userBE = Mapper.Map<UserViewModel, UserBE>(user);
                entities = songDAL.GetUserSongs(userBE);
                userBE.Songs = entities.ToList();
                return Mapper.Map<UserBE, UserViewModel>(userBE);
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        protected override bool IsValid(SongViewModel viewModel)
        { 
            if(viewModel.Name.Length > 0 && viewModel.Name.Length < 101 &&
               viewModel.Category.Id != Guid.Empty &&
               viewModel.Album.Id != Guid.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool ValidFile(SongViewModel viewModel)
        {
            if (viewModel.File != null)
            {
                if (this.formats.Contains(Path.GetExtension(viewModel.File.FileName.ToLower())) && viewModel.File.ContentLength <= (1024 * 1024 * 100))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


    }
}
