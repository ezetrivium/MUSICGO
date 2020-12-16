<template>
<div>
<b-container>
    


    <div class="music-player">


        <div class="player" id="player">

        <div class="container">

            <div class="player-contents-wrap">

            <div class="album-art" v-show="currentSong.Name != ''">
                <b-img :src="currentSong.Album.ImageBase64" class="img" />
            </div>

            <div class="main-controls ">
                <div class="controls">



                <div class="play">
                    <b-icon icon="play" class="icon-player" scale="2" v-if="!isPlaying" @click="playCurrentSong()"></b-icon>
                    <b-icon icon="pause" class="icon-player" scale="2" v-else @click="pause()"></b-icon>
                </div>
                <div class="play">
                     <b-icon v-if="!mutedFlag" class="icon-player" icon="volume-up" scale="2" @click="mute()"></b-icon>
                     <b-icon v-if="mutedFlag" class="icon-player" icon="volume-mute" scale="2" @click="mute()"></b-icon>
                </div>

        

                </div>

                <div class="seek">
                    <div class="title-and-time">

                        <div class="title">
                        {{currentSong.Name}}: {{currentSong.User.ArtistName}}
                        </div>

                        <div class="time">
                        {{currentPlayedTime()}} / {{duration()}}
                        </div>
                    </div>

                    <!-- <div class="progress-container">
                        <div class="progress" id="progress-wrap">

                          <div class="progress-handle" :style="{left:progressPercentageValue}"></div>

                          <div class="transparent-seeker-layer" @click="seek"></div>

                          <div class="bar" :style="{width:progressPercentageValue}"></div>
                         </div>

                   </div> -->

              

                </div>

            </div>

            </div>

        </div>

        </div>
        <!-- the audio player code ends here -->

    </div>

  

  <audio ref="audiofile" preload style="display: none" controls></audio>
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
export default class AudioPlayer extends Vue{
    

    private songservice: SongService = songService;
    private voteService: VoteService = voteService;
    private claimservice: ClaimService = claimService;

   
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

    private currentSong : SongViewModel = new SongViewModel();
    private previousPlaylistIndex: number = 0;
    private hasNext: boolean = false;
    
    private originalSongArray: any[] = []


    private forwardSong : SongViewModel = new SongViewModel();
    private backwardSong : SongViewModel = new SongViewModel();

    private vote : VoteViewModel = new VoteViewModel();

    private mutedFlag : boolean = false;

    // @Prop({ required:true}) currentSong: SongViewModel = new SongViewModel();

    mounted() {
        this.audioPlayer = this.$el.querySelectorAll("audio")[0];
        this.initPlayer();
    }
    
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

    play(song : SongViewModel){
         
      this.currentSong = song;   
      this.setCurrentSong(this.currentSong);
      
      
      this.audioPlayer.src = process.env.VUE_APP_API+
      'sample/getsample?songKey='+this.currentSong.SongKey+'&songID='+this.currentSong.Id;  

      this.playCurrentSong();
    }


    initPlayer() {

 

      this.audioPlayer.addEventListener("timeupdate", this.updateTimer);
      this.audioPlayer.addEventListener("loadeddata", this.load);
      this.audioPlayer.addEventListener("pause", () => {
        this.isPlaying = false;
      });
      this.audioPlayer.addEventListener("play", () => {
        this.isPlaying = true;
      });


    }



     playCurrentSong() {
      this.audioPlayer.play();
      this.isPlaying = true;
    }


    setCurrentSong(song : SongViewModel) {
        
      this.currentSong = song;
      
     
    }

    



    pause() {
      this.audioPlayer.pause();
      this.isPlaying = false;
    }

    stop() {
      this.audioPlayer.currentTime = 0;
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


    formatTime(secs: number) {
      var minutes = Math.floor(secs / 60) || 0;
      var seconds = Math.floor(secs - minutes * 60) || 0;
      return minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
    }


    // seek(e : any) {
    //   if (this.isLoaded) {
    //     let el = e.target.getBoundingClientRect();
    //     let seekPos = (e.clientX - el.left) / el.width;
    //     let seekPosPercentage = seekPos * 100 + "%";

  

    //     let songPlayTimeAfterSeek = this.audioPlayer.duration * seekPos;


    //     this.audioPlayer.currentTime = songPlayTimeAfterSeek as number;

    //     this.progressPercentageValue = seekPosPercentage;
        
    //   } else {
    //     throw new Error("Song Not Loaded");
    //   }
    // }



    

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

<style scoped>

@media (max-width: 600px) {
    .main-controls{
        left:-1px;
    }

    .controls{
        width:40% !important;
    }

    .seek{
        left:100px !important;
    }
}

.icon-player{

	 color: white;
     margin-top:10px
}

.song .wrapper {
	 position: relative;
	 width: 100%;
	 height: auto;
}
 .song .wrapper .overlay-play {
	 cursor: pointer;
	 position: absolute;
	 width: 40px;
	 height: 40px;
	 background-color: #fd6a02;
	 bottom: 5px;
	 right: 5px;
	 border-radius: 50%;
}
 .song .wrapper .overlay-play i {
	 font-size: 1.8em;
	 line-height: 40px;
	 color: #fff;
}
 .song .song-title {
	 font-weight: 700;
	 margin-bottom: 0;
	 line-height: auto;
}
 .song .song-artiste {
	 line-height: auto;
}
 .music-player {
	 width: 100%;
	 bottom: 0;
	 position: fixed;
     left:-1px;
}

 .music-player .player {
	 position: relative;
	 z-index: 2;
	 width: 100%;
	 height: 60px;
	 background-color: #212529 !important;
}
 .music-player .player .player-contents-wrap {
	 position: relative;
	 width: 100%;
	 height: 60px;
}
 .music-player .player .player-contents-wrap .album-art {
	 background-color: white;
	 width: 60px;
	 height: 60px;
}
 .music-player .player .player-contents-wrap .album-art .img {
	 width: 60px;
	 height: 60px;
}
 .music-player .player .player-contents-wrap .main-controls {
	 position: absolute;
	 right: 0;
	 width: calc(100% - 60px);
	 height: 60px;
	 top: 0;
}
 .music-player .player .player-contents-wrap .main-controls i {
	 cursor: pointer;
}
 .music-player .player .player-contents-wrap .main-controls .controls {
	 width: 15%;
	 height: 60px;
	 display: flex;
	 justify-content: space-around;
	 text-align: center;
	 align-items: center;
	 background-color: rgba(0, 0, 0, 0.05);
}
 .music-player .player .player-contents-wrap .main-controls .controls i {
	 font-size: 1.8em;
	 color: white;
}
 .music-player .player .player-contents-wrap .main-controls .controls .play {
	 text-align: center;
	 border-radius: 25px;
	 width: 40px;
	 height: 40px;
	 border: 1px solid #fff;
}
 .music-player .player .player-contents-wrap .main-controls .controls .play i {
	 line-height: 40px;
	 text-align: center;
}
 .music-player .player .player-contents-wrap .main-controls .seek {
	 padding-left: 20px;
	 padding-top: 0;
	 position: absolute;
	 height: 60px;
	 width: 85%;
	 top: 0;
	 left: unset;
	 right: 0;
}
 .music-player .player .player-contents-wrap .main-controls .seek .title-and-time {
	 height: 25px;
	 width: 100%;
	 display: flex;
	 flex-direction: row;
	 justify-content: space-between;
	 align-items: center;
     color:white;
}
 .music-player .player .player-contents-wrap .main-controls .seek .progress-container {
	 position: relative;
	 height: 10px;
	 width: auto;
	 display: flex;
	 align-items: center;
}
 .music-player .player .player-contents-wrap .main-controls .seek .progress-container .progress {
	 background-color: rgba(0, 0, 0, 0.05);
	 height: 4px;
	 width: 100%;
	 margin: 0;
	 padding: 0 2px;
	 border-radius: 0;
	 display: flex;
	 align-items: center;
	 box-shadow: 0 1px 1px rgba(0, 0, 0, .1);
}
 .music-player .player .player-contents-wrap .main-controls .seek .progress-container .progress .progress-handle {
	 display: block;
	 position: absolute;
	 z-index: 6;
	 margin-top: 0;
	 margin-left: -2px;
	 width: 8px;
	 height: 8px;
	 border-radius: 100%;
	 background-color: #fff;
	 box-shadow: 0 1px 6px rgba(0, 0, 0, .2);
	 cursor: pointer;
}
 .music-player .player .player-contents-wrap .main-controls .seek .progress-container .progress .progress-handle:hover {
	 background-color: #000;
}
 .music-player .player .player-contents-wrap .main-controls .seek .progress-container .progress .transparent-seeker-layer {
	 width: 100%;
	 height: 6px;
	 background-color: transparent;
	 position: absolute;
	 cursor: pointer;
	 z-index: 5;
}
 .music-player .player .player-contents-wrap .main-controls .seek .progress-container .progress .bar {
	 width: 0;
	 background-color: #fff;
	 height: 4px;
	 position: absolute;
}

 .height-enter-active {
	 animation: bounce-in 0.5s;
}
 .height-leave-active {
	 animation: bounce-in 0.5s reverse;
}

 
</style>