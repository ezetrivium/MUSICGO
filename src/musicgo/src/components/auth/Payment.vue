<template >
    <div>
        <b-container fluid >
            
                <div style="text-align:center;padding-top:50px;" v-show="loading"> 
                    <b-spinner style=" width: 5rem; height: 5rem; margin-top: 100px; margin-bottom:100px" label="Loading..."  ></b-spinner>
                    <h4 style="color:white">Por favor no cierre la ventana mientras se completa el proceso</h4>
                </div>

                <div style="text-align:center; color:white;padding-top:50px;" v-show="!loading">
                    <h4>{{message}}</h4>
                    <b-icon icon="check-circle" style="padding-top:20px; width: 120px; height: 120px;" variant="danger" v-show="showAlert"></b-icon>
                    <b-icon icon="check-circle" style="padding-top:20px; width: 120px; height: 120px;" variant="success" v-show="!showAlert" ></b-icon>
                </div>
        </b-container>
    </div> 
</template>

<script lang="ts">

import { Component, Vue, Watch, Prop } from "vue-property-decorator";
import { UserViewModel } from '@/shared/classes/UserViewModel';
import contractService, { ContractService } from "@/shared/services/ContractService";
@Component({})
export default class Payment extends Vue {
    private loading: boolean = true;
    private message: string = "";
    private showAlert: boolean = false;
    private contractservice: ContractService = contractService;
    // @Prop({ type: String, required: true }) external_reference : any;


    mounted(){
        if(document.getElementsByClassName('mp-mercadopago-checkout-wrapper')[0] != null){
            document.getElementsByClassName('mp-mercadopago-checkout-wrapper')[0].remove();
        }
        
        this.setPayment();
        
    }

     setPayment() {
        this.loading = true;  

         if (this.$route.query.external_reference != null){
             var extRef = this.$route.query.external_reference as string;
             this.contractservice.notification(extRef)
            .then(response => {
            this.message = response.data;
            this.showAlert = false;  
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
                
                this.showAlert = true;
                this.loading = false;     
                                        
            })
        }
        

      }
}
</script>


<style scoped>

</style>