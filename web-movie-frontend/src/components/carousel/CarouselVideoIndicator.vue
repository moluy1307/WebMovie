<template>
    <div
        class="item"
        v-for="(img, index) in imgVideo"
        :key="index"
        @click="btnSwitchSlide(index)"
        :class="{ active: currentIndex === index }"
        ref="itemSlideVideo"
        :style="yValue"
    >
        <div class="trailer-img">
            <img :src="img.trailerImg" alt="photo by Barn Images" width="4096" height="2737" />
        </div>
        <div class="trailer-infor">
            <h4 class="desc">Wonder Woman</h4>
            <p>2:30</p>
        </div>
    </div>
</template>

<script>
export default {
    name: 'CarouselVideoIndicator',
    emits: ['switch'],
    props: ['imgVideo', 'currentIndex', 'direction'],
    computed: {
        yValue() {
            return `transform: translateY(-${this.currentIndex * 87}px);`;
        },
    },
    methods: {
        btnSwitchSlide(index) {
            this.$emit('switch', index);
        },
    },
};
</script>

<style scoped>
.slider-nav-2 .item {
    display: flex;
    flex-direction: row;
    padding: 15px;
    transition: all 1s ease-in-out;
}

.slider-nav-2 .item .trailer-img {
    width: 100px !important;
    max-height: 57px;
    overflow: hidden;
}

.slider-nav-2 .item .trailer-img img {
    width: 100% !important;
}

.slider-nav-2 .item .trailer-infor {
    margin-left: 15px;
    padding-top: 3px;
    cursor: pointer;
    flex: 1;
}

.active {
    background-color: #1c3d5d;
}
</style>
