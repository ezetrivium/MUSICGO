<template>
<div>
  
  <b-navbar type="dark" variant="dark" v-if="!mobileView">
    <b-collapse id="nav-collapse" is-nav>
    <b-navbar-nav>
      <b-nav-item style="float:right"><router-link class="link" :to="{ name: 'home' }"><img class="img-home" src="@/assets/images/MusicGoLogo.png"></router-link></b-nav-item>

    
      <!-- <b-nav-item-dropdown :text="$t('user')" right>              -->
      <b-nav-item-dropdown id="gestion" class="link" style="padding-top:15px" text="Gestion" v-show="hasPermission('SuperAdmin') ||hasPermission('Admin')" > 
        <b-dropdown-item v-show="hasPermission('GetUsers')" >
          <router-link class="link-dropdown"  :to="{ name: 'usersList' }">Users</router-link>
        </b-dropdown-item>
      </b-nav-item-dropdown>
  </b-navbar-nav>

  <b-navbar-nav class="ml-auto">
      <b-nav-item-dropdown id="config" right  > 
        <template slot="button-content">
            <b-icon font-scale="2" icon="person"></b-icon>
        </template>
          <b-dropdown-item v-show="loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'MyProfile' }">Mi Perfil</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="loggedIn()">
            <router-link class="link-dropdown"  :to="{ path: `/contractservice/${this.userLogged().Id}` }">Contratar</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="!loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'Subscribe' }">Suscribirse</router-link>
          </b-dropdown-item>
          <b-dropdown-divider></b-dropdown-divider>
          <b-dropdown-item v-show="!loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'login' }">Login</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'logout' }">Logout</router-link>
          </b-dropdown-item>
      </b-nav-item-dropdown>
    </b-navbar-nav>
    </b-collapse>
  </b-navbar>

  <b-navbar type="dark" variant="dark" v-if="mobileView">
    <b-collapse id="nav-collapse" is-nav>
      <b-navbar-nav>
        <b-nav-item>
          <router-link  :to="{ name: 'home' }"><img class="img-home" src="@/assets/images/MusicGoLogo.png"></router-link>
        </b-nav-item>

      <!-- <b-nav-item-dropdown :text="$t('user')" right>              -->
        <b-nav-item-dropdown id="gestion" class="link" style="padding-top:10px" v-show="hasPermission('SuperAdmin') ||hasPermission('Admin')" >
          
          <template slot="button-content">
            <b-icon font-scale="1.5" icon="table"></b-icon>
          </template>
        <b-dropdown-item v-show="hasPermission('GetUsers')" >
          <router-link class="link-dropdown"  :to="{ name: 'usersList' }">Users</router-link>
        </b-dropdown-item>
      </b-nav-item-dropdown>
    </b-navbar-nav>


     <b-navbar-nav class="ml-auto">
      <b-nav-item-dropdown id="config" right  > 
        <template slot="button-content">
            <b-icon font-scale="2" icon="person"></b-icon>
        </template>
          <b-dropdown-item v-show="loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'MyProfile' }">Mi Perfil</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="loggedIn()">
            <router-link class="link-dropdown"  :to="{ path: `/contractservice/${this.userLogged().Id}` }">Contratar</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="!loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'Subscribe' }">Suscribirse</router-link>
          </b-dropdown-item>
          <b-dropdown-divider></b-dropdown-divider>
          <b-dropdown-item v-show="!loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'login' }">Login</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'logout' }">Logout</router-link>
          </b-dropdown-item>
      </b-nav-item-dropdown>
    </b-navbar-nav>
    </b-collapse>
  </b-navbar> 
  
</div>
</template>


<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { Permissionshelper } from '../classes/Permissions-helper';
import { UserViewModel } from '../classes/UserViewModel';

@Component({})
export default class Navbar extends Vue{
  private mobileView : boolean = false;
  private user : UserViewModel = new UserViewModel();
  created(){
    this.handleView();
     
  }

  mounted(){
    this.userLogged();
  }

  computed() {
    
   
  }

  userLogged(){
    if(this.loggedIn()){
      this.user = this.$store.getters.user as UserViewModel
    } 
    return this.user;
  }

  loggedIn(){
      return this.$store.getters.loggedIn
  }

  hasPermission(permission : string){
      var result = Permissionshelper.HasPermission(permission);
      return result;      
  }

  handleView(){
    this.mobileView = window.innerWidth <= 990;
  }
}

</script>

<style >

.item-menu{
  padding-top:25px;
}

.link{
  color: white;
  text-decoration: none;
  margin-right: 10px;
  font-size: 20px
} 

.dropdown-menu > li :hover{
  background-color:inherit;
}

.dropdown-menu{
   background-color:#212529 !important;
   border-color: white !important;
}

.link-dropdown{
  color: white;
  text-decoration: none;
  font-size: 16px
}
.link-dropdown:hover{
  padding-left: 7px;
  color: white;
  text-decoration: none;
}
.mobile-button{
  background-color: inherit;
  border: none;

}

.img-home{
  height: 60px;
    width: 80px;
    top: 0;
}


</style>