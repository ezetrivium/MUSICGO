<template>
<div>
  
  <b-navbar type="dark" variant="dark" v-if="!mobileView">
    <b-collapse id="nav-collapse" is-nav>
    <b-navbar-nav style="width: 380px">
      <b-nav-item style="float:right"><router-link class="link" :to="{ name: 'home' }"><img class="img-home" src="@/assets/images/MusicGoLogo.png"></router-link></b-nav-item>


      <!-- <b-nav-item-dropdown :text="$t('user')" right>              -->
      <b-nav-item-dropdown id="gestion" class="link" style="padding-top:15px" :text="$t('management')" v-show="hasPermission('AddLanguage') ||hasPermission('GetUsers') ||hasPermission('Backup')||hasPermission('GetBinnacle')" > 
        <b-dropdown-item v-show="hasPermission('GetUsers')" >
          <router-link class="link-dropdown"  :to="{ name: 'UsersReport' }">{{$t('users_report')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('GetSongs')" >
          <router-link class="link-dropdown"  :to="{ name: 'SongsReport' }">{{$t('songs_report')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('GetUsers')" >
          <router-link class="link-dropdown"  :to="{ name: 'usersList' }">{{$t('users')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('GetSongs')" >
          <router-link class="link-dropdown"  :to="{ name: 'Songs' }">{{$t('songs')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('AddLanguage')" >
          <router-link class="link-dropdown"  :to="{ name: 'LanguageComponent' }">{{$t('languages')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('Backup') ||hasPermission('Restore')" >
          <router-link class="link-dropdown"  :to="{ name: 'BackUpRestore' }">{{$t('backup')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('GetBinnacle')" >
          <router-link class="link-dropdown"  :to="{ name: 'Binnacle' }">{{$t('binnacle')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('GetClaims')" >
          <router-link class="link-dropdown"  :to="{ name: 'Claims' }">{{$t('claims')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('GetPermissions')" >
          <router-link class="link-dropdown"  :to="{ name: 'Permissions' }">{{$t('permissions')}}</router-link>
        </b-dropdown-item>
      </b-nav-item-dropdown>

     
  </b-navbar-nav>
  
   <b-navbar-nav class="mx-auto " v-show="hasPermission('Play')" href="#">
      <b-nav-item class="play-nav-margin">
            <router-link class="link" :to="{ name: 'Play' }"><b-icon scale="2.5" icon="play-fill"></b-icon></router-link>
      </b-nav-item>      
    </b-navbar-nav>

  <b-navbar-nav class="ml-auto">
      <div style="margin-top:5px" >
        <Language />
       </div>
      <b-nav-item-dropdown id="config" right  > 
        <template slot="button-content">
            <b-icon font-scale="2" icon="person"></b-icon>
        </template>
          <b-dropdown-item v-show="loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'MyProfile' }">{{$t('my_profile')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="hasPermission('GetUserSongs')">
            <router-link class="link-dropdown"  :to="{ name: 'MySongs' }">{{$t('my_songs')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="hasPermission('Play')">
            <router-link class="link-dropdown"  :to="{ name: 'SongsVoted' }">{{$t('voted')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="hasPermission('GetUserClaims')">
            <router-link class="link-dropdown"  :to="{ name: 'MyClaims' }">{{$t('myclaims')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="loggedIn()">
            <router-link class="link-dropdown"  :to="{ path: `/contractservice/${this.userLogged().Id}` }">{{$t('contract')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="!loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'Subscribe' }">{{$t('subscribe')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-divider></b-dropdown-divider>
          <b-dropdown-item v-show="!loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'login' }">{{$t('login')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'logout' }">{{$t('logout')}}</router-link>
          </b-dropdown-item>
      </b-nav-item-dropdown>
    </b-navbar-nav>
    </b-collapse>
  </b-navbar>

  <b-navbar class="navbar-mobile" type="dark" variant="dark" v-if="mobileView">
    <b-collapse id="nav-collapse" is-nav>
      <b-navbar-nav>
        <b-nav-item class="home-mobile-nav">
          <router-link  :to="{ name: 'home' }"><img class="img-home" src="@/assets/images/MusicGoLogo.png"></router-link>
        </b-nav-item>

      <!-- <b-nav-item-dropdown :text="$t('user')" right>              -->
        <b-nav-item-dropdown id="gestion" class="link" style="padding-top:10px" v-show="hasPermission('AddLanguage') ||hasPermission('GetUsers') ||hasPermission('Backup')||hasPermission('GetBinnacle')" >
          
          <template slot="button-content">
            <b-icon font-scale="1.9" icon="table"></b-icon>
          </template>

        <b-dropdown-item v-show="hasPermission('GetUsers')" >
          <router-link class="link-dropdown"  :to="{ name: 'UsersReport' }">{{$t('users_report')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('GetSongs')" >
          <router-link class="link-dropdown"  :to="{ name: 'SongsReport' }">{{$t('songs_report')}}</router-link>
        </b-dropdown-item>  
        <b-dropdown-item v-show="hasPermission('GetUsers')" >
          <router-link class="link-dropdown"  :to="{ name: 'usersList' }">{{$t('users')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('GetSongs')" >
          <router-link class="link-dropdown"  :to="{ name: 'Songs' }">{{$t('songs')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('AddLanguage')" >
          <router-link class="link-dropdown"  :to="{ name: 'LanguageComponent' }">{{$t('languages')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('Backup') ||hasPermission('Restore')" >
          <router-link class="link-dropdown"  :to="{ name: 'BackUpRestore' }">{{$t('backup')}}</router-link>
        </b-dropdown-item>
         <b-dropdown-item v-show="hasPermission('GetBinnacle')" >
          <router-link class="link-dropdown"  :to="{ name: 'Binnacle' }">{{$t('binnacle')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('GetClaims')" >
          <router-link class="link-dropdown"  :to="{ name: 'Claims' }">{{$t('claims')}}</router-link>
        </b-dropdown-item>
        <b-dropdown-item v-show="hasPermission('GetPermissions')" >
          <router-link class="link-dropdown"  :to="{ name: 'Permissions' }">{{$t('permissions')}}</router-link>
        </b-dropdown-item>
      </b-nav-item-dropdown>

       <b-nav-item class="play-nav"  v-show="hasPermission('Play')" href="#">
        <router-link class="link" :to="{ name: 'Play' }"><b-icon scale="2" icon="play-fill"></b-icon></router-link>
      </b-nav-item>
    </b-navbar-nav>

    
     <b-navbar-nav class="ml-auto">
       <div style="padding-top:2px; margin-right:5px" >
        <Language />
       </div>
      <b-nav-item-dropdown id="config" right  > 
        <template slot="button-content">
            <b-icon font-scale="2" icon="person"></b-icon>
        </template>
          <b-dropdown-item v-show="loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'MyProfile' }">{{$t('my_profile')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="hasPermission('GetUserSongs')">
            <router-link class="link-dropdown"  :to="{ name: 'MySongs' }">{{$t('my_songs')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="hasPermission('Play')">
            <router-link class="link-dropdown"  :to="{ name: 'SongsVoted' }">{{$t('voted')}}</router-link>
          </b-dropdown-item>
           <b-dropdown-item v-show="hasPermission('GetUserClaims')">
            <router-link class="link-dropdown"  :to="{ name: 'MyClaims' }">{{$t('myclaims')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="loggedIn()">
            <router-link class="link-dropdown"  :to="{ path: `/contractservice/${this.userLogged().Id}` }">{{$t('contract')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="!loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'Subscribe' }">{{$t('subscribe')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-divider></b-dropdown-divider>
          <b-dropdown-item v-show="!loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'login' }">{{$t('login')}}</router-link>
          </b-dropdown-item>
          <b-dropdown-item v-show="loggedIn()">
            <router-link class="link-dropdown"  :to="{ name: 'logout' }">{{$t('logout')}}</router-link>
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
import  Language  from '../components/Language.vue'

@Component({
  components:{
        Language
    }
})
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

@media (max-width: 600px) {
.home-mobile-nav .nav-link{
  padding-left: 0 !important;
  padding-right: 0 !important;
}

.play-nav-margin{
  margin-left:0 !important;
}

}

.play-nav{
  margin-top : 10px;
}

.play-nav-margin{
 
}
.navbar-mobile{
  padding-left:0;
  padding-right:8px;
}
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