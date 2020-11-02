<template>
<div>
    
 <b-card  class="container-form " >
     <b-card-title class="form-title">
          <b-row>
              <b-col>
                <h2>Suscribirse</h2>
              </b-col>

          </b-row>
    </b-card-title> 
  <b-container fluid >
      
        <div style="text-align:center"> 
            <b-spinner style="margin-top: 100px; margin-bottom:100px" label="Loading..."  v-show="loading"></b-spinner>
        </div>
        <div> 
            <div>
                <b-alert :variant="variant" dismissible v-model="showAlert">{{message}}</b-alert>
            </div> 
            <b-form  ref="formsuscribe" @submit.stop.prevent="handleAddSubmit" v-show="!loading">
                    <b-row class="mb-1">

                      <b-col lg="6">
                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-username"
                            label="Username:"
                            label-for="input-text-username"
                            class="input-width"
                          >
                            <b-form-input
                              
                              id="input-text-username"
                              v-model="userCreated.UserName"
                              type="text"
                              required
                              placeholder="Enter Username"
                              :state="usernameState()"
                              aria-describedby="input-username-feedback"
                              
                            ></b-form-input>
                            <b-form-invalid-feedback :state="usernameState()" id="input-username-feedback">
                              Su nombre de usuario debe tener entre 5 y 12 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-name"
                            label="Name:"
                            label-for="input-text-name"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-name"
                              v-model="userCreated.Name"
                              type="text"                              
                              required
                              
                              placeholder="Enter Name"
                              :state="nameState()"
                              aria-describedby="input-name-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="nameState()" id="input-name-feedback">
                              Su nombre debe tener máximo 30 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-lastname"
                            label="Last Name:"
                            label-for="input-text-lastname"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-lastname"
                              
                              v-model="userCreated.LastName"
                              type="text"
                              required
                              placeholder="Enter Last Name"
                              :state="lastnameState()"
                              aria-describedby="input-lastname-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="lastnameState()" id="input-lastname-feedback">
                              Su apellido debe tener máximo 30 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                         <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-password"
                            label="Password:"
                            label-for="input-text-password"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-password"
                              
                              v-model="userCreated.Password"
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
                        </b-row>

                        

                        
                      </b-col>


                      <b-col lg="6">
                        

                        <b-row class="mb-1 row-data">

                          
                          <b-form-group
                            id="input-email"
                            label="Email:"
                            label-for="input-text-email"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-email"
                              v-model="userCreated.Email"
                              type="email"
                              :state="emailState()"
                              required
                              placeholder="Enter Email"
                              aria-describedby="input-email-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="emailState()" id="input-email-feedback">
                              El email es requerido
                            </b-form-invalid-feedback>

                          </b-form-group>
                        </b-row>

                        

                    
                        <b-row class="mb-1 row-data">
                           <b-form-group label="Image" class="input-width" >
                             <b-row >
                               <b-col lg="9" md="9" sm="9">
                                  <b-form-file
                                  v-model="file"
                                  placeholder="Seleccione una imagen..."
                                  drop-placeholder="Arratre una imagen..."
                                  
                                  accept=".jpg"
                                  @change="showPreviewImage($event)"
                                  ></b-form-file>
                               </b-col>
                               <b-col lg="3" md="3" sm="3">
                                  <b-img 
                                    v-bind="avatarSize" 
                                    :src="this.userCreated.ImageBase64" 
                                    rounded="circle" alt="user avatar" 
                                    v-show="this.userCreated.ImageBase64"
                                    >                  
                                    </b-img>
                               </b-col>
                             
                             </b-row>
                            
                          </b-form-group>
                         
                        </b-row>

                        <b-row class="mb-1 row-data">

                           <b-form-group id="input-group-language" 
                           label="Lenguaje:" 
                           label-for="input-language" 
                           class="input-width"
                           required>
                            <b-form-select
                             id="input-language"
                             v-model="userCreated.Language"
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
                              El lenguaje es requerido
                            </b-form-invalid-feedback>
                          </b-form-group>

                        </b-row>


                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-artistname"
                            label="Artist Name:"
                            label-for="input-text-artistname"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-artistname"
                              v-model="userCreated.ArtistName"
                              type="text"
                              
                              required
                              placeholder="Enter Artist Name"
                              :state="artistnameState()"
                              aria-describedby="input-artistname-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="artistnameState()" id="input-artistname-feedback">
                              Su nombre de artista debe tener máximo 50 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        

                        


                      </b-col>
                </b-row>
                <b-row> 
                    <b-col md="12" sm="12" style="text-align:center; margin-top:10px">
                         
                        <b-button class="button-primary" type="submit" >Suscribirse</b-button>
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
import servicesService, { ServicesService } from "@/shared/services/ServicesService";
import permissionsService, { PermissionsService } from "@/shared/services/PermissionsService";
import { LanguageViewModel } from '@/shared/classes/LanguageViewModel';
import { ServiceViewModel } from '@/shared/classes/ServiceViewModel';
import { PermissionViewModel } from '@/shared/classes/PermissionViewModel';
import { Permissionshelper } from '@/shared/classes/Permissions-helper';
import { ContractViewModel } from '@/shared/classes/ContractViewModel';
@Component({})
export default class Subscribe extends Vue {
 private loading: boolean = false;
 private message: string = "";
 private variant: string = "";
 private showAlert: boolean = false;
 private imagePreview: string='';
 private file:any|null=null;
 private userCreated: UserViewModel = new UserViewModel();
 private languageservice: LanguageService = languageService;
 private languages: LanguageViewModel[] = [];
 private userservice: UserService = userService;
 private avatarSize: any[] = [
      {
        width: 40, height: 40
      }
    ]
    created(){
      this.getLanguages();
    }

    getLanguages(){
            
      this.languageservice.get()
        .then(response => {
          this.languages = response.data; 
        })
        .catch(error =>{                          
        })
    }

    showPreviewImage(e : any){
     if (e.target.files && e.target.files[0]) {
      
      var reader = new FileReader();
      var base64;

      
      reader.onload = (e) => {
        var target = e.target as FileReader
        this.userCreated.ImageBase64 = target.result as string
      }

      reader.readAsDataURL(e.target.files[0]);
      
    }
    else{
        this.userCreated.ImageBase64="";
    }
    

   }



   handleAddSubmit(bvModalEvt : any) {
      bvModalEvt.preventDefault();

      if(!this.checkFormValidity()){
          return
      }

      let formData = new FormData();
      formData.append("File", this.file);
      formData.append("user", JSON.stringify(this.userCreated));
      this.loading = true;
      this.showAlert = false;
      this.userservice.subscribe(formData)
      .then(response => {
        
        this.loading = false;
        // this.variant = "success";       
        // this.message = response.data;
        this.file = null;
        this.$router.push({
            path: `/contractservice/${response.data}`,
            // name:'contractservice',
            // params:{userID : response.data}
            });
        })
      .catch(error => {
        this.file = null;
        this.loading = false;
        this.userCreated.ImageBase64 = "" 
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

        if(!this.passwordState())
          return false;

        return true;
      }


    usernameState(){
        return (this.userCreated.UserName.length > 4 && this.userCreated.UserName.length < 13) ? true : false
      }

      nameState(){
        return (this.userCreated.Name.length < 31 && this.userCreated.Name.length > 0) ? true : false
      }

      lastnameState(){
        return (this.userCreated.LastName.length < 31 && this.userCreated.LastName.length > 0) ? true : false
      }

      artistnameState(){
        return (this.userCreated.ArtistName.length < 51 && this.userCreated.ArtistName.length > 0) ? true : false
      }

      emailState(){
        return (this.userCreated.Email.length < 251 && this.userCreated.Email.length > 0) ? true : false
      }

      languageState(){
        var language = this.userCreated.Language as LanguageViewModel;

        return (language.Name != '') ? true : false
      }

      passwordState(){
        return (this.userCreated.Password.length < 33 && this.userCreated.Password.length > 7) ? true : false
      }



}



</script>

<style >
.container-form{
    margin-top: 30px;
    background-color: inherit;
    border-color: #343a40;
    margin-bottom: 20px;
}

.input-width{
  width:100% !important;
  color: white;
  margin: 0 20px;
}

.form-title {
    color: #fff;
    padding: 16px 25px;
    border: solid 1px #343a40;
    
}

</style>