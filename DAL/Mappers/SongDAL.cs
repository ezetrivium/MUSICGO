using BE.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Exceptions;

namespace DAL.Mappers
{
    public class SongDAL : IDAL<SongBE>
    {
        public Guid Add(SongBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();


                var songguid = Guid.NewGuid();
                parameters = new SqlParameter[6];

                parameters[0] = dbContext.CreateParameters("@songID", songguid);
                parameters[1] = dbContext.CreateParameters("@name", entity.Name);
                parameters[2] = dbContext.CreateParameters("@songKey", entity.SongKey);
                parameters[3] = entity.Album == null || entity.Album.Id == Guid.Empty ? (dbContext.CreateNullParameters("@albumID")) : dbContext.CreateParameters("@albumID", entity.Album.Id);
                parameters[4] = dbContext.CreateParameters("@categoryID", entity.Category.Id);
                parameters[5] = dbContext.CreateParameters("@userID", entity.User.Id);


                if (dbContext.Write("AddSong", parameters) > 0)
                {
                    return songguid;

                }

                return Guid.Empty;
            }
            catch (Exception ex)
            {
               
                throw new Exception(Messages.Generic_Error);
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();

                parameters = new SqlParameter[1];

                parameters[0] = dbContext.CreateParameters("@songID", id);

                if (dbContext.Write("DeleteSong", parameters) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }

        public IList<SongBE> Get()
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<SongBE> songs = new List<SongBE>();

                dataSet = dbContext.Read("GetSongs", null);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        songs.Add(new SongBE()
                        {
                            Id = Helper.GetGuidDB(dr["SongID"]),
                            Name = Helper.GetStringDB(dr["Name"]),
                            UploadDate = Helper.GetDateTimeDB(dr["UploadDate"]),
                            Playbacks = Helper.GetIntDB(dr["Playbacks"]),
                        });
                    }

                }


                return songs;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }




        public IList<SongBE> GetUserSongs(UserBE user)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<SongBE> songs = new List<SongBE>();
                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@userID", user.Id);

                dataSet = dbContext.Read("GetUserSongs", parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        songs.Add(new SongBE()
                        {
                            Id = Helper.GetGuidDB(dr["SongID"]),
                            Name = Helper.GetStringDB(dr["Name"]),
                            UploadDate = Helper.GetDateTimeDB(dr["UploadDate"]),
                            Playbacks = Helper.GetIntDB(dr["Playbacks"]),
                        });
                    }

                }


                return songs;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public IList<SongBE> GetCategorySongs(CategoryBE category, UserBE user)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<SongBE> songs = new List<SongBE>();
                var parameters = new SqlParameter[2];
                parameters[0] = dbContext.CreateParameters("@categoryID", category.Id);
                parameters[1] = dbContext.CreateParameters("@userID", user.Id);

                dataSet = dbContext.Read("GetCategorySongs", parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        var song = new SongBE()
                        {
                            Id = Helper.GetGuidDB(dr["SongID"]),
                            Name = Helper.GetStringDB(dr["Name"]),
                            UploadDate = Helper.GetDateTimeDB(dr["UploadDate"]),
                            Playbacks = Helper.GetIntDB(dr["Playbacks"]),
                            SongKey = Helper.GetStringDB(dr["SongKey"])
                        };

                        if (Helper.GetGuidDB(dr["AlbumID"]) != Guid.Empty)
                        {
                            song.Album = new AlbumBE()
                            {
                                Id = Helper.GetGuidDB(dr["AlbumID"]),
                                Name = Helper.GetStringDB(dr["AlbumName"]),
                                ImgKey = Helper.GetStringDB(dr["ImgKey"])
                            };
                        };

                        song.User = new UserBE()
                        {
                            Id = Helper.GetGuidDB(dr["UserID"]),
                            UserName = Helper.GetStringDB(dr["ArtistName"])
                        };


                        songs.Add(song);
                    }

                }


                return songs;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public bool CountPlayback(SongBE song)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();

                parameters = new SqlParameter[2];

                parameters[0] = dbContext.CreateParameters("@songID", song.Id);
                parameters[1] = dbContext.CreateParameters("@userID", song.User.Id);

                if (dbContext.Write("CountPlayback", parameters) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public IList<SongBE> GetSimilarVotesSongs(UserBE user)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<SongBE> songs = new List<SongBE>();
                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@userID", user.Id);

                dataSet = dbContext.Read("GetSimilarVotes", parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        var song = new SongBE()
                        {
                            Id = Helper.GetGuidDB(dr["SongID"]),
                            Name = Helper.GetStringDB(dr["Name"]),
                            UploadDate = Helper.GetDateTimeDB(dr["UploadDate"]),
                            Playbacks = Helper.GetIntDB(dr["Playbacks"]),
                            SongKey = Helper.GetStringDB(dr["SongKey"])
                        };

                        if (Helper.GetGuidDB(dr["AlbumID"]) != Guid.Empty)
                        {
                            song.Album = new AlbumBE()
                            {
                                Id = Helper.GetGuidDB(dr["AlbumID"]),
                                Name = Helper.GetStringDB(dr["AlbumName"]),
                                ImgKey = Helper.GetStringDB(dr["ImgKey"])
                            };
                        };

                        song.User = new UserBE()
                        {
                            Id = Helper.GetGuidDB(dr["UserID"]),
                            UserName = Helper.GetStringDB(dr["ArtistName"])
                        };


                        songs.Add(song);
                    }

                }


                return songs;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }

        public IList<SongBE> GetRandomSongs(UserBE user)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<SongBE> songs = new List<SongBE>();

                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@userID", user.Id);

                dataSet = dbContext.Read("GetRandomSongs", parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        var song = new SongBE()
                        {
                            Id = Helper.GetGuidDB(dr["SongID"]),
                            Name = Helper.GetStringDB(dr["Name"]),
                            UploadDate = Helper.GetDateTimeDB(dr["UploadDate"]),
                            Playbacks = Helper.GetIntDB(dr["Playbacks"]),
                            SongKey = Helper.GetStringDB(dr["SongKey"])
                        };

                        if (Helper.GetGuidDB(dr["AlbumID"]) != Guid.Empty)
                        {
                            song.Album = new AlbumBE()
                            {
                                Id = Helper.GetGuidDB(dr["AlbumID"]),
                                Name = Helper.GetStringDB(dr["AlbumName"]),
                                ImgKey = Helper.GetStringDB(dr["ImgKey"])
                            };
                        };

                        song.User = new UserBE()
                        {
                            Id = Helper.GetGuidDB(dr["UserID"]),
                            ArtistName = Helper.GetStringDB(dr["ArtistName"])
                        };


                        songs.Add(song);
                    }

                }


                return songs;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }



        public IList<SongBE> GetSongsReport()
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<SongBE> songs = new List<SongBE>();

      

                dataSet = dbContext.Read("GetSongsReport", null);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        var song = new SongBE()
                        {
                            Id = Helper.GetGuidDB(dr["SongID"]),
                            Name = Helper.GetStringDB(dr["Name"]),
                            UploadDate = Helper.GetDateTimeDB(dr["UploadDate"]),
                            Playbacks = Helper.GetIntDB(dr["Playbacks"]),
                            VotesCount = Helper.GetIntDB(dr["Count"])
                        };

                        if (Helper.GetGuidDB(dr["AlbumID"]) != Guid.Empty)
                        {
                            song.Album = new AlbumBE()
                            {
                                Id = Helper.GetGuidDB(dr["AlbumID"]),
                                Name = Helper.GetStringDB(dr["AlbumName"]),
                            };
                        };

                        song.User = new UserBE()
                        {
                            Id = Helper.GetGuidDB(dr["UserID"]),
                            ArtistName = Helper.GetStringDB(dr["ArtistName"])
                        };

                        song.Category = new CategoryBE()
                        {
                            Id = Helper.GetGuidDB(dr["CategoryID"]),
                            Name = Helper.GetStringDB(dr["CategoryName"])
                        };



                        songs.Add(song);
                    }

                }


                return songs;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public IList<SongBE> GetSongsToCalculate(CategoryBE category)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<SongBE> songs = new List<SongBE>();

                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@categoryID", category.Id);

                dataSet = dbContext.Read("GetSongsToCalculate", parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        var song = new SongBE()
                        {
                            Id = Helper.GetGuidDB(dr["SongID"]),
                            Name = Helper.GetStringDB(dr["Name"]),
                            UploadDate = Helper.GetDateTimeDB(dr["UploadDate"]),
                            Playbacks = Helper.GetIntDB(dr["Playbacks"]),
                            SongKey = Helper.GetStringDB(dr["SongKey"])
                        };



                        songs.Add(song);
                    }

                }


                return songs;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public IList<SongBE> GetSongsVoted(UserBE user)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<SongBE> songs = new List<SongBE>();

                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@userID", user.Id);

                dataSet = dbContext.Read("GetSongsVoted", parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        var song = new SongBE()
                        {
                            Id = Helper.GetGuidDB(dr["SongID"]),
                            Name = Helper.GetStringDB(dr["Name"]),
                            UploadDate = Helper.GetDateTimeDB(dr["UploadDate"]),
                            Playbacks = Helper.GetIntDB(dr["Playbacks"]),
                            SongKey = Helper.GetStringDB(dr["SongKey"])
                        };

                        if (Helper.GetGuidDB(dr["AlbumID"]) != Guid.Empty)
                        {
                            song.Album = new AlbumBE()
                            {
                                Id = Helper.GetGuidDB(dr["AlbumID"]),
                                Name = Helper.GetStringDB(dr["AlbumName"]),
                                ImgKey = Helper.GetStringDB(dr["ImgKey"])
                            };
                        };

                        song.User = new UserBE()
                        {
                            Id = Helper.GetGuidDB(dr["UserID"]),
                            ArtistName = Helper.GetStringDB(dr["ArtistName"])
                        };


                        songs.Add(song);
                    }

                }


                return songs;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }



        public SongBE GetById(Guid id)
        {
            try
            {
                SongBE song = new SongBE();
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@songID", id);

                dataSet = dbContext.Read("GetSongByID", parameters);


                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dataSet.Tables[0].Rows[0];
                    song.Id = Helper.GetGuidDB(dr["SongID"]);
                    song.Name = Helper.GetStringDB(dr["Name"]);
                    song.SongKey = Helper.GetStringDB(dr["SongKey"]);
                    song.Playbacks = Helper.GetIntDB(dr["Playbacks"]);
                    song.UploadDate = Helper.GetDateTimeDB(dr["UploadDate"]);
                    song.User = new UserBE()
                    {
                        Id = Helper.GetGuidDB(dr["UserID"]),
                        UserName = Helper.GetStringDB(dr["UserName"])
                    };

                    if (Helper.GetGuidDB(dr["AlbumID"]) != Guid.Empty)
                    {
                        song.Album = new AlbumBE()
                        {
                            Id = Helper.GetGuidDB(dr["AlbumID"]),
                            Name = Helper.GetStringDB(dr["AlbumName"])
                        };
                    };

                    song.Category = new CategoryBE()
                    {
                        Id = Helper.GetGuidDB(dr["CategoryID"]),
                        Name = Helper.GetStringDB(dr["CategoryName"])
                    };
                    

                }
                else
                {
                    throw new BusinessException(Messages.InvalidDataSongID);
                }

                return song;
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

        public bool Update(SongBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();
                PermissionDAL permissionDAL = new PermissionDAL();

                parameters = new SqlParameter[4];

                parameters[0] = dbContext.CreateParameters("@songID", entity.Id);
                parameters[1] = dbContext.CreateParameters("@name", entity.Name);
                parameters[2] = entity.Album == null ? (dbContext.CreateNullParameters("@albumID")) : dbContext.CreateParameters("@albumID",entity.Album.Id);
                parameters[3] = dbContext.CreateParameters("@categoryID", entity.Category.Id);



                if (dbContext.Write("UpdateSong", parameters) > 0)
                {

                    return true;

                }

                return false;
            }
            catch (Exception ex)
            {

                throw new Exception(Messages.Generic_Error);
            }
        }
    }
}
