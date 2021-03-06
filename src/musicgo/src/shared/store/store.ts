import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import userService, {
  UserService
} from "@/shared/services/UserService.ts";
import songService, {
  SongService
} from "@/shared/services/SongService.ts";
import { UserViewModel } from '../classes/UserViewModel';
import { AuthenticationViewModel } from '../classes/AuthenticationViewModel';
import  languageStore  from '../store/languageStore';


Vue.use(Vuex)

const store = new Vuex.Store({
  modules:{
    languageStore
  },
  state: {
    token: localStorage.getItem('access_token') || null,
    user: localStorage.getItem('userLog') || null,
    usersList: localStorage.getItem('usersList') || null,
    songsList: localStorage.getItem('songsList') || null,
    songsToPlay: localStorage.getItem('songsToPlay') || null
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
    songsList(state){
      return JSON.parse(state.songsList!);
    },
    songsToPlay(state){
      return JSON.parse(state.songsToPlay!);
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
     },
     destroySongsList(state) {
      state.songsList = null
    },
     retrieveSongsList(state, songsList){
        state.songsList = songsList
     },
     destroySongsToPlay(state) {
      state.songsToPlay= null
    },
     retrieveSongsToPlay(state, songsToPlay){
        state.songsToPlay = songsToPlay
     }
  },
  actions: {
     destroyUsersList(context) {
     
       if (context.getters.loggedIn) {
              localStorage.removeItem('usersList')
              context.commit('destroyUsersList')
        }
     },
     destroySongsList(context) {
     
      if (context.getters.loggedIn) {
             localStorage.removeItem('songsList')
             context.commit('destroySongsList')
       }
    },
    destroySongsToPlay(context) {
     
      if (context.getters.loggedIn) {
             localStorage.removeItem('songsToPlay')
             context.commit('destroySongsToPlay')
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
            context.commit('retrieveUsersList',JSON.stringify(res.data))
            localStorage.setItem('usersList', JSON.stringify(res.data))
            resolve(res)
          }
        })
        .catch(error =>{
          console.log(error)
          reject(error)
        })})
      },
      retrieveSongsList(context){
        return new Promise((resolve,reject)=>{
        songService.get().then(res=> {
          if(res.status === 200){
            context.commit('retrieveSongsList',JSON.stringify(res.data))
            localStorage.setItem('songsList', JSON.stringify(res.data))
            resolve(res)
          }
        })
        .catch(error =>{
          console.log(error)
          reject(error)
        })})
      },
      retrieveSongsToPlay(context){
        return new Promise((resolve,reject)=>{
        songService.getSongsToPlay().then(res=> {
          if(res.status === 200){
            context.commit('retrieveSongsToPlay',JSON.stringify(res.data))
            localStorage.setItem('songsToPlay', JSON.stringify(res.data))
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