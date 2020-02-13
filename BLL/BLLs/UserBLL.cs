using BE.Entities;
using DAL.Mappers;
using SL;
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
    public class UserBLL : BaseBLL<UserBE,UserViewModel>
    {
        
        public UserBLL()
        {
            this.Dal = new UserDAL();
        }

        public UserViewModel LogIn(UserViewModel viewModel)
        {
            try
            {
                UserBE userBE = Mapper.Map<UserViewModel, UserBE>(viewModel);
                PermissionsBLL permissionBLL = new PermissionsBLL();
                BinnacleSL binnacleSL = new BinnacleSL();
                UserBE userLogin = new UserBE();
                Encryptor encryptor = new Encryptor();
                DVVerifier dVVerifier = new DVVerifier();
                userLogin = CheckUserName(userBE);
                if (userLogin.Id != Guid.Empty)
                {
                    if (encryptor.CheckEncryption(encryptor.Encrypt(viewModel.Password), userLogin.Password))
                    {
                        userLogin.Permissions = permissionBLL.GetUserPermission(userLogin);
                        if (VerifyLoginPermission(userLogin.Permissions))
                        {
                            if (!userLogin.Blocked)
                            {
                                binnacleSL.AddBinnacle(new BinnacleBE()
                                {
                                    User = userLogin,
                                    Description = "Login",
                                });

                                dVVerifier.DVVerify();

                                userLogin.Language = new LanguageBLL().GetUserLanguage(userLogin);

                                UserViewModel uservm = Mapper.Map<UserBE, UserViewModel>(userLogin);

                                permissionBLL.CastPermissions(userLogin.Permissions, uservm.Permissions);


                                return uservm;
                            }
                            else
                            {
                                throw new BusinessException(Messages.User_Blocked);
                            }
                        }
                        else
                        {
                            throw new BusinessException(Messages.LoginNotOk);
                        }

                    }
                    else
                    {
                        throw new BusinessException(Messages.PasswordNotOk);
                    }
                }
                else
                {
                    throw new BusinessException(Messages.UserNotExists);
                }
                
            }
            catch(BusinessException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        public UserBE CheckUserName(UserBE user)
        {
            UserDAL userDAL = new UserDAL();
            
            return userDAL.GetUserByUserName(user);
            
        }

        private bool VerifyLoginPermission(IList<PermissionBE> permissions)
        {
            bool result = false;
            foreach (var per in permissions)
            {            
                 if (typeof(PermissionBE) == per.GetType())
                 {
                     if (per.Check("Login"))
                     {
                         result = true;
                     }
                 }
                 else
                 {
                     PermissionsGroupBE pg = (PermissionsGroupBE)per;
                       
                     result = VerifyLoginPermission(pg.Permissions);
                 }
                if (result)
                {
                    break;
                }
                
            }

            return result;
        }




    }
}
