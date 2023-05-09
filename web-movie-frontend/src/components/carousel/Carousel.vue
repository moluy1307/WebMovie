<template>
    <div class="carousel">
        <div class="carousel-inner">
            <!-- <div class="bottom-carousel">
                <div class="general-intro">
                    <h3>Birdie Wing: Golf Girls' Story Season 2</h3>
                    <p class="title2"></p>
                    <p class="time">01/?? Tập</p>
                    <p class="txt">Mùa thứ 2 của Series anime Birdie Wing: Golf Girls' Story</p>
                </div>
                <carousel-indicators
                    v-if="indicators"
                    :total="slides.length"
                    :current-index="currentSlide"
                    @switch="switchSlide($event)"
                ></carousel-indicators>
            </div> -->
            <carousel-indicators
                v-if="indicators"
                :total="slides.length"
                :current-index="currentSlide"
                @switch="switchSlide($event)"
            ></carousel-indicators>
            <carousel-item
                v-for="(slide, index) in slides"
                :slide="slide.imagePath"
                :key="`item-${index}`"
                :current-slide="currentSlide"
                :index="index"
                :direction="direction"
                @mouseenter="stopSlideTimer"
                @mouseout="startSlideTimer"
                :titleSlide="slide.movieName"
                :episodeNumber="slide.newestEpisode.episodeName"
                :movieContent="slide.movieInfor"
                @click="$emit('transferMovieHotId', slide.movieId)"
            ></carousel-item>
            <carousel-controls v-if="controls" @prev="prev" @next="next"></carousel-controls>
        </div>
    </div>
</template>

<script>
import CarouselItem from './CarouselItem.vue';
import CarouselControls from './CarouselControls.vue';
import CarouselIndicators from './CarouselIndicators.vue';
export default {
    name: 'CarouselSlide',
    components: { CarouselItem, CarouselControls, CarouselIndicators },
    props: {
        slides: {
            type: Array,
            required: true,
        },
        controls: {
            type: Boolean,
            default: false,
        },
        indicators: {
            type: Boolean,
            default: false,
        },
        interval: {
            type: Number,
            default: 5000,
        },
    },
    methods: {
        setCurrentSlide(index) {
            this.currentSlide = index;
        },
        prev(step = -1) {
            const index = this.currentSlide > 0 ? this.currentSlide + step : this.slides.length - 1;
            this.setCurrentSlide(index);
            this.direction = 'left';
            this.startSlideTimer();
        },
        _next(step = 1) {
            const index = this.currentSlide < this.slides.length - 1 ? this.currentSlide + step : 0;
            this.setCurrentSlide(index);
            this.direction = 'right';
        },
        next(step = 1) {
            this._next(step);
            this.startSlideTimer();
        },
        startSlideTimer() {
            this.stopSlideTimer();
            this.slideInterval = setInterval(() => {
                this._next();
            }, this.interval);
        },
        stopSlideTimer() {
            clearInterval(this.slideInterval);
        },
        switchSlide(index) {
            const step = index - this.currentSlide;
            if (step > 0) {
                this.next(step);
            } else {
                this.prev(step);
            }
        },
    },
    mounted() {
        this.startSlideTimer();
    },
    beforeUnmount() {
        this.stopSlideTimer();
    },
    data() {
        return {
            currentSlide: 0,
            slideInterval: null,
            direction: 'right',
        };
    },
};
</script>

<style scoped>
.carousel {
    display: flex;
    justify-content: center;
}
.carousel-inner {
    position: relative;
    width: 900px;
    height: 400px;
    overflow: hidden;
    cursor: pointer;
}

/*.bottom-carousel {
    display: flex;
    justify-content: space-around;
    align-items: end;
    position: absolute;
    bottom: 1.5em;
    z-index: 2;
    width: 100%;
}

 .general-intro {
    background: rgba(0, 0, 0, 0.6);
    border-radius: 15px;
    margin: 10px 10px 10px 0;
    padding: 14px 12px 9px 14px;
    max-width: 280px;
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
} */
</style>
