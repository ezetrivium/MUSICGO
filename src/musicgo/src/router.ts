import Vue from 'vue';
import Router from 'vue-router';
import Home from '@/components/Home.vue';
import MyProfile from '@/components/MyProfile.vue';
import Login from '@/components/auth/Login.vue';
import Logout from '@/components/auth/Logout.vue';
import Subscribe from '@/components/auth/Subscribe.vue';
import RecoverPassword from '@/components/auth/RecoverPassword.vue';
import Users from '@/components/Users.vue';
import ContractServiceComponent from '@/components/auth/ContractServiceComponent.vue';
import Payment from '@/components/auth/Payment.vue';
import LanguageConfiguration from '@/components/LanguageConfiguration.vue';
import DictionaryComponent from '@/components/DictionaryComponent.vue';
import BackUpRestore from '@/components/BackUpRestore.vue';
import Binnacle from '@/components/Binnacle.vue';
import MySongs from '@/components/MySongs.vue';
import Songs from '@/components/Songs.vue';
import MyAlbums from '@/components/MyAlbums.vue';
import Play from '@/components/Play.vue';
import Claims from '@/components/Claims.vue';
import MyClaims from '@/components/MyClaims.vue';
import SongsVoted from '@/components/SongsVoted.vue';
import Permissions from '@/components/Permissions.vue';
import CalculateSuccess from '@/components/CalculateSuccess.vue';
import UsersReport from '@/components/UsersReport.vue';
import SongsReport from '@/components/SongsReport.vue';


Vue.use(Router);

export default new Router({
  routes: [
     {
       path: '/',
       name: 'home',
       component: Home,
     },
    {
        path: '/login',
        name: 'login',
        component: Login,
        meta: {
            requiresVisitor: true,
        },
    },
    {
      path: '/logout',
      name: 'logout',
      component: Logout,
      meta: {
        requiresAuth: true
          },
     },
    {
      path: '/recoverpassword/:code',
      name: 'recoverpassword',
      component: RecoverPassword,
      props: true,
      meta: {
          requiresVisitor: true,
      },
    }, {
      path: '/usersList',
      name: 'usersList',
      component: Users,
      meta: {
        requiresAuth: true
      },
    },
    {
      path: '/subscribe',
      name: 'Subscribe',
      component: Subscribe,
      meta: {
        requiresVisitor: true,
      },
    },
    {
      path: '/contractservice/:userID',
      name: 'ContractService',
      component: ContractServiceComponent,
      props: true
    },
    {
      path: '/payment/',
      name: 'Payment',
      component: Payment,
      props: true
    },
    {
    path: '/myprofile',
    name: 'MyProfile',
    component: MyProfile,
    meta: {
      requiresAuth: true
    },
    
    },
    {
      path: '/language',
      name: 'LanguageComponent',
      component: LanguageConfiguration,
      meta: {
        requiresAuth: true
      },
      
    },
    {
      path: '/dictionary/:languageProp',
      name: 'DictionaryComponent',
      component: DictionaryComponent,
      meta: {
        requiresAuth: true
      },
      props: true
      
    },

    {
      path: '/backuprestore',
      name: 'BackUpRestore',
      component: BackUpRestore,
      meta: {
        requiresAuth: true
      },
 
      
    },
    {
      path: '/binnacle',
      name: 'Binnacle',
      component: Binnacle,
      meta: {
        requiresAuth: true
      },
    },
    {
      path: '/mysongs',
      name: 'MySongs',
      component: MySongs,
      meta: {
        requiresAuth: true
      },
    },
    {
      path: '/myalbums',
      name: 'MyAlbums',
      component: MyAlbums,
      meta: {
        requiresAuth: true
      },
    },
    {
      path: '/songs',
      name: 'Songs',
      component: Songs,
      meta: {
        requiresAuth: true
      },
    },
    {
      path: '/play',
      name: 'Play',
      component: Play,
      meta: {
        requiresAuth: true
      },
    },
    {
      path: '/claims',
      name: 'Claims',
      component: Claims,
      meta: {
        requiresAuth: true
      },
    },
    {
      path: '/myclaims',
      name: 'MyClaims',
      component: MyClaims,
      meta: {
        requiresAuth: true
      },
    },
    {
      path: '/songsvoted',
      name: 'SongsVoted',
      component: SongsVoted,
      meta: {
        requiresAuth: true //pasar esto al navbar
      },
    },
    {
      path: '/permissions',
      name: 'Permissions',
      component: Permissions,
      meta: {
        requiresAuth: true
      },
    },
    {
      path: '/calculatesuccess',
      name: 'CalculateSuccess',
      component: CalculateSuccess,
      meta: {
        requiresAuth: true
      },
      props: true
    },
    {
      path: '/usersreport',
      name: 'UsersReport',
      component: UsersReport,
      meta: {
        requiresAuth: true
      },
    },
    {
      path: '/songsreport',
      name: 'SongsReport',
      component: SongsReport,
      meta: {
        requiresAuth: true
      },
    },
  ],
});
