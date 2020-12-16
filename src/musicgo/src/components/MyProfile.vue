<template>
    <div>
    
 <b-card  class="container-form " >
     <b-card-title class="form-title">
          <b-row>
              <b-col>
                <h2>{{ $t("my_profile") }}</h2>
              </b-col>

          </b-row>
    </b-card-title> 
  <b-container fluid >
      
        <div style="text-align:center"> 
            <b-spinner style="margin-top: 100px; margin-bottom:100px" label="Loading..."  v-show="loading"></b-spinner>
        </div>
        <div> 
            <div>
                <b-alert :variant="variant" dismissible v-model="showAlert">{{$t(message)}}</b-alert>
            </div> 
            <b-form @submit.stop.prevent="handleUpdateSubmit" v-show="!loading">
                    <b-row class="mb-1">

                      <b-col lg="6">
                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-username"
                            :label="$t('username')"
                            label-for="input-text-username"
                            class="input-width"
                          >
                            <b-form-input
                              
                              id="input-text-username"
                              v-model="userUpdated.UserName"
                              type="text"
                              required
                              :placeholder="$t('enterusername')"
                              :state="usernameState()"
                              aria-describedby="input-username-feedback"
                              trim
                            ></b-form-input>
                            <b-form-invalid-feedback :state="usernameState()" id="input-username-feedback">
                              {{ $t("username_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-name"
                            :label="$t('name')"
                            label-for="input-text-name"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-name"
                              v-model="userUpdated.Name"
                              type="text"                              
                              required
                              trim
                              :placeholder="$t('entername')"
                              :state="nameState()"
                              aria-describedby="input-name-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="nameState()" id="input-name-feedback">
                                {{ $t("name_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-lastname"
                            :label="$t('lastname')"
                            label-for="input-text-lastname"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-lastname"
                              trim
                              v-model="userUpdated.LastName"
                              type="text"
                              required
                              :placeholder="$t('enterlastname')"
                              :state="lastnameState()"
                              aria-describedby="input-lastname-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="lastnameState()" id="input-lastname-feedback">
                              {{ $t("lastname_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                         <!-- <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-password"
                            label="Password:"
                            label-for="input-text-password"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-password"
                              
                              v-model="userUpdated.Password"
                              type="password"
                              required
                              placeholder="Enter Password"
                              :state="passwordState()"
                              aria-describedby="input-password-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="passwordState()" id="input-password-feedback">
                              Su Contraseña debe tener entre 8 y 32 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row> -->

                        <b-row class="mb-1 row-data">

                          
                          <b-form-group
                            id="input-email"
                            :label="$t('email')"
                            label-for="input-text-email"
                            class="input-width"
                          >
                            <b-form-input
                              trim
                              id="input-text-email"
                              v-model="userUpdated.Email"
                              type="email"
                              :state="emailState()"
                              required
                              :placeholder="$t('enteremail')"
                              aria-describedby="input-email-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="emailState()" id="input-email-feedback">
                              {{ $t("email_validate_char") }}
                            </b-form-invalid-feedback>

                          </b-form-group>
                        </b-row>

                        

                        
                      </b-col>


                      <b-col lg="6">
                        
           

                    
                        <b-row class="mb-1 row-data">
                           <b-form-group label="Image" class="input-width" >
                             <b-row >
                               <b-col lg="9" md="9" sm="9">
                                  <b-form-file
                                  v-model="file"
                                  :placeholder="$t('select_an_image')"
                                  :drop-placeholder="$t('drag_an_image')"
                                  
                                  accept=".jpg"
                                  @change="showPreviewImage($event)"
                                  ></b-form-file>
                               </b-col>
                               <b-col lg="3" md="3" sm="3">
                                  <b-img 
                                    v-bind="avatarSize" 
                                    :src="this.userUpdated.ImageBase64" 
                                    rounded="circle" alt="user avatar" 
                                    v-show="this.userUpdated.ImageBase64"
                                    >                  
                                    </b-img>
                               </b-col>
                             
                             </b-row>
                            
                          </b-form-group>
                         
                        </b-row>

                        <b-row class="mb-1 row-data">

                           <b-form-group id="input-group-language" 
                           :label="$t('language')"
                           label-for="input-language" 
                           class="input-width"
                           required>
                            <b-form-select
                             id="input-language"
                             v-model="userUpdated.Language"
                             required
                             :state="languageState()" 
                             aria-describedby="input-language-feedback"                          
                             >
                             <b-form-select-option
                                  v-for="(option, index) in languages"
                                  :key="'language-' + index"
                                  :value="option"            
                                >{{ option.Name }}</b-form-select-option>                           
                            </b-form-select>
                            <b-form-invalid-feedback :state="languageState()" id="input-language-feedback">
                              {{ $t("language_validate") }}
                            </b-form-invalid-feedback>
                          </b-form-group>

                        </b-row>


                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-artistname"
                            :label="$t('artistname')"
                            label-for="input-text-artistname"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-artistname"
                              v-model="userUpdated.ArtistName"
                              type="text"
                              trim
                              required
                              :placeholder="$t('enterartistname')"
                              :state="artistnameState()"
                              aria-describedby="input-artistname-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="artistnameState()" id="input-artistname-feedback">
                              {{ $t("artistname_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        

                        


                      </b-col>
                </b-row>
                <b-row> 
                    <b-col md="12" sm="12" style="text-align:center; margin-top:10px">
                         
                        <b-button class="button-primary" type="submit" >{{ $t("update") }}</b-button>
                    </b-col>
                </b-row>

                 <b-row> 
                    <b-col md="12" sm="12" style="text-align:center; margin-top:10px">
                         
                        <small style ="color:white">{{ $t("login_again_to_confirm_the_changes") }}</small>
                    </b-col>

                    
                </b-row>

                <b-row> 
                    <b-col md="12" sm="12" style="text-align:center; margin-top:10px">
                         
                        <b-button variant="link" @click="deleteUser()"><small>{{ $t("unsubscribe_my_profile") }}</small></b-button>
                    </b-col>
                   
                </b-row>
            </b-form>
        </div>
    </b-container>
  </b-card>
</div>
</template>

<script lang="ts">
import { Component, Vue, Watch, Prop } from "vue-property-decorator";
import { UserViewModel } from '@/shared/classes/UserViewModel';
import userService, { UserService } from "@/shared/services/UserService";
import languageService, { LanguageService } from "@/shared/services/LanguageService";
import { LanguageViewModel } from '@/shared/classes/LanguageViewModel';
@Component({})
export default class MyProfile extends Vue {
    private loading: boolean = false;
    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;
    private imagePreview: string='';
    private file:any|null=null;
    private languageservice: LanguageService = languageService;
    private languages: LanguageViewModel[] = [];
    private userservice: UserService = userService;
    private userUpdated: UserViewModel = new UserViewModel();
    
    private avatarSize: any[] = [
      {
        width: 40, height: 40
      }
    ]
    mounted(){
      this.getLanguages();
      this.getUser()
    }

    showPreviewImage(e : any){
     if (e.target.files && e.target.files[0]) {
      
      var reader = new FileReader();
      var base64;

      
      reader.onload = (e) => {
        var target = e.target as FileReader
        this.userUpdated.ImageBase64 = target.result as string
      }

      reader.readAsDataURL(e.target.files[0]);
      
    }
    else{
        this.userUpdated.ImageBase64="";
    }
   }

  

    getLanguages(){
            
      this.languageservice.get()
        .then(response => {
          this.languages = response.data; 
        })
        .catch(error =>{                          
        })
    }


    checkFormValidity() {

        if(!this.usernameState())
          return false;

        if(!this.nameState())
          return false;

        if(!this.lastnameState())
          return false;

        if(!this.artistnameState())
          return false;

        if(!this.emailState())
          return false;

        if(!this.languageState())
          return false;

        // if(!this.passwordState())
        //   return false;

        return true;
      }

    getUser() {
        var user = this.$store.getters.user as UserViewModel
        this.loading = true;
        this.userservice.getProfile(user)
        .then(response => {
          this.userUpdated = response.data;
          this.loading = false;

         
        })
        .catch(error =>{
          this.file = null;
        this.loading = false;
        this.userUpdated.ImageBase64 = "" 
        if(error.response == null){
          this.message = error;
        }
        else{
            if(error.response.data.Message != null){
                 this.message = error.response.data.Message;
            }
            else{
                this.message = error.response.data
            }
         
        }
        this.variant = "danger";
        this.showAlert = true;                           
        })

      }


      deleteUser() {
        // this.infoModal.title = item.UserName;
        this.showAlert = false;
        this.$bvModal.msgBoxConfirm(this.$t("are_you_sure?") as string, {
          title: this.$t("confirmation") as string,
          size: 'sm',
          okVariant: 'success',
          centered: true,
          bodyBgVariant: 'dark',
          bodyTextVariant : 'light',
          headerBgVariant:'dark',
          headerTextVariant:'light',
          footerBgVariant:'dark',
          footerTextVariant:'light'
        })
          .then(value => {
            if(value){
              this.userservice.deleteMyProfile(this.userUpdated)
                .then(response => {
                  
                  
                    this.loading = false;
            
                    this.file = null;

                    this.$router.push({           
                        name:'logout',
                    });

                })
                .catch(error => {
                  if(error.response == null){
                        this.message = error;
                      }
                      else{
                            if(error.response.data.Message != null){
                                this.message = error.response.data.Message;
                            }
                            else{
                                this.message = error.response.data
                            }
                      }
                      this.variant = "danger";
                      this.showAlert = true;                
                });
            }    

          })
          .catch(error => {
            // if(error.response == null){
            //   this.message = error;
            // }
            // else{
            //   this.message = error.response.data;
            // }
            // this.variant = "danger";
            // this.showAlert = true;
          })

      }


      handleUpdateSubmit() {
       

        if(!this.checkFormValidity()){
            return
        }
   
        let formData = new FormData();
        formData.append("File", this.file);
        formData.append("user", JSON.stringify(this.userUpdated));
        this.loading = true;
        this.showAlert = false;

    
        
        this.userservice.myProfile(formData).then(response => {
            
            this.loading = false;
            
            this.file = null;

            this.$router.push({           
                name:'logout',
            });
        })
        .catch(error => {
            // this.file = null;
            this.loading = false;
            if(error.response == null){
            this.message = error;
            }
            else{
                if(error.response.data.Message != null){
                    this.message = error.response.data.Message;
                }
                else{
                    this.message = error.response.data
                }
            }
            this.variant = "danger";
            this.showAlert = true;
        
        });
    }


    usernameState(){
        return (this.userUpdated.UserName.length > 4 && this.userUpdated.UserName.length < 13) ? true : false
      }

      nameState(){
        return (this.userUpdated.Name.length < 31 && this.userUpdated.Name.length > 0) ? true : false
      }

      lastnameState(){
        return (this.userUpdated.LastName.length < 31 && this.userUpdated.LastName.length > 0) ? true : false
      }

      artistnameState(){
        return (this.userUpdated.ArtistName.length < 51 && this.userUpdated.ArtistName.length > 0) ? true : false
      }

      emailState(){
        return (this.userUpdated.Email.length < 251 && this.userUpdated.Email.length > 0) ? true : false
      }

      languageState(){
        var language = this.userUpdated.Language as LanguageViewModel;

        return (language.Name != '') ? true : false
      }

    //   passwordState(){
    //     return (this.password.length < 33 && this.password.length > 7) ? true : false
    //   }

    


}
</script>

<style scoped>

</style>