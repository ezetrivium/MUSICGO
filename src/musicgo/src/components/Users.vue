<template>
<div style="margin-top:50px">
  <b-container fluid >
    <div class="table-wrapper">
        <div class="table-title">
          <b-row>
              <b-col sm="4">
                <h2>Usuarios</h2>
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
                      <b-button :disabled="!filter" @click="filter = ''">Clear</b-button>
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
              responsive
              :items="items"
              :fields="fields"
              :current-page="currentPage"
              :per-page="perPage"
              :filter="filter"
              :filterIncludedFields="filterOn"
              @filtered="onFiltered"
              :busy="isBusy"
              class="table-items"
              >

              <template v-slot:table-busy>
                <div class="text-center text-danger my-2">
                  <b-spinner class="align-middle"></b-spinner>
                </div>
              </template>
              <template v-slot:cell(actions)="row">
                <b-button size="sm" @click="info(row.item, row.index, $event.target)" class="mr-1">
                  Info modal
                </b-button>
              </template>

            
              </b-table> 

            
              <b-modal :id="infoModal.id" :title="infoModal.title" ok-only @hide="resetInfoModal">
                <pre>{{ infoModal.content }}</pre>
              </b-modal> 
            </div>
            
          </b-col>
        </b-row>
        <b-row>
          <b-col md="6" class="results-length" >
             <p>{{totalRows}} Resultados</p>
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
import userService, { UserService } from "../shared/services/UserService";
@Component({})
export default class Users extends Vue {

    private isBusy : boolean = false;
    private loading: boolean = false;
    private userservice: UserService = userService;
    private items : UserViewModel[] =[];
    private infoModal  : any = {id: 'info-modal',title: '',content: ''};
    private totalRows : number = 1;
    private currentPage : number = 1; 
    private perPage : number = 5;
    private pageOptions : number[] = [5, 10, 15];
    private filter : string | null = null
    private filterOn: string[] = ['Name']
    private fields: any[] = [
          { key: 'UserName', label: 'UserName', sortable: true},
          { key: 'Name', label: 'Name', sortable: true},
          { key: 'LastName', label: 'LastName', sortable: true},
          { key: 'Email', label: 'Email', sortable: true, class: 'text-center' },
          {
            key: 'Blocked',
            label: 'Blocked',
            formatter: (value : boolean,key : string, item : UserViewModel) => {
              return value ? 'Yes' : 'No'
            }
          },
          { key: 'actions', label: 'Actions' }
        ]
  
    created(){
      if(this.$store.getters.usersList.length>0){
        
        this.items = this.$store.getters.usersList;
        this.totalRows = this.items.length
      }
      else{
        this.getUsersList()
      }
    }  
    mounted() {
      // Set the initial number of items
      
    }

      info(item : any, index : number, button : any) {
        this.infoModal.title = `Row index: ${index}`
        this.infoModal.content = JSON.stringify(item, null, 2)
        this.$root.$emit('bv::show::modal', this.infoModal.id, button)
      }
      resetInfoModal() {
        this.infoModal.title = ''
        this.infoModal.content = ''
      }
      onFiltered(filteredItems: any) {
        // Trigger pagination to update the number of buttons/pages due to filtering
        this.totalRows = filteredItems.length
        this.currentPage = 1
      }

    getUsersList(){
      this.loading =true;
      this.isBusy = true;
      this.$store.dispatch('retrieveUsersList')
      .then(response => {
        this.items = response.data;
        this.loading = false;
        this.isBusy = false;
        this.totalRows = this.items.length
      })
      .catch(error =>{
        this.loading = false;
        this.isBusy = false;
      })}

    
}
</script>

<style>

.results-length{
  color:  white;
}

.page-item.active .page-link{
  background-color: white !important;
  color: #212529 !important;
  border-color: white !important;
}
.page-link{
  color: white !important;
  background-color: inherit !important
}

.per-page{
      width: 60px;
}
.table-title {
    color: #fff;
    background-color: #343a40;
    padding: 16px 25px;
    margin: -20px -25px 10px;
    border-radius: 3px 3px 0 0;
}
.table{
  color: white !important;
}

.table-wrapper{
    padding:20px 25px;
    margin: 30px auto;
    border-radius: 3px;
    box-shadow: 0 1px 1px rgba(0,0,0,.05);
}

.table-filter {
    padding: 5px 0 15px;
    margin-bottom: 5px;
}
</style>