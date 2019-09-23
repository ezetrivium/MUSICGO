using BE.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public class PermissionDAL : IDAL<PermissionBE>
    {
        #region interface methods
        public bool Add(PermissionBE entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PermissionBE> Get()
        {
            var dbContext = new DBContext();
            var parameters = Array.Empty<SqlParameter>();
            DataSet dataSet;

            dataSet = dbContext.Read("GetPermissions", null);

            return ReturnPermissionsTree(dataSet);

        }

        public PermissionBE GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(PermissionBE viewModel)
        {
            throw new NotImplementedException();
        }

        #endregion


        public IEnumerable<PermissionBE> GetUserPermissions(UserBE user)
        {
            var dbContext = new DBContext();
            var parameters = Array.Empty<SqlParameter>();
            DataSet dataSet;

            
            parameters = new SqlParameter[1];
            parameters[0] = dbContext.CreateParameters("@UserID", user.Id.ToString());
            dataSet = dbContext.Read("GetUserPermissions", parameters);

            return ReturnPermissionsTree(dataSet);
            
        }



        private IEnumerable<PermissionBE> ReturnPermissionsTree(DataSet dataSet)
        {
            List<PermissionBE> permissions = new List<PermissionBE>();
            List<PermissionBE> roots = new List<PermissionBE>();

            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                Guid parent = Guid.Parse(dr["ParentID"].ToString()) == Guid.Empty ? Guid.Empty : Guid.Parse(dr["ParentID"].ToString());
                PermissionBE permission;
                bool exists = false;

                foreach(PermissionBE per in permissions)
                {
                    if(per.Id == Guid.Parse(dr["PermissionID"].ToString()))
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

            if(roots.ToList().Count > 0)
            {
                foreach(PermissionsGroupBE root in roots)
                {
                    AddChildren(permissions, dataSet, root);
                }
            }

            return roots;
        }



        private static void AddChildren(List<PermissionBE> permissions, DataSet ds, PermissionsGroupBE node)
        {

            foreach (var per in permissions)
            {              
               if(node.Id == per.Id)
               {                
                  node.Permissions.ToList().Add(per);                                
                  foreach(PermissionsGroupBE child in node.Permissions)
                  {
                        AddChildren(permissions, ds, child);
                  }
    
               }
            }
        }
    }
}
