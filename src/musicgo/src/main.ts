import Vue from 'vue';
import '@/plugins/bootstrap-vue/bootstrap-vue';
import App from './App.vue';
import router from './router';
import store from './shared/store/store';
import i18n from '@/plugins/i18n/i18n';
import './assets/css/style.css'

Vue.config.productionTip = false;


//aca abajo hago la logica y pregunto si el usuario tiene acceso a la pagina adonde va sino lo dirigo a una pagina
//404
router.beforeEach((to, from, next) => {
    if (to.matched.some((record) => record.meta.requiresAuth)) {
      if (!store.getters.loggedIn) {
        next({
          name: 'login',
        });
      } else {
        next();
      }
    } else if (to.matched.some((record) => record.meta.requiresVisitor)) {
      if (store.getters.loggedIn) {
        next({
          name: 'home',
        });
      } else {
        next();
      }
    } else {
      next();
    }
  });

new Vue({
    i18n,
    router,
    store,
    render: (h) => h(App),
}).$mount('#app');
