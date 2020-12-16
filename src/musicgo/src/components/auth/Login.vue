<template>
  <div>
    <b-container style="padding-top:50px">
      <b-row>
        <b-col md="12">
          <div class="text-center" v-show="loading">
            <b-spinner label="Loading..."></b-spinner>
          </div>
        </b-col>
      </b-row>
      

      <b-row v-show="recoverPasswordShow">
        <b-col md="6" style="margin:auto;float:none">
          <div :class="{'loader-background':loading}">
            <b-card
              text-variant="white"
              style="background-color:inherit; "
              class="card-primary"
              no-body
            >
              <b-card-header>
                <h2>{{ $t("recover_password") }}</h2>
              </b-card-header>
              <b-card-body>
                <b-container>
                  <b-row>
                    <b-form action="#" @submit.prevent="recoverPassword" style="width:100%">
                      <div>
                        <b-alert :variant="variant" dismissible v-model="showAlert">{{ $t(message) }}</b-alert>
                      </div>                    
                      <b-form-group
                        id="usernameRecover"
                        :label="$t('username')"
                        label-for="input-username-recover"
                        :description="$t('enterusername')"
                      >
                        <b-form-input
                          id="input-username-recover"
                          v-model="userNameToRecover.UserName"
                          type="text"
                          required
                          :placeholder="$t('enterusername')"
                          class="input-text-primary"
                          trim
                        ></b-form-input>
                      </b-form-group>
                   
                      <b-row> 
                        <b-col md="12" sm="12" lg="6" style="text-align:center">                       
                          <b-button class="button-primary" type="submit">{{ $t("recover_password") }}</b-button>
                        </b-col>
                   
                      </b-row>                     
                    </b-form>
                  </b-row>
                </b-container>
              </b-card-body>
            </b-card>
          </div>
        </b-col>
      </b-row>

      <b-row v-show="!recoverPasswordShow">
        <b-col md="6" style="margin:auto;float:none">
          <div :class="{'loader-background':loading}">
            <b-card
              text-variant="white"
              style="background-color:inherit;"
              class="card-primary"
              no-body
            >
              <b-card-header>
                <h2>{{ $t("login") }}</h2>
              </b-card-header>
              <b-card-body>
                <b-container>
                  <b-row>
                    <b-form action="#" @submit.prevent="login" style="width:100%">
                      <div>
                        <b-alert :variant="variant" dismissible v-model="showAlert">{{$t(message)}}</b-alert>
                      </div>                    
                      <b-form-group
                        id="username"
                        :label="$t('username')"
                        label-for="input-username"
                        :description="$t('enterusername')"
                      >
                        <b-form-input
                          id="input-username"
                          v-model="userLogin.UserName"
                          type="text"
                          required
                          :placeholder="$t('enterusername')"
                          class="input-text-primary"
                          trim
                        ></b-form-input>
                      </b-form-group>

                      <b-form-group
                        id="password"
                        :label="$t('password')"
                        label-for="input-password"
                        :description="$t('enterpassword')"
                      >
                        <b-form-input
                          id="input-password"
                          v-model="userLogin.Password"
                          type="password"
                          required
                          :placeholder="$t('enterpassword')"
                          class="input-text-primary"
                        ></b-form-input>
                      </b-form-group>
                      <b-row> 
                        <b-col md="6" sm="12" style="text-align:center">
                         
                          <b-button class="button-primary" type="submit" >{{$t("login")}}</b-button>
                        </b-col>
                        <b-col md="6" sm="12" style="text-align:center">
                        
                            <b-button class="forgot-password" variant="link" v-on:click="changeRecoverPassword">{{$t("forgot_password?")}}</b-button>                    
                        </b-col>
                      
                      </b-row>  
                      <b-row>
                        <b-col style="text-align:center; padding-top:20px">
                            <small>{{$t("You_do_not_have_an_account?")}}  <router-link :to="{ name: 'Subscribe' }">{{$t("sign_up")}}</router-link></small>
                        </b-col>
                      </b-row>                    
                    </b-form>
                  </b-row>
                </b-container>
              </b-card-body>
            </b-card>
          </div>
        </b-col>
      </b-row>
      
    </b-container>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { AuthenticationViewModel } from "../../shared/classes/AuthenticationViewModel";
import userService, { UserService } from "../../shared/services/UserService";
import { UserViewModel } from '../../shared/classes/UserViewModel';
import { LanguageHelper } from "@/shared/classes/Language-helper";
@Component({})
export default class Login extends Vue {
  private userservice: UserService = userService;
  private userLogin: UserViewModel = new UserViewModel();
  private loading: boolean = false;
  private userNameToRecover : UserViewModel = new UserViewModel();
  private recoverPasswordShow = false;
  private message: string = "";
  private variant: string = "";
  private showAlert: boolean = false;
  login() {
    this.loading = true;
    this.showAlert = false;
    this.$store
      .dispatch("retrieveToken", this.userLogin)
      .then(response => {
        this.loading = false;
        
        this.$i18n.locale = response.data.Language.Code;
        this.$router.push({ name: "home" });
      })
      .catch(error => {
        this.loading = false;
        if(error.response == null){
          this.message = error;
        }
        else{
          
            this.message = error.response.data.ExceptionMessage;
           
          
        }
        this.variant = "danger";
        this.showAlert = true;
        this.clearform();
      });
  }

  clearform() {
    this.userLogin.UserName = "";
    this.userLogin.Password = "";
  }

  changeRecoverPassword(){
    this.recoverPasswordShow = true;
    this.clearform();
  }

  recoverPassword(){
    this.loading = true;
    this.showAlert = false;
    this.userservice.recoverPassword(this.userNameToRecover).then(res=>{
      this.loading = false;
      if (res.status === 200) {
        this.message = res.data;
        this.variant = "success";
        this.showAlert = true;
        this.clearform();
      }
    })
    .catch(error => {
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
        this.clearform();
      });
  }
}
</script>

<style scoped>
h2 {
  color: white;
}



.container-login {
  width: 100%;
  min-height: 100vh;
  display: flex;
  flex-wrap: wrap;
  text-align: center;
  padding: 15px !important;
}


.text-center {
  text-align: center;
  color: white;
  position: fixed;
  left:50%;
  top:35%;
  z-index: 1000
}

.loader-background {
  opacity: 0.6;
  pointer-events: none;
}



</style>