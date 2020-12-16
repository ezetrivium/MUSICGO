<template>
    <div>
        <b-dropdown id="input-group-language" :text="languageSelected.Code" >
            <b-dropdown-item style="color:white" v-for="item in languages" :key="item.Id" @click="changeLang(item.Code)">{{item.Name}}</b-dropdown-item>
            <!-- <b-form-select
                id="languages"
                v-model="languageSelected"                                                                           
                >
                <b-form-select-option
                    v-for="(option, index) in languages"
                    :key="'lang-' + index"
                    :value="option"            
                    >{{ option.Code }}</b-form-select-option>                           
            </b-form-select> -->
                           
        </b-dropdown>
    </div>
</template>

<script lang="ts">

import { Component, Vue, Watch, Prop } from "vue-property-decorator";
import languageService, { LanguageService } from "@/shared/services/LanguageService";
import { LanguageViewModel } from '@/shared/classes/LanguageViewModel';
import { LocaleMessageObject } from 'vue-i18n';
import { LanguageHelper } from "@/shared/classes/Language-helper";

@Component({})
export default class Language extends Vue {
    private languageservice: LanguageService = languageService;
    private languages: LanguageViewModel[] = [];
    private languageSelected : LanguageViewModel | undefined = new LanguageViewModel();
    private languageToTranslate : LanguageViewModel | undefined = new LanguageViewModel();

    created(){
        this.getLanguages();
    }

    getLanguages(){
            
      this.languageservice.get()
        .then(response => {
          this.languages = response.data; 
        //   this.languageSelected = this.languages.find(l=>l.Code==LanguageHelper.locale)
          this.changeLang(LanguageHelper.locale)
        })
        .catch(error =>{                          
        })
    }



    @Watch("$i18n.locale")
    changeLang(lang: string){
        this.languageservice.getByCode(lang).
        then(res => {
            this.languageSelected = this.languages.find(l=>l.Code == lang)
            this.languageToTranslate = res.data;
            this.$i18n.setLocaleMessage(this.languageToTranslate.Code, this.languageToTranslate.Dictionary as LocaleMessageObject)
            this.$i18n.locale = this.languageToTranslate.Code;
            LanguageHelper.locale = lang;
            

        }).catch(error =>{})
    }
}
</script>


<style scoped>
.b-dropdown  >>> ul{
  right: 0;
  left: auto;
 
}

.b-dropdown  >>> .dropdown-item{
  color: white;
 
}

li:hover{
  margin-left: 5px;
}
</style>