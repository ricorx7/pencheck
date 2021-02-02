<template>
    <div class="home">
        <img alt="PenCheck logo" src="../assets/logo-pencheck.png" />

        <br />
        <Plans />
    </div>
</template>

<script>
    // @ is an alias to /src
    import Plans from "@/components/Plans.vue";


    export default {
      name: "Home",
      components: {
          Plans,
      }
};
</script>
