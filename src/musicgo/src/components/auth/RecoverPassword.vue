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

        <b-row v-show="showAlert" style="margin-top: 80px">
            <b-col md="6" style="margin:auto;float:none;text-align:center;">
                <div style="font-size:140px">
                    <b-icon :variant="variant" scale="1" :icon="icon"></b-icon>
                </div>
                <div class="icon-alert">
                    <b-alert :variant="variant" v-model="showAlert">{{$t(message)}}</b-alert>
                </div> 
            </b-col>           
        </b-row>

      <b-row v-show="!showAlert">
        <b-col md="6" style="margin:auto;float:none">
          <div :class="{'loader-background':loading}">
            <b-card
              text-variant="white"
              style="background-color:inherit; min-width:30rem;"
              class="card-primary"
              no-body
            >
              <b-card-header>
                <h2>{{$t('enternewpassword')}}</h2>
              </b-card-header>
              <b-card-body>
                <b-container>
                  <b-row>
                    <b-form action="#" @submit.prevent="updatePassword" style="width:100%">                   
                      <b-form-group
                        id="username"
                        :label="$t('username')"
                        label-for="input-username"
                        :description="$t('enterusername')"
                      >
                        <b-form-input
                          id="input-username"
                          v-model="userUpdate.UserName"
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
                          v-model="userUpdate.Password"
                          type="password"
                          required
                          :state="passwordState()"
                          :placeholder="$t('enterpassword')"
                          class="input-text-primary"
                          aria-describedby="input-password-feedback"
                        ></b-form-input>
                        <b-form-invalid-feedback :state="passwordState()" id="input-password-feedback">
                              {{ $t("password_validate_char") }}
                        </b-form-invalid-feedback>
                      </b-form-group>

                      <b-form-group
                        id="confirmPassword"
                        :label="$t('confirmpassword')"
                        label-for="input-confirmPassword"
                        :description="$t('enterpasswordagain')"
                      >
                        <b-form-input
                          id="input-confirmPassword"
                          v-model="userUpdate.ConfirmPassword"
                          type="password"
                          required
                          :placeholder="$t('enterpasswordagain')"
                          class="input-text-primary"
                          aria-describedby="input-passwordagain-feedback"
                          :state="passwordConfirmState()"
                        ></b-form-input>
                        <b-form-invalid-feedback :state="passwordConfirmState()" id="input-passwordagain-feedback">
                              {{ $t("password_confirm_validate") }}
                        </b-form-invalid-feedback>
                      </b-form-group>
                      <b-row> 
                        <b-col md="6" sm="12" style="text-align:center">
                         
                          <b-button class="button-primary" type="submit" >{{$t('change_password')}}</b-button>
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
import { Component, Vue, Watch, Prop } from "vue-property-decorator";
import { RecoverPasswordViewModel } from "../../shared/classes/RecoverPasswordViewModel";
import userService, { UserService } from "../../shared/services/UserService";
@Component({})
export default class RecoverPassword extends Vue {
  private userservice: UserService = userService;
  private userUpdate: RecoverPasswordViewModel = new RecoverPasswordViewModel();
  private loading: boolean = false;
  private message: string = "";
  private variant: string = "";
  private showAlert: boolean = false;
  private icon: string = "";


 @Prop({ type: String }) code: any;
  passwordState(){
        return (this.userUpdate.Password.length > 7 && this.userUpdate.Password.length < 33) ? true : false
      }

  passwordConfirmState(){
        return (this.userUpdate.ConfirmPassword == this.userUpdate.Password) && this.userUpdate.ConfirmPassword.length > 0 ? true : false
    }


  checkFormValidity() {


        if(!this.passwordState())
          return false;

        if(!this.passwordConfirmState())
          return false;

        return true;
      }

  updatePassword(){

    if(!this.checkFormValidity()){
          return
      }

    this.loading = true;
    this.showAlert = false;
    this.userUpdate.Code = this.code;
    this.userservice.updatePassword(this.userUpdate).then(res=>{
      this.loading = false;
      if (res.status === 200) {
        this.message = res.data;
        this.variant = "success";
        this.icon = "check-circle"
        this.showAlert = true;

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
        this.icon ="x-circle"
        this.variant = "danger";
        this.showAlert = true;
      });
  }
}
</script>

<style scoped>
.icon-alert{
    margin-top: 20px
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