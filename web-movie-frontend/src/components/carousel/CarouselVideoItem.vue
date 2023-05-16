<template>
    <transition :name="transitionEffect">
        <div v-show="currentSlide === index">
            <!-- <iframe class="item-video" :src="videoSlide"></iframe> -->
            <div v-lazyload-youtube="videoSlide"></div>
        </div>
    </transition>
</template>

<script>
export default {
    name: 'CarouselVideoItem',
    props: ['videoSlide', 'currentSlide', 'index', 'direction', 'titleSlide', 'episodeNumber', 'movieContent'],
    computed: {
        transitionEffect() {
            return this.direction === 'right' ? 'slide-out' : 'slide-in';
        },
    },
};
</script>

<style scoped>
.item-video {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
}
.slide-in-enter-active,
.slide-in-leave-active,
.slide-out-enter-active,
.slide-out-leave-active {
    transition: all 1s ease-in-out;
}
.slide-in-enter-from {
    /* transform: translateX(-100%); */
    opacity: 1;
}
.slide-in-leave-to {
    /* transform: translateX(100%); */
    opacity: 0;
}
.slide-out-enter-from {
    /* transform: translateX(100%); */
    opacity: 0;
}
.slide-out-leave-to {
    /* transform: translateX(-100%); */
    opacity: 1;
}
</style>
