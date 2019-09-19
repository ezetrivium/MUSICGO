using BE.Entities;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using ViewModels.ViewModels;

namespace BLL.BLLs
{
    public class PermissionsBLL : BaseBLL<PermissionBE,PermissionViewModel>
    {
        public IEnumerable<PermissionBE> GetUserPermission(UserBE user)
        {
            PermissionDAL permisoDAL = new PermissionDAL();
            return permisoDAL.GetUserPermissions(user);
        }
    }
}
