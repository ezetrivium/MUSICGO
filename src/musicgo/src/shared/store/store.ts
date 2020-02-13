import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import userService, {
  UserService
} from "@/shared/services/UserService.ts";
import { UserViewModel } from '../classes/UserViewModel';


Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    token: localStorage.getItem('access_token') || null,
    user: localStorage.getItem('userLog') || null 
  },
  getters: {
    loggedIn(state) {
      return state.token !== null
    },
    user(state){
      return JSON.parse(state.user!);
    }
  },
  mutations: {

    retrieveToken(state, token) {
      state.token = token
    },
    saveUser(state,user){
      state.user = user
    },
    destroyToken(state) {
       state.token = null,
       state.user=null
     },
  },
  actions: {
     destroyToken(context) {
      //  axios.defaults.headers.common['authorization'] = 'Bearer ' + context.state.token
      //  lo de arriba es por si necesito enviar algo al backend cuando se desloguea
       if (context.getters.loggedIn) {
              localStorage.removeItem('access_token')
              context.commit('destroyToken')}
     },
     retrieveToken(context, obj:UserViewModel) {
      return new Promise((resolve,reject)=>{
          userService.login(obj).then(res => {
            console.log(res);
            if (res.status === 200) {
              const token = res.headers["authorization"]
              var objtoken = JSON.parse(token);
              localStorage.setItem('access_token', objtoken.access_token);
              localStorage.setItem('userLog', JSON.stringify(res.data))
              context.commit('retrieveToken', token)
              context.commit('saveUser', JSON.stringify(res.data))
              resolve(res);
            }
          }).catch(error => {
            console.log(error)         
            reject(error);
          });

      })
      
    }    
  }
})
