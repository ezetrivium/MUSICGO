<template>
    <div>
  <b-navbar type="dark" variant="dark">
    <b-navbar-nav>
      <b-nav-item><router-link :to="{ name: 'login' }">Login</router-link></b-nav-item>
      <b-nav-item><router-link :to="{ name: 'home' }">Home</router-link></b-nav-item>

      <!-- <b-nav-item-dropdown :text="$t('user')" right>
        <b-dropdown-item href="#">Account</b-dropdown-item>
        <b-dropdown-item href="#">Settings</b-dropdown-item>
      </b-nav-item-dropdown> -->
    </b-navbar-nav>
  </b-navbar>
</div>
</template>


<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";


export default class Login extends Vue{
  computed() {
    this.loggedIn();
  }

  loggedIn(){
      return this.$store.getters.loggedIn
  }
}

</script>

<style scoped>

</style>