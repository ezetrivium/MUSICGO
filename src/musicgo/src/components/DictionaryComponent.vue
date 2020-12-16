<template>
    <div style="margin-top:50px">
  <b-container fluid >
    <router-link :to="{ name: 'LanguageComponent'}"><b-icon icon="arrow-left" ></b-icon> {{ $t("back_to_languages") }}</router-link>
    <div>
        <b-alert :variant="variant" dismissible v-model="showAlert">{{$t(message)}}</b-alert>
    </div> 
    <div class="table-wrapper">
        <div class="table-title">
          <b-row>
              <b-col sm="4" >
                  <div>
                    <h2>{{language.Name}}</h2>

                  </div>
                    
                  
              </b-col>
              <b-col sm="8">
                <b-button class="add-user-button" v-b-modal.crud-modal v-show="hasPermission('AddLanguage')">
                  <b-icon icon="plus" ></b-icon><p class="add-button-text" >{{ $t("add_dictionary") }}</p>
                </b-button>
              </b-col>
          </b-row>
        </div>
        <div class="table-filter">
          <b-row>  

              <b-col sm="2" class="my-1">
                <b-form-group
                  label="Per page"
                  label-cols-sm="6"
                  label-cols-md="6"
                  label-align-sm="left"
                  label-align-md="left"
                  label-size="sm"
                  label-for="perPageSelect"
                  class="mb-0 "
                  style="color:white;"
                >
                  <b-form-select
                    v-model="perPage"
                    id="perPageSelect"
                    size="sm"
                    :options="pageOptions"
                  ></b-form-select>
                </b-form-group>
              </b-col>   
              <b-col sm="10" class="my-1">
                <b-form-group
                label="Filter"
                label-cols-sm="3"
                label-align-sm="right"
                label-size="sm"
                label-for="filterInput"
                class="mb-0"
                style="color:white;"
                >
                <b-input-group size="sm">
                  <b-form-input
                    v-model="filter"
                    type="search"
                    id="filterInput"
                    placeholder="Type to Search"
                  ></b-form-input>
                    <b-input-group-append>
                      <b-button :disabled="!filter" @click="filter = ''">{{ $t("clear") }}</b-button>
                    </b-input-group-append>
                  </b-input-group>
                </b-form-group>
               </b-col>

              


          </b-row>
        </div>
        <b-row>
          <b-col md="12" style="position:unset !important">
            <div> 
              <b-table
              show-empty
              :emptyText="$t('no_items_to_show')"
              responsive="sm"

              :items="items"
              :fields="fields"
              :current-page="currentPage"
              :per-page="perPage"
              :filter="filter"
              :filterIncludedFields="filterOn"
              @filtered="onFiltered"
              :busy="isBusy"
              class="table-items"
              thead-tr-class="table-head-row"
              >

              <template v-slot:table-busy>
                <div class="text-center text-danger my-2">
                  <b-spinner class="align-middle"></b-spinner>
                </div>
              </template>
              <template v-slot:cell(actions)="row" >
                <b-row>
                 <b-col lg="2">    
                    <b-icon scale="1" icon="pen" v-show="hasPermission('UpdateLanguage')"
                     @click="updateSelect(row.item, row.index, $event.target)" style="cursor:pointer"></b-icon>
                  </b-col>

                </b-row>
 
              </template>

            
              </b-table> 




              <b-modal 
              centered 
              id="update-modal" 
              :title="$t('update_dictionary')"
              @ok="handleUpdateSubmit"
              @hidden="resetUpdateModal"
              size="sm"
              header-bg-variant="dark"
              header-text-variant="light"
              body-bg-variant="dark"
              body-text-variant="light"
              footer-bg-variant="dark"
              footer-text-variant="light"
              >
                <b-container fluid>
                   <div style="text-align:center"> 
                    <b-spinner label="Loading..."  v-show="loadingUpdate"></b-spinner>
                  </div>
 
                  <b-form  ref="modalupdateform" @submit.stop.prevent="handleUpdateSubmit" v-show="!loadingUpdate">
                    <b-row class="mb-1">

                      <b-col lg="12">
                        
                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-value"
                            :label="$t('value')"
                            label-for="input-text-value"
                            class="input-width"
                          >
                            <b-form-input
                              
                              id="input-text-value"
                              v-model="valueUpdated"
                              type="text"
                              required
                              trim
                              :placeholder="$t('entervalue')"
                              :state="valueState()"
                              aria-describedby="input-value-feedback"
                              
                            ></b-form-input>
                            <b-form-invalid-feedback :state="valueState()" id="input-value-feedback">
                              {{ $t("value_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-key"
                            :label="$t('key')"
                            label-for="input-text-key"
                            class="input-width"
                          >
                            <b-form-input
                              
                              id="input-text-key"
                              v-model="keyUpdated"
                              type="text"
                              required
                              :placeholder="$t('enterkey')"
                              :state="keyState()"
                              aria-describedby="input-key-feedback"
                              disabled
                            ></b-form-input>
                            <b-form-invalid-feedback :state="keyState()" id="input-key-update-feedback">
                               {{ $t("key_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        

                      </b-col>
                    </b-row>
                  </b-form>
                </b-container>
                
              </b-modal>


              <b-modal 
              centered 
              id="crud-modal" 
              :title="$t('update_dictionary')"
              @show="resetAddModal"
              @hidden="resetAddModal"
              @ok="handleAddSubmit"
              
              size="sm"
              header-bg-variant="dark"
              header-text-variant="light"
              body-bg-variant="dark"
              body-text-variant="light"
              footer-bg-variant="dark"
              footer-text-variant="light"
              >
                <b-container fluid>
                   <div style="text-align:center"> 
                    <b-spinner label="Loading..."  v-show="loadingAdd"></b-spinner>
                  </div>
 
                  <b-form  ref="modalcrudform" @submit.stop.prevent="handleAddSubmit" v-show="!loadingAdd">
                    <b-row class="mb-1">

                      <b-col lg="12">
                       <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-value"
                            :label="$t('value')"
                            label-for="input-text-value"
                            class="input-width"
                          >
                            <b-form-input
                              
                              id="input-text-value"
                              v-model="valueCreated"
                              type="text"
                              required
                              :placeholder="$t('entervalue')"
                              :state="valueState()"
                              aria-describedby="input-value-feedback"
                              trim
                            ></b-form-input>
                            <b-form-invalid-feedback :state="valueState()" id="input-value-feedback">
                              {{ $t("value_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                         <b-row class="mb-1 row-data">

                           <b-form-group id="input-group-keys-add" 
                           :label="$t('key')"
                           label-for="input-keys-add" 
                           class="input-width"
                           required>
                            <b-form-select
                             id="input-keys-add"
                             v-model="keyCreated"
                             required
                             :state="keyState()" 
                             aria-describedby="input-keys-add-feedback"                          
                             >
                             <b-form-select-option
                                  v-for="(option, index) in  dictionaryKeys"
                                  :key="'key-add-' + index"
                                  :value="option.key"            
                                >{{ option.value }}</b-form-select-option>                           
                            </b-form-select>
                            <b-form-invalid-feedback :state="keyState()" id="input-keys-add-feedback">
                              {{ $t("key_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>

                        </b-row>


                      </b-col>
                    </b-row>
                  </b-form>
                </b-container>
                
              </b-modal> 

            
           
            </div>
            
          </b-col>
        </b-row>
        <b-row>
          <b-col md="6" class="results-length" >
             <p>{{totalRows}} {{ $t("results") }}</p>
          </b-col>
          <b-col md="6" class="my-1" sm="7">
            <b-pagination
                  v-model="currentPage"
                  :total-rows="totalRows"
                  :per-page="perPage"
                  align="fill"
                  size="sm"
                  class="my-0"
                ></b-pagination>
          </b-col>
        </b-row>
    </div>
  </b-container>
</div>
</template>

<script lang="ts">
import { Component, Vue, Watch, Prop } from "vue-property-decorator";

import languageService, { LanguageService } from "../shared/services/LanguageService";
import { Permissionshelper } from '../shared/classes/Permissions-helper';
import { LanguageViewModel } from '@/shared/classes/LanguageViewModel';
import { Dictionary } from 'vue-router/types/router';

@Component({})
export default class DictionaryComponent extends Vue {
    private isBusy : boolean = false;

    private loadingAdd: boolean = false;
    private loadingUpdate: boolean = false;

    private languageservice: LanguageService = languageService;

    
    private totalRows : number = 1;
    private currentPage : number = 1; 
    private perPage : number = 5;
    private pageOptions : number[] = [5, 10, 15];
    private filter : string | null = null
    private filterOn: string[] = ['key'];   

    private dictionaryCreated: Dictionary<string> = {};
    private dictionaryUpdated: Dictionary<string> = {};
    private items : any[] = []
    private valueUpdated = "";
    private valueCreated = "";
    private keyUpdated = "";
    private keyCreated = "";
    private languageUpdated: LanguageViewModel = new LanguageViewModel();
    private language: LanguageViewModel = new LanguageViewModel();
    private languageCreated: LanguageViewModel = new LanguageViewModel();
    private message: string = "";
    private variant: string = "";
    private selectedKey : any = "";
    private showAlert: boolean = false;
    private dictionaryKeys: any[] = [];

    private dictionaryKeysAll: any[] = [];
    


    private fields: any[] = [
          { key: 'key', label: 'Key', sortable: true},
          { key: 'value', label: 'Value', sortable: true},
          { key: 'actions', label: 'Actions'}
        ]

    @Prop({ required:true, type:String}) languageProp: any;

    mounted(){
        this.getLanguage();
        this.getDictionaryKeys();
        this.getDictionaryKeysAll();
    }


    hasPermission(permission : string){
      return Permissionshelper.HasPermission(permission);      
    }


    onFiltered(filteredItems: any) {
        
        this.totalRows = filteredItems.length
        this.currentPage = 1
    }

    getDictionaryKeys(){
        var keys :any[] = [];
        this.languageservice.getDictionaryKeys(this.languageProp)
        .then(response => {
            Object.keys(response.data!).forEach(function(key){
                var item = { key:key, value: response.data![key]};
                 keys.push(item) 
            });
            this.dictionaryKeys = keys;
        })
        .catch(error =>{     
                               
        })
    
    }

    getDictionaryKeysAll(){
        var keys :any[] = [];
        this.languageservice.getDictionaryKeysAll()
        .then(response => {
            Object.keys(response.data!).forEach(function(key){
                var item = { key:key, value: response.data![key]};
                 keys.push(item) 
            });
            this.dictionaryKeysAll = keys;
        })
        .catch(error =>{     
                               
        })
    
    }

    getLanguage(){
        this.isBusy = true;
          
      var lang = new LanguageViewModel();
      lang.Id = this.languageProp;
      this.languageservice.getById(lang)
        .then(response => {
            this.isBusy = false;
            
            // this.language = ;  
            // // 

            var items : any = []
            Object.keys(response.data.Dictionary!).forEach(function(key){

              if(key!=""){
                var item = { key:key, value: response.data.Dictionary![key]};
                 items.push(item) 
              }
                
            });

            this.items = items;
            this.totalRows = this.items.length;
            this.language = response.data;


        })
        .catch(error =>{     
            this.isBusy = false;                     
        })
    }


    resetAddModal() {
        this.dictionaryCreated  = {};
        this.languageCreated = new LanguageViewModel();
        this.keyCreated = "";
        this.valueCreated = "";

   
    }
    resetUpdateModal() {
        this.dictionaryUpdated = {};
        this.languageUpdated = new LanguageViewModel();
        this.keyUpdated = "";
        this.valueUpdated="";
        this.selectedKey = "";
    }





    valueState(){
        return (this.valueUpdated.length > 0 || this.valueCreated.length > 0 ) &&
        (this.valueUpdated.length < 1001 || this.valueCreated.length < 1001 ) ? true : false
    }

    keyState(){
        return (this.keyUpdated.length > 0 || this.keyCreated.length > 0 ) &&
        (this.valueUpdated.length < 1001 || this.valueCreated.length < 1001 ) ? true : false
    }


    checkFormValidity() {

        if(!this.valueState())
          return false;

        if(!this.keyState())
          return false;



        return true;
      }




    updateSelect(item : any, index : number, button : any) {

       this.selectedKey = this.dictionaryKeysAll.find(dk=>dk.value==item.key);
       this.keyUpdated = this.selectedKey.value;
       this.valueUpdated = item.value;
       
       this.$root.$emit('bv::show::modal', 'update-modal', button)

    }

    
    
    handleUpdateSubmit(bvModalEvt : any) {
      bvModalEvt.preventDefault();

      if(!this.checkFormValidity()){
          return
      }

      
      this.loadingUpdate = true;
      this.showAlert = false;
      this.dictionaryUpdated = { [this.selectedKey.key] : this.valueUpdated};
      this.languageUpdated.Dictionary = this.dictionaryUpdated;
      this.languageUpdated.Code = this.language.Code;
      this.languageUpdated.Id = this.language.Id;
      this.languageservice.updateDictionary(this.languageUpdated).then(response => {
        this.getLanguage();
        
        this.loadingUpdate = false;
        this.variant = "success";       
        this.$bvModal.hide('update-modal');
        this.message = response.data;

      })
      .catch(error => {

        this.loadingUpdate = false;
        if(error.response == null){
          this.message = error;
        }
        else{
          if(error.response.data.Message != null){
                 this.message = error.response.data.Message;
            }
            else{
                this.message = error.response.data
            }
        }
        this.variant = "danger";
        this.showAlert = true;
        
        this.$bvModal.hide('update-modal');
      });
    }



    
    handleAddSubmit(bvModalEvt : any) {
      bvModalEvt.preventDefault();

      if(!this.checkFormValidity()){
          return
      }

      this.loadingAdd = true;
      this.showAlert = false;
      this.dictionaryCreated = { [this.keyCreated] : this.valueCreated }
      this.languageCreated.Dictionary = this.dictionaryCreated;
      this.languageCreated.Code = this.language.Code;
      this.languageCreated.Id = this.language.Id;
      this.languageservice.addDictionary(this.languageCreated)
      .then(response => {
        this.getLanguage();
        this.getDictionaryKeys();
        this.loadingAdd = false;
        this.variant = "success";       
        this.$bvModal.hide('crud-modal');
        this.message = response.data;
    
      })
      .catch(error => {
       
        this.loadingAdd = false;
        if(error.response == null){
          this.message = error;
        }
        else{
          if(error.response.data.Message != null){
                 this.message = error.response.data.Message;
            }
            else{
                this.message = error.response.data
            }
        }
        this.variant = "danger";
        this.showAlert = true;
        
        this.$bvModal.hide('crud-modal');
      });
    }

}

</script>

<style >
  
</style>