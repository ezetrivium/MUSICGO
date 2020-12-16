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
                    <h2>{{ $t("users_report") }}</h2>
                  </div>
                    
                  
              </b-col>
              <b-col sm="8">
                <b-button style="margin-left:10px" class="add-user-button">
                  <download-excel worksheet="MusicGO"
                      name="usersReport.xls" :data="report.Users" :fields="excelFields">
                    <p class="add-button-text" >{{ $t("export_xls") }}</p>
                  </download-excel>
                  
                </b-button>
                <b-button style="margin-left:10px" class="add-user-button" @click="exportToPDF()">
                  <p class="add-button-text" >{{ $t("export_pdf") }}</p>
                </b-button>

              </b-col>
          </b-row>
        </div>
      
        <b-row>
          <b-col md="6" style="float:left">
            <div> 
              <b-table
              show-empty
              :emptyText="$t('no_items_to_show')"
              responsive="sm"
              id="users-table"
              :items="report.Users"
              :fields="fields"
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
          <b-col md="6" >
              <div style="float:right; margin-top: 40px" id="chart">

              </div>
          </b-col>
        </b-row>
 
    </div>
  </b-container>
</div>

</template>

<script lang="ts">
import { Component, Vue, Watch, Prop } from "vue-property-decorator";
import { UserViewModel } from '../shared/classes/UserViewModel';
import { UserReportViewModel } from '../shared/classes/UserReportViewModel';
import userService, { UserService } from "../shared/services/UserService";

import { jsPDF } from "jspdf";
import 'jspdf-autotable';
import ApexCharts from 'apexcharts'
import downloadExcel from "vue-json-excel";
@Component({
  components:{
        downloadExcel
    }
})
export default class UsersReport extends Vue {

    private isBusy : boolean = false;

    private loading: boolean = false;
   

    private userservice: UserService = userService;
    
    private report : UserReportViewModel = new UserReportViewModel();
   
    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;
    

    private fields: any[] = [
          { key: 'UserName', label:  'Usuario' , sortable: true},
          { key: 'Name', label: 'Nombre', sortable: true},
          { key: 'LastName', label: 'Apellido', sortable: true},
          { key: 'Email', label: 'Email', sortable: true},
          { key: 'Playbacks', label:'Reproducciones', sortable: true, class: 'text-center' }
        ]


    private excelFields: any = {
      "Nombre de Usuario":"UserName",
      "Nombre":"Name",
      "Apellido":"LastName",
      "Email":"Email",
      "Playbacks":"Playbacks",
      "Bloqueado" : {
        field: "Blocked",
        callback:(value : any)=>{
          return value ? 'Si' : 'No'
        }
      },

    
    }

    buildChart(){
        var options = {
            chart: {
                width: 280,
                type: 'pie',
            },
            legend: {
                position: 'bottom'
            },
            series:[this.report.Users.length,this.report.InactiveUsers, this.report.TotalUsers - this.report.InactiveUsers - this.report.Users.length],
            labels:[this.$t("new_users"),this.$t("inactive_users"),this.$t("not_renewed_users")],
            responsive: [{
              breakpoint: 480,
              options: {
                chart: {
                  width: 200
                },
                legend: {
                  position: 'bottom'
                }
              }
            }]

        }

        var chart = new ApexCharts(document.querySelector("#chart"), options);

        chart.render();
    }
    
    exportToPDF(){
      const doc = new jsPDF() as any;
      const users = this.report.Users;
      doc.autoTable({ 
        
        body:users,

        
          columns: [
            { header: 'Nombre de Usuario', dataKey: 'UserName' },
            { header: 'Nombre', dataKey: 'Name' },
            { header: 'Apellido', dataKey: 'LastName' },
            { header: 'Email', dataKey: 'Email' },
            { header: 'Reproducciones', dataKey: 'Playbacks' },
            { header: 'Bloquedo', dataKey: 'Blocked' },
          ],
      })
      
      doc.save('userreport.pdf')
    }
  
    created(){
      
    }  
    mounted() {

      
      this.getUsersReport();
      
      
    }

    

  
   
    

    getUsersReport(){
      this.loading =true;
      this.isBusy = true;
      this.userservice.getReport()
      .then(response => {
        this.report = response.data;
        this.loading = false;
        this.isBusy = false;
        this.buildChart()
      })
      .catch(error =>{
        this.loading = false;
        this.isBusy = false;
      })}


    
}
</script>

<style >

</style>