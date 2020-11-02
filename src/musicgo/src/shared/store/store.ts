import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import userService, {
  UserService
} from "@/shared/services/UserService.ts";
import { UserViewModel } from '../classes/UserViewModel';
import { AuthenticationViewModel } from '../classes/AuthenticationViewModel';


Vue.use(Vuex)

const store = new Vuex.Store({
  state: {
    token: localStorage.getItem('access_token') || null,
    user: localStorage.getItem('userLog') || null,
    usersList: localStorage.getItem('usersList') || null
  },
  getters: {
    loggedIn(state) {
      return state.token !== null
    },
    user(state){
      return JSON.parse(state.user!);
    },
    usersList(state){
      return JSON.parse(state.usersList!);
    },
    getToken(state){
      return state.token
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
     destroyUsersList(state) {
      state.usersList = null
    },
     retrieveUsersList(state, usersList){
        state.usersList = usersList
     }
  },
  actions: {
     destroyUsersList(context) {
     
       if (context.getters.loggedIn) {
              localStorage.removeItem('usersList')
              context.commit('destroyUsersList')
        }
     },
     destroyToken(context) {
  
       if (context.getters.loggedIn) {
              localStorage.removeItem('access_token')
              localStorage.removeItem('userLog')
              context.commit('destroyToken')
      }
     },
     retrieveToken(context, obj:UserViewModel) {
      return new Promise((resolve,reject)=>{
          userService.login(obj).then(res => {
            console.log(res);
            if (res.status === 200) {

              localStorage.setItem('userLog', JSON.stringify(res.data))                   
              context.commit('saveUser', JSON.stringify(res.data))

              
              let userLogin: AuthenticationViewModel = new AuthenticationViewModel();
              userLogin.username = obj.UserName;
              userLogin.password = obj.Password;

                return new Promise((resolveUser,rejectUser)=>{
                  userService.getAuthToken(userLogin).then(resUser => {
                    console.log(resUser);
                    if (resUser.status === 200) {
                      var token = JSON.parse(JSON.stringify(resUser.data)).access_token;
                      if(token != null){
                        localStorage.setItem('access_token', token);
                        context.commit('retrieveToken', token);
                        resolve(res);
                      }
                                           
                    }
                  }).catch(error => {
                    
                    localStorage.removeItem('userLog')
                    localStorage.removeItem('access_token')
                    context.commit('destroyToken')
                    console.log(error)         
                    reject(error);
                  });
        
              })
              

            }

          }).catch(error => {
            console.log(error)         
            reject(error);
          });

      })
      
      },
      retrieveUsersList(context){
        return new Promise((resolve,reject)=>{
        userService.get().then(res=> {
          if(res.status === 200){
            context.commit('retrieveUsersList',res.data)
            localStorage.setItem('usersList', JSON.stringify(res.data))
            resolve(res)
          }
        })
        .catch(error =>{
          console.log(error)
          reject(error)
        })})
      }    
  }
})


export default store;