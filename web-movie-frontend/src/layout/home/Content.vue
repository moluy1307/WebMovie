<template>
    <div class="page">
        <TheHeader :detectScroll="detectScroll"></TheHeader>
        <TheBody></TheBody>
        <TheFooter></TheFooter>
    </div>
</template>

<script>
import TheHeader from './header/TheHeader.vue';
import TheBody from './body/TheBody.vue';
import TheFooter from './footer/TheFooter.vue';
export default {
    name: 'ContentWatchmovie',
    components: {
        TheHeader,
        TheBody,
        TheFooter,
    },
    mounted() {
        window.addEventListener('scroll', this.handleScroll);
    },
    unmounted() {
        window.removeEventListener('scroll', this.handleScroll);
    },
    methods: {
        handleScroll() {
            if (window.pageYOffset > 180) {
                this.detectScroll = true;
            } else {
                this.detectScroll = false;
            }
        },
    },

    data() {
        return {
            detectScroll: false,
        };
    },
};
</script>

<style>
@import url('./content.css');
</style>
