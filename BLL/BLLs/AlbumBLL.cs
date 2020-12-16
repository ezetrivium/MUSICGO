using BE.Entities;
using DAL.Mappers;
using Newtonsoft.Json;
using SL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utilities;
using Utilities.Exceptions;
using ViewModels.ViewModels;

namespace BLL.BLLs
{
    public class AlbumBLL : BaseBLL<AlbumBE, AlbumViewModel>
    {

        private readonly string[] formats = { ".jpg", ".jpeg" };
        public AlbumBLL()
        {
            this.Dal = new AlbumDAL();
           
        }



        public override Guid Add(AlbumViewModel viewModel)
        {
            try
            {

                if (this.IsValid(viewModel))
                {
                    AlbumBE entity;
                    entity = Mapper.Map<AlbumViewModel, AlbumBE>(viewModel);
                    BinnacleSL binnacleSL = new BinnacleSL();
                    if (!ValidImage(viewModel))
                    {
                        throw new BusinessException(Messages.InvalidImageFormat);

                    }

                    var guid = Guid.NewGuid().ToString();
                    string path = FileUtils.GetRepoImagePath(guid + Path.GetExtension(viewModel.File.FileName));
                    viewModel.File.SaveAs(path);

                    entity.ImgKey = guid + Path.GetExtension(viewModel.File.FileName);


                    Guid result = this.Dal.Add(entity);

                    if (result != Guid.Empty)
                    {
                        var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                        string userObj = identityClaims.FindFirst("userObject").Value;
                        var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);

                        binnacleSL.AddBinnacle(new BinnacleBE()
                        {
                            User = userLogged,
                            Description = "Add Album",
                        });
                        return result;
                    }
                    throw new BusinessException(Messages.ErrorAddAlbum);
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



        public override bool Update(AlbumViewModel viewModel)
        {
            try
            {

                if (this.IsValid(viewModel))
                {
                    AlbumBE entity;
                    entity = Mapper.Map<AlbumViewModel, AlbumBE>(viewModel);
                    BinnacleSL binnacleSL = new BinnacleSL();

                    if (!ValidImage(viewModel))
                    {
                        throw new BusinessException(Messages.InvalidImageFormat);
                        
                    }

                   
                    if(viewModel.File != null)
                    {
                        var guid = Guid.NewGuid().ToString();

                        string path = FileUtils.GetRepoImagePath(guid + Path.GetExtension(viewModel.File.FileName));
                        viewModel.File.SaveAs(path);

                        entity.ImgKey = guid + Path.GetExtension(viewModel.File.FileName);

                    }



                    bool result = this.Dal.Update(entity);

                    if (result)
                    {
                        var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                        string userObj = identityClaims.FindFirst("userObject").Value;
                        var userLogged = JsonConvert.DeserializeObject<UserBE>(userObj);

                        binnacleSL.AddBinnacle(new BinnacleBE()
                        {
                            User = userLogged,
                            Description = "Update Album",
                        });
                        return result;
                    }
                    throw new BusinessException(Messages.ErrorUpdateAlbum);
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
                throw new BusinessException(Messages.ErrorDeleteAlbum);

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


        public UserViewModel GetUserAlbums(UserViewModel user)
        {
            try
            {
                AlbumDAL AlbumDAL = new AlbumDAL();
                IList<AlbumBE> entities;
                UserBE userBE = Mapper.Map<UserViewModel, UserBE>(user);
                entities = AlbumDAL.GetUserAlbums(userBE);

                userBE.Albums = entities.ToList();
                var uvm = Mapper.Map<UserBE, UserViewModel>(userBE);

                foreach(var vm in uvm.Albums)
                {
                    var file = FileUtils.GetImageBytes(FileUtils.GetRepoImagePath(vm.ImgKey));
                    vm.ImageBase64 = "data:image/jpg;base64," + Convert.ToBase64String(file);
                }
                return uvm;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }



        protected bool ValidImage(AlbumViewModel viewModel)
        {
            if (viewModel.File != null)
            {
                if (this.formats.Contains(Path.GetExtension(viewModel.File.FileName.ToLower())) && viewModel.File.ContentLength <= (1024 * 1024 * 5))
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



        protected override bool IsValid(AlbumViewModel viewModel)
        {
            if (viewModel.Name.Length > 0 && viewModel.Name.Length < 101)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
