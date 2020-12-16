<template>
  <div>
    <b-container fluid >
    <b-row>
        <b-col>
            <div>
                <b-alert class ="alert-form" style="margin-top:20px" :variant="variant" dismissible v-model="showAlert">{{$t(message)}}</b-alert>
            </div> 
        <b-card  class="container-form" >
        
            <b-card-title class="form-title">
                <b-row>
                    <b-col>
                        <h2>{{ $t("contract_service") }}</h2>
                    </b-col>

                </b-row>
            </b-card-title> 
    

            <b-container fluid >
      
                <div style="text-align:center"> 
                    <b-spinner style="margin-top: 100px; margin-bottom:100px" label="Loading..."  v-show="loading"></b-spinner>
                </div>
                <div v-show="!loading"> 
                <b-row>
                    <b-col class="text-center">
                        <b-card
                        :title="artistService.Name"
                        :img-src="require('@/assets/images/artistservice.jpg')"
                        img-alt="Image"
                        img-top
                        tag="article"
                        style="max-width: 20rem;"
                        class="mb-2 service-card"
                        >
                            
                            <!-- <b-card-text>
                                {{artistService.Description}}
                            </b-card-text> -->

                            <b-button  href="#" variant="primary" @click="getPreference($event.target, artistService)">{{ $t("contract") }}</b-button>
                        </b-card>
                    </b-col>
                    <b-col>
                        <b-card
                        :title="userService.Name"
                        :img-src="require('@/assets/images/userservice.jpg')"
                        img-alt="Image"
                        img-top
                        tag="article"
                        style="max-width: 20rem;"
                        class="mb-2 service-card"
                        >
                            <!-- <b-card-text>
                                 {{userService.Description}}
                            </b-card-text> -->

                            <b-button href="#" variant="primary" @click="getPreference($event.target,userService)">{{ $t("contract") }}</b-button>
                        </b-card>
                    </b-col>
                </b-row>
                
                </div>
            </b-container>
        </b-card>
        </b-col>
    </b-row>
  </b-container>

  <b-modal 
              centered 
              id="payment-modal" 
              
              @hidden="resetModal"
              size="lg"
              header-bg-variant="dark"
              header-text-variant="light"
              body-bg-variant="dark"
              body-text-variant="light"
              footer-bg-variant="dark"
              footer-text-variant="light"
              body-class="modal-body-padding"
              @shown="mercadoPagoForm"
              >
                <template #modal-title>
                    <h3>{{serviceSelected.Name}}</h3>
                </template>
                    
                 
                        <!-- <div style="text-align:center"> 
                            <b-spinner label="Loading..."  v-show="loadingPreference"></b-spinner>
                        </div> -->
                        <div> 
                            <b-card
                            :title="serviceSelected.Name"
                            :img-src="imageSelected"
                            img-alt="Image"
                            tag="article"
                            img-left 
                            class="service-card"
                            >
                            <b-card-text>
                                {{$t(serviceSelected.Description)}}
                            </b-card-text>
                            <div ref="MPButton" >
                                
                            </div> 
                            </b-card>
                            
                            
                            <!-- <form action="/procesar-pago" method="POST" ref="MPButton">
                                
                                <script
                                
                                src="https://www.mercadopago.com.ar/integrations/v1/web-payment-checkout.js"
                                data-preference-id="preference.id">
                                </script> 
                            </form>  -->
                        </div>
                 
                
              </b-modal>
 
</div>
</template>

<script lang="ts">
import { Component, Vue, Watch, Prop } from "vue-property-decorator";
import { UserViewModel } from '@/shared/classes/UserViewModel';
import userService, { UserService } from "@/shared/services/UserService";
import servicesService, { ServicesService } from "@/shared/services/ServicesService";
import contractService, { ContractService } from "@/shared/services/ContractService";
import { ServiceViewModel } from '@/shared/classes/ServiceViewModel';
import { Permissionshelper } from '@/shared/classes/Permissions-helper';
import { ContractViewModel } from '@/shared/classes/ContractViewModel';
import { PreferenceViewModel } from '@/shared/classes/PreferenceViewModel';

@Component({})
export default class ContractServiceComponent extends Vue {
    private servicesservice: ServicesService = servicesService;
    private contractservice: ContractService = contractService;
    private services: ServiceViewModel[] = []; 
    private loading: boolean = false;
    private loadingPreference: boolean = false;
    private artistService: ServiceViewModel | undefined = new ServiceViewModel();
    private userService: ServiceViewModel | undefined = new ServiceViewModel();
    private serviceSelected: ServiceViewModel | undefined = new ServiceViewModel();
    private user: UserViewModel  = new UserViewModel();
    private preference: PreferenceViewModel = new PreferenceViewModel();
    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;

    private imageSelected: string = "";


    @Prop({ type: String, required: true }) userID : any;


    created(){
      this.getServices();
      
    }

    mounted(){

        
    }

    mercadoPagoForm(){
        const el = this.$refs.MPButton as Element
        var form = document.createElement("form");
        form.action="/procesar-pago";
        form.method= "POST"
        var script = document.createElement("script");
        script.setAttribute("src","https://www.mercadopago.com.ar/integrations/v1/web-payment-checkout.js");
        script.setAttribute('data-preference-id',this.preference.Id);
        form.appendChild(script);
        el.appendChild(script);
    }

    resetModal(){
        this.preference = new PreferenceViewModel();
        this.serviceSelected = new ServiceViewModel();
        this.imageSelected = "";
    }
    getServices(){
      this.loading = true;       
      this.servicesservice.get()
        .then(response => {
          this.services = response.data; 
          this.artistService = this.services.find(s=>s.Name =="Artist")
          this.userService = this.services.find(s=>s.Name =="User")
          this.loading = false; 
        })
        .catch(error =>{    
            this.loading = false;                       
        })
    }



    getPreference(button : any, serviceP : ServiceViewModel) {
        this.loading = true;  
        this.user.Id= this.userID;
        this.user.Contract = new ContractViewModel();
        
        this.user.Contract.Service = serviceP;
    
        this.contractservice.preference(this.user)
        .then(response => {
          this.preference = response.data;
         
          this.serviceSelected = serviceP;
          this.imageSelected = require("@/assets/images/" + serviceP.Name + "pay.jpg");
          this.$root.$emit('bv::show::modal', 'payment-modal', button)
          
          this.loading = false; 
        })
        .catch(error =>{
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
            this.loading = false;     
            this.$bvModal.hide('payment-modal');
                                      
        })

      }
//que eme habraun modal luego de  clickear un servicio y que me retorne la preference, en ese modal
// esta el boton de mercado pago, cuando llegue a la successpage va al backend y me hace el contrato
// en el external reference deberia ir el id de usuario y el id de servicio
}
</script>

<style  >
@media (min-width: 920px) {
 
  .container-form{
      margin-left: 175px;
  }
    .alert-form{
        margin-left: 175px;
    }
}


.container-form{
    margin-top: 30px;
    background-color: inherit;
    border-color: #343a40;
    margin-bottom: 20px;
    max-width: 700px;
}

.input-width{
  width:100% !important;
  color: white;
  margin: 0 20px;
  
}

.mercadopago-button{
    width:70%
}

.form-title {
    color: #fff;
    padding: 16px 25px;
    border: solid 1px #343a40;
    
}

.modal-content{
    border: 1px solid white;
}
.modal-body-padding{
    padding: 0 !important
}
.service-card{
    background-color: inherit;
    color: white;
    border: solid 1px #343a40;
    text-align: center;
}
</style>