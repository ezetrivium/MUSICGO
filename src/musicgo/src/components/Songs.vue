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
                    <h2>{{ $t("songs") }}</h2>
                     <b-button v-show="hasPermission('GetSongs')" @click="refreshSongs()"  class="repeat-button">
                      <b-icon icon="arrow-repeat"></b-icon>
                    </b-button>
                  </div>
                    
                  
              </b-col>

              <b-col sm="8">
                <b-button style="margin-left:10px" class="add-user-button">
                  <download-excel worksheet="MusicGO"
                      name="songTable.xls" :data="songs" :fields="excelFields">
                    <p class="add-button-text" >{{ $t("export_xls") }}</p>
                  </download-excel>
                  
                </b-button>
                <b-button  class="add-user-button" @click="exportToPDF()">
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
              id="songs-table"
              :items="songs"
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
                    <b-icon scale="1" icon="eye" v-show="hasPermission('GetSongs')"
                    @click="info(row.item, row.index, $event.target)" style="cursor:pointer"></b-icon>
                  </b-col>
                  
                  <b-col>    
                    <b-icon scale="1" icon="trash" v-show="hasPermission('DeleteSong')"
                     @click="deleteSong(row.item, row.index, $event.target)" style="cursor:pointer"></b-icon>
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
                    
                    <p style="margin-bottom:0">{{this.songSelected.Name}}</p>
                  </b-col>                   
                  </b-row>
              </div>
                

                <b-container fluid>
                  <b-row class="mb-1">

                    <b-col >
                      
                     

                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>{{ $t("plays") }}</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.songSelected.Playbacks}}
                        </b-col>
                      </b-row>
                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>{{ $t("upload_date") }}</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.songSelected.UploadDate.toString().split('T')[0] }}
                        </b-col>
                      </b-row>
                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>{{ $t("category") }}</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.songSelected.Category.Name}}
                        </b-col>
                      </b-row>
                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>{{ $t("album") }}</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.songSelected.Album != null ? this.songSelected.Album.Name : 'Undefined' }}
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
import songService, { SongService } from "../shared/services/SongService";
import categoryService, { CategoryService } from "../shared/services/CategoryService";
import albumService, { AlbumService } from "../shared/services/AlbumService";

import { SongViewModel } from '@/shared/classes/SongViewModel';
import { AlbumViewModel } from '@/shared/classes/AlbumViewModel';
import { CategoryViewModel } from '@/shared/classes/CategoryViewModel';

import { Permissionshelper } from '../shared/classes/Permissions-helper';
import { jsPDF } from "jspdf";
import 'jspdf-autotable';

import downloadExcel from "vue-json-excel";



@Component({
    components:{
        downloadExcel
    }
})
export default class Songs extends Vue {

    private isBusy : boolean = false;
    private loadingAdd: boolean = false;
    private loadingUpdate: boolean = false;
    private loading: boolean = false;

    private songservice: SongService = songService;


    private songs : SongViewModel[] =[];
    private totalRows : number = 1;
    private currentPage : number = 1; 
    private perPage : number = 5;
    private pageOptions : number[] = [5, 10, 15];
    private filter : string | null = null
    private filterOn: string[] = ['Name'];
  
    private songSelected: SongViewModel = new SongViewModel();


    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;

    private file:any|null=null


    private fields: any[] = [
          { key: 'Name', label:  'Name' , sortable: true},
          { key: 'UploadDate', label: 'Upload Date', sortable: true,
          formatter: (value : boolean,key : string, item : SongViewModel) => {
              return item.UploadDate.toString().split('T')[0]
            }
          },
          { key: 'Playbacks', label: 'Playbacks', sortable: true},         
          { key: 'actions', label: 'Actions' }
        ]

    private excelFields: any = {
      "Nombre":"Name",
      "Fecha de subida" : {
        field: "UploadDate",
        callback:(value : any)=>{
          return value.toString().split('T')[0]
        }
      },
      "Reproducciones" : "Playbacks",
      

    }     
  
    created(){
      
    }  
    mounted() {
      
      
      
        this.getSongs(false);
      
    }

    hasPermission(permission : string){
      return Permissionshelper.HasPermission(permission);      
    }



    resetInfoModal() {
        this.songSelected = new SongViewModel();
    }


    exportToPDF(){
      const doc = new jsPDF() as any;
      const songs = this.songs;
      doc.autoTable({ 
        
        body:songs,

        
          columns: [
            { header: 'Nombre', dataKey: 'Name' },
            { header: 'Fecha de Subida', dataKey: 'UploadDate' },
            { header: 'Reproducciones', dataKey: 'Playbacks' },
          ],
      })
      
      doc.save('songtable.pdf')
    }





    getSongs(refresh:boolean){

      this.isBusy = true;
      this.songservice.getRefresh(refresh)
      .then(response => {
        this.songs = response.data;

        this.isBusy = false;
        this.totalRows = this.songs.length
      })
      .catch(error =>{
 
        this.isBusy = false;
      })}



  









      info(item : any, index : number, button : any) {
        // this.infoModal.title = item.UserName;
        this.loading =true;
        this.songservice.getById(item)
        .then(response => {
          this.songSelected = response.data;
          // this.imageSelected = this.showImage();
          this.loading = false;
          this.$root.$emit('bv::show::modal', 'info-modal', button)
        })
        .catch(error =>{
          this.loading = false;                              
        })

      }


      deleteSong(item : any, index : number, button : any) {
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
              this.songservice.delete(item)
                .then(response => {
                  this.getSongs(true);
                  
                  this.variant = "success";       
                  this.message = response.data;
                  this.showAlert = true;    
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
            // if(error.response == null){
            //   this.message = error;
            // }
            // else{
            //   this.message = error.response.data;
            // }
            // this.variant = "danger";
            // this.showAlert = true;
          })

      }


    
      refreshSongs(){
        this.getSongs(true);
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