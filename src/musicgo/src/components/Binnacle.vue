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
                    <h2>{{ $t("binnacle") }}</h2>

                  </div>
                    
                  
              </b-col>
              <b-col sm="8">
                <b-button style="margin-left:10px" class="add-user-button">
                  <download-excel worksheet="MusicGO"
                      name="binnacleTable.xls" :data="binnacles" :fields="excelFields">
                    <p class="add-button-text" >{{ $t("export_xls") }}</p>
                  </download-excel>
                  
                </b-button>
                <b-button style="margin-left:10px" class="add-user-button" @click="exportToPDF()">
                  <p class="add-button-text" >{{ $t("export_pdf") }}</p>
                </b-button>
                <b-button class="add-user-button" @click="getBinnacle()" v-show="hasPermission('GetBinnacle')">
                  <p class="add-button-text" >{{ $t("get_binnacle") }}</p>
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
            
          <b-row>  
            <b-col sm="12" md="4" lg="4" class="my-1">
                <b-input-group class="mb-3">
                    <b-form-input
                        id="datefrom-input"
                        v-model="dateFrom"
                        type="text"
                        
                        :placeholder="$t('enterdatefrom')"
                        autocomplete="off"
                    ></b-form-input>
                    <b-input-group-append>
                        <b-form-datepicker
                        v-model="dateFrom"
                        button-only
                        right
                        locale="en-US"
                        aria-controls="datefrom-input"
                        
                        ></b-form-datepicker>
                    </b-input-group-append>
                </b-input-group>
            </b-col>
            <b-col sm="12" md="4" lg="4" class="my-1">
                <b-input-group class="mb-3">
                    <b-form-input
                        id="dateto-input"
                        v-model="dateTo"
                        type="text"
                        
                        :placeholder="$t('enterdateto')"
                        autocomplete="off"
                    ></b-form-input>
                    <b-input-group-append>
                        <b-form-datepicker
                        v-model="dateTo"
                        button-only
                        right
                        locale="en-US"
                        aria-controls="dateto-input"
                        
                        ></b-form-datepicker>
                    </b-input-group-append>
                </b-input-group>
            </b-col>
            <b-col sm="12" md="4" lg="4" class="my-1">
                <b-form-input
                    id="input-text-username"
                    v-model="userName"
                    type="text"
                    :placeholder="$t('username')"
                              
                ></b-form-input>
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
              id="binnacle-table"
              :items="binnacles"
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

import { Permissionshelper } from '../shared/classes/Permissions-helper';
import { BinnacleViewModel } from '@/shared/classes/BinnacleViewModel';
import { LanguageHelper } from "@/shared/classes/Language-helper";
import binnacleService, { BinnacleService } from "../shared/services/BinnacleService";
import { jsPDF } from "jspdf";
import 'jspdf-autotable';

import downloadExcel from "vue-json-excel";

@Component({
  components:{
        downloadExcel
    }
})
export default class Binnacle extends Vue {
    private isBusy : boolean = false;

 
    private binnacleservice: BinnacleService = binnacleService;

    
    private totalRows : number = 1;
    private currentPage : number = 1; 
    private perPage : number = 5;
    private pageOptions : number[] = [5, 10, 15];
    private filter : string | null = null
    private filterOn: string[] = ['Description'];   
    private binnacles: BinnacleViewModel[] = [];
    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;

    private dateTo : string  = new Date().toLocaleDateString("fr-CA");
    private dateFrom : string = new Date().toLocaleDateString("fr-CA");
    private userName:string ="";


    private fields: any[] = [
          { key: 'Date', label: 'Date', sortable: true},
          {
            key: 'User.UserName',
            label: 'UserName',
            formatter: (value : boolean,key : string, item : BinnacleViewModel) => {
              return item.User == null ? 'Undefined' : item.User!.UserName
            }
          },
          { key: 'Description', label: 'Description', sortable: true}
          
        ]

    private excelFields: any = {
      "Fecha":"Date",
      "Usuario" : {
        field:"User.UserName"
      },
        
      "Descripción" : "Description",
      

    }         


    mounted(){
        
    }


    exportToPDF(){
      const doc = new jsPDF() as any;
      const binnacles = this.binnacles;
      doc.autoTable({ 
        
        html:"#binnacle-table",

        
          columns: [
            { header: 'Fecha', dataKey: 'Date' },
            { header: 'Usuario', dataKey: 'User.UserName' },
            { header: 'Descripción', dataKey: 'Description' },
          ],
      })
      
      doc.save('binnacletable.pdf')
    }


    hasPermission(permission : string){
      return Permissionshelper.HasPermission(permission);      
    }


    onFiltered(filteredItems: any) {
        // Trigger pagination to update the number of buttons/pages due to filtering
        this.totalRows = filteredItems.length
        this.currentPage = 1
    }

    getBinnacle(){
        this.isBusy = true;
          
      this.binnacleservice.getWithFilters(this.dateTo, this.dateFrom,this.userName)
        .then(response => {
            this.isBusy = false;
            
            this.binnacles= response.data; 
            this.totalRows = this.binnacles.length
        })
        .catch(error =>{     
            this.isBusy = false;                     
        })
    }



}

</script>

<style >

</style>