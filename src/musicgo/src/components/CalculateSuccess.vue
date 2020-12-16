<template >
    <div>
        <b-container fluid >
            
                <div style="text-align:center;padding-top:50px;" v-show="loading"> 
                    <b-spinner style=" width: 5rem; height: 5rem; margin-top: 100px; margin-bottom:100px" label="Loading..."  ></b-spinner>
                    <h4 style="color:white">{{$t('do_not_close_the_window')}}</h4>
                </div>

                <div style="text-align:center; color:white;padding-top:50px;" v-show="!loading">
                    

                    <b-row v-show="showAlert">
                      <h4 >{{$t(message)}}</h4>
                      <b-icon icon="check-circle" style="padding-top:20px; width: 120px; height: 120px;" variant="danger"></b-icon>
                    </b-row>
                    <div v-show="!showAlert">
                      <b-row  style="text-align">
                          <b-img  rounded="0" :src="this.success.ImageBase64" class="image-width"></b-img>
                          
                      </b-row>
                      <strong class="text-coincidence">{{$t('coincidences')}}: <div class="percentage">{{this.success.Percentage}}%</div></strong> 
                    </div>
                </div>
        </b-container>
    </div> 
</template>

<script lang="ts">

import { Component, Vue, Watch, Prop } from "vue-property-decorator";
import { UserViewModel } from '@/shared/classes/UserViewModel';
import songService, { SongService } from "@/shared/services/SongService";
import { SongViewModel } from '@/shared/classes/SongViewModel';
import { SuccessViewModel } from '@/shared/classes/SuccessViewModel';
@Component({})
export default class CalculateSuccess extends Vue {

    private songservice: SongService = songService;
    private loading: boolean =false;

    private success : SuccessViewModel = new SuccessViewModel();
    private message: string = "";
    private variant: string = "";
    private showAlert: boolean = false;
   

    @Prop({ required:true}) songProp: any;

    mounted(){
      this.calculateSuccess();
    }

   calculateSuccess(){

      this.loading = true;
      this.songservice.calculateSuccess(this.songProp)
      .then(response => {

        this.success = response.data;
        this.loading = false;       
      })
      .catch(error =>{
 
        this.loading = false;
      })}
}
</script>


<style scoped>
.image-width{
  max-width:100%;
  max-height: 260px;
  margin:auto;
  }

.text-coincidence{
  margin-top: 150px;
  font-size: 3rem;
}  

.percentage{
  background-color: gray;
  max-width: 160px;
  height: 80px;
  border-radius: 30px ;
  text-align: center;
  margin:auto
}
</style>