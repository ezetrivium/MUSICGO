using BE.Entities;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Exceptions;
using ViewModels.ViewModels;

namespace BLL.BLLs
{
    public class PermissionsBLL : BaseBLL<PermissionBE,PermissionViewModel>
    {
        public PermissionsBLL()
        {
            Dal = new PermissionDAL();
        }


        public override IList<PermissionViewModel> Get()
        {
            try
            {
                IList<PermissionBE> entities;
                entities = this.Dal.Get();

                List<PermissionViewModel> pervmList = new List<PermissionViewModel>();

                this.CastPermissions(entities, pervmList);

                return pervmList;

            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }

        public IList<PermissionViewModel> GetPermissionsGroups()
        {
            try
            {
                PermissionDAL permissionDAL = new PermissionDAL();
                IList<PermissionBE> entities;

                entities = permissionDAL.GetPermissionsGroups();

                return Mapper.Map<PermissionBE, PermissionViewModel>(entities);

            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public IList<PermissionViewModel> GetChildPermissions()
        {
            try
            {
                PermissionDAL permissionDAL = new PermissionDAL();
                IList<PermissionBE> entities;

                entities = permissionDAL.GetChildPermissions();

                return Mapper.Map<PermissionBE, PermissionViewModel>(entities);

            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public Guid Add(PermissionViewModel viewModel,PermissionViewModel parent)
        {
            try
            {

                if (this.IsValid(viewModel))
                {
                    PermissionDAL permissionDAL = new PermissionDAL();
                    PermissionBE entity;
                    entity = Mapper.Map<PermissionViewModel, PermissionBE>(viewModel);
                    var parentEntity = Mapper.Map<PermissionViewModel, PermissionBE>(parent);
                    Guid result = permissionDAL.Add(entity, parentEntity);
                    return result;
                }
                else
                {
                    throw new Exception(Messages.InvalidData);
                }
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


        public override bool Update(PermissionViewModel viewModel)
        {
            try
            {

                if (this.IsValid(viewModel))
                {
                    PermissionBE entity;
                    entity = Mapper.Map<PermissionViewModel, PermissionBE>(viewModel);

                    return this.Dal.Update(entity);

                   
                }
                else
                {
                    throw new Exception(Messages.Generic_Error);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public bool AddPermissionGroup(PermissionViewModel viewModel, PermissionViewModel parent)
        {
            try
            {
                if(viewModel.Id != parent.Id)
                {
                    PermissionDAL permissionDAL = new PermissionDAL();
                    PermissionBE entity;
                    entity = Mapper.Map<PermissionViewModel, PermissionBE>(viewModel);
                    var entityparent = Mapper.Map<PermissionViewModel, PermissionBE>(parent);

                    return permissionDAL.AddPermissionGroup(entity, entityparent);
                }
                else
                {
                    throw new Exception(Messages.Generic_Error);
                }
               

            }
            catch (Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
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


        protected override bool IsValid(PermissionViewModel viewModel)
        {
            if(viewModel.Name.Length >0 &&
                viewModel.Name.Length < 51)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
