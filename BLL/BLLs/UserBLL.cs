using BE.Entities;
using DAL.Mappers;
using SL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
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
                PermissionsBLL permissions = new PermissionsBLL();
                BinnacleSL binnacleSL = new BinnacleSL();
                UserBE userLogin = new UserBE();
                Encryptor encryptor = new Encryptor();
                DVVerifier dVVerifier = new DVVerifier();
                userLogin = CheckUserName(userBE);
                if (encryptor.CheckEncryption(userBE.Password, userLogin.Password))
                {
                    userLogin.Permissions = permissions.GetUserPermission(userLogin);
                    if (VerifyLoginPermission(userLogin))
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

                            return Mapper.Map<UserBE,UserViewModel>(userLogin);
                        }
                        else
                        {
                            throw new Exception(Messages.User_Blocked);
                        }
                    }
                    else
                    {
                        throw new Exception(Messages.LoginNotOk);
                    }

                }
                else
                {
                    throw new Exception(Messages.PasswordNotOk);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(Messages.Generic_Error);
            }
        }


        private UserBE CheckUserName(UserBE user)
        {
            UserDAL userDAL = new UserDAL();
            
            user = userDAL.GetUserByUserName(user);
            if(user.UserName != "")
            {
                return user;
            }
            else
            {
                throw new Exception(Messages.UserNotExists);
            }
        }

        private bool VerifyLoginPermission(UserBE user)
        {
            foreach(PermissionBE per in user.Permissions)
            {
                if (per.Check("Login"))
                {
                    return true;
                }              
            }
            return false;
        }
    }
}
