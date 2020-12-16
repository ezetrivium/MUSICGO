<template>
       <div style="margin-top:50px">
  <b-container fluid >
    <div>
        <b-alert :variant="variant" dismissible v-model="showAlert">{{$t(message)}}</b-alert>
    </div> 
    <div class="table-wrapper">
        <div class="table-title">
          <b-row>
              <b-col sm="6" >
                  <div>
                    <h2>{{ $t("backups") }}</h2>

                  </div>
                    
                  
              </b-col>
              <b-col sm="6">
                <b-button class="add-user-button" @click="setBackup()" v-show="hasPermission('Backup')">
                  <p class="add-button-text" >{{ $t("set_backup") }}</p>
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

              :items="backups"
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
                    <b-icon scale="1" icon="server" v-show="hasPermission('Restore')"
                     @click="restore(row.item)" style="cursor:pointer"></b-icon>
                  </b-col>

                </b-row>
 
              </template>
             

            
              </b-table> 

           
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
import { BackupData } from '@/shared/classes/BackupData';
import backupService, { BackupService } from "../shared/services/BackupService";
import { Permissionshelper } from '../shared/classes/Permissions-helper';

@Component({})
export default class BackUpRestore extends Vue {
    private isBusy : boolean = false;

    private loadingAdd: boolean = false;
    private loadingUpdate: boolean = false;

    private backupservice: BackupService = backupService;

    
    private totalRows : number = 1;
    private currentPage : number = 1; 
    private perPage : number = 5;
    private pageOptions : number[] = [5, 10, 15];
    private filter : string | null = null
    private filterOn: string[] = ['Date'];   

    private backups: BackupData[] = [];
    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;

    private fields: any[] = [
          { key: 'Date', label: 'Date', sortable: true},
          { key: 'Path', label: 'Path', sortable: true},
          { key: 'actions', label: 'Actions' }
        ]
    
    mounted(){
        this.getBackups();
    }

    hasPermission(permission : string){
      return Permissionshelper.HasPermission(permission);      
    }

    onFiltered(filteredItems: any) {
        // Trigger pagination to update the number of buttons/pages due to filtering
        this.totalRows = filteredItems.length
        this.currentPage = 1
    }

    getBackups(){
        this.isBusy = true;
          
      this.backupservice.get()
        .then(response => {
            this.isBusy = false;
            
            this.backups = response.data; 
            this.totalRows = this.backups.length;
        })
        .catch(error =>{     
            this.isBusy = false;                     
        })
    }


     setBackup(){
        this.isBusy = true;
          
        this.backupservice.set()
        .then(response => {
            this.isBusy = false;
            this.variant = "success"; 
            this.message = response.data;
            this.showAlert = true;
            this.getBackups();
        })
        .catch(error =>{     
            this.isBusy = false;  
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
                
        })
    }


    restore(backup:BackupData ){
        this.isBusy = true;
          
        this.backupservice.restore(backup)
        .then(response => {
            this.isBusy = false;
            this.variant = "success"; 
            this.message = response.data;
            this.showAlert = true;
            this.getBackups();
        })
        .catch(error =>{     
            this.isBusy = false;  
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
                
        })
    }

}
</script>

<style>

</style>