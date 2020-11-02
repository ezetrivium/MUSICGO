using BE.Entities;
using DAL.Mappers;
using Newtonsoft.Json;
using SL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Management;
using Utilities;
using Utilities.Exceptions;
using ViewModels.ViewModels;

namespace BLL.BLLs
{
    public class UserBLL : BaseBLL<UserBE,UserViewModel>
    {

        private readonly string[] formats = { ".jpg", ".jpeg" };    
        
        public UserBLL()
        {
            this.Dal = new UserDAL();
        }

        public bool SetUserPermissions(UserViewModel viewModel)
        {
            
            var entity = Mapper.Map<UserViewModel, UserBE>(viewModel);
            var entityold = this.Dal.GetById(entity.Id);
            PermissionDAL perDAL = new PermissionDAL();
            if (entityold.Contract.Service == null )
            {
                return false;

            }
            else
            {
                return perDAL.SetUserPermission(entity);
            }
            
        }

        public override bool Update(UserViewModel viewModel)
        {
            try
            {
                if (this.IsValid(viewModel))
                {
                    UserBE entity;
                    entity = Mapper.Map<UserViewModel, UserBE>(viewModel);
                    BinnacleSL binnacleSL = new BinnacleSL();
                    DVVerifier dvvv = new DVVerifier();
                    Encryptor encryptor = new Encryptor();
                   

                    var entityold = this.Dal.GetById(entity.Id);
                    var newentity = this.CheckUserName(entity);

                    if (newentity.Id == Guid.Empty || entity.UserName == entityold.UserName)
                    {
                        if (!ValidImage(viewModel))
                        {
                            throw new BusinessException(Messages.InvalidImageFormat);
                            //PROBAR
                        }

                        if (viewModel.File != null)
                        {
                            var guid = Guid.NewGuid().ToString();
                            FileUtils.DeleteImageFile(entityold.ImgKey);
                            string path = FileUtils.GetRepoImagePath(guid + Path.GetExtension(viewModel.File.FileName));
                            viewModel.File.SaveAs(path);

                            entity.ImgKey = guid + Path.GetExtension(viewModel.File.FileName);
                        }
                        PermissionDAL permissionDAL = new PermissionDAL();
                   
                        if (entity.Contract != null && entity.Contract.Service.Id != Guid.Empty && (entityold.Contract == null || (entity.Contract.Service.Id != entityold.Contract.Service.Id || entityold.Contract.ExpirationDate < DateTime.Now)))
                        {                      
                                entity.Permissions = permissionDAL.GetServicePermissions(entity.Contract.Service);
                        }
                        else 
                        {
                            entity.Contract = null;
                            if (entityold.Contract != null)
                            {
                                entity.Permissions = permissionDAL.GetUserPermissions(entity);
                            }
                               
                        }

                        if(entity.Password != entityold.Password)
                        {
                            entity.Password = encryptor.Encrypt(entity.Password);
                        }
 
                        bool result = this.Dal.Update(entity);
                        if (result)
                        {


                            UserDAL userdal = new UserDAL();
                            var userDVH = userdal.GetDVHEntity(entity.Id);
                            userDVH.DVH = dvvv.DVHCalculate(userDVH);
                            userdal.SetDVH(userDVH);
                            dvvv.DVCalculate("UserDAL");

                            if (HttpContext.Current.User.Identity.IsAuthenticated) 
                            {
                                var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                                string userObj = identityClaims.FindFirst("userObject").Value;

                                binnacleSL.AddBinnacle(new BinnacleBE()
                                {
                                    User = JsonConvert.DeserializeObject<UserBE>(userObj),
                                    Description = "Update User",
                                });
                            }
                            else
                            {
                                binnacleSL.AddBinnacle(new BinnacleBE()
                                {
                                    User = userdal.GetById(entity.Id),
                                    Description = "Update User",
                                });
                            }


                            return true;
                        }
                        throw new BusinessException(Messages.ErrorUpdateUser);
                    }
                    else
                    {
                        throw new BusinessException(Messages.UserExists);
                    }

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


        public override bool Delete(Guid id)
        {
            try
            {
                BinnacleSL binnacleSL = new BinnacleSL();
                DVVerifier dvvv = new DVVerifier();
                var entityold = this.Dal.GetById(id);

                bool result;
                result = this.Dal.Delete(id);
                if (!result)
                {
                    throw new BusinessException(Messages.ErrorDeleteUser);
                }

                FileUtils.DeleteImageFile(entityold.ImgKey);
                dvvv.DVCalculate("UserDAL");


                var identityClaims = (ClaimsIdentity)HttpContext.Current.User.Identity;

                string userObj = identityClaims.FindFirst("userObject").Value;



                binnacleSL.AddBinnacle(new BinnacleBE()
                {
                    User = JsonConvert.DeserializeObject<UserBE>(userObj),
                    Description = "Delete User " + id,
                });

                return result;
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

   

        public override Guid Add(UserViewModel viewModel)
        {
            try
            {
                
                if (this.IsValid(viewModel))
                {
                    UserBE entity;
                    entity = Mapper.Map<UserViewModel, UserBE>(viewModel);
                    BinnacleSL binnacleSL = new BinnacleSL();
                    DVVerifier dvvv = new DVVerifier();
                    Encryptor encryptor = new Encryptor();

                    var entitynew = this.CheckUserName(entity);

                    if(entitynew.Id == Guid.Empty)
                    {
                        if (!ValidImage(viewModel))  
                        {
                            throw new BusinessException(Messages.InvalidImageFormat);
                            //PROBAR
                        }

                        if(viewModel.File != null)
                        {
                            var guid = Guid.NewGuid().ToString();
                            string path = FileUtils.GetRepoImagePath(guid + Path.GetExtension(viewModel.File.FileName));
                            viewModel.File.SaveAs(path);

                            entity.ImgKey = guid + Path.GetExtension(viewModel.File.FileName);
                        }
                        

                        PermissionDAL permissionDAL = new PermissionDAL();

                        if (entity.Contract.Service.Id == Guid.Empty)
                        {
                            entity.Permissions.Add(permissionDAL.GetLoginPermission());
                        }
                        else
                        {
                            entity.Permissions = permissionDAL.GetServicePermissions(entity.Contract.Service);
                        }

                        

                        entity.Password = encryptor.Encrypt(entity.Password);

                        Guid result = this.Dal.Add(entity);

                    

                        if (result != Guid.Empty)
                        {


                            UserDAL userdal = new UserDAL();
                            var userDVH = userdal.GetDVHEntity(result);
                            userDVH.DVH = dvvv.DVHCalculate(userDVH);
                            userdal.SetDVH(userDVH);
                            dvvv.DVCalculate("UserDAL");



                           

                            binnacleSL.AddBinnacle(new BinnacleBE()
                            {
                                User = entity,
                                Description = "Add User",
                            });


                            return result;
                        }
                        throw new BusinessException(Messages.ErrorAddUser);
                    }
                    else
                    {
                        throw new BusinessException(Messages.UserExists);
                    }

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

                                List<PermissionViewModel> pervmList = new List<PermissionViewModel>();

                                permissionBLL.CastPermissions(userLogin.Permissions, pervmList);

                                uservm.Permissions = pervmList;

                                var file = FileUtils.GetImageBytes(FileUtils.GetRepoImagePath(uservm.ImgKey));
                                uservm.ImageBase64 = "data:image/jpg;base64," + Convert.ToBase64String(file);
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
            UserDAL userDAL = new UserDAL();
            user.Blocked = true;
            userDAL.Block(user);
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



        public override UserViewModel GetById(Guid id)
        {
            try
            {
                PermissionsBLL permissionBLL = new PermissionsBLL();
                UserBE entity;
                entity = this.Dal.GetById(id);
                entity.Permissions = permissionBLL.GetUserPermission(entity);

                List<PermissionViewModel> pervmList = new List<PermissionViewModel>();

                permissionBLL.CastPermissions(entity.Permissions, pervmList);

                UserViewModel uvm = Mapper.Map<UserBE, UserViewModel>(entity);

                uvm.Permissions = pervmList;


                
                var file = FileUtils.GetImageBytes(FileUtils.GetRepoImagePath(uvm.ImgKey));
                uvm.ImageBase64 = "data:image/jpg;base64," + Convert.ToBase64String(file);
                

                return uvm;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected bool ValidImage(UserViewModel viewModel)
        {
            if(viewModel.File != null)
            {
                if (this.formats.Contains(Path.GetExtension(viewModel.File.FileName.ToLower())) && viewModel.File.ContentLength <= (1024 * 1024 * 5) )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        protected override bool IsValid(UserViewModel viewModel)
        {

            if(viewModel.UserName != "" &&
                viewModel.UserName.Length > 4 &&
                viewModel.UserName.Length < 13 &&
                viewModel.Email !="" && 
                viewModel.LastName != "" &&
                viewModel.LastName.Length < 31 &&
                viewModel.Password != "" &&
                viewModel.Password.Length > 7 &&
                viewModel.Password.Length < 33 &&
                viewModel.Language.Id != Guid.Empty && 
                viewModel.Name != "" &&
                viewModel.Name.Length < 31)
            {
                try
                {
                    MailAddress m = new MailAddress(viewModel.Email);

                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
