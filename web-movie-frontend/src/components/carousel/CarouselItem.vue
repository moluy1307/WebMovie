<template>
    <transition :name="transitionEffect">
        <div
            class="carousel-item"
            v-show="currentSlide === index"
            @mouseenter="$emit('mouseenter')"
            @mouseout="$emit('mouseout')"
        >
            <img v-lazy="slide" />
            <div class="general-intro">
                <h3>{{ titleSlide }}</h3>
                <p class="title2"></p>
                <p class="time">{{ episodeNumber }}</p>
                <p class="txt">{{ movieContent }}</p>
            </div>
        </div>
    </transition>
</template>

<script>
export default {
    name: 'CarouselItem',
    emits: ['mouseenter', 'mouseout'],
    props: ['slide', 'currentSlide', 'index', 'direction', 'titleSlide', 'episodeNumber', 'movieContent'],
    computed: {
        transitionEffect() {
            return this.direction === 'right' ? 'slide-out' : 'slide-in';
        },
        // imageSource() {
        //     return `/img/${this.slide}`;
        // },
    },
};
</script>

<style scoped>
.carousel-item {
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
    transform: translateX(-100%);
}
.slide-in-leave-to {
    transform: translateX(100%);
}
.slide-out-enter-from {
    transform: translateX(100%);
}
.slide-out-leave-to {
    transform: translateX(-100%);
}

.general-intro {
    background: rgba(0, 0, 0, 0.6);
    border-radius: 15px;
    margin: 10px 10px 10px 0;
    padding: 14px 12px 9px 14px;
    max-width: 280px;
    position: absolute;
    /* z-index: 100; */
    bottom: 1.5em;
    left: 2.5em;
}

.general-intro h3 {
    color: #f1592a;
    font-size: 18px;
    font-weight: 700;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    text-align: center;
    margin-top: 0;
}

.general-intro .title2 {
    color: #fff;
    font-size: 13px;
    font-weight: 700;
    overflow: hidden;
    text-align: center;
    text-overflow: ellipsis;
    white-space: nowrap;
}

p {
    margin: 0 0 10px;
}

.general-intro .time {
    color: #6a3;
    font-size: 15px;
    font-weight: 700;
}

.general-intro .txt {
    color: #fff;
    font-size: 12px;
    font-weight: 400;
    max-height: 70px;
    overflow: hidden;
    text-align: center;
    max-width: 100%;
}
</style>
