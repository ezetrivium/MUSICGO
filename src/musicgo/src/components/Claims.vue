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
                  <div style="display:inline-flex">
                    <h2>{{ $t("claims") }}</h2>
                     
                  </div>
                    
                  
              </b-col>
               <b-col sm="8">
                <b-button style="margin-left:10px" class="add-user-button">
                  <download-excel worksheet="MusicGO"
                      name="claimsTable.xls" :data="claims" :fields="excelFields">
                    <p class="add-button-text" >{{ $t("export_xls") }}</p>
                  </download-excel>
                  
                </b-button>
                <b-button style="margin-left:10px" class="add-user-button" @click="exportToPDF()">
                  <p class="add-button-text" >{{ $t("export_pdf") }}</p>
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

              

              <!-- <b-col sm="7" md="6" class="my-1">
                <b-pagination
                  v-model="currentPage"
                  :total-rows="totalRows"
                  :per-page="perPage"
                  align="fill"
                  size="sm"
                  class="my-0"
                ></b-pagination>
              </b-col> -->
          </b-row>
        </div>
        <b-row>
          <b-col md="12" style="position:unset !important">
            <div> 
              <b-table
              show-empty
              :emptyText="$t('no_items_to_show')"
              responsive="sm"
              id="claims-table"
              :items="claims"
              :fields="fields"
              :current-page="currentPage"
              :per-page="perPage"
              :filter="filter"
              :filterIncludedFields="filterOn"
              @filtered="onFiltered"
              :busy="isBusy"
              thead-tr-class="table-head-row"
              class="table-items"
              >

              <template v-slot:table-busy>
                <div class="text-center text-danger my-2">
                  <b-spinner class="align-middle"></b-spinner>
                </div>
              </template>
              <template v-slot:cell(actions)="row" >
                <b-row>
                  <b-col >
                    <b-icon scale="1" icon="eye" 
                    @click="info(row.item, row.index, $event.target)" style="cursor:pointer"></b-icon>
                  </b-col>
                  
                  <b-col>    
                    <b-icon scale="1" icon="circle-fill" variant="success" v-show="hasPermission('GetClaims')"
                     @click="changeClaimState(row.item,states.find(s=>s.Name=='approved'))" style="cursor:pointer"></b-icon>
                  </b-col>
                  <b-col>    
                    <b-icon scale="1" icon="circle-fill" variant="warning" v-show="hasPermission('GetClaims')"
                     @click="changeClaimState(row.item,states.find(s=>s.Name=='pending'))" style="cursor:pointer"></b-icon>
                  </b-col>
                  <b-col>    
                    <b-icon scale="1" icon="circle-fill" variant="danger" v-show="hasPermission('GetClaims')"
                     @click="changeClaimState(row.item,states.find(s=>s.Name=='declined'))" style="cursor:pointer"></b-icon>
                  </b-col>
                </b-row>
 
              </template>

            
              </b-table> 

            
              <b-modal 
              centered 
              id="info-modal" 
              @hidden="resetInfoModal"
              
              ok-only 
              scrollable
              size="md"
              header-bg-variant="dark"
              header-text-variant="light"
              body-bg-variant="dark"
              body-text-variant="light"
              footer-bg-variant="dark"
              footer-text-variant="light"
              >

              <div slot="modal-title">
                <b-row>
                  <b-col>
                    
                    <p style="margin-bottom:0">{{$t('claim')}}</p>
                  </b-col>                   
                  </b-row>
              </div>
                

                <b-container fluid>
                  <b-row class="mb-1">

                    <b-col >
                      
                     

                      <b-row class="mb-1 row-data">
                        <b-col>
                          <strong>{{ $t("description") }}</strong>
                        </b-col>
                      </b-row>
                      <b-row class="mb-1 row-data">

                        <b-col>
                          {{this.claimSelected.Description}}
                        </b-col>
                      </b-row>


                      
                    </b-col>


                  
                  </b-row>
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
import { UserViewModel } from '../shared/classes/UserViewModel';


import claimService, { ClaimService } from "../shared/services/ClaimService";
import stateService, { StateService } from "../shared/services/StateService";

import { ClaimViewModel } from '@/shared/classes/ClaimViewModel';


import { Permissionshelper } from '../shared/classes/Permissions-helper';
import { StateViewModel } from '@/shared/classes/StateViewModel';
import { jsPDF } from "jspdf";
import 'jspdf-autotable';

import downloadExcel from "vue-json-excel";

@Component({
  components:{
        downloadExcel
    }
})
export default class Claims extends Vue {

    private isBusy : boolean = false;
    
    private loading: boolean = false;

    private claimservice: ClaimService = claimService;

    private stateservice:StateService = stateService;

    private states : StateViewModel[] = [];


    private claims : ClaimViewModel[] =[];
    private totalRows : number = 1;
    private currentPage : number = 1; 
    private perPage : number = 5;
    private pageOptions : number[] = [5, 10, 15];
    private filter : string | null = null
    private filterOn: string[] = ['UserName'];
  
    private claimSelected: ClaimViewModel = new ClaimViewModel();


    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;

   


    private fields: any[] = [
          { key: 'Date', label:  'Date' , sortable: true},
          { key: 'User.UserName', label: 'UserName', sortable: true},
          { key: 'SongClaimed.Name', label: 'Song', sortable: true},   
          { key: 'State.Name', label: 'State', sortable: true},         
          { key: 'actions', label: 'Actions' }
        ]
  
    created(){
      
    }  
    mounted() {
      
      
      
        this.getClaims();
        this.getStates();
      
    }


    private excelFields: any = {
      "Fecha":"Date",
      "Usuario" : {
        field:"User.UserName"
      },
      "Canción" : {
        field:"SongClaimed.Name"
      },
      "Estado" : {
        field:"State.Name"
      },    

    }


    exportToPDF(){
      const doc = new jsPDF() as any;
      const claims = this.claims;
      doc.autoTable({ 
        
        html:"#claims-table",

        
          columns: [
            { header: 'Fecha', dataKey: 'Date' },
            { header: 'Usuario', dataKey: 'User.UserName' },
            { header: 'Canción', dataKey: 'SongClaimed.Name' },
            { header: 'Estado', dataKey: 'State.Name' }
          ],
      })
      
      doc.save('claimstable.pdf')
    }

    
    changeClaimState(item : ClaimViewModel,state:StateViewModel){

    this.isBusy = true;

    item.State = state;

      this.claimservice.update(item)
      .then(response => {
        this.getClaims();
        this.isBusy = false;
        
        this.message = response.data;
        this.variant = "success";  
        this.showAlert = true;
    
      })
      .catch(error => {

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
        
       
      });
    }


    hasPermission(permission : string){
      return Permissionshelper.HasPermission(permission);      
    }



    resetInfoModal() {
        this.claimSelected= new ClaimViewModel();
    }





    getClaims(){

      this.isBusy = true;
      this.claimservice.get()
      .then(response => {
        this.claims = response.data;

        this.isBusy = false;
        this.totalRows = this.claims.length
      })
      .catch(error =>{
 
        this.isBusy = false;
    })}

    getStates(){

      this.isBusy = true;
      this.stateservice.get()
      .then(response => {
        this.states = response.data;

      })
      .catch(error =>{

    })}




      info(item : any, index : number, button : any) {
        this.claimSelected = item;
        this.$root.$emit('bv::show::modal', 'info-modal', button)
      }




    
     

 
      onFiltered(filteredItems: any) {
        // Trigger pagination to update the number of buttons/pages due to filtering
        this.totalRows = filteredItems.length
        this.currentPage = 1
      }

    

    


    
}
</script>

<style >
</style>