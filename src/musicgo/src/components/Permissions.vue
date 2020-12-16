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
                    <h2>{{ $t("permissions") }}</h2>
                  </div>
                                     
              </b-col>
              <b-col sm="8">
                <b-button class="add-user-button" style="width:200px" v-b-modal.crud-modal v-show="hasPermission('CreatePermission')">
                  <b-icon icon="plus" ></b-icon><p class="add-button-text" >{{ $t("add_permission") }}</p>
                </b-button>
                <b-button class="add-user-button" style="margin-right:10px; width:200px" v-b-modal.assigner-modal v-show="hasPermission('CreatePermission')">
                  <b-icon icon="plus" ></b-icon><p class="add-button-text" >{{ $t("assign_permission") }}</p>
                </b-button>
              </b-col>

              <b-col sm="4">
                
              </b-col>

          </b-row>
        </div>
         <div class="table-filter">
          <b-row> 
              <b-col cols="6" class="my-1">

              </b-col>

          </b-row>    
        </div> 
        <b-row>
          <b-col md="12" style="position:unset !important">
            <div> 

             
             <TreeItem 
             class="item"
             v-for="(child, index) in permissions"
            :key="index"
            :item="child"
             />


             <b-modal 
              centered 
              id="crud-modal" 
              :title="$t('add_permission')"
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
                              v-model="permissionCreated.Child.Name"
                              type="text"
                              required
                              :placeholder="$t('entername')"
                              :state="nameState()"
                              aria-describedby="input-name-add-feedback"
                              trim
                            ></b-form-input>
                            <b-form-invalid-feedback :state="nameState()" id="input-name-add-feedback">
                              {{ $t("name_permission_validate_char") }}
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>


                        <b-row class="mb-1 row-data">

                           <b-form-group id="input-group-parent" 
                           :label="$t('parent')"
                           label-for="input-parent" 
                           class="input-width"
                           required>
                            <b-form-select
                             id="input-parent"
                             v-model="permissionCreated.Parent"
                                                                            
                             >
                             <b-form-select-option
                                  v-for="(option, index) in permissionsGroups"
                                  :key="'category-' + index"
                                  :value="option"            
                                >{{ option.Name }}</b-form-select-option>                           
                            </b-form-select>

                          </b-form-group>

                        </b-row>


                        
                      </b-col>


                    </b-row>
                  </b-form>
                </b-container>
                
              </b-modal> 

                 <b-modal 
              centered 
              id="assigner-modal" 
              :title="$t('assign_permission')"
              @show="resetAssignModal"
              @hidden="resetAssignModal"
              @ok="handleAssignSubmit"
              
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
 
                  <b-form  ref="modalassignform" @submit.stop.prevent="handleAssignSubmit" v-show="!loadingUpdate">
                    <b-row class="mb-1">

                      <b-col >
                         <b-row class="mb-1 row-data">

                           <b-form-group id="input-group-child" 
                           :label="$t('child')"
                           label-for="input-child" 
                           class="input-width"
                           required>
                            <b-form-select
                             id="input-child"
                             v-model="permissionAssign.Child"
                                              
                             >
                             <b-form-select-option
                                  v-for="(option, index) in permissionsChilds"
                                  :key="'permissionchild-' + index"
                                  :value="option"            
                                >{{ option.Name }}</b-form-select-option>                           
                            </b-form-select>

                          </b-form-group>

                        </b-row>


                        <b-row class="mb-1 row-data">

                           <b-form-group id="input-group-parent" 
                           :label="$t('parent')"
                           label-for="input-parent" 
                           class="input-width"
                           required>
                            <b-form-select
                             id="input-parent"
                             v-model="permissionAssign.Parent"
                                                                            
                             >
                             <b-form-select-option
                                  v-for="(option, index) in permissionsGroups"
                                  :key="'permission-' + index"
                                  :value="option"            
                                >{{ option.Name }}</b-form-select-option>                           
                            </b-form-select>

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
       
    </div>
  </b-container>
</div>

</template>

<script lang="ts">
import { Component, Vue, Watch, Prop } from "vue-property-decorator";
import { UserViewModel } from '../shared/classes/UserViewModel';
import permissionsService, { PermissionsService } from "../shared/services/PermissionsService";


import { SongViewModel } from '@/shared/classes/SongViewModel';
import { AlbumViewModel } from '@/shared/classes/AlbumViewModel';
import { CategoryViewModel } from '@/shared/classes/CategoryViewModel';

import { Permissionshelper } from '../shared/classes/Permissions-helper';
import { PermissionViewModel } from '@/shared/classes/PermissionViewModel';
import { PermissionsParentViewModel } from '@/shared/classes/PermissionsParentViewModel';


import  TreeItem  from '../shared/components/TreeItem.vue'

@Component({
  components:{
        TreeItem
    }
})
export default class Permissions extends Vue {

    private isBusy : boolean = false;
    private loadingAdd: boolean = false;
    private loadingUpdate: boolean = false;
    private loading: boolean = false;

    private permissionservice: PermissionsService = permissionsService;


    private permissions : PermissionViewModel[] =[];

    private permissionsGroups : PermissionViewModel[] =[];
    private permissionsChilds : PermissionViewModel[] =[];



  
    private permissionSelected: PermissionViewModel = new PermissionViewModel();
    private permissionCreated: PermissionsParentViewModel = new PermissionsParentViewModel();
    private permissionAssign: PermissionsParentViewModel = new PermissionsParentViewModel();


    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;


  


  
    created(){
      
    }  
    mounted() {
   
        this.getPermissionsRoots();
        this.getPermissionsGroups();
        this.getPermissionsChilds();
      
    }

    computed(){
        
    }

    hasPermission(permission : string){
      return Permissionshelper.HasPermission(permission);      
    }


    checkFormValidity() {

        if(!this.nameState())
          return false;


        return true;
      }

    resetAddModal() {
        this.permissionCreated= new PermissionsParentViewModel();
    }


    resetAssignModal() {
        this.permissionAssign = new PermissionsParentViewModel();
    }

    nameState(){
        return (this.permissionCreated.Child!.Name.length < 31 && this.permissionCreated.Child!.Name.length > 0)  ? true : false
      }





    getPermissionsRoots(){

      this.isBusy = true;
      this.permissionservice.get()
      .then(response => {
        this.permissions = response.data;

      })
      .catch(error =>{
 
        this.isBusy = false;
      })}


    getPermissionsGroups(){

     
      this.permissionservice.getGroups()
      .then(response => {
        this.permissionsGroups = response.data;

      })
      .catch(error =>{
 
        
      })}  


    getPermissionsChilds(){

     
      this.permissionservice.getChilds()
      .then(response => {
        this.permissionsChilds= response.data;

      })
      .catch(error =>{
 
        
      })}  



    
    handleAddSubmit(bvModalEvt : any) {
      bvModalEvt.preventDefault();

      if(!this.checkFormValidity()){
          return
      }

      
      this.loadingAdd = true;
      this.showAlert = false;
      this.permissionservice.addPermissionsParent(this.permissionCreated)
      .then(response => {
        this.getPermissionsRoots();
        this.getPermissionsGroups();
        this.getPermissionsChilds();
        this.loadingAdd = false;
        this.variant = "success";       
        this.$bvModal.hide('crud-modal');
        this.message = response.data;
        this.showAlert = true;
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



    handleAssignSubmit(bvModalEvt : any) {
      bvModalEvt.preventDefault();

      if(this.permissionAssign.Child!.Name == "" && this.permissionAssign.Parent!.Name == ""){
          return
      }
      


      this.loadingUpdate = true;
      this.showAlert = false;
      
      this.permissionservice.addPermissionGroup(this.permissionAssign).then(response => {
        this.getPermissionsRoots()
        this.loadingUpdate = false;
        this.variant = "success";       
        this.$bvModal.hide('assigner-modal');
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
        
        this.$bvModal.hide('assigner-modal');
      });
    }


    

    


    
}
</script>

<style >

.item {
  cursor: pointer;
  color:white;
}

</style>