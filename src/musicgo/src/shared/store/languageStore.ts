import Vue from 'vue';



const languageStore = {
  state: {
    language: localStorage.getItem('language') || null,
  },
  getters: {
    getLanguage(state:any){
        return state.language  //en el componente de idioma en el created tengo que ir a bsucar el idioma inicial
      }
  },
  mutations: {
    SET_LANGUAGE(state: any, lang : any) {
              
        state.language = lang;
    },
  },
  actions: {
    setLanguage(context : any, lang : any) {
      
        context.commit('SET_LANGUAGE', lang)
        localStorage.setItem('language', lang)  
       
    },
  },
};

export default languageStore;
