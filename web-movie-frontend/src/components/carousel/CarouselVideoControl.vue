<template>
    <button class="carousel-control up" @click="$emit('prev')">
        <MIcon
            nameIcon="arrow-next"
            styleSvg="width: 24px; height: 24px"
            :newClass="{ 'arrow-video': true, 'arrow-video-up': true }"
        ></MIcon>
    </button>
    <button class="carousel-control down" @click="$emit('next')">
        <MIcon
            nameIcon="arrow-next"
            styleSvg="width: 24px; height: 24px"
            :newClass="{ 'arrow-video': true, 'arrow-video-down': true }"
        ></MIcon>
    </button>
</template>

<script>
export default {
    name: 'CarouselVideoControl',
    emits: ['prev', 'next'],
};
</script>

<style scoped>
.carousel-control {
    /* background-color: rgba(0, 0, 0, 0.5); */
    background-color: #0b1a2a;
    border: none;
    /* display: inline-block; */
    position: absolute;
    height: 30px;
    /* width: 70px; */
    width: 100%;
    /* left: calc(50% - 25px); */
    display: flex;
    justify-content: center;
    align-items: start;
    color: #f3f3f3;
    cursor: pointer;
    z-index: 2;
}
.up {
    top: 0;
}
.down {
    bottom: 0;
}

.arrow-video {
    position: relative !important;
    background-color: transparent !important;
    color: #abb7c4;
}
.arrow-video-up {
    transform: rotateZ(-90deg);
}

.arrow-video-down {
    transform: rotateZ(90deg);
}

.arrow-video-up:hover,
.arrow-video-down:hover {
    color: #dcf836;
}

.arrow-video-up:focus,
.arrow-video-down:focus {
    color: #dcf836;
}
</style>
