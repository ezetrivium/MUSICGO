<template>
    <div>
     <b-container>
         <b-row>
             <b-col>
                <b-carousel
                id="carousel-1"
                v-model="slide"
                class="slides"
                :interval="4000"
                controls
                indicators
                background="#ababab"
         
                style="text-shadow: 1px 1px 2px #333;"
                @sliding-start="onSlideStart"
                @sliding-end="onSlideEnd"
                    >
                <!-- Text slides with image -->
                <b-carousel-slide
                    :caption="$t('artists_service')"
                    :text="$t('artists_service_description_slide')"
                    :img-src="require('@/assets/images/slide1.jpg')"
                    
                ></b-carousel-slide>

                <!-- Slides with custom text -->
                <b-carousel-slide
                    :caption="$t('users_service')"
                    :text="$t('users_service_description_slide')"
                :img-src="require('@/assets/images/slide2.jpg')"
                >
                    
                </b-carousel-slide>

            
                <b-carousel-slide 
                :caption="$t('vote_favorite_songs')"
                    :text="$t('vote_favorite_songs_description')"
                :img-src="require('@/assets/images/slide3.jpg')"
                ></b-carousel-slide>

            
                </b-carousel>
            </b-col>
        </b-row>
        <b-row>
                    <b-col class="text-center">
                        <b-card
                        :title="artistService.Name"
                        :img-src="require('@/assets/images/artistservice.jpg')"
                        img-alt="Image"
                        img-top
                        tag="article"
                        
                        class="mb-2 service-card home-service-card"
                        >
                            
                            <b-card-text>
                                {{$t(artistService.Description)}}
                            </b-card-text>

                           <b-button v-show="!loggedIn()"  href="#" variant="primary" @click="subscribe()">{{ $t("subscribe") }}</b-button>
                           <b-button v-show="loggedIn()" href="#" variant="primary" @click="contractService()">{{ $t("contract") }}</b-button>
                        </b-card>
                    </b-col>
                    <b-col>
                        <b-card
                        :title="userService.Name"
                        :img-src="require('@/assets/images/userservice.jpg')"
                        img-alt="Image"
                        img-top
                        tag="article"
                        
                        class="mb-2 service-card home-service-card"
                        >
                            <b-card-text>
                                 {{$t(userService.Description)}}
                            </b-card-text>

                           <b-button v-show="!loggedIn()"  href="#" variant="primary" @click="subscribe()">{{ $t("subscribe") }}</b-button>
                           <b-button v-show="loggedIn()" href="#" variant="primary" @click="contractService()">{{ $t("contract") }}</b-button>
                        </b-card>
                    </b-col>
                </b-row>
    </b-container>   
    </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { Permissionshelper } from "../shared/classes/Permissions-helper"
import servicesService, { ServicesService } from "@/shared/services/ServicesService";
import { ServiceViewModel } from '@/shared/classes/ServiceViewModel';
import { UserViewModel } from '@/shared/classes/UserViewModel';
@Component({})
export default class Home extends Vue{
    
    private slide : number = 0;

    private sliding : any = null;
    private servicesservice: ServicesService = servicesService;
    private services: ServiceViewModel[] = []; 
    private artistService: ServiceViewModel | undefined = new ServiceViewModel();
    private userService: ServiceViewModel | undefined = new ServiceViewModel();
    
    mounted(){
        
        this.getServices();
    }

    onSlideStart(slide: number) {
        this.sliding = true
      }
    onSlideEnd(slide:number) {
        this.sliding = false
      }

    subscribe(){

        this.$router.push({
          name:'Subscribe',
          
        })
    }

    contractService(){
        var user = this.$store.getters.user as UserViewModel;
        this.$router.push({
            path: `/contractservice/${user.Id}`,
            // name:'contractservice',
            // params:{userID : response.data}
            });
    }

    loggedIn(){
      return this.$store.getters.loggedIn
    }

    getServices(){
         
      this.servicesservice.get()
        .then(response => {
          this.services = response.data; 
          this.artistService = this.services.find(s=>s.Name =="Artist")
          this.userService = this.services.find(s=>s.Name =="User")
        
        })
        .catch(error =>{    
                                 
        })
    }  
}
</script>


<style scoped>

@media (max-width: 600px) {

    .slides{
        margin-left:0 !important;
        width:100% !important;
        left:-50px;
        width:22.5rem !important
    }
}
    .slides{
        margin-top:15px;
        width:50rem;
        margin-left:130px;
    }

.home-service-card{
    margin-top:20px;
    
}    
</style>