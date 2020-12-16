<template>
    <div>
        <b-container>
            <b-row class="row-image">
                <b-col lg="2" md="2" sm="2" class="ref-col">
      
                    <div class="album-art-left" >

                        <div >
                            <p class="title-ref-song"><strong>{{backwardSong.Name}}</strong></p> 
                            <img :src="backwardSong.Album.ImageBase64" class="img " />

                        </div>
                    
                    </div>

                </b-col>
                <b-col lg="8" md="8" sm="8">

        
                
                    <div class="album-art">
                        <div class="text-center" style="padding-top:150px" v-show="loading">
                            <b-spinner label="Loading..."></b-spinner>
                        </div>
                        <div v-show="!error && !loading" style="position:relative">
                            <div class="no-vote" @click="negativeVote()" >
                                <b-iconstack font-scale="1.5" animation="spin">
                                    <b-icon stacked icon="x-circle-fill"  scale="6" variant="danger" ></b-icon>
                                </b-iconstack> 
                            </div>
                            <div class="no-vote-mobile" @click="negativeVote()">
                                <b-iconstack font-scale="1.5" animation="spin" >
                                    <b-icon stacked icon="x-circle-fill" scale="3" variant="danger" ></b-icon>
                                </b-iconstack> 
                            </div>
                            <!-- <div class="vote">
                                
                                <b-icon :icon="iconVoted" scale="4" :variant="iconVariant" ></b-icon>
                                
                            </div> -->

                            <div class="claim-menu">
                                    <b-dropdown id="dropdown-left" variant="link" right toggle-class="text-decoration-none" no-caret>
                                        

                                        <template #button-content>
                                            <b-icon style="color:white" icon="three-dots-vertical"></b-icon>
                                        </template>
                                        <b-dropdown-item variant="light" v-b-modal.claim-modal href="#">{{$t('claim_song')}}</b-dropdown-item>
                                    </b-dropdown>
                                    
                            </div>

                            <img  :src="currentSong.Album.ImageBase64" class="img img-center "  />

                            <div class="yes-vote" @click="positiveVote()" >
                                <b-iconstack font-scale="1.5" animation="spin" >
                                    <b-icon stacked icon="check-circle-fill" scale="6" variant="success" ></b-icon>
                                </b-iconstack> 
                            </div>
                            <div class="yes-vote-mobile" @click="positiveVote()">
                                <b-iconstack font-scale="1.5" animation="spin" >
                                    <b-icon stacked icon="check-circle-fill" scale="3" variant="success" ></b-icon>
                                </b-iconstack> 
                            </div>
                            <div class="back-info-image" >
                                <p><strong>{{currentSong.Name}}</strong></p>        
                                <p>{{currentSong.User.ArtistName}} - {{currentSong.Album.Name || currentSong.User.ArtistName}}</p> 
                            </div>
                            <div class="back-info-image-mobile" >
                                <p><strong>{{currentSong.Name}}</strong></p>        
                                <p>{{currentSong.User.ArtistName}} - {{currentSong.Album.Name || currentSong.User.ArtistName}}</p> 
                            </div>
                        </div>
                        
                        
                    </div>

                </b-col>

                <b-col class="ref-col" lg="2" sm="2" md="2">
          
                    <div class="album-art-right">
                         <p class="title-ref-song"><strong>{{forwardSong.Name}}</strong></p> 
                        <img :src="forwardSong.Album.ImageBase64" class="img " />
                    </div>

                </b-col>
            </b-row>
             <b-row>
                <div class="music-player">

                    

                    <!-- the audio player code starts here -->
                    <div class="player" id="player">

                    <b-container>

                         <b-row>

                            <div class="player-contents-wrap">

                                

                                <div class="main-controls ">
                                    <div class="controls">

                                        <b-col class="play">
                                            <b-icon class="icon-player" icon="skip-backward" scale="2" @click="skip('backward')"></b-icon>
                                        </b-col>

                                        <b-col class="play">
                                            <b-icon class="icon-player" icon="play" scale="2" v-if="!isPlaying" @click="playCurrentSong()"></b-icon>
                                            <b-icon class="icon-player" icon="pause" scale="2" v-else @click="pause()"></b-icon>
                                        </b-col>
                                        <b-col class="play">
                                            <b-icon v-if="!mutedFlag" class="icon-player" icon="volume-up" scale="2" @click="mute()"></b-icon>
                                            <b-icon v-if="mutedFlag" class="icon-player" icon="volume-mute" scale="2" @click="mute()"></b-icon>
                                        </b-col>

                                        <b-col class="play">
                                            <b-icon class="icon-player" icon="skip-forward" scale="2" @click="skip('forward')"></b-icon>
                                        </b-col>

                                    </div>

                                
                                </div>

                            </div>
                        </b-row>
                    </b-container>

                    </div>
                    <!-- the audio player code ends here -->

                </div>
            </b-row>
            <div>
             <audio  ref="audiofile"  style="display: none" controls>
            
            </audio>
            </div> 


            <b-modal 
              centered 
              id="claim-modal" 
              :title="$t('claim_song')"
              @ok="handleClaimSubmit"
              @hidden="resetClaimModal"
              size="md"
              header-bg-variant="dark"
              header-text-variant="light"
              body-bg-variant="dark"
              body-text-variant="light"
              footer-bg-variant="dark"
              footer-text-variant="light"
              thead-tr-class="table-head-row"
              >
                <b-container fluid>
                   <div style="text-align:center"> 
                    <b-spinner label="Loading..."  v-show="loadingClaim"></b-spinner>
                  </div>
 
                  <b-form  @submit.stop.prevent="handleClaimSubmit" v-show="!loadingClaim">
                    <b-row class="mb-1">

                      <b-col >
                        <div>
                            <b-alert :variant="variant" dismissible v-model="showAlert">{{$t(message)}}</b-alert>
                        </div> 

                        <b-form-group
                        
                        :label="$t('claim')"
                        label-for="textarea-md"
                        
                        >
                            <b-form-textarea
                                id="textarea-md"
                                size="md"
                                :placeholder="$t('description_of_your_claim')"
                                v-model="claim.Description"
                                required
                                rows="3"
                                :state="claimState()"
                                aria-describedby="input-claim-feedback"
                            ></b-form-textarea>
                            <b-form-invalid-feedback :state="claimState()" id="input-claim-feedback">
                              {{ $t("claim_validate") }}
                            </b-form-invalid-feedback>
                        </b-form-group>

                      </b-col>
                    </b-row>
                  </b-form>
                </b-container>
                
              </b-modal>
        </b-container>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Watch, Prop } from "vue-property-decorator";
import { UserViewModel } from '../shared/classes/UserViewModel';
import songService, { SongService } from "../shared/services/SongService";
import voteService, { VoteService } from "../shared/services/VoteService";
import albumService, { AlbumService } from "../shared/services/AlbumService";
import claimService, { ClaimService } from "../shared/services/ClaimService";

import { SongViewModel } from '@/shared/classes/SongViewModel';
import { AlbumViewModel } from '@/shared/classes/AlbumViewModel';
import { VoteViewModel } from '@/shared/classes/VoteViewModel';
import { ClaimViewModel } from '@/shared/classes/ClaimViewModel';


import { Permissionshelper } from '../shared/classes/Permissions-helper';

@Component({})
export default class Play extends Vue {
    private songservice: SongService = songService;
    private voteService: VoteService = voteService;
    private claimservice: ClaimService = claimService;

    private songs : SongViewModel[] =[];
   
    private claim : ClaimViewModel =  new ClaimViewModel();
    
    private iconVoted:string = "";
    private iconVariant:string="";

    private message: string = "";
    private variant: string = "danger";
    private showAlert: boolean = false;

    private loading: boolean = false;
    private loadingClaim: boolean = false;

    private error: boolean = false;

    private isPlaying: boolean = false;
    private isLoaded: boolean = false;
    private isCurrentlyPlaying: string= "";
    private onRepeat: boolean = false;
    private shuffle: boolean =false;

    private currentIndex : number = 0;

    private durationSeconds: number = 0;
    private currentSeconds: number = 0;
    private audioPlayer: any;
    private sourcePlayer: any;
    private previousVolume: number = 35;
    private volume: number = 100;
    private autoPlay:boolean =  false;
    private progressPercentageValue: string = "0%";


    private previousPlaylistIndex: number = 0;
    private hasNext: boolean = false;
    
    private originalSongArray: any[] = []

    private currentSong : SongViewModel = new SongViewModel();
    private forwardSong : SongViewModel = new SongViewModel();
    private backwardSong : SongViewModel = new SongViewModel();

    private vote : VoteViewModel = new VoteViewModel();

    private mutedFlag : boolean = false;
    
    currentPlayedTime() {
        return this.formatTime(this.currentSeconds);
    }
    duration() {
        return this.formatTime(this.durationSeconds);
    }
    progressPercentage() {
        return this.currentSeconds / this.durationSeconds * 100;
    }
    muted() {
        //this returns true or false
        return this.volume / 100 === 0;
    }



    mounted() {
        this.audioPlayer = this.$el.querySelectorAll("audio")[0];


        
        this.getSongs(false);
       
        
    }


    checkFormValidity() {

        if(!this.claimState())
          return false;


        return true;
      }

    claimState(){
        return (this.claim.Description.length > 0) ? true : false
    }  

    resetClaimModal() {
        this.claim = new ClaimViewModel();
    }

    handleClaimSubmit(bvModalEvt : any){
        bvModalEvt.preventDefault();

      if(!this.checkFormValidity()){
          return
      }

    this.claim.SongClaimed = this.currentSong;  

    this.loadingClaim = true;

      this.claimservice.add(this.claim)
      .then(response => {
        
        this.loadingClaim = false;
            
        this.$bvModal.hide('claim-modal');

      })
      .catch(error => {

        this.loadingClaim = false;
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

    getSongs(refresh:boolean){
      this.error = false;  
      this.loading = true;
      this.songservice.getSongsToPlay(refresh)
      .then(response => {
        this.songs = response.data;
        this.initPlayer();
        this.loading = false;
        
        
      
      })
      .catch(error =>{
        this.error = true;  
 
        this.loading = false;

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
      })}


    positiveVote(){
        
        this.vote.Positive = true;
        this.vote.TimeToPositive = this.currentSeconds;
        this.vote.Song = this.currentSong;
        
 
        this.voteService.add(this.vote)
        .then(response => {
            var vote = document.getElementsByClassName('img-center')[0];

            vote.className = 'img img-center vote-positive vote-fade-out'
            
            setTimeout(function(){
                
                vote.className = 'img img-center';
            }, 1000);   
        })
        .catch(error =>{
               
        })
    }


    negativeVote(){
        
        this.vote.Positive = false;
        this.vote.TimeToPositive = this.currentSeconds;
        this.vote.Song = this.currentSong;
 
        this.voteService.add(this.vote)
        .then(response => {
             

            this.skip('forward');
        })
        .catch(error =>{
               
        })
    }

     

    initPlayer() {
    //   this.songservice.play(this.songs[0]).then(response => {
    //     this.audioPlayer.src = response.data;
      
    //   })
      
      this.setCurrentSong(this.songs[0]);
      
      
      this.audioPlayer.src = process.env.VUE_APP_API+
      'sample/getsample?songKey='+this.currentSong.SongKey+'&songID='+this.currentSong.Id;  


      

      this.audioPlayer.addEventListener("timeupdate", this.updateTimer);
      this.audioPlayer.addEventListener("loadeddata", this.load);
      this.audioPlayer.addEventListener("pause", () => {
        this.isPlaying = false;
      });
      this.audioPlayer.addEventListener("play", () => {
        this.isPlaying = true;
      });

      this.audioPlayer.addEventListener("ended", this.playNextSongInPlaylist);
    }

     playCurrentSong() {
      this.audioPlayer.play();
      this.isPlaying = true;
    }


    setCurrentSong(song : SongViewModel) {
        
      this.currentSong = song;
      this.forwardSong = this.songs[this.currentIndex + 1] != null ? this.songs[this.currentIndex + 1] : new SongViewModel();
      this.backwardSong = this.songs[this.currentIndex - 1] != null ? this.songs[this.currentIndex - 1] : new SongViewModel();
     
    }

    



    pause() {
      this.audioPlayer.pause();
      this.isPlaying = false;
    }

    stop() {
      this.audioPlayer.currentTime = 0;
    }


    skip(direction: string) {
      if (direction === "forward") {
        this.currentIndex += 1;
      } else if (direction === "backward") {
        this.currentIndex -= 1;
      }

      /**if the current Index of the playlist is greater or equal to the length of the playlist songs (the index is out of range)
       reset the index to 0. It could also mean that the playlist is at its end. */

      if (this.currentIndex >= this.songs.length ) {
         
         this.songservice.getSongsToPlay(true)
            .then(response => {
                this.songs = response.data;

                this.loading = false;
                this.currentIndex = 0;
                
                this.setCurrentSong(this.songs[this.currentIndex]);
                 
               
                this.audioPlayer.src = process.env.VUE_APP_API+'sample/getsample?songKey='+this.currentSong.SongKey+'&songID='+this.currentSong.Id; 
            })
            .catch(error =>{
        
                this.loading = false;
            })
      }

      if (this.currentIndex < 0) {
        this.currentIndex = 0;
        
        this.setCurrentSong(this.songs[this.currentIndex]);
        
        this.audioPlayer.src = process.env.VUE_APP_API+
      'sample/getsample?songKey='+this.currentSong.SongKey+'&songID='+this.currentSong.Id; 
      }
      else{
        this.setCurrentSong(this.songs[this.currentIndex]);
        
        this.audioPlayer.src = process.env.VUE_APP_API+
      'sample/getsample?songKey='+this.currentSong.SongKey+'&songID='+this.currentSong.Id; 
      }


      //the code below checks if a song is playing so it can go ahead and auto play
      if (this.isPlaying) {
        this.audioPlayer.play();
      }
    }


    mute() {
      //this.muted is a computed variable available down below
      this.audioPlayer.muted = !this.audioPlayer.muted
      this.mutedFlag= !this.mutedFlag;
    }



    updateTimer() {
      this.currentSeconds = parseInt(this.audioPlayer.currentTime);
      this.progressPercentageValue =
        (this.currentSeconds / parseInt(this.audioPlayer.duration) * 100 || 0) +
        "%";
    }


    playNextSongInPlaylist() {
    
        if (this.songs.length > 1) {
            
            this.currentIndex++;

             if (this.currentIndex >= this.songs.length ) {
        
                    this.songservice.getSongsToPlay(true)
                    .then(response => {
                        this.songs = response.data;
                        this.currentIndex = 0;
                        this.setCurrentSong(this.songs[this.currentIndex]);
                        
                        this.audioPlayer.src = process.env.VUE_APP_API+'sample/getsample?songKey='+this.currentSong.SongKey+'&songID='+this.currentSong.Id; 
                        this.audioPlayer.play();
                    })
                    .catch(error =>{
                
                    })
            }else{
                this.setCurrentSong(this.songs[this.currentIndex]);
                
                this.audioPlayer.src = process.env.VUE_APP_API+'sample/getsample?songKey='+this.currentSong.SongKey+'&songID='+this.currentSong.Id; 
                this.audioPlayer.play();
            }
            
            
            
          
        } 
      
    }

    formatTime(secs: number) {
      var minutes = Math.floor(secs / 60) || 0;
      var seconds = Math.floor(secs - minutes * 60) || 0;
      return minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
    }


    

    load() {
      if (this.audioPlayer.readyState >= 2) {
        this.isLoaded = true;
        this.durationSeconds = parseInt(this.audioPlayer.duration);
      } else {
        this.message = "fail_load_sound_file";
        this.showAlert = true;

      }
    }

}


</script>

<style  scoped>
@import url("https://fonts.googleapis.com/css?family=Montserrat");


@media (max-width: 600px) {
  .album-art {
    padding-top:30% ! important;   
    }

    .album-art-left{
        display:none
    }

    .album-art-right{
        display:none
    }

    .music-player {
        margin-left:6.1% !important;
        
    }

    .music-player .player {
        
        margin-bottom: 50px !important;


    }

    .row-image .ref-col{
        display:none
    }

    .back-info-image{
        display: none;
    }
    

    .back-info-image-mobile{
        display: block !important;
        

    }

    .back-info-image-mobile p{
        color:white;
        font-size: 12px;
        
    }


    .no-vote{
    display:none
    }

    .yes-vote{
        display:none
    }

    .no-vote-mobile{
        position: absolute;
        display:block !important;
        top: 90px;
        left: 0;
    }

    .yes-vote-mobile{
        position: absolute;
        display: block !important;
        top: 90px;
        right: 0;
    }

    
}


.table-head-row{
  background-color: #343a40;
  border: solid 0.5px;
}

.vote-positive{
    -webkit-transition: opacity 1s;
	-moz-transition: opacity 1s;
	-o-transition: opacity 1s;
	transition: opacity 1s;
    opacity:1;
    -webkit-animation:spin 1s linear infinite;
    -moz-animation:spin 1s linear infinite;
    animation:spin 1s linear infinite;
    border:solid #28a745 !important;
} 

.vote-negative{
    -webkit-transition: opacity 1s;
	-moz-transition: opacity 1s;
	-o-transition: opacity 1s;
	transition: opacity 1s;
    opacity:1;
    -webkit-animation:spin 1s linear infinite;
    -moz-animation:spin 1s linear infinite;
    animation:spin 1s linear infinite;
    border:solid #dc3545 !important;
} 

@-moz-keyframes spin { 100% { -moz-transform: rotate(360deg); } }
@-webkit-keyframes spin { 100% { -webkit-transform: rotate(360deg); } }
@keyframes spin { 100% { -webkit-transform: rotate(360deg); transform:rotate(360deg); } }

.vote-fade-out
{
    
    opacity:0;
}

.no-vote{
    position: absolute;
    left: 0;
    top: 167px;
}

.yes-vote{
    position: absolute;
    right:0;
    top: 168px;
}

.no-vote-mobile{
    display: none;
    
}

.yes-vote-mobile{
     display:none;
        
}


.claim-menu{
    position: absolute;
    top: 10px;
    right: 10px;
}

.toggle-claim{
    background-color: transparent !important;
}


.album-art-left{
    padding-top:120px;
}

.album-art-right{
    padding-top:120px;
}

.title-ref-song{
    font-size:12px;
    color: white;
}

.album-art-left .img{
    width:40%;
    border: solid
}

.album-art-right .img{
    width:40%;
    border: solid
}

.back-info-image-mobile{
        display: none;
        
    }

.back-info-image{
    background-color: #343a40 !important;
    top: 280px;
    position: absolute;
    border: solid 1px white;
    border-radius: 20px;
    min-width: 200px;
    min-height: 100px;
    margin-left: 20px;
    padding: 10px;
    padding-top: 20px;
}



.back-info-image p{
    color:white;
    font-size: 12px;
}
 
 .music-player {
	 width: 100%;
	 bottom: 0;
     margin-left: 3.4%;	
}

 .music-player .player {
	 position: fixed;
	 z-index: 2;
	 width: 100%;
     bottom : 0;
     margin-bottom: 20px;


}
 .music-player .player .player-contents-wrap {
	 position: relative;
	 width: 100%;
	 
}
 .album-art {

	 
    max-width: 400px;
    height: auto;
    max-height: 400px;
    margin: auto;
    padding-top:1%
    
}
 .album-art .img {
	 width: 100%;
	 height: 100%;
     border: solid
     
}

.album-art-left .img {
	width: 100%;
}

.album-art-right .img {
	width: 100%;
}

 .music-player .player .player-contents-wrap .main-controls {
	 
	 right: 0;
	 width: 100%;
	 top: 0;
}
 .music-player .player .player-contents-wrap .main-controls > i {
	 cursor: pointer;
}
 .music-player .player .player-contents-wrap .main-controls .controls {
	 width: 50%;
	 
	 display: flex;
	 justify-content: space-around;
	 text-align: center;
	 align-items: center;
	 background-color: rgba(0, 0, 0, 0.05);
     margin-left: 10%;
}
.b-icon.bi{
    margin-top:25px;
    cursor: pointer;
}

 .icon-player{

	 color: white;
}
 .music-player .player .player-contents-wrap .main-controls .controls .play {
	 text-align: center;
	 border-radius: 40px;
	 min-width: 70px;
	 min-height: 70px;
	 border: 1px solid #fff;
     margin: 10px
}

 

 
 
 
 .height-enter-active {
	 animation: bounce-in 0.5s;
}
 .height-leave-active {
	 animation: bounce-in 0.5s reverse;
}


 
</style>