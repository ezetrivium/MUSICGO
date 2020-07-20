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


        public override bool Add(UserViewModel viewModel)
        {
            try
            {
                if (this.IsValid(viewModel))
                {
                    UserBE entity;
                    entity = Mapper.Map<UserViewModel, UserBE>(viewModel);

                    DVVerifier dvvv = new DVVerifier();

                    entity.DVH = dvvv.DVHCalculate(entity);
                    bool result = this.Dal.Add(entity);
                    return result;
                }
                else
                {
                    throw new BusinessException(Messages.InvalidData);
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
                        var attempsLogin = "Attemps" + userLogin.Id;
                        if (!CacheManager.HasKey(attempsLogin))
                        {
                            CacheManager.SetWithTimeout(attempsLogin, 0, TimeSpan.FromMinutes(30));
                        }
                        
                        CacheManager.Set(attempsLogin, Convert.ToInt32(CacheManager.GetWithTimeout(attempsLogin)) + 1);
                        
                            
                        if(CacheManager.HasKey(attempsLogin)&& Convert.ToInt32(CacheManager.GetWithTimeout(attempsLogin))>3)
                        {
                            this.Block(userLogin);
                            CacheManager.Remove(attempsLogin);
                            binnacleSL.AddBinnacle(new BinnacleBE()
                            {
                                User = userLogin,
                                Description = "UserBlocked",
                            });
                            throw new BusinessException(Messages.User_Blocked);
                        }

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

            UserBE userbe = userDAL.GetUserByUserName(user);

            return userbe;
  
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


        public void Block(UserBE user)
        {
            user.Blocked = true;
            this.Dal.Update(user);
        }




        public async Task RecoverPassword(UserViewModel viewModel)
        {
            try
            {
                UserBE userBE = Mapper.Map<UserViewModel, UserBE>(viewModel);
                UserBE userToReset = CheckUserName(userBE);
                BinnacleSL binnacleSL = new BinnacleSL();
                if (userToReset.Id != Guid.Empty)
                {
                    Encryptor encryptor = new Encryptor();
                    string code = encryptor.Encrypt(userToReset.Id.ToString() + DateTime.Now.ToString());
                    CacheManager.SetWithTimeout("ResetPassword" + userToReset.Id.ToString() , code, TimeSpan.FromHours(12));

                    string url = GlobalValues.RecoverPasswordURL + code;

                    string body = Email.BodyResetPassword + System.Environment.NewLine + url;

                    await Email.SendAsyncEmail(userToReset.Email, Email.SubjectResetPassword, body);

                    binnacleSL.AddBinnacle(new BinnacleBE()
                    {
                        User = userToReset,
                        Description = "Email to Reset Password",
                    });
                }
                else
                {
                    throw new BusinessException(Messages.ResetPasswordNotOk);
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



        public void UpdatePassword(RecoverPasswordViewModel viewModel)
        {
            try
            {
                if(viewModel.Password == viewModel.ConfirmPassword)
                {
                    UserDAL userDAL = new UserDAL();
                    BinnacleSL binnacleSL = new BinnacleSL();
                    UserBE userBE = new UserBE();
                    userBE.UserName = viewModel.UserName;
                    userBE = CheckUserName(userBE);

                    if (CacheManager.GetWithTimeout("ResetPassword" + userBE.Id.ToString()) != null)
                    {

                        string code = CacheManager.GetWithTimeout("ResetPassword" + userBE.Id.ToString()).ToString();

                        if (code == viewModel.Code)
                        {
                            Encryptor encryptor = new Encryptor();
                            userBE.Password = encryptor.Encrypt(viewModel.Password);
                            userDAL.Update(userBE);
                            binnacleSL.AddBinnacle(new BinnacleBE()
                            {
                                User = userBE,
                                Description = "UpdatePassword"
                            });
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }                  
                    else
                    {
                        throw new Exception();
                    }
                

                }
                else
                {
                    throw new BusinessException(Messages.InvalidUpdatePasswordRequest);
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



        protected override bool IsValid(UserViewModel viewModel)
        {

            if(viewModel.UserName != "" && viewModel.Email !="" && viewModel.LastName != "" && viewModel.Password != "" && viewModel.Language.Id != Guid.Empty && viewModel.Name != "")
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
