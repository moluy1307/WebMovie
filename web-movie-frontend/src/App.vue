<template>
    <div class="container-admin" v-if="accountAdmin">
        <TheSidebar></TheSidebar>
        <ContentAdmin></ContentAdmin>
    </div>
    <ContentWatchmovie v-else></ContentWatchmovie>
</template>

<script>
import ContentWatchmovie from './layout/home/Content.vue';
import ContentAdmin from './layout/home/ContentAdmin.vue';
import TheSidebar from './layout/home/TheSidebar.vue';
export default {
    name: 'App',
    components: {
        ContentWatchmovie,
        ContentAdmin,
        TheSidebar,
    },
    created() {
        const storage = localStorage.getItem('adminInfor');
        if (storage !== null) {
            this.userInformation = JSON.parse(storage);
            this.accountAdmin = true;
            this.$router.push({ name: 'dashboard', params: { id: this.userInformation.userId } });
        } else {
            this.accountAdmin = false;
        }
    },
    data() {
        return {
            accountAdmin: false,
            userInformation: {},
        };
    },
};
</script>

<style>
@import url('./css/main.css');

.container-admin {
    display: flex;
    flex-wrap: wrap;
    width: 100%;
    height: 100vh;
}
</style>
