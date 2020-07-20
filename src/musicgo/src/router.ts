import Vue from 'vue';
import Router from 'vue-router';
import Home from '@/components/Home.vue';
import Login from '@/components/auth/Login.vue';
import Logout from '@/components/auth/Logout.vue';
import Register from '@/components/auth/Register.vue';
import RecoverPassword from '@/components/auth/RecoverPassword.vue';
import Users from '@/components/Users.vue';

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
      path: '/register',
      name: 'register',
      component: Register,
      meta: {
          requiresVisitor: true,
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
    }

  ],
});
