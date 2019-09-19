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
            throw new NotImplementedException();
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

        public List<PermissionBE> GetUserPermissions(UserBE user)
        {
            var dbContext = new DBContext();
            var parameters = Array.Empty<SqlParameter>();
            DataSet dataSet;

            PermissionsGroupBE root = new PermissionsGroupBE
            {
                Id = new Guid(),
                Name = "Root",
                Permissions = new List<PermissionBE>()
            };

            parameters = new SqlParameter[1];
            parameters[0] = dbContext.CreateParameters("@UserID", user.Id.ToString());
            dataSet = dbContext.Read("GetUserPermissions", parameters);
           

            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                Guid parent = Guid.Parse(dr["ParentID"].ToString()) == Guid.Empty ? Guid.Empty : Guid.Parse(dr["ParentID"].ToString());
                var group = GetGroup(root, parent);
                if (group == null)
                    group = root;

                if (bool.Parse(dr["IsGroup"].ToString()) == true)
                {
                    group.Permissions.ToList().Add(new PermissionsGroupBE
                    {
                        Id = Guid.Parse(dr["PermissionID"].ToString()),
                        Name = dr["Name"].ToString()
                    });
                }
                else
                {
                    group.Permissions.ToList().Add(new PermissionBE
                    {
                        Id = Guid.Parse(dr["PermissionID"].ToString()),
                        Name = dr["Name"].ToString()
                    });
                }
            }

            return root.Permissions.ToList();
        }

       
        private PermissionsGroupBE GetGroup(PermissionsGroupBE group, Guid parentID)
        {
            var returnGroup = group;
            if (parentID == Guid.Empty)
                return returnGroup;

            foreach (PermissionBE permission in group.Permissions)
            {
                PermissionsGroupBE permissionGroup = permission as PermissionsGroupBE;
                if (permissionGroup != null)
                {
                    if (permissionGroup.Id == parentID)
                        returnGroup = permissionGroup;

                    if (permissionGroup.Permissions.ToList().Count > 0)
                        returnGroup = GetGroup(permissionGroup, parentID);
                }
            }

            return returnGroup;
        }
    }
}
