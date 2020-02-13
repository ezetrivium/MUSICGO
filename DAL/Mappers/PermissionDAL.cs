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

        public IList<PermissionBE> Get()
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


        public IList<PermissionBE> GetUserPermissions(UserBE user)
        {
            var dbContext = new DBContext();
            DataSet dataSet;

            var parameters = new SqlParameter[1];
            parameters[0] = dbContext.CreateParameters("@UserID", user.Id.ToString());


            dataSet = dbContext.Read("GetUserPermissions", parameters);

            var roots = ReturnPermissionsTree(dataSet);
           
            return roots;
            
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

            foreach (var per in permissions)
            {
               List<PermissionBE> list = new List<PermissionBE>(); 
               foreach(DataRow dr in ds.Tables[0].Rows)
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
    }
}
