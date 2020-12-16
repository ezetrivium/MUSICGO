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
                  </div>
                    
                  
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
                    <b-icon scale="1" icon="play" v-show="hasPermission('Play')"
                    @click="playSong(row.item)" style="cursor:pointer"></b-icon>
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
        <b-row>
            <AudioPlayer ref="audioPlayer"/>
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

import  AudioPlayer  from '../components/AudioPlayer.vue'

@Component({
    components:{
        AudioPlayer
    }
})
export default class SongsVoted extends Vue {

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



    private fields: any[] = [
          { key: 'Name', label:  'Name' , sortable: true},
          { key: 'UploadDate', label: 'UploadDate', sortable: true},
          { key: 'Playbacks', label: 'Playbacks', sortable: true},         
          { key: 'actions', label: 'Actions' }
        ]
  
    created(){
      
    }  
    mounted() {
      
      
      
        this.getSongs();
      
    }

    hasPermission(permission : string){
      return Permissionshelper.HasPermission(permission);      
    }



    playSong(song:SongViewModel){
        const ap: any = this.$refs.audioPlayer;

        ap.play(song);

    }


    getSongs(){

      this.isBusy = true;
      this.songservice.getSongsVoted()
      .then(response => {
        this.songs = response.data;

        this.isBusy = false;
        this.totalRows = this.songs.length
      })
      .catch(error =>{
 
        this.isBusy = false;
      })}


 
      onFiltered(filteredItems: any) {
        // Trigger pagination to update the number of buttons/pages due to filtering
        this.totalRows = filteredItems.length
        this.currentPage = 1
      }

    

    


    
}
</script>

<style >
</style>