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

namespace DAL.Mappers
{
    public class AlbumDAL : IDAL<AlbumBE>
    {
        public Guid Add(AlbumBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();


                var albumguid = Guid.NewGuid();
                parameters = new SqlParameter[4];

                parameters[0] = dbContext.CreateParameters("@name", entity.Name);
                parameters[1] = dbContext.CreateParameters("@imgKey", entity.ImgKey);
                parameters[2] = dbContext.CreateParameters("@albumID", albumguid);
                parameters[3] = dbContext.CreateParameters("@userID", entity.User.Id);



                if (dbContext.Write("AddAlbum", parameters) > 0)
                {
                    return albumguid;

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

                parameters[0] = dbContext.CreateParameters("@albumID", id);

                if (dbContext.Write("DeleteAlbum", parameters) > 0)
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

        public IList<AlbumBE> GetUserAlbums(UserBE user)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<AlbumBE> albums = new List<AlbumBE>();
                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@userID", user.Id);

                dataSet = dbContext.Read("GetUserAlbums", parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        if (!albums.Exists(a=>a.Id.ToString() == dr["AlbumID"].ToString()))
                        {
                            AlbumBE album = new AlbumBE()
                            {
                                Id = Helper.GetGuidDB(dr["AlbumID"]),
                                Name = Helper.GetStringDB(dr["Name"]),
                                UploadDate = Helper.GetDateTimeDB(dr["UploadDate"]),
                                ImgKey = Helper.GetStringDB(dr["ImgKey"]),
                                User = new UserBE()
                                {
                                    Id = Helper.GetGuidDB(dr["UserID"])
                                },

                            };
                            album.Songs = new List<SongBE>();
                            foreach (DataRow drs in dataSet.Tables[0].Rows)
                            {
                                if (Helper.GetGuidDB(drs["SongID"]) != Guid.Empty && drs["AlbumID"].ToString() == dr["AlbumID"].ToString())
                                {
                                    
                                    album.Songs.Add(new SongBE()
                                    {
                                        Id = Helper.GetGuidDB(drs["SongID"]),
                                        Name = Helper.GetStringDB(drs["SongName"]),
                                    });
                                }
                            }
                            albums.Add(album);
                        }
                        
                    }

                }


                return albums;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public IList<AlbumBE> Get()
        {
            throw new NotImplementedException();
        }

        public AlbumBE GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(AlbumBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();
                PermissionDAL permissionDAL = new PermissionDAL();

                parameters = new SqlParameter[3];

                parameters[0] = dbContext.CreateParameters("@name", entity.Name);
                parameters[1] = dbContext.CreateParameters("@imgKey", entity.ImgKey);
                parameters[2] = dbContext.CreateParameters("@albumID", entity.Id);



                if (dbContext.Write("UpdateAlbum", parameters) > 0)
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
