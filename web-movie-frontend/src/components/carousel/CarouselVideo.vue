<template>
    <div class="videos">
        <div class="slider-for-2 video-ft">
            <div class="carousel-video">
                <div class="carousel-inner-video">
                    <!-- <div >
                        <iframe class="item-video" :src="vid"></iframe>
                    </div> -->
                    <CarouselVideoItem
                        v-for="(vid, indexVid) in videoSlides"
                        :key="`item-${indexVid}`"
                        :videoSlide="vid.trailerUrl"
                        :index="indexVid"
                        :current-slide="currentSlide"
                        :direction="direction"
                    ></CarouselVideoItem>
                </div>
            </div>
        </div>
        <div class="slider-nav-2 thumb-ft">
            <CarouselVideoControl v-if="controls" @prev="prev" @next="next"></CarouselVideoControl>
            <CarouselVideoIndicator
                v-if="indicators"
                :imgVideo="videoSlides"
                :current-index="currentSlide"
                @switch="switchSlide($event)"
                :direction="direction"
            ></CarouselVideoIndicator>
            <!-- <div class="item">
                <div class="trailer-img">
                    <img
                        src="https://fastly.picsum.photos/id/4/5000/3333.jpg?hmac=ghf06FdmgiD0-G4c9DdNM8RnBIN7BO0-ZGEw47khHP4"
                        alt="photo by Barn Images"
                        width="4096"
                        height="2737"
                    />
                </div>
                <div class="trailer-infor">
                    <h4 class="desc">Wonder Woman</h4>
                    <p>2:30</p>
                </div>
            </div>
            <div class="item">
                <div class="trailer-img">
                    <img
                        src="https://fastly.picsum.photos/id/9/5000/3269.jpg?hmac=cZKbaLeduq7rNB8X-bigYO8bvPIWtT-mh8GRXtU3vPc"
                        alt="photo by Barn Images"
                        width="350"
                        height="200"
                    />
                </div>
                <div class="trailer-infor">
                    <h4 class="desc">Oblivion: Official Teaser Trailer</h4>
                    <p>2:37</p>
                </div>
            </div>
            <div class="item">
                <div class="trailer-img">
                    <img
                        src="https://fastly.picsum.photos/id/13/2500/1667.jpg?hmac=SoX9UoHhN8HyklRA4A3vcCWJMVtiBXUg0W4ljWTor7s"
                        alt="photo by Joshua Earle"
                    />
                </div>
                <div class="trailer-infor">
                    <h4 class="desc">Exclusive Interview: Skull Island</h4>
                    <p>2:44</p>
                </div>
            </div>
            <div class="item">
                <div class="trailer-img">
                    <img
                        src="https://fastly.picsum.photos/id/4/5000/3333.jpg?hmac=ghf06FdmgiD0-G4c9DdNM8RnBIN7BO0-ZGEw47khHP4"
                        alt="photo by Alexander Dimitrov"
                        width="100"
                        height="56"
                    />
                </div>
                <div class="trailer-infor">
                    <h4 class="desc">Logan: Director James Mangold Interview</h4>
                    <p>2:43</p>
                </div>
            </div>
            <div class="item">
                <div class="trailer-img">
                    <img
                        src="https://fastly.picsum.photos/id/4/5000/3333.jpg?hmac=ghf06FdmgiD0-G4c9DdNM8RnBIN7BO0-ZGEw47khHP4"
                        alt="photo by Wojciech Szaturski"
                        width="100"
                        height="56"
                    />
                </div>
                <div class="trailer-infor">
                    <h4 class="desc">Beauty and the Beast: Official Teaser Trailer 2</h4>
                    <p>2: 32</p>
                </div>
            </div>
            <div class="item">
                <div class="trailer-img">
                    <img
                        src="https://fastly.picsum.photos/id/4/5000/3333.jpg?hmac=ghf06FdmgiD0-G4c9DdNM8RnBIN7BO0-ZGEw47khHP4"
                        alt="photo by Wojciech Szaturski"
                        width="360"
                        height="189"
                    />
                </div>
                <div class="trailer-infor">
                    <h4 class="desc">Fast&Furious 8</h4>
                    <p>3:11</p>
                </div>
            </div> -->
        </div>
    </div>
</template>

<script>
import CarouselVideoItem from './CarouselVideoItem.vue';
import CarouselVideoControl from './CarouselVideoControl.vue';
import CarouselVideoIndicator from './CarouselVideoIndicator.vue';
export default {
    name: 'CarouselVideo',
    components: { CarouselVideoItem, CarouselVideoControl, CarouselVideoIndicator },
    props: {
        videoSlides: {
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
            const index = this.currentSlide > 0 ? this.currentSlide + step : this.videoSlides.length - 1;
            this.setCurrentSlide(index);
            this.direction = 'left';
            this.startSlideTimer();
            console.log('gia tri current prev: ', this.currentSlide);
        },
        _next(step = 1) {
            const index = this.currentSlide < this.videoSlides.length - 1 ? this.currentSlide + step : 0;
            this.setCurrentSlide(index);
            this.direction = 'right';
        },
        next(step = 1) {
            this._next(step);
            this.startSlideTimer();
            console.log('gia tri current: ', this.currentSlide);
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
.carousel-video {
    display: flex;
    justify-content: center;
}

.carousel-inner-video {
    position: relative;
    /* width: 900px;
    height: 400px; */
    width: 100%;
    height: 435px;
    overflow: hidden;
}
</style>
