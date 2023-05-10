<template>
    <!-- <Swiper
        :slides-per-view="1"
        :key="photo"
        :module="modules"
        navigation
        :pagination="{ clickable: true }"
        @swiper="onSwiper"
        @slideChange="onSlideChange"
        grab-cursor
    >
        <SwiperSlide v-for="photo in photos" :key="photo">
            <img :src="`${photo}`" alt="" />
        </SwiperSlide>
    </Swiper> -->

    <div class="container carousel-banner">
        <CarouselSlide
            @transferMovieHotId="getMovieHotId"
            :slides="movieHot"
            :interval="3000"
            controls
            indicators
        ></CarouselSlide>
    </div>
    <div class="movie-items">
        <div class="container">
            <div class="row ipad-width">
                <div class="col-md-8">
                    <div class="title-hd">
                        <h2>Phim bộ mới cập nhật</h2>
                        <a href="#" class="viewall"
                            >Xem tất cả<MIcon nameIcon="arrow-nextMIcon" :newClass="{ 'arrow-next': true }"></MIcon
                        ></a>
                    </div>
                    <div class="tabs">
                        <!-- <ul class="tab-links">
                            <li class="active"><a href="#tab1">#Popular</a></li>
                            <li><a href="#tab2"> #Coming soon</a></li>
                            <li><a href="#tab3"> #Top rated </a></li>
                            <li><a href="#tab4"> #Most reviewed</a></li>
                        </ul> -->
                        <div class="tab-content">
                            <div id="tab1" class="tab active tab-item-film">
                                <template v-for="(n, indexN) in 8" :key="indexN">
                                    <li
                                        class="item"
                                        @mouseover="showButtonPlay(indexN)"
                                        @mouseout="hideButtonPlay"
                                        @click="showMovieSingle"
                                    >
                                        <router-link :to="{ name: 'MovieSingle', params: { id: indexN } }">
                                            <span class="label">Tập 1 Vietsub</span>
                                            <img
                                                class="img-film"
                                                data-original="https://mp-focus-opensocial.googleusercontent.com/gadgets/proxy?container=focus&amp;refresh=604800&amp;url=https%3A%2F%2Fi0.wp.com%2Fimage.mpapis.xyz%2Fposter%2Fthanh-guom-diet-quy-phan-lang-ren-kiem-11155.jpg%3F1681151349"
                                                title="Thanh Gươm Diệt Quỷ: Phần Làng Rèn Kiếm"
                                                alt="Thanh Gươm Diệt Quỷ: Phần Làng Rèn Kiếm"
                                                src="https://mp-focus-opensocial.googleusercontent.com/gadgets/proxy?container=focus&amp;refresh=604800&amp;url=https%3A%2F%2Fi0.wp.com%2Fimage.mpapis.xyz%2Fposter%2Fthanh-guom-diet-quy-phan-lang-ren-kiem-11155.jpg%3F1681151349"
                                                style="height: 227.2px; width: 100% !important"
                                            />
                                            <div class="name">
                                                <span
                                                    ><a
                                                        href="/phim/thanh-guom-diet-quy-phan-lang-ren-kiem-11163.html"
                                                        title="Thanh Gươm Diệt Quỷ: Phần Làng Rèn Kiếm 2023"
                                                        >Thanh Gươm Diệt Quỷ: Phần Làng Rèn Kiếm 2023</a
                                                    ></span
                                                >
                                            </div>
                                            <a
                                                id="play-video"
                                                class="video-play-button"
                                                href="#"
                                                :class="{ 'show-button-play': enableShowBtnPlay['index'] === indexN }"
                                                ref="btnPlay"
                                            >
                                                <span></span>
                                            </a>
                                        </router-link>
                                    </li>
                                </template>
                            </div>
                        </div>
                    </div>
                    <div class="title-hd">
                        <h2>PHIM LẺ MỚI CẬP NHẬT</h2>
                        <a href="#" class="viewall"
                            >XEM TẤT CẢ<MIcon nameIcon="arrow-next" :newClass="{ 'arrow-next': true }"></MIcon
                        ></a>
                    </div>
                    <div class="tabs">
                        <!-- <ul class="tab-links-2">
                            <li><a href="#tab21">#Popular</a></li>
                            <li class="active"><a href="#tab22"> #Coming soon</a></li>
                            <li><a href="#tab23"> #Top rated </a></li>
                            <li><a href="#tab24"> #Most reviewed</a></li>
                        </ul> -->
                        <div class="tab-content">
                            <div id="tab1" class="tab active tab-item-film">
                                <template v-for="n in 8" :key="n">
                                    <li class="item">
                                        <span class="label">Tập 1 Vietsub</span
                                        ><a
                                            href="/phim/thanh-guom-diet-quy-phan-lang-ren-kiem-11163.html"
                                            title="Thanh Gươm Diệt Quỷ: Phần Làng Rèn Kiếm"
                                            ><img
                                                class="img-film"
                                                data-original="https://mp-focus-opensocial.googleusercontent.com/gadgets/proxy?container=focus&amp;refresh=604800&amp;url=https%3A%2F%2Fi0.wp.com%2Fimage.mpapis.xyz%2Fposter%2Fthanh-guom-diet-quy-phan-lang-ren-kiem-11155.jpg%3F1681151349"
                                                title="Thanh Gươm Diệt Quỷ: Phần Làng Rèn Kiếm"
                                                alt="Thanh Gươm Diệt Quỷ: Phần Làng Rèn Kiếm"
                                                src="https://mp-focus-opensocial.googleusercontent.com/gadgets/proxy?container=focus&amp;refresh=604800&amp;url=https%3A%2F%2Fi0.wp.com%2Fimage.mpapis.xyz%2Fposter%2Fthanh-guom-diet-quy-phan-lang-ren-kiem-11155.jpg%3F1681151349"
                                                style="height: 227.2px; width: 100% !important" /><i
                                                class="icon-play"
                                            ></i
                                        ></a>
                                        <div class="name">
                                            <span
                                                ><a
                                                    href="/phim/thanh-guom-diet-quy-phan-lang-ren-kiem-11163.html"
                                                    title="Thanh Gươm Diệt Quỷ: Phần Làng Rèn Kiếm 2023"
                                                    >Thanh Gươm Diệt Quỷ: Phần Làng Rèn Kiếm 2023</a
                                                ></span
                                            >
                                        </div>
                                    </li>
                                </template>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="sidebar">
                        <!-- <div class="ads">
                            <img src="images/uploads/ads1.png" alt="" width="336" height="296" />
                        </div> -->
                        <div class="celebrities">
                            <h4 class="sb-title" style="text-align: center">BẢNG XẾP HẠNG</h4>
                            <ul class="tab-links-2">
                                <li><a href="#tab21">Ngày</a></li>
                                <li class="active"><a href="#tab22">Tuần</a></li>
                                <li><a href="#tab23">Tháng</a></li>
                            </ul>
                            <template v-for="n in 5" :key="n">
                                <div class="celeb-item">
                                    <span class="number-rank absolute">{{ n }}</span>
                                    <a href="#"
                                        ><img
                                            src="../../assets/img/poster1.jpg"
                                            alt=""
                                            style="width: 70px !important; height: 70px !important"
                                    /></a>
                                    <div class="celeb-author">
                                        <h6 style="margin-bottom: 8px"><a href="#">Guardian Of The Galaxy</a></h6>
                                        <span style="font-style: italic; font-size: 12px">1002 lượt xem</span>
                                    </div>
                                </div>
                            </template>
                            <!-- <a href="#" class="btn">See all celebrities<i class="ion-ios-arrow-right"></i></a> -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="trailers">
        <div class="container">
            <div class="row ipad-width">
                <div class="col-md-12">
                    <div class="title-hd">
                        <h2>Trailer phim mới</h2>
                        <a href="#" class="viewall"
                            >Xem tất cả <MIcon nameIcon="arrow-next" :newClass="{ 'arrow-next': true }"></MIcon
                        ></a>
                    </div>
                    <CarouselVideo :videoSlides="videoSlides" controls indicators></CarouselVideo>
                </div>
            </div>
        </div>
    </div>
    <!-- <MovieSingle v-if="showMovie"></MovieSingle> -->
</template>

<script>
// import { Swiper, SwiperSlide } from 'swiper/vue';
// import { Navigation, Pagination, A11y } from 'swiper';
// import 'swiper/css';
// import 'swiper/css/navigation';
// import 'swiper/css/pagination';
import CarouselSlide from '@/components/carousel/Carousel.vue';
import CarouselVideo from '@/components/carousel/CarouselVideo.vue';
// import MovieSingle from '../movieSingle/MovieSingle.vue';

import axios from 'axios';

// import commonJS from '@/js/common';
export default {
    name: 'HomeMovie',
    // components: {
    //     Swiper,
    //     SwiperSlide,
    // },
    components: { CarouselSlide, CarouselVideo },
    methods: {
        onSwiper(swiper) {
            console.log(swiper);
        },
        onSlideChange() {
            console.log('slide change');
        },
        showButtonPlay(index) {
            // this.$refs.btnPlay[index].classList;
            this.enableShowBtnPlay = { ...this.enableShowBtnPlay, index };
        },
        hideButtonPlay() {
            this.enableShowBtnPlay = {};
        },

        getMovieHotId(idHot) {
            console.log('nhan id truyen: ', idHot);
            this.$router.push({ name: 'MovieSingle', params: { id: idHot } });
        },
        // showMovieSingle() {
        //     this.showMovie = true;
        //     console.log('show: ', this.showMovie);
        // },
    },
    watch: {
        '$route.params.id': function (value) {
            console.log('lay duoc id thay doi: ', value);
            this.idUser = value;
            this.$emit('getID', value);
        },
    },

    created() {
        //Lấy danh sách phim hot
        axios
            .get(
                `${this.$MResource.API}/Movies/GetAllMovieByTypeAndFilter?pageNumber=1&pageSize=1000&typeMovie=4&columnFilter=2`,
            )
            .then((response) => {
                this.movieHot = response.data.data;

                // if (me.movieInfor.dateOfManufactor != null || me.movieInfor.dateOfManufactor != undefined) {
                //     me.movieInfor.dateOfManufactor = commonJS.bindingFormatDate(me.movieInfor.dateOfManufactor);
                // }
                this.movieHot.forEach((movieItem) => {
                    if (movieItem.imgByte != null && movieItem.imgByte.length > 0) {
                        movieItem.imagePath = 'data:image/png;base64,' + movieItem.imgByte;
                    }
                });
                // if (this.movieInfor.categories != null) {
                //     this.movieInfor.categories.forEach((element) => {
                //         this.valueCategory.push(element.value);
                //     });
                // }
            })
            .catch((err) => {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            });
    },
    data() {
        return {
            movieHot: [],

            typeOfMovie: null,
            limitListMovie: null,

            idUser: null,
            // photos: ['bannerMovie1.jpg', 'bannerMovie2.jpg'],
            slides: [
                {
                    titleSlide: `'Birdie Wing: Golf Girls' Story Season 2'`,
                    episodeNumber: '1/?? tập',
                    movieContent: `'Mùa thứ 2 của Series anime Birdie Wing: Golf Girls' Story'`,
                    inlustratingImages:
                        'https://fastly.picsum.photos/id/4/5000/3333.jpg?hmac=ghf06FdmgiD0-G4c9DdNM8RnBIN7BO0-ZGEw47khHP4',
                },
                {
                    titleSlide: `KANOJO GA KOUSHAKU-TEI NI ITTA RIYUU`,
                    episodeNumber: '1/?? tập',
                    movieContent: `Một học sinh trung học lưu ban, đã chết mà không rõ lý do.
Sau đó, cô chiếm hữu các nhân vật trong tiểu thuyết,
Tuy nhiên, đối tượng bị chiếm hữu không phải là nhân vật chính, mà là một nhân vật phụ sắp chết!`,
                    inlustratingImages:
                        'https://fastly.picsum.photos/id/9/5000/3269.jpg?hmac=cZKbaLeduq7rNB8X-bigYO8bvPIWtT-mh8GRXtU3vPc',
                },
                {
                    titleSlide: `'Birdie Wing: Golf Girls' Story Season 2'`,
                    episodeNumber: '1/?? tập',
                    movieContent: `'Mùa thứ 2 của Series anime Birdie Wing: Golf Girls' Story'`,
                    inlustratingImages:
                        'https://fastly.picsum.photos/id/13/2500/1667.jpg?hmac=SoX9UoHhN8HyklRA4A3vcCWJMVtiBXUg0W4ljWTor7s',
                },
                {
                    titleSlide: `'Birdie Wing: Golf Girls' Story Season 2'`,
                    episodeNumber: '1/?? tập',
                    movieContent: `'Mùa thứ 2 của Series anime Birdie Wing: Golf Girls' Story'`,
                    inlustratingImages:
                        'https://fastly.picsum.photos/id/15/2500/1667.jpg?hmac=Lv03D1Y3AsZ9L2tMMC1KQZekBVaQSDc1waqJ54IHvo4',
                },
            ],
            videoSlides: [
                {
                    trailerVideo: 'https://www.youtube.com/embed/1Q8fG0TtVAY',
                    trailerImg:
                        'https://fastly.picsum.photos/id/9/5000/3269.jpg?hmac=cZKbaLeduq7rNB8X-bigYO8bvPIWtT-mh8GRXtU3vPc',
                },
                {
                    trailerVideo: 'https://www.youtube.com/embed/w0qQkSuWOS8',
                    trailerImg:
                        'https://fastly.picsum.photos/id/4/5000/3333.jpg?hmac=ghf06FdmgiD0-G4c9DdNM8RnBIN7BO0-ZGEw47khHP4',
                },

                {
                    trailerVideo: 'https://www.youtube.com/embed/44LdLqgOpjo',
                    trailerImg:
                        'https://fastly.picsum.photos/id/13/2500/1667.jpg?hmac=SoX9UoHhN8HyklRA4A3vcCWJMVtiBXUg0W4ljWTor7s',
                },

                {
                    trailerVideo: 'https://www.youtube.com/embed/gbug3zTm3Ws',
                    trailerImg:
                        'https://fastly.picsum.photos/id/4/5000/3333.jpg?hmac=ghf06FdmgiD0-G4c9DdNM8RnBIN7BO0-ZGEw47khHP4',
                },

                {
                    trailerVideo: 'https://www.youtube.com/embed/NxhEZG0k9_w',
                    trailerImg:
                        'https://fastly.picsum.photos/id/4/5000/3333.jpg?hmac=ghf06FdmgiD0-G4c9DdNM8RnBIN7BO0-ZGEw47khHP4',
                },

                {
                    trailerVideo: 'https://www.youtube.com/embed/e3Nl_TCQXuw',
                    trailerImg:
                        'https://fastly.picsum.photos/id/15/2500/1667.jpg?hmac=Lv03D1Y3AsZ9L2tMMC1KQZekBVaQSDc1waqJ54IHvo4',
                },
            ],
            // modules: [Navigation, Pagination, A11y],
            enableShowBtnPlay: {},
            showMovie: false,
        };
    },
};
</script>

<style scoped>
@import url('./home.css');
</style>
