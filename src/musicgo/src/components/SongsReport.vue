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
                    <h2>{{ $t("songs_report") }}</h2>
                  </div>
                    
                  
              </b-col>
              <b-col sm="8">
                <b-button style="margin-left:10px" class="add-user-button">
                  <download-excel worksheet="MusicGO"
                      name="songsReport.xls" :data="songs" :fields="excelFields">
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
              :items="songs"
              :fields="fields"
              :busy="isBusy"
              class="table-items"
              :per-page="perPage"
              :current-page="currentPage"
              thead-tr-class="table-head-row"
              >

              <template v-slot:table-busy>
                <div class="text-center text-danger my-2">
                  <b-spinner class="align-middle"></b-spinner>
                </div>
              </template>
     

            
              </b-table> 





         
            </div>
            <div>
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
            </div>
          </b-col>


          <b-col md="6" >
              <div style="float:right; margin-top: 50px" id="chart">

              </div>
          </b-col>
        </b-row>

    </div>
  </b-container>
</div>

</template>

<script lang="ts">
import { Component, Vue, Watch, Prop } from "vue-property-decorator";


import songService, { SongService } from "../shared/services/SongService";
import { SongViewModel } from '@/shared/classes/SongViewModel';

import { jsPDF } from "jspdf";
import 'jspdf-autotable';
import ApexCharts from 'apexcharts'
import downloadExcel from "vue-json-excel";
@Component({
  components:{
        downloadExcel
    }
})
export default class SongsReport extends Vue {

    private isBusy : boolean = false;

    private loading: boolean = false;
   

    private songservice: SongService = songService;
    
    private songs : SongViewModel[] =[];
   
    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;
    

    private currentPage : number = 1; 
    private perPage : number = 5;
    private totalRows : number = 1;
    private fields: any[] = [
          { key: 'Name', label:  'Nombre' , sortable: true},
          { key: 'Album.Name', label:  'Álbum' , sortable: true},
          { key: 'User.ArtistName', label:  'Artista' , sortable: true},
          { key: 'Playbacks', label: 'Reproducciones', sortable: true},         
          { key: 'VotesCount', label: 'Votos', sortable: true}, 
        ]


    private excelFields: any = {
      "Nombre":"Name",

      "Álbum" : {
        field: "Album.Name",
      },
      "Artista" : {
        field: "User.ArtistName",
      },
      "Reproducciones" : "Playbacks",
      "Votos" : "VotesCount",
      

    }  


     buildChart(){

         var songsArray :any[] = [];
         this.songs.forEach(function element(value,index,array){
             songsArray.push(value.Name)
         })

         var playbacksArray :any[] = [];
         this.songs.forEach(function element(value,index,array){
             playbacksArray.push(value.Playbacks)
         })

         var votesArray :any[] = [];
         this.songs.forEach(function element(value,index,array){
             votesArray.push(value.VotesCount)
         })
        
        var options = {
            chart: {
                width: 380,
                type: 'bar',
            },
            series:[{
                name: "Reproducciones",
                data:playbacksArray
            },{
                name: "Votos Positivos",
                data:votesArray
            }],
            plotOptions: {
              bar: {
                horizontal: false,
                columnWidth: '55%',
                endingShape: 'rounded'
              },
            },
            legend: {
                position: 'bottom'
            },
            dataLabels: {
              enabled: false
            },
            stroke: {
              show: true,
              width: 2,
              colors: ['transparent']
            },
            xaxis: {
              categories: songsArray,
            },
            yaxis: {
              title: {
                text: "Actividad de Usuarios"
              }
            },
            fill: {
              opacity: 1
            },
            

        }

        var chart = new ApexCharts(document.querySelector("#chart"), options);

        chart.render();
    }
    
    exportToPDF(){
      const doc = new jsPDF() as any;
      const songs = this.songs
      doc.autoTable({ 
        
        html:"#users-table",

        
          columns: [
            { header: 'Nombre', dataKey: 'Nombre' },
            { header: 'Album', dataKey: 'Álbum' },
            { header: 'Artista', dataKey: 'Artista' },
            { header: 'Reproducciones', dataKey: 'Reproducciones' },
            { header: 'Votos', dataKey: 'Votos' },
          ],
      })
      
      doc.save('songsreport.pdf')
    }
  
    created(){
      
    }  
    mounted() {

      
      this.getSongsReport();
      
      
    }

    

    

    getSongsReport(){
      this.loading =true;
      this.isBusy = true;
      this.songservice.getReport()
      .then(response => {
        this.songs = response.data;
        this.loading = false;
        this.isBusy = false;
        this.totalRows = this.songs.length
        this.buildChart();
      })
      .catch(error =>{
        this.loading = false;
        this.isBusy = false;
      })}


    
}
</script>

<style >

</style>