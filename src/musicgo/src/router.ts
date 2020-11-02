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

  ],
});
