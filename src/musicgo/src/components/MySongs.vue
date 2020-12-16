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
              <b-col sm="8">
                
                <b-button class="add-user-button" v-b-modal.crud-modal v-show="hasPermission('AddSong')">
                  <b-icon icon="plus" ></b-icon><p class="add-button-text" >{{ $t("add_song") }}</p>
                </b-button>
                <b-button class="add-user-button" style="margin-right:5px"  v-show="hasPermission('GetUserSongs')">
                  <p class="add-button-text" ><router-link style="color:white;text-decoration: none;" :to="{ name: 'MyAlbums'}">{{ $t("albums") }}</router-link></p>
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
                    <b-icon scale="1" icon="eye" v-show="hasPermission('GetUserSongs')"
                    @click="info(row.item, row.index, $event.target)" style="cursor:pointer"></b-icon>
                  </b-col>
                  <b-col>    
                    <b-icon scale="1" icon="pen" v-show="hasPermission('UpdateSong')"
                     @click="updateSelect(row.item, row.index, $event.target)" style="cursor:pointer"></b-icon>
                  </b-col>
                  <b-col>    
                    <b-icon scale="1" icon="trash" v-show="hasPermission('DeleteSong')"
                     @click="deleteSong(row.item, row.index, $event.target)" style="cursor:pointer"></b-icon>
                  </b-col>
                  <b-col>    
                    <b-icon scale="1" icon="check-circle-fill" v-show="hasPermission('CalculateSuccess')"
                     @click="calculateSuccess(row.item)" style="cursor:pointer"></b-icon>
                  </b-col>
                </b-row>
 
              </template>

            
              </b-table> 




              <b-modal 
              centered 
              id="update-modal" 
              :title="$t('update_song')"
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

                      <b-col>
                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-update-name"
                            :label="$t('name')"
                            label-for="input-text-update-name"
                            class="input-width"
                          >
                            <b-form-input
                              
                              id="input-text-update-name"
                              v-model="songUpdated.Name"
                              type="text"
                              required
                              :placeholder="$t('entername')"
                              :state="nameState()"
                              aria-describedby="input-name-update-feedback"
                              trim
                            ></b-form-input>
                            <b-form-invalid-feedback :state="nameState()" id="input-name-update-feedback">
                              {{ $t("name_song_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>


                        <b-row class="mb-1 row-data">

                           <b-form-group id="input-group-category" 
                           :label="$t('category')"
                           label-for="input-category" 
                           class="input-width"
                           required>
                            <b-form-select
                             id="input-category"
                             v-model="songUpdated.Category"
                             required
                             :state="categoryState()" 
                             aria-describedby="input-category-feedback"                          
                             >
                             <b-form-select-option
                                  v-for="(option, index) in categories"
                                  :key="'category-' + index"
                                  :value="option"            
                                >{{ option.Name }}</b-form-select-option>                           
                            </b-form-select>
                            <b-form-invalid-feedback :state="categoryState()" id="input-category-feedback">
                              {{ $t("category_validate") }}
                            </b-form-invalid-feedback>
                          </b-form-group>

                        </b-row>



                        <b-row class="mb-1 row-data">

                           <b-form-group id="input-group-album" 
                           :label="$t('album')"
                           label-for="input-album" 
                           class="input-width"
                           required>
                            <b-form-select
                             id="input-album"
                             v-model="songUpdated.Album"
                             :state="albumState()" 
                             aria-describedby="input-album-feedback"                                                  
                             >
                             <b-form-select-option
                                  v-for="(option, index) in albums"
                                  :key="'album-' + index"
                                  :value="option"            
                                >{{ option.Name }}</b-form-select-option>                           
                            </b-form-select>
                            <b-form-invalid-feedback :state="albumState()" id="input-album-feedback">
                              {{ $t("album_validate") }}
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
              :title="$t('add_song')"
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

                      <b-col >
                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-add-name"
                            :label="$t('name')"
                            label-for="input-text-add-name"
                            class="input-width"
                          >
                            <b-form-input
                              
                              id="input-text-add-name"
                              v-model="songCreated.Name"
                              type="text"
                              required
                              :placeholder="$t('entername')"
                              :state="nameState()"
                              aria-describedby="input-name-add-feedback"
                              trim
                            ></b-form-input>
                            <b-form-invalid-feedback :state="nameState()" id="input-name-add-feedback">
                              {{ $t("name_song_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>


                        <b-row class="mb-1 row-data">

                           <b-form-group id="input-group-category" 
                           :label="$t('category')"
                           label-for="input-category" 
                           class="input-width"
                           required>
                            <b-form-select
                             id="input-category"
                             v-model="songCreated.Category"
                             required
                             :state="categoryState()" 
                             aria-describedby="input-category-feedback"                          
                             >
                             <b-form-select-option
                                  v-for="(option, index) in categories"
                                  :key="'category-' + index"
                                  :value="option"            
                                >{{ option.Name }}</b-form-select-option>                           
                            </b-form-select>
                            <b-form-invalid-feedback :state="categoryState()" id="input-category-feedback">
                              {{ $t("category_validate") }}
                            </b-form-invalid-feedback>
                          </b-form-group>

                        </b-row>



                        <b-row class="mb-1 row-data">

                           <b-form-group id="input-group-album" 
                           :label="$t('album')"
                           label-for="input-album" 
                           class="input-width"
                           required>
                            <b-form-select
                             id="input-album"
                             v-model="songCreated.Album"  
                             :state="albumState()" 
                             aria-describedby="input-album-feedback"                                               
                             >
                             <b-form-select-option
                                  v-for="(option, index) in albums"
                                  :key="'album-' + index"
                                  :value="option"            
                                >{{ option.Name }}</b-form-select-option>                           
                            </b-form-select>
                            <b-form-invalid-feedback :state="albumState()" id="input-album-feedback">
                              {{ $t("album_validate") }}
                            </b-form-invalid-feedback>
                          </b-form-group>

                        </b-row>



                        <b-row class="mb-1 row-data">
                           <b-form-group :label="$t('song')" class="input-width">

                                  <b-form-file
                                  v-model="file"
                                  :placeholder="$t('select_a_file')"
                                  :drop-placeholder="$t('drag_a_file')"                                 
                                  accept=".wav"
                                  required
                                  :state="fileState()" 
                                  aria-describedby="input-file-feedback"    
                                  ></b-form-file>
                                  <b-form-invalid-feedback :state="fileState()" id="input-file-feedback">
                                    {{ $t("file_required_validate") }}
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

@Component({})
export default class MySongs extends Vue {

    private isBusy : boolean = false;
    private loadingAdd: boolean = false;
    private loadingUpdate: boolean = false;
    private loading: boolean = false;

    private songservice: SongService = songService;
    private categoryservice: CategoryService = categoryService;
    private albumservice: AlbumService = albumService;

    private songs : SongViewModel[] =[];
    private totalRows : number = 1;
    private currentPage : number = 1; 
    private perPage : number = 5;
    private pageOptions : number[] = [5, 10, 15];
    private filter : string | null = null
    private filterOn: string[] = ['Name'];
  
    private songSelected: SongViewModel = new SongViewModel();
    private songCreated: SongViewModel = new SongViewModel();
    private songUpdated: SongViewModel = new SongViewModel();
    private albums: AlbumViewModel[] = [];
    private categories: CategoryViewModel[] = []; 

    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;

    private file:any|null=null


    private fields: any[] = [
          { key: 'Name', label:  'Name' , sortable: true},
          { key: 'UploadDate', label: 'UploadDate', sortable: true},
          { key: 'Playbacks', label: 'Playbacks', sortable: true},         
          { key: 'actions', label: 'Actions' }
        ]
  
    created(){
      
    }  
    mounted() {
      
      this.getCategories();
      this.getAlbums();
      this.getSongs();
    }

    hasPermission(permission : string){
      return Permissionshelper.HasPermission(permission);      
    }

    checkFormValidity() {

       
        if(!this.nameState())
          return false;

        if(!this.categoryState())
          return false;

        if(!this.albumState())
          return false;


        return true;
      }

    resetAddModal() {
        this.songCreated = new SongViewModel();
        this.file = null;
    }
    resetUpdateModal() {
        this.songUpdated = new SongViewModel();

    }

    resetInfoModal() {
        this.songSelected = new SongViewModel();
    }



    getCategories(){
      this.loading = true;       
      this.categoryservice.get()
        .then(response => {
          this.categories = response.data; 
        })
        .catch(error =>{                          
        })
    }

    getAlbums(){
      this.loading = true;       
      this.albumservice.getUserAlbums()
        .then(response => {
          this.albums = response.data.Albums; 
        })
        .catch(error =>{                          
        })
    }

    getSongs(){

      this.isBusy = true;
      this.songservice.getUserSongs()
      .then(response => {
        this.songs = response.data.Songs;

        this.isBusy = false;
        this.totalRows = this.songs.length
      })
      .catch(error =>{
 
        this.isBusy = false;
      })}

  

    handleAddSubmit(bvModalEvt : any) {
      bvModalEvt.preventDefault();

      if(!this.checkFormValidity() || !this.fileState()){
          return
      }

      let formData = new FormData();
      formData.append("File", this.file);
      formData.append("song", JSON.stringify(this.songCreated));
      this.loadingAdd = true;
      this.showAlert = false;
      this.songservice.add(formData)
      .then(response => {
        this.getSongs();
        this.loadingAdd = false;
        this.variant = "success";       
        this.$bvModal.hide('crud-modal');
        this.message = response.data;
        this.file = null;
        this.showAlert = true;
      })
      .catch(error => {
        this.file = null;
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



    handleUpdateSubmit(bvModalEvt : any) {
      bvModalEvt.preventDefault();

      if(!this.checkFormValidity()){
          return
      }
      


      this.loadingUpdate = true;
      this.showAlert = false;
      
      this.songservice.update(this.songUpdated).then(response => {
        this.getSongs();
        this.loadingUpdate = false;
        this.variant = "success";       
        this.$bvModal.hide('update-modal');
        this.message = response.data;

        this.showAlert = true;
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



      nameState(){
        return (this.songCreated.Name.length < 101 && this.songCreated.Name.length > 0) ||
              (this.songUpdated.Name.length < 101 && this.songUpdated.Name.length > 0) ? true : false
      }


      categoryState(){
        var category = this.songCreated.Category as CategoryViewModel;
        var categoryUpdate = this.songUpdated.Category as CategoryViewModel;
        return (category.Name != '') || (categoryUpdate.Name!='') ? true : false
      }

      albumState(){
        var album = this.songCreated.Album as AlbumViewModel;
        var albumUpdate = this.songUpdated.Album as AlbumViewModel;
        return (album.Name != '') || (albumUpdate.Name!='') ? true : false
      }

      
      fileState(){
        
        return this.file != null ? true : false
      }


      calculateSuccess(item : any) {
        // this.infoModal.title = item.UserName;
        this.$router.push({
          name:'CalculateSuccess',
          params:{songProp:item}
        })

      }


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
                  this.getSongs();
                  
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


      updateSelect(item : any, index : number, button : any) {
        
       
        this.songservice.getById(item)
        .then(response => {
          this.songUpdated = response.data;
          
          
          this.$root.$emit('bv::show::modal', 'update-modal', button)
        })
        .catch(error =>{
                                    
        })

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