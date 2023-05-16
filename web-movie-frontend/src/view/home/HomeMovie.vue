<template>
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
                        <a @click="btnShowDetailMovie(1)" class="viewall"
                            >Xem tất cả<MIcon nameIcon="arrow-next" :newClass="{ 'arrow-next': true }"></MIcon
                        ></a>
                    </div>
                    <div class="tabs">
                        <div class="tab-content">
                            <div class="tab">
                                <div class="flex-wrap-movielist">
                                    <template v-for="(seriesMovieItem, indexN) in serieMovieList" :key="indexN">
                                        <li
                                            class="movie-item-style-2 movie-item-style-1"
                                            @mouseover="showButtonPlay(indexN)"
                                            @mouseout="hideButtonPlay"
                                            @click="showMovieSingle"
                                        >
                                            <div @click="btnShowMovieSingle(seriesMovieItem.movieId)">
                                                <span class="label">{{
                                                    seriesMovieItem.newestEpisode.episodeName
                                                }}</span>
                                                <img
                                                    class="img-film"
                                                    :title="seriesMovieItem.movieName"
                                                    :alt="seriesMovieItem.movieName"
                                                    v-lazy="seriesMovieItem.imagePath"
                                                    style="
                                                        height: 260.96px;
                                                        width: 100% !important;
                                                        margin-bottom: 0;
                                                        border-radius: 0;
                                                    "
                                                />
                                                <div class="name">
                                                    <span
                                                        ><a>{{ seriesMovieItem.movieName }}</a></span
                                                    >
                                                </div>
                                                <a
                                                    id="play-video"
                                                    class="video-play-button"
                                                    :class="{
                                                        'show-button-play': enableShowBtnPlay['index'] === indexN,
                                                    }"
                                                    ref="btnPlay"
                                                >
                                                    <span></span>
                                                </a>
                                            </div>
                                        </li>
                                    </template>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="title-hd">
                        <h2>PHIM LẺ MỚI CẬP NHẬT</h2>
                        <a @click="btnShowDetailMovie(2)" class="viewall"
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
                            <div class="tab">
                                <div class="flex-wrap-movielist">
                                    <template v-for="(oddMovieItem, indexN) in oddMovieList" :key="indexN">
                                        <li
                                            class="movie-item-style-2 movie-item-style-1"
                                            @mouseover="showButtonPlay(indexN)"
                                            @mouseout="hideButtonPlay"
                                            @click="showMovieSingle"
                                        >
                                            <div @click="btnShowMovieSingle(oddMovieItem.movieId)">
                                                <span class="label">{{ oddMovieItem.newestEpisode.episodeName }}</span>
                                                <img
                                                    class="img-film"
                                                    :title="oddMovieItem.movieName"
                                                    :alt="oddMovieItem.movieName"
                                                    v-lazy="oddMovieItem.imagePath"
                                                    style="
                                                        height: 260.96px;
                                                        width: 100% !important;
                                                        margin-bottom: 0;
                                                        border-radius: 0;
                                                    "
                                                />
                                                <div class="name">
                                                    <span
                                                        ><a>{{ oddMovieItem.movieName }}</a></span
                                                    >
                                                </div>
                                                <a
                                                    id="play-video"
                                                    class="video-play-button"
                                                    :class="{
                                                        'show-button-play': enableShowBtnPlay['index'] === indexN,
                                                    }"
                                                    ref="btnPlay"
                                                >
                                                    <span></span>
                                                </a>
                                            </div>
                                        </li>
                                    </template>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="sidebar">
                        <!-- <div class="ads">
                            <img v-lazy="images/uploads/ads1.png" alt="" width="336" height="296" />
                        </div> -->
                        <div class="celebrities">
                            <h4 class="sb-title" style="text-align: center">BẢNG XẾP HẠNG</h4>
                            <ul class="tab-links-2">
                                <template v-for="col in dataSortReview" :key="col">
                                    <li
                                        @click="btnChooseValueFilter(col.valueSort)"
                                        :class="{ active: filterAndSortReview == col.valueSort }"
                                        style="cursor: pointer"
                                    >
                                        <a>{{ col.titleSort }}</a>
                                    </li>
                                </template>
                                <!-- <li class="active"><a >Tuần</a></li>
                                <li><a >Tháng</a></li> -->
                            </ul>
                            <template v-for="(movieMostViewItem, indexMostView) in listMostView" :key="indexMostView">
                                <div class="celeb-item" @click="btnShowMovieSingle(movieMostViewItem.movieId)">
                                    <span class="number-rank absolute">{{ indexMostView + 1 }}</span>
                                    <a
                                        ><img
                                            v-lazy="movieMostViewItem.imagePath"
                                            alt=""
                                            style="width: 70px !important; height: 70px !important"
                                    /></a>
                                    <div class="celeb-author">
                                        <h6 style="margin-bottom: 8px; cursor: pointer">
                                            <a>{{ movieMostViewItem.movieName }}</a>
                                        </h6>
                                        <span style="font-style: italic; font-size: 12px"
                                            >{{ movieMostViewItem.movieReview }} lượt xem</span
                                        >
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
                        <!-- <a href="#" class="viewall"
                            >Xem tất cả <MIcon nameIcon="arrow-next" :newClass="{ 'arrow-next': true }"></MIcon
                        ></a> -->
                    </div>
                    <CarouselVideo
                        :videoSlides="videoSlides"
                        controls
                        indicators
                        :interval="1000000000"
                    ></CarouselVideo>
                </div>
            </div>
        </div>
    </div>
    <MLoadingClient v-if="showLoadingClient"></MLoadingClient>
</template>

<script>
import CarouselSlide from '@/components/carousel/Carousel.vue';
import CarouselVideo from '@/components/carousel/CarouselVideo.vue';

import axios from 'axios';

// import commonJS from '@/js/common';
// import lazyload from '@/js/lazyload';
export default {
    name: 'HomeMovie',
    components: { CarouselSlide, CarouselVideo },
    watch: {
        '$route.params.id': function (value) {
            console.log('lay duoc id thay doi: ', value);
            this.idUser = value;
            this.$emit('getID', value);
        },
        filterAndSortReview: function (value) {
            if (value) {
                this.callApiSortReview();
            }
        },
    },
    methods: {
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

        callApiMoviesHot() {
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
                    this.showLoadingClient = false;
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        messageToast: err,
                        showToastMessage: true,
                        typeToast: 'errorToast',
                    });
                });
        },

        callApiGetTrailer() {
            axios
                .get(`${this.$MResource.API}/Trailers`)
                .then((response) => {
                    this.videoSlides = response.data;
                })
                .catch((err) => {
                    this.showLoadingClient = false;
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        messageToast: err,
                        showToastMessage: true,
                        typeToast: 'errorToast',
                    });
                });
        },

        callApiSeriesMovie() {
            axios
                .get(
                    `${this.$MResource.API}/Movies/GetAllMovieByTypeAndFilter?pageNumber=1&pageSize=9&typeMovie=1&columnFilter=2`,
                )
                .then((response) => {
                    this.serieMovieList = response.data.data;
                    this.serieMovieList.forEach((movieItem) => {
                        if (movieItem.imgByte != null && movieItem.imgByte.length > 0) {
                            movieItem.imagePath = 'data:image/png;base64,' + movieItem.imgByte;
                        }
                    });
                })
                .catch((err) => {
                    this.showLoadingClient = false;
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        messageToast: err,
                        showToastMessage: true,
                        typeToast: 'errorToast',
                    });
                });
        },
        callApiOddMovie() {
            axios
                .get(
                    `${this.$MResource.API}/Movies/GetAllMovieByTypeAndFilter?pageNumber=1&pageSize=9&typeMovie=2&columnFilter=2`,
                )
                .then((response) => {
                    this.oddMovieList = response.data.data;
                    this.oddMovieList.forEach((movieItem) => {
                        if (movieItem.imgByte != null && movieItem.imgByte.length > 0) {
                            movieItem.imagePath = 'data:image/png;base64,' + movieItem.imgByte;
                        }
                    });
                })
                .catch((err) => {
                    this.showLoadingClient = false;
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        messageToast: err,
                        showToastMessage: true,
                        typeToast: 'errorToast',
                    });
                });
        },

        callApiSortReview() {
            axios
                .get(
                    `${this.$MResource.API}/Movies/GetAllMovieByTypeAndFilter?pageNumber=1&pageSize=5&columnFilter=4&columnSort=3&filterAndSortReview=${this.filterAndSortReview}`,
                )
                .then((response) => {
                    this.listMostView = response.data.data;
                    this.listMostView.forEach((movieItem) => {
                        if (movieItem.imgByte != null && movieItem.imgByte.length > 0) {
                            movieItem.imagePath = 'data:image/png;base64,' + movieItem.imgByte;
                        }
                    });
                })
                .catch((err) => {
                    this.showLoadingClient = false;
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        messageToast: err,
                        showToastMessage: true,
                        typeToast: 'errorToast',
                    });
                });
        },

        btnShowMovieSingle(idMovie) {
            this.$router.push({ name: 'MovieSingle', params: { id: idMovie } });
        },

        btnChooseValueFilter(valuesort) {
            this.filterAndSortReview = valuesort;
        },

        btnShowDetailMovie(index) {
            this.$router.push({ name: 'moviegrid', params: { id: index } });
        },
    },

    created() {
        setTimeout(() => {
            this.showLoadingClient = false;
            //Lấy danh sách phim hot
            this.callApiMoviesHot();

            //Lấy danh sách phim lẻ mới cập nhật
            this.callApiOddMovie();

            //Lấy danh sách phim bộ mới cập nhật
            this.callApiSeriesMovie();

            //Lấy danh sách trailer mới nhất
            this.callApiGetTrailer();

            //Lấy danh sách bảng xếp hạng phim được xem nhiều nhất
            this.filterAndSortReview = 2;
            this.callApiSortReview();
        }, 1000);
    },

    data() {
        return {
            movieHot: [],

            typeOfMovie: null,
            limitListMovie: null,

            idUser: null,
            // photos: ['bannerMovie1.jpg', 'bannerMovie2.jpg'],

            videoSlides: [],
            // modules: [Navigation, Pagination, A11y],
            enableShowBtnPlay: {},
            showMovie: false,

            showLoadingClient: true,

            serieMovieList: [],

            oddMovieList: [],

            dataSortReview: [
                { valueSort: 1, titleSort: 'Ngày' },
                { valueSort: 2, titleSort: 'Tuần' },
                { valueSort: 3, titleSort: 'Tháng' },
            ],
            listMostView: [],
            filterAndSortReview: 1,
        };
    },
    // directives: {
    //     lazyload,
    // },
};
</script>

<style scoped>
@import url('./home.css');
</style>
