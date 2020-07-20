<template>
  <div>
    <Navbar />
     <router-view class="margin-page" ></router-view>
  </div>
</template>

<script>
import { Component, Vue, Watch } from "vue-property-decorator";
import Navbar from "@/shared/components/Navbar.vue";
@Component({
  components:{
    Navbar
  }
})
export default class App extends Vue{

}
</script>

<style scoped>
.margin-page{
   width: 80% !important;
    left: 0;
    right: 0;
    margin-left: auto;
    margin-right: auto;
}
   
</style>
