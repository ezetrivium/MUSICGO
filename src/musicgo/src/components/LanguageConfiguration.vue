<template>
    <div style="margin-top:50px">
  <b-container fluid >
    <div>
        <b-alert :variant="variant" dismissible v-model="showAlert">{{$t(message)}}</b-alert>
    </div> 
    <div class="table-wrapper">
        <div class="table-title">
          <b-row>
              <b-col sm="4" >
                  <div>
                    <h2>{{ $t("language") }}</h2>

                  </div>
                    
                  
              </b-col>
              <b-col sm="8">
                <b-button class="add-user-button" v-b-modal.crud-modal v-show="hasPermission('AddLanguage')">
                  <b-icon icon="plus" ></b-icon><p class="add-button-text" >{{ $t("add_language") }}</p>
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

              :items="languages"
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
                  <b-col lg="2">    
                    <b-icon scale="1" icon="trash" v-show="hasPermission('DeleteLanguage')"
                     @click="deleteLanguage(row.item, row.index, $event.target)" style="cursor:pointer"></b-icon>
                  </b-col>
                  <b-col lg="2">
                      <div class="dictionary-icon">
                          <router-link class="link-dropdown"  :to="{ name: 'DictionaryComponent', params:{languageProp:row.item.Id} }" v-show="hasPermission('UpdateLanguage')">
                        <b-icon scale="1" icon="book" 
                         style="cursor:pointer"></b-icon></router-link>  
                      </div>
                                 
                  </b-col>
                </b-row>
 
              </template>

            
              </b-table> 




              <b-modal 
              centered 
              id="update-modal" 
              :title="$t('update_language')"
              @ok="handleUpdateSubmit"
              @hidden="resetUpdateModal"
              size="md"
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
                            id="input-name"
                            :label="$t('name')"
                            label-for="input-text-name"
                            class="input-width"
                          >
                            <b-form-input
                              
                              id="input-text-name"
                              v-model="languageUpdated.Name"
                              type="text"
                              required
                              :placeholder="$t('entername')"
                              :state="nameState()"
                              aria-describedby="input-name-feedback"
                              trim
                            ></b-form-input>
                            <b-form-invalid-feedback :state="nameState()" id="input-name-feedback">
                              {{ $t("languagename_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-code"
                            :label="$t('code')"
                            label-for="input-text-code"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-code"
                              v-model="languageUpdated.Code"
                              type="text"                              
                              required
                              trim
                              :placeholder="$t('entercode')"
                              :state="codeState()"
                              aria-describedby="input-code-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="codeState()" id="input-code-feedback">
                              {{ $t("code_validate_char") }}
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
              :title="$t('add_language')"
              @show="resetAddModal"
              @hidden="resetAddModal"
              @ok="handleAddSubmit"
              
              size="md"
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
                            id="input-name"
                            :label="$t('name')"
                            label-for="input-text-name"
                            class="input-width"
                          >
                            <b-form-input
                              trim
                              id="input-text-name"
                              v-model="languageCreated.Name"
                              type="text"
                              required
                              :placeholder="$t('entername')"
                              :state="nameState()"
                              aria-describedby="input-name-feedback"
                              
                            ></b-form-input>
                            <b-form-invalid-feedback :state="nameState()" id="input-name-feedback">
                              {{ $t("languagename_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-code"
                            :label="$t('code')"
                            label-for="input-text-code"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-code"
                              v-model="languageCreated.Code"
                              type="text"                              
                              required
                              trim
                              :placeholder="$t('entercode')"
                              :state="codeState()"
                              aria-describedby="input-code-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="codeState()" id="input-code-feedback">
                              {{ $t("code_validate_char") }}
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
import { LanguageHelper } from "@/shared/classes/Language-helper";

@Component({})
export default class LanguageConfiguration extends Vue {
    private isBusy : boolean = false;

    private loadingAdd: boolean = false;
    private loadingUpdate: boolean = false;

    private languageservice: LanguageService = languageService;

    
    private totalRows : number = 1;
    private currentPage : number = 1; 
    private perPage : number = 5;
    private pageOptions : number[] = [5, 10, 15];
    private filter : string | null = null
    private filterOn: string[] = ['Name'];   
    private languageCreated: LanguageViewModel = new LanguageViewModel();
    private languageUpdated: LanguageViewModel = new LanguageViewModel();
    private languages: LanguageViewModel[] = [];
    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;

    private fields: any[] = [
          { key: 'Name', label: 'Name', sortable: true},
          { key: 'Code', label: 'Code', sortable: true},
          { key: 'actions', label: 'Actions' }
        ]


    mounted(){
        this.getLanguages();
    }


    hasPermission(permission : string){
      return Permissionshelper.HasPermission(permission);      
    }


    onFiltered(filteredItems: any) {
        // Trigger pagination to update the number of buttons/pages due to filtering
        this.totalRows = filteredItems.length
        this.currentPage = 1
    }

    getLanguages(){
        this.isBusy = true;
          
      this.languageservice.get()
        .then(response => {
            this.isBusy = false;
            
            this.languages = response.data; 
            this.totalRows = this.languages.length
        })
        .catch(error =>{     
            this.isBusy = false;                     
        })
    }


    resetAddModal() {
        this.languageCreated = new LanguageViewModel();
   
    }
    resetUpdateModal() {
        this.languageUpdated = new LanguageViewModel();

    }


    nameState(){
        return (this.languageCreated.Name.length > 0 && this.languageCreated.Name.length < 51)
          || (this.languageUpdated.Name.length > 0 && this.languageUpdated.Name.length < 51) ? true : false
    }

    codeState(){
        return (this.languageCreated.Code.length < 6 && this.languageCreated.Code.length > 0) ||
              (this.languageUpdated.Code.length < 6 && this.languageUpdated.Code.length > 0) ? true : false
    }


    checkFormValidity() {

        if(!this.codeState())
          return false;

        if(!this.nameState())
          return false;


        return true;
      }




    updateSelect(item : any, index : number, button : any) {
        this.languageUpdated = item as LanguageViewModel;
       this.$root.$emit('bv::show::modal', 'update-modal', button)

    }

    deleteLanguage(item : any, index : number, button : any) {
        // this.infoModal.title = item.UserName;
        this.showAlert = false;
        this.$bvModal.msgBoxConfirm(this.$t("are_you_sure?") as string, {
          title: this.$t("confirmation") as string,
          size: 'sm',
          okVariant: 'success',
          centered: true,
          bodyBgVariant: 'dark',
          bodyTextVariant : 'light',
          headerBgVariant:'dark',
          headerTextVariant:'light',
          footerBgVariant:'dark',
          footerTextVariant:'light'
        })
          .then(value => {
            if(value){
              this.languageservice.delete(item)
                .then(response => {
                  this.getLanguages();
                  
                  this.variant = "success";       
                  this.message = response.data
                })
                .catch(error => {
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
                });
            }    

          })
          .catch(error => {

          })

      }
    
    handleUpdateSubmit(bvModalEvt : any) {
      bvModalEvt.preventDefault();

      if(!this.checkFormValidity()){
          return
      }

   
      this.loadingUpdate = true;
      this.showAlert = false;
      
      this.languageservice.update(this.languageUpdated).then(response => {
        
        this.loadingUpdate = false;
        this.variant = "success";       
        this.$bvModal.hide('update-modal');
        this.message = response.data;
        
        LanguageHelper.removeLanguage();
        
        
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
      })
      .finally(() =>{
          this.getLanguages();
      });
    }



    
    handleAddSubmit(bvModalEvt : any) {
      bvModalEvt.preventDefault();

      if(!this.checkFormValidity()){
          return
      }

      this.loadingAdd = true;
      this.showAlert = false;
      this.languageservice.add(this.languageCreated)
      .then(response => {
        this.getLanguages();
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

<style scoped>
    .dictionary-icon :hover{
       
        padding:0
    }
</style>