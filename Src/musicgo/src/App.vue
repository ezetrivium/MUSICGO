<template>
  <div>
    <Navbar />
     <router-view ></router-view>
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

<style>

</style>
