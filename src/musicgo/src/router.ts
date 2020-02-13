import Vue from 'vue';
import Router from 'vue-router';
import Home from '@/components/Home.vue';
import Login from '@/components/auth/Login.vue';
import Register from '@/components/auth/Register.vue';

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
      path: '/register',
      name: 'register',
      component: Register,
      meta: {
          requiresVisitor: true,
      },
    },
    
  ],
});
