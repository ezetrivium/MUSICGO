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
    public class PermissionDAL : IDAL<PermissionBE>
    {
        #region interface methods
        public Guid Add(PermissionBE entity, PermissionBE parent)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();


                var permissionguid = Guid.NewGuid();
                parameters = new SqlParameter[3];

                parameters[0] = dbContext.CreateParameters("@permissionID", permissionguid);
                parameters[1] = dbContext.CreateParameters("@name", entity.Name);
                parameters[2] = parent.Id == Guid.Empty ? dbContext.CreateNullParameters("@parentID") :dbContext.CreateParameters("@parentID", parent.Id);



                if (dbContext.Write("AddPermission", parameters) > 0)
                {
                    return permissionguid;

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

        public IList<PermissionBE> Get()
        {
            var dbContext = new DBContext();
            var parameters = Array.Empty<SqlParameter>();
            DataSet dataSet;

            dataSet = dbContext.Read("GetPermissions", null);

            return ReturnPermissionsTree(dataSet);

        }


        public IList<PermissionBE> GetPermissionsGroups()
        {
            var dbContext = new DBContext();
            var parameters = Array.Empty<SqlParameter>();
            DataSet dataSet;
            List<PermissionBE> permissions = new List<PermissionBE>();

            dataSet = dbContext.Read("GetPermissionsGroups", null);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    permissions.Add(new PermissionBE()
                    {
                        Id = Helper.GetGuidDB(dr["PermissionID"]),
                        Name = Helper.GetStringDB(dr["Name"]),

                    });
                }

            }

            return permissions;


        }

        public IList<PermissionBE> GetChildPermissions()
        {
            var dbContext = new DBContext();
            var parameters = Array.Empty<SqlParameter>();
            DataSet dataSet;
            List<PermissionBE> permissions = new List<PermissionBE>();

            dataSet = dbContext.Read("GetChildPermissions", null);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    permissions.Add(new PermissionBE()
                    {
                        Id = Helper.GetGuidDB(dr["PermissionID"]),
                        Name = Helper.GetStringDB(dr["Name"]),

                    });
                }

            }

            return permissions;


        }

        public PermissionBE GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(PermissionBE entity)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();
                PermissionDAL permissionDAL = new PermissionDAL();

                parameters = new SqlParameter[2];

                parameters[0] = dbContext.CreateParameters("@permissionID", entity.Id);
                parameters[1] = dbContext.CreateParameters("@name", entity.Name);




                if (dbContext.Write("UpdatePermission", parameters) > 0)
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






        #endregion

        public bool AddPermissionGroup(PermissionBE entity, PermissionBE parent)
        {
            var dbContext = new DBContext();
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();
                PermissionDAL permissionDAL = new PermissionDAL();

                parameters = new SqlParameter[2];

                parameters[0] = dbContext.CreateParameters("@permissionID", entity.Id);
                parameters[1] = dbContext.CreateParameters("@parentID", parent.Id);




                if (dbContext.Write("AddPermissionGroup", parameters) > 0)
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



        public bool DeleteUserPermission(UserBE entity, DBContext dbCon)
        {
            try
            {

                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();
                bool result = false;
                UserDAL userDAL = new UserDAL();

                
                try
                {

                    var permissions = this.GetUserPermissions(entity);
                    PermissionDAL per = new PermissionDAL();

                    foreach (var perold in permissions)
                    {
                        var pernew = entity.Permissions.Where(p => p.Name == perold.Name).FirstOrDefault();
                        if (pernew == null && perold.Name != "Login")
                        {
                            parameters = new SqlParameter[2];

                            parameters[1] = dbCon.CreateParameters("@userID", entity.Id);
                            parameters[0] = dbCon.CreateParameters("@permissionID", perold.Id);
 

                            return (dbCon.Write("DeleteUserPermission", parameters) > 0) ? true : false;
                            
                        }

                    }

                    return result;

                }
                catch (Exception ex)
                {
                    throw ex;
                }



                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }

        }

        public bool SetUserPermission(UserBE entity, DBContext dbCon)
        {
            try
            {
                
                var dataSet = new DataSet();
                var parameters = Array.Empty<SqlParameter>();
                bool result = false;
                

                //probar bien esto
                try
                {
                    
                    foreach (var userper in entity.Permissions)
                    {
                        parameters = new SqlParameter[3];

                        parameters[1] = dbCon.CreateParameters("@userID", entity.Id);
                        parameters[0] = dbCon.CreateParameters("@permissionID", userper.Id);
                        parameters[2] = dbCon.CreateParameters("@userpermissionID", Guid.NewGuid());

                        result =  (dbCon.Write("SetUserPermission", parameters) > 0) ? true : false;
                      
                    }
                    return result;
                   
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                


                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }

        }


        public bool SetUserPermission(UserBE entity)
        {
            var dbContext = new DBContext();
           
            bool result = false;
           
            try
            {
                dbContext.BeginTran();
                
                PermissionDAL per = new PermissionDAL();

                if (per.DeleteUserPermission(entity, dbContext))
                {
                    result = per.SetUserPermission(entity, dbContext);
                }
                

                if (result)
                {
                    dbContext.CommitTran();
                    return result;
                }
                
                dbContext.RollBackTran();
                return result;

            }
            catch (Exception)
            {
                return result;
            }

        }



        public IList<PermissionBE> GetUserPermissions(UserBE user)
        {
            var dbContext = new DBContext();
            DataSet dataSet;
            DataSet dataSetUser;

            var permissionsUser = new List<PermissionBE>();

            var parameters = new SqlParameter[1];
            parameters[0] = dbContext.CreateParameters("@UserID", user.Id.ToString());

            dataSetUser = dbContext.Read("GetUserPermissions", parameters);



            var roots = this.Get();

            foreach(var root in roots)
            {
                if (dataSetUser.Tables.Count > 0)
                {
                    foreach (DataRow dr in dataSetUser.Tables[0].Rows)
                    {
                        if (root.Id.ToString() == dr[0].ToString())
                        {
                            permissionsUser.Add(root);
                        }
                    }
                }
                
                
            }
           
            return permissionsUser;
            
        }


        public IList<PermissionBE> GetRootPermissions()
        {
            var dbContext = new DBContext();
            DataSet dataSet;
   

            var permissions = new List<PermissionBE>();

      

            dataSet = dbContext.Read("GetRootPermissions", null);

            if (dataSet.Tables.Count > 0)
            {
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    if (Helper.GetBoolDB(dr["IsGroup"]))
                    {
                        permissions.Add(new PermissionsGroupBE()
                        {
                            Id = Helper.GetGuidDB(dr["PermissionID"]),
                            Name = Helper.GetStringDB(dr["Name"])
                        });
                    }
                    else
                    {
                        permissions.Add(new PermissionBE()
                        {
                            Id = Helper.GetGuidDB(dr["PermissionID"]),
                            Name = Helper.GetStringDB(dr["Name"])
                        });
                    }
                    
                }
            }

            return permissions;

        }


        public PermissionBE GetLoginPermission()
        {
            var dbContext = new DBContext();
            DataSet dataSet;
            var permission = new PermissionBE();

            dataSet = dbContext.Read("GetLoginPermission", null);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                permission.Id = Helper.GetGuidDB(dataSet.Tables[0].Rows[0]["PermissionID"]);
                permission.Name = Helper.GetStringDB(dataSet.Tables[0].Rows[0]["Name"]);
            }

            return permission;
        }
        public IList<PermissionBE> GetServicePermissions(ServiceBE service)
        {
            var dbContext = new DBContext();
            DataSet dataSet;
            

            var permissions = new List<PermissionBE>();

            var parameters = new SqlParameter[1];
            parameters[0] = dbContext.CreateParameters("@serviceCode", service.Code);

            dataSet= dbContext.Read("GetServicePermissions", parameters);



            if (dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    if (Helper.GetBoolDB(dr["IsGroup"]))
                    {
                        permissions.Add(new PermissionsGroupBE()
                        {
                            Id = Helper.GetGuidDB(dr["PermissionID"]),
                            Name = Helper.GetStringDB(dr["Name"])
                        });
                    }
                    else
                    {
                        permissions.Add(new PermissionBE()
                        {
                            Id = Helper.GetGuidDB(dr["PermissionID"]),
                            Name = Helper.GetStringDB(dr["Name"])
                        });
                    }
                }

            }


            return permissions;

        }



        private IList<PermissionBE> ReturnPermissionsTree(DataSet dataSet)
        {
            List<PermissionBE> permissions = new List<PermissionBE>();
            List<PermissionBE> roots = new List<PermissionBE>();

            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                Guid parent = dr["ParentID"].ToString() == string.Empty ? Guid.Empty : Guid.Parse(dr["ParentID"].ToString());
                PermissionBE permission;
                bool exists = false;

                foreach (PermissionBE per in permissions)
                {
                    if (per.Id == Guid.Parse(dr["PermissionID"].ToString()))
                    {
                        exists = true;
                    }
                }
                if (!exists)
                {

                    if (Convert.ToBoolean(dr["IsGroup"]))
                    {
                        permission = new PermissionsGroupBE()
                        {
                            Id = Guid.Parse(dr["PermissionID"].ToString()),
                            Name = dr["Name"].ToString()
                        };
                    }
                    else
                    {
                        permission = new PermissionBE()
                        {
                            Id = Guid.Parse(dr["PermissionID"].ToString()),
                            Name = dr["Name"].ToString()
                        };
                    }

                    if (parent == Guid.Empty)
                    {
                        roots.Add(permission);
                    }
                    else
                    {
                        permissions.Add(permission);
                    }
                }

            }

            if (roots.ToList().Count > 0)
            {
                foreach (var root in roots)
                {
                    if(typeof(PermissionsGroupBE) == root.GetType())
                    AddChildren(permissions, dataSet, (PermissionsGroupBE)root);
                }
            }

            return roots;
        }


      

        private static void AddChildren(List<PermissionBE> permissions, DataSet ds, PermissionsGroupBE node)
        {
            try
            {
                foreach (var per in permissions)
                {
                    List<PermissionBE> list = new List<PermissionBE>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (node.Id.ToString() == dr["ParentID"].ToString())
                        {
                            if (dr["PermissionID"].ToString() == per.Id.ToString())
                            {
                                if (!node.Permissions.Any(p => p == per))
                                {
                                    node.Permissions.Add(per);
                                }
                                foreach (var child in node.Permissions)
                                {
                                    if (typeof(PermissionsGroupBE) == child.GetType())
                                        AddChildren(permissions, ds, (PermissionsGroupBE)child);

                                }
                                break;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }

        public Guid Add(PermissionBE entity)
        {
            throw new NotImplementedException();
        }
    }
}
