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
    public class ClaimDAL : IDAL<ClaimBE>
    {
        public Guid Add(ClaimBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();


                var claimguid = Guid.NewGuid();
                parameters = new SqlParameter[5];

                parameters[0] = dbContext.CreateParameters("@claimID", claimguid);
                parameters[1] = dbContext.CreateParameters("@userID", entity.User.Id);
                parameters[2] = dbContext.CreateParameters("@songClaimed", entity.SongClaimed.Id);
                parameters[3] = dbContext.CreateParameters("@stateID", entity.State.Id);
                
                parameters[4] = dbContext.CreateParameters("@description", entity.Description);




                if (dbContext.Write("AddClaim", parameters) > 0)
                {
                    return claimguid;

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
            throw new NotImplementedException();
        }

        public IList<ClaimBE> Get()
        {
            try
            {

                var dbContext = new DBContext();

                DataSet dataSet;
                List<ClaimBE> claims = new List<ClaimBE>();


                dataSet = dbContext.Read("GetClaims", null);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        claims.Add(new ClaimBE()
                        {
                            Id = Helper.GetGuidDB(dr["ClaimID"]),
                            Description = Helper.GetStringDB(dr["Description"]),
                            Date = Helper.GetDateTimeDB(dr["Date"]),
                            SongClaimed = new SongBE()
                            {
                                Id = Helper.GetGuidDB(dr["SongClaimedID"]),
                                Name = Helper.GetStringDB(dr["SongName"])
                            },
                            User = new UserBE()
                            {
                                Id = Helper.GetGuidDB(dr["UserID"]),
                                UserName = Helper.GetStringDB(dr["UserName"])
                            },
                            State = new StateBE()
                            {
                                Id = Helper.GetGuidDB(dr["StateID"]),
                                Name = Helper.GetStringDB(dr["StateName"])
                            }
                        });
                    }

                }

                return claims;

            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public IList<ClaimBE> GetUserClaims(UserBE user)
        {
            try
            {

                var dbContext = new DBContext();


                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@userID", user.Id);


                DataSet dataSet;
                List<ClaimBE> claims = new List<ClaimBE>();

                dataSet = dbContext.Read("GetUserClaims", parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        claims.Add(new ClaimBE()
                        {
                            Id = Helper.GetGuidDB(dr["ClaimID"]),
                            Description = Helper.GetStringDB(dr["Description"]),
                            Date = Helper.GetDateTimeDB(dr["Date"]),
                            SongClaimed = new SongBE()
                            {
                                Id = Helper.GetGuidDB(dr["SongClaimedID"]),
                                Name = Helper.GetStringDB(dr["SongName"])
                            },
                            User = new UserBE()
                            {
                                Id = Helper.GetGuidDB(dr["UserID"]),
                                UserName = Helper.GetStringDB(dr["UserName"])
                            },
                            State = new StateBE()
                            {
                                Id = Helper.GetGuidDB(dr["StateID"]),
                                Name = Helper.GetStringDB(dr["StateName"])
                            }
                        });
                    }

                }

                return claims;

            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }



        public ClaimBE GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ClaimBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();
                PermissionDAL permissionDAL = new PermissionDAL();

                parameters = new SqlParameter[2];

                parameters[0] = dbContext.CreateParameters("@claimID", entity.Id);
                parameters[1] = dbContext.CreateParameters("@stateID", entity.State.Id);



                if (dbContext.Write("ChangeState", parameters) > 0)
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
