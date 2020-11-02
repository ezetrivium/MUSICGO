﻿using BE.Entities;
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
        public PermissionsBLL()
        {
            Dal = new PermissionDAL();
        }


        public IList<PermissionBE> GetUserPermission(UserBE user)
        {
            PermissionDAL permisoDAL = new PermissionDAL();
            return permisoDAL.GetUserPermissions(user);
        }

        public IList<PermissionViewModel> GetRootPermissions()
        {
            PermissionDAL permisoDAL = new PermissionDAL();
            List<PermissionViewModel> pervm = new List<PermissionViewModel>();
            var permissions = permisoDAL.GetRootPermissions();
            this.CastPermissions(permissions,pervm);
            return pervm;
        }

        public void CastPermissions(IList<PermissionBE> permissions, IList<PermissionViewModel> permissionViewModels)
        {
            
            foreach (var per in permissions)
            {
                
                if (typeof(PermissionsGroupBE) == per.GetType())
                {
                    PermissionsGroupBE pg = (PermissionsGroupBE)per;
                    //var pergrvm = Mapper.Map<PermissionsGroupBE, PermissionsGroupViewModel>(pg);
                    PermissionsGroupViewModel pergrvm = new PermissionsGroupViewModel()
                    {
                        Id = pg.Id,
                        Name = pg.Name
                    };
                    permissionViewModels.Add(pergrvm);
                    //permissionViewModels.Remove(permissionViewModels.Where(p=>p.Id == pergrvm.Id).FirstOrDefault());
                    CastPermissions(pg.Permissions, pergrvm.Permissions);
                    
                }
                else if(typeof(PermissionBE) == per.GetType())
                {
                    PermissionBE p = (PermissionBE)per;
                    PermissionViewModel pervm = new PermissionViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name
                    };
                    permissionViewModels.Add(pervm);
                }


            }

        }


    }
}
