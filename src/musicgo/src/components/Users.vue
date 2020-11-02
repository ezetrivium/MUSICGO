<template>
<div style="margin-top:50px">
  <b-container fluid >
    <div>
        <b-alert :variant="variant" dismissible v-model="showAlert">{{message}}</b-alert>
    </div> 
    <div class="table-wrapper">
        <div class="table-title">
          <b-row>
              <b-col sm="4" >
                <h2>Usuarios</h2>
              </b-col>
              <b-col sm="8">
                <b-button class="add-user-button" v-b-modal.crud-modal v-show="hasPermission('AddUser')">
                  <b-icon icon="plus" ></b-icon><p class="add-button-text" >Agregar Usuario</p>
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
              emptyText="No hay nada para mostrar"
              responsive="sm"

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
              <template v-slot:cell(actions)="row" >
                <b-row>
                  <b-col >
                    <b-icon scale="1" icon="eye" v-show="hasPermission('GetUsers')"
                    @click="info(row.item, row.index, $event.target)" style="cursor:pointer"></b-icon>
                  </b-col>
                  <b-col>    
                    <b-icon scale="1" icon="pen" v-show="hasPermission('UpdateUser')"
                     @click="updateSelect(row.item, row.index, $event.target)" style="cursor:pointer"></b-icon>
                  </b-col>
                  <b-col>    
                    <b-icon scale="1" icon="trash" v-show="hasPermission('DeleteUser')"
                     @click="deleteUser(row.item, row.index, $event.target)" style="cursor:pointer"></b-icon>
                  </b-col>
                </b-row>
 
              </template>

            
              </b-table> 




              <b-modal 
              centered 
              id="update-modal" 
              title="Modificar Usuario" 
              @ok="handleUpdateSubmit"
              @hidden="resetUpdateModal"
              size="lg"
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

                      <b-col lg="6">
                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-update-username"
                            label="Username:"
                            label-for="input-text-update-username"
                            class="input-width"
                          >
                            <b-form-input
                              
                              id="input-text-update-username"
                              v-model="userUpdated.UserName"
                              type="text"
                              required
                              placeholder="Enter Username"
                              :state="usernameState()"
                              aria-describedby="input-username-update-feedback"
                              
                            ></b-form-input>
                            <b-form-invalid-feedback :state="usernameState()" id="input-username-update-feedback">
                              Su nombre de usuario debe tener entre 5 y 12 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-update-name"
                            label="Name:"
                            label-for="input-text-update-name"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-update-name"
                              v-model="userUpdated.Name"
                              type="text"                              
                              required
                              
                              placeholder="Enter Name"
                              :state="nameState()"
                              aria-describedby="input-name-update-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="nameState()" id="input-name-update-feedback">
                              Su nombre debe tener máximo 30 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-update-lastname"
                            label="Last Name:"
                            label-for="input-update-text-lastname"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-update-text-lastname"
                              
                              v-model="userUpdated.LastName"
                              type="text"
                              required
                              placeholder="Enter Last Name"
                              :state="lastnameState()"
                              aria-describedby="input-lastname-update-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="lastnameState()" id="input-lastname-update-feedback">
                              Su apellido debe tener máximo 30 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                         <!-- <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-update-password"
                            label="Password:"
                            label-for="input-text-update-password"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-update-password"
                              
                              v-model="userUpdated.Password"
                              type="password"
                              required
                              placeholder="Enter Password"
                              :state="passwordState()"
                              aria-describedby="input-password-update-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="passwordState()" id="input-password-update-feedback">
                              Su Contraseña debe tener entre 8 y 12 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row> -->

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-update-artistname"
                            label="Artist Name:"
                            label-for="input-update-text-artistname"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-update-text-artistname"
                              v-model="userUpdated.ArtistName"
                              type="text"
                              
                              required
                              placeholder="Enter Artist Name"
                              :state="artistnameState()"
                              aria-describedby="input-update-artistname-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="artistnameState()" id="input-update-artistname-feedback">
                              Su nombre de artista debe tener máximo 50 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>


                        
                      </b-col>


                      <b-col lg="6">
                        

                        <b-row class="mb-1 row-data">

                          
                          <b-form-group
                            id="input-update-email"
                            label="Email:"
                            label-for="input-text-update-email"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-update-email"
                              v-model="userUpdated.Email"
                              type="email"
                              :state="emailState()"
                              required
                              placeholder="Enter Email"
                              aria-describedby="input-update-email-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="emailState()" id="input-update-email-feedback">
                              El email es requerido
                            </b-form-invalid-feedback>

                          </b-form-group>
                        </b-row>

                        

                        <b-row class="mb-1 row-data">

                          
                            <b-form-group id="input-group-permission-update"                                                       
                            class="input-width"                           
                            required>
                            <template v-slot:label>
                              <b-row>
                                <b-col lg="4" md="4" sm="4">
                                    <div >
                                      Permisos
                                    </div>
                                </b-col>
                                <b-col lg="8" md="8" sm="8">
                                    <div >
                                      <b-form-checkbox v-model="serviceUpdate" :disabled="serviceUpdateDisabled" name="check-admin" switch>
                                          Servicio
                                      </b-form-checkbox>
                                    </div>
                                </b-col> 
                                
                              </b-row>
                            </template>
                           
                               
                                    <b-form-select
                                    id="input-permission-update"
                                    v-model="permissionsUpdate"
                                    v-show="!serviceUpdate"                            
                                    >                            
                                        <b-form-select-option
                                          v-for="(option, index) in permissions"
                                          :key="'permission-update-' + index"
                                          :value="option"            
                                        >{{ option.Name }}</b-form-select-option>
                                    </b-form-select> 
                                    <b-form-select
                                      id="input-service-update"
                                      v-model="userUpdated.Contract.Service" 
                                      v-show="serviceUpdate"                           
                                      >                            
                                          <b-form-select-option
                                            v-for="(option, index) in services"
                                            :key="'service-update-' + index"
                                            :value="option"            
                                          >{{ option.Name }}</b-form-select-option>
                                      </b-form-select>
                               
                            
                            </b-form-group>

                                  
                        </b-row>

                        <b-row class="mb-1 row-data">
                           <b-form-group label="Image" style="width:80%; margin-left:20px; margin-bottom:0">
                             <b-row >
                               <b-col lg="9" md="9" sm="6" >
                                  <b-form-file
                                  v-model="fileUpdate"
                                  placeholder="Seleccione una imagen..."
                                  drop-placeholder="Arratre una imagen..."
                                  
                                  accept=".jpg"
                                  @change="showPreviewImageUpdate($event)"
                                  ></b-form-file>
                               </b-col>
                               <b-col lg="3" md="3" sm="6" >
                                  <b-img 
                                    v-bind="avatarSize" 
                                    :src="this.userUpdated.ImageBase64" 
                                    rounded="circle" alt="user avatar" 
                                    v-show="this.userUpdated.ImageBase64"
                                    >                  
                                    </b-img>
                               </b-col>
                             
                             </b-row>
                            
                          </b-form-group>
                         
                        </b-row>

                        <b-row class="mb-1 row-data">

                           <b-form-group id="input-group-language-update" 
                           label="Lenguaje:" 
                           label-for="input-language-update" 
                           class="input-width"
                           required>
                            <b-form-select
                             id="input-language-update"
                             v-model="userUpdated.Language"
                             required
                             :state="languageState()" 
                             aria-describedby="input-language-update-feedback"                          
                             >
                             <b-form-select-option
                                  v-for="(option, index) in languages"
                                  :key="'language-update-' + index"
                                  :value="option"            
                                >{{ option.Name }}</b-form-select-option>                           
                            </b-form-select>
                            <b-form-invalid-feedback :state="languageState()" id="input-language-update-feedback">
                              El lenguaje es requerido
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
              title="Crear Usuario" 
              @show="resetAddModal"
              @hidden="resetAddModal"
              @ok="handleAddSubmit"
              
              size="lg"
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

                      <b-col lg="6">
                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-username"
                            label="Username:"
                            label-for="input-text-username"
                            class="input-width"
                          >
                            <b-form-input
                              
                              id="input-text-username"
                              v-model="userCreated.UserName"
                              type="text"
                              required
                              placeholder="Enter Username"
                              :state="usernameState()"
                              aria-describedby="input-username-feedback"
                              
                            ></b-form-input>
                            <b-form-invalid-feedback :state="usernameState()" id="input-username-feedback">
                              Su nombre de usuario debe tener entre 5 y 12 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-name"
                            label="Name:"
                            label-for="input-text-name"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-name"
                              v-model="userCreated.Name"
                              type="text"                              
                              required
                              
                              placeholder="Enter Name"
                              :state="nameState()"
                              aria-describedby="input-name-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="nameState()" id="input-name-feedback">
                              Su nombre debe tener máximo 30 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-lastname"
                            label="Last Name:"
                            label-for="input-text-lastname"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-lastname"
                              
                              v-model="userCreated.LastName"
                              type="text"
                              required
                              placeholder="Enter Last Name"
                              :state="lastnameState()"
                              aria-describedby="input-lastname-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="lastnameState()" id="input-lastname-feedback">
                              Su apellido debe tener máximo 30 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                         <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-password"
                            label="Password:"
                            label-for="input-text-password"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-password"
                              
                              v-model="userCreated.Password"
                              type="password"
                              required
                              placeholder="Enter Password"
                              :state="passwordState()"
                              aria-describedby="input-password-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="passwordState()" id="input-password-feedback">
                              Su Contraseña debe tener entre 8 y 32 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>

                        <b-row class="mb-1 row-data">
                          <b-form-group
                            id="input-artistname"
                            label="Artist Name:"
                            label-for="input-text-artistname"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-artistname"
                              v-model="userCreated.ArtistName"
                              type="text"
                              
                              required
                              placeholder="Enter Artist Name"
                              :state="artistnameState()"
                              aria-describedby="input-artistname-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="artistnameState()" id="input-artistname-feedback">
                              Su nombre de artista debe tener máximo 50 caracteres
                            </b-form-invalid-feedback>
                          </b-form-group>
                        </b-row>


                        
                      </b-col>


                      <b-col lg="6">
                        

                        <b-row class="mb-1 row-data">

                          
                          <b-form-group
                            id="input-email"
                            label="Email:"
                            label-for="input-text-email"
                            class="input-width"
                          >
                            <b-form-input
                              id="input-text-email"
                              v-model="userCreated.Email"
                              type="email"
                              :state="emailState()"
                              required
                              placeholder="Enter Email"
                              aria-describedby="input-email-feedback"
                            ></b-form-input>
                            <b-form-invalid-feedback :state="emailState()" id="input-email-feedback">
                              El email es requerido
                            </b-form-invalid-feedback>

                          </b-form-group>
                        </b-row>

                        

                        <b-row class="mb-1 row-data">

                          
                            <b-form-group id="input-group-permission"                                                       
                            class="input-width"                           
                            required>
                            <template v-slot:label>
                              <b-row>
                                <b-col lg="4" md="4" sm="4">
                                    <div >
                                      Permisos
                                    </div>
                                </b-col>
                                <b-col lg="8" md="8" sm="8">
                                    <div >
                                      <b-form-checkbox v-model="adminChecked" name="check-admin" switch>
                                          Servicio
                                      </b-form-checkbox>
                                    </div>
                                </b-col>
                                
                              </b-row>
                            </template>
                           
                               
                                    <b-form-select
                                    id="input-permission"
                                    v-model="userCreated.Permissions[0]"
                                    v-show="!adminChecked"                            
                                    >                            
                                        <b-form-select-option
                                          v-for="(option, index) in permissions"
                                          :key="'permission-' + index"
                                          :value="option"            
                                        >{{ option.Name }}</b-form-select-option>
                                    </b-form-select> 
                                    <b-form-select
                                      id="input-service"
                                      v-model="userCreated.Contract.Service"  
                                      v-show="adminChecked"                                                                  
                                      >                            
                                          <b-form-select-option
                                            v-for="(option, index) in services"
                                            :key="'service-' + index"
                                            :value="option"            
                                          >{{ option.Name }}</b-form-select-option>
                                      </b-form-select>
                               
                            
                            </b-form-group>

                             <!-- <b-form-group id="input-group-service" 
                              label="Servicio:" 
                              label-for="input-service" 
                              class="input-width"
                              v-show="!adminChecked"
                              required>
                                
                              </b-form-group> -->
                                  
                        </b-row>

                        <b-row class="mb-1 row-data">
                           <b-form-group label="Image" style="width:80%; margin-left:20px; margin-bottom:0">
                             <b-row >
                               <b-col lg="9" md="9" sm="9">
                                  <b-form-file
                                  v-model="file"
                                  placeholder="Seleccione una imagen..."
                                  drop-placeholder="Arratre una imagen..."
                                  
                                  accept=".jpg"
                                  @change="showPreviewImage($event)"
                                  ></b-form-file>
                               </b-col>
                               <b-col lg="3" md="3" sm="3">
                                  <b-img 
                                    v-bind="avatarSize" 
                                    :src="this.userCreated.ImageBase64" 
                                    rounded="circle" alt="user avatar" 
                                    v-show="this.userCreated.ImageBase64"
                                    >                  
                                    </b-img>
                               </b-col>
                             
                             </b-row>
                            
                          </b-form-group>
                         
                        </b-row>

                        <b-row class="mb-1 row-data">

                           <b-form-group id="input-group-language" 
                           label="Lenguaje:" 
                           label-for="input-language" 
                           class="input-width"
                           required>
                            <b-form-select
                             id="input-language"
                             v-model="userCreated.Language"
                             required
                             :state="languageState()" 
                             aria-describedby="input-language-feedback"                          
                             >
                             <b-form-select-option
                                  v-for="(option, index) in languages"
                                  :key="'language-' + index"
                                  :value="option"            
                                >{{ option.Name }}</b-form-select-option>                           
                            </b-form-select>
                            <b-form-invalid-feedback :state="languageState()" id="input-language-feedback">
                              El lenguaje es requerido
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
              
              ok-only 
              scrollable
              size="xl"
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
                    <b-img 
                    v-bind="avatarSize" 
                    :src="this.userSelected.ImageBase64" 
                    rounded="circle" alt="user avatar">                  
                    </b-img>
                    <p class="title-modal-text">{{this.userSelected.UserName}}</p>
                  </b-col>                   
                  </b-row>
              </div>
                <!-- <template v-slot:modal-header>
                  <b-row>
                    <b-img 
                    v-bind="avatarSize" 
                    :src="'data:image/jpg;base64,' " 
                    rounded="circle" alt="user avatar">                  
                    </b-img>
                    {{this.userSelected.UserName}} 
                  </b-row>
                  
                </template> -->

                <b-container fluid>
                  <b-row class="mb-1">

                    <b-col lg="6">
                      
                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>Name</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.userSelected.Name}}
                        </b-col>
                      </b-row>

                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>LastName</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.userSelected.LastName}}
                        </b-col>
                      </b-row>
                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>Blocked</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.userSelected.Blocked ? 'Yes' : 'No' }}
                        </b-col>
                      </b-row>
                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>Fecha de Contratación</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.userSelected.Contract !=null ? this.userSelected.Contract.HireDate.toString().split('T')[0] : 'Undefined' }}
                        </b-col>
                      </b-row>
                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>Reproducciones</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.userSelected.Playbacks }}
                        </b-col>
                      </b-row>
                      
                    </b-col>


                    <b-col lg="6">
                      
                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>Email</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.userSelected.Email}}
                        </b-col>
                      </b-row>
                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>Servicio</strong>
                        </b-col>
                        <b-col cols="6">
                           {{this.userSelected.Contract != null ? this.userSelected.Contract.Service.Name : 'Undefined'}}
                        </b-col>
                      </b-row>
                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>Fecha de Expiración</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.userSelected.Contract != null ? this.userSelected.Contract.ExpirationDate.toString().split('T')[0] : 'Undefined'}}
                        </b-col>
                      </b-row>
                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>Lenguaje</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.userSelected.Language != null ? this.userSelected.Language.Name : 'Undefined'}}
                        </b-col>
                      </b-row>
                      <b-row class="mb-1 row-data">
                        <b-col cols="6">
                          <strong>Artist Name</strong>
                        </b-col>
                        <b-col cols="6">
                          {{this.userSelected.ArtistName != null  ? this.userSelected.ArtistName : 'Undefined' }}
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
import languageService, { LanguageService } from "../shared/services/LanguageService";
import servicesService, { ServicesService } from "../shared/services/ServicesService";
import permissionsService, { PermissionsService } from "../shared/services/PermissionsService";
import { LanguageViewModel } from '@/shared/classes/LanguageViewModel';
import { ServiceViewModel } from '@/shared/classes/ServiceViewModel';
import { PermissionViewModel } from '@/shared/classes/PermissionViewModel';
import { Permissionshelper } from '../shared/classes/Permissions-helper';
import { ContractViewModel } from '@/shared/classes/ContractViewModel';
@Component({})
export default class Users extends Vue {

    private isBusy : boolean = false;
    private loadingAdd: boolean = false;
    private loadingUpdate: boolean = false;
    private loading: boolean = false;
    private adminChecked: boolean = false;
    private serviceUpdate: boolean = false;
    private serviceUpdateDisabled: boolean = false;
    private userservice: UserService = userService;
    private languageservice: LanguageService = languageService;
    private servicesservice: ServicesService = servicesService;
    private permissionsservice: PermissionsService = permissionsService;
    private items : UserViewModel[] =[];
    private totalRows : number = 1;
    private currentPage : number = 1; 
    private perPage : number = 5;
    private pageOptions : number[] = [5, 10, 15];
    private filter : string | null = null
    private filterOn: string[] = ['Name'];
    private imageSelected: string = ''
    private userSelected: UserViewModel = new UserViewModel();
    private userCreated: UserViewModel = new UserViewModel();
    private userUpdated: UserViewModel = new UserViewModel();
    private languages: LanguageViewModel[] = [];
    private services: ServiceViewModel[] = []; 
    private permissions: PermissionViewModel[] = []; 
    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;
    private imagePreview: string=''
    private file:any|null=null
    private fileUpdate:any|null=null
    private permissionsUpdate : PermissionViewModel = new PermissionViewModel();

    private avatarSize: any[] = [
      {
        width: 40, height: 40
      }
    ]
    private fields: any[] = [
          { key: 'UserName', label: 'UserName', sortable: true},
          { key: 'Name', label: 'Name', sortable: true},
          { key: 'LastName', label: 'LastName', sortable: true},
          { key: 'Email', label: 'Email', sortable: true, class: 'text-center' },
          {
            key: 'Blocked',
            label: 'Blocked',
            formatter: (value : boolean,key : string, item : UserViewModel) => {
              return item.Blocked ? 'Yes' : 'No'
            }
          },
          { key: 'actions', label: 'Actions' }
        ]
  
    created(){
      
    }  
    mounted() {
      this.getLanguages();
      this.getServices();
      this.getPermissions();

      var users = this.$store.getters.usersList as UserViewModel[];
      if(users != null && users.length>0){
        
        this.items = this.$store.getters.usersList;
        this.totalRows = this.items.length
      }
      else{
        this.getUsersList()
      }
      
    }

    hasPermission(permission : string){
      return Permissionshelper.HasPermission(permission);      
    }

    checkFormValidity() {

        if(!this.usernameState())
          return false;

        if(!this.nameState())
          return false;

        if(!this.lastnameState())
          return false;

        if(!this.artistnameState())
          return false;

        if(!this.emailState())
          return false;

        if(!this.languageState())
          return false;

        if(!this.passwordState())
          return false;

        return true;
      }

    resetAddModal() {
        this.userCreated = new UserViewModel();
        this.file = null;
    }
    resetUpdateModal() {
        this.userUpdated = new UserViewModel();
        this.permissionsUpdate = new PermissionViewModel();
        this.fileUpdate = null;
    }

   showPreviewImage(e : any){
     if (e.target.files && e.target.files[0]) {
      
      var reader = new FileReader();
      var base64;

      
      reader.onload = (e) => {
        var target = e.target as FileReader
        this.userCreated.ImageBase64 = target.result as string
      }

      reader.readAsDataURL(e.target.files[0]);
      
    }
    else{
        this.userCreated.ImageBase64="";
    }
   }

   showPreviewImageUpdate(e : any){
     if (e.target.files && e.target.files[0]) {
      
      var reader = new FileReader();
      var base64;

      
      reader.onload = (e) => {
        var target = e.target as FileReader
        this.userUpdated.ImageBase64 = target.result as string
      }

      reader.readAsDataURL(e.target.files[0]);
      
    }
    else{
        this.userUpdated.ImageBase64="";
    }
   }

    getLanguages(){
      this.loading = true;       
      this.languageservice.get()
        .then(response => {
          this.languages = response.data; 
        })
        .catch(error =>{                          
        })
    }

    getPermissions(){
      this.loading = true;       
      this.permissionsservice.getRoots()
        .then(response => {
          this.permissions = response.data; 
        })
        .catch(error =>{                          
        })
    }

    getServices(){
      this.loading = true;       
      this.servicesservice.get()
        .then(response => {
          this.services = response.data; 
          
        })
        .catch(error =>{                          
        })
    }

    handleAddSubmit(bvModalEvt : any) {
      bvModalEvt.preventDefault();

      if(!this.checkFormValidity()){
          return
      }

      let formData = new FormData();
      formData.append("File", this.file);
      formData.append("user", JSON.stringify(this.userCreated));
      this.loadingAdd = true;
      this.showAlert = false;
      this.userservice.add(formData)
      .then(response => {
        this.getUsersList();
        this.loadingAdd = false;
        this.variant = "success";       
        this.$bvModal.hide('crud-modal');
        this.message = response.data;
        this.file = null;
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
      this.userUpdated.Permissions = [];
      this.userUpdated.Permissions.push(this.permissionsUpdate);
      
      let formData = new FormData();
      formData.append("File", this.fileUpdate);
      formData.append("user", JSON.stringify(this.userUpdated));
      this.loadingUpdate = true;
      this.showAlert = false;
      
      this.userservice.update(formData).then(response => {
        this.getUsersList();
        this.loadingUpdate = false;
        this.variant = "success";       
        this.$bvModal.hide('update-modal');
        this.message = response.data;
        this.fileUpdate = null;
      })
      .catch(error => {
        this.fileUpdate = null;
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

      usernameState(){
        return (this.userCreated.UserName.length > 4 && this.userCreated.UserName.length < 13)
          || (this.userUpdated.UserName.length > 4 && this.userUpdated.UserName.length < 13) ? true : false
      }

      nameState(){
        return (this.userCreated.Name.length < 31 && this.userCreated.Name.length > 0) ||
              (this.userUpdated.Name.length < 31 && this.userUpdated.Name.length > 0) ? true : false
      }

      lastnameState(){
        return (this.userCreated.LastName.length < 31 && this.userCreated.LastName.length > 0) 
        ||  (this.userUpdated.LastName.length < 31 && this.userUpdated.LastName.length > 0) ? true : false
      }

      artistnameState(){
        return (this.userCreated.ArtistName.length < 51 && this.userCreated.ArtistName.length > 0) 
        || (this.userUpdated.ArtistName.length < 51 && this.userUpdated.ArtistName.length > 0)? true : false
      }

      emailState(){
        return (this.userCreated.Email.length < 251 && this.userCreated.Email.length > 0)
        || (this.userUpdated.Email.length < 251 && this.userUpdated.Email.length > 0) ? true : false
      }

      languageState(){
        var language = this.userCreated.Language as LanguageViewModel;
        var languageUpdate = this.userUpdated.Language as LanguageViewModel;
        return (language.Name != '') || (languageUpdate.Name!='') ? true : false
      }

      passwordState(){
        return (this.userCreated.Password.length < 33 && this.userCreated.Password.length > 7)
        || this.userUpdated.Password.length > 0 ? true : false
      }




      info(item : any, index : number, button : any) {
        // this.infoModal.title = item.UserName;
        this.loading =true;
        this.userservice.getById(item)
        .then(response => {
          this.userSelected = response.data;
          // this.imageSelected = this.showImage();
          this.loading = false;
          this.$root.$emit('bv::show::modal', 'info-modal', button)
        })
        .catch(error =>{
          this.loading = false;                              
        })

      }


      deleteUser(item : any, index : number, button : any) {
        // this.infoModal.title = item.UserName;
        this.showAlert = false;
        this.$bvModal.msgBoxConfirm('¿Estas seguro?', {
          title: 'Confirmacion',
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
              this.userservice.delete(item)
                .then(response => {
                  this.getUsersList();
                  
                  this.variant = "success";       
                  this.message = response.data
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
        // this.infoModal.title = item.UserName;
        // this.userUpdated = new UserViewModel();
       
        this.userservice.getById(item)
        .then(response => {
          this.userUpdated = response.data;
          this.serviceUpdateDisabled = false;
          if(this.userUpdated.Contract != null ){
              this.serviceUpdateDisabled = true;
              this.serviceUpdate = true;
          }
          else if(this.userUpdated.Permissions.filter(p =>p.Name != 'Login').length>0){ // sacar o ignorar login
              this.serviceUpdateDisabled = true;
              this.serviceUpdate = false;
              this.permissionsUpdate = this.userUpdated.Permissions.filter(p =>p.Name != 'Login')[0];
              this.permissionsUpdate.Permissions = [];
          }

          if(this.userUpdated.Contract==null){
            this.userUpdated.Contract = new ContractViewModel();
          }
          // this.imageSelected = this.showImage();
          
          this.$root.$emit('bv::show::modal', 'update-modal', button)
        })
        .catch(error =>{
                                    
        })

      }

      // resetInfoModal() {
      //   this.userSelected = null;
      // }

      // resetUpdateModal() {
      //    this.userUpdated = new UserViewModel();
      // }
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

<style >

@media (max-width: 600px) {
  .add-user-button{
    text-align: center;
    text-decoration: none;
    background-color:RGB(23,21,32);
    margin-top: 4px;
  }

  
}

@media (min-width: 600px) {
 
  .add-user-button{
  float:right;
  text-align: center;
  text-decoration: none;
  background-color:RGB(23,21,32);
  margin-top: 4px;
}
}


.input-width{
  width:80%
}

 .add-button-text{
    display:inline-block; 
    margin-bottom:0
  }

.title-modal-text{
  display: inline;
  margin-left: 15px;
}

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

.row-data{
  margin-bottom: 15px !important
}
</style>