<template>

</template>

<script>
export default {
  mounted() {

    this.$store.dispatch('destroyUsersList');

    this.$store.dispatch('destroyToken')
      .then(response => {
        this.$router.push({ name: 'home' })
      })
  }
}
</script>

