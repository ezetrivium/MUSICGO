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
    public class VoteDAL : IDAL<VoteBE>
    {
        public Guid Add(VoteBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();


                var voteguid = Guid.NewGuid();
                parameters = new SqlParameter[5];

                parameters[0] = dbContext.CreateParameters("@voteID", voteguid);
                parameters[1] = dbContext.CreateParameters("@positive", entity.Positive);
                parameters[2] = dbContext.CreateParameters("@songID", entity.Song.Id);
                parameters[3] = dbContext.CreateParameters("@userID", entity.User.Id);
                parameters[4] = dbContext.CreateParameters("@timeToPositive", entity.TimeToPositive);



                if (dbContext.Write("AddVote", parameters) > 0)
                {
                    return voteguid;

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

        public IList<VoteBE> Get()
        {
            throw new NotImplementedException();
        }

        public IList<VoteBE> GetUserVotes(UserBE user)
        {
            try
            {
                var dbContext = new DBContext();
                var dataSet = new DataSet();
                List<VoteBE> votes = new List<VoteBE>();
                var parameters = new SqlParameter[1];
                parameters[0] = dbContext.CreateParameters("@userID", user.Id);

                dataSet = dbContext.Read("GetUserVotes", parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        votes.Add(new VoteBE()
                        {
                            Id = Helper.GetGuidDB(dr["VoteID"]),
                            User = new UserBE()
                            {
                                Id = Helper.GetGuidDB(dr["UserID"])
                            },
                            Positive = Helper.GetBoolDB(dr["Positive"]),
                            Song = new SongBE()
                            {
                                Id = Helper.GetGuidDB(dr["SongID"]),
                                Name = Helper.GetStringDB(dr["SongName"]),
                                SongKey = Helper.GetStringDB(dr["SongKey"]),
                                Category = new CategoryBE()
                                {
                                    Id = Helper.GetGuidDB(dr["CategoryID"])
                                }
                            },
                            TimeToPositive = Helper.GetIntDB(dr["TimeToPositive"]),
                        });
                    }

                }


                return votes;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }

        public VoteBE GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(VoteBE entity)
        {
            throw new NotImplementedException();
        }
    }
}
