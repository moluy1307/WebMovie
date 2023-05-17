<template>
    <div class="container">
        <div style="margin-top: 36px">
            <div class="col-md-4 col-sm-12 col-xs-12">
                <div class="column-element actor-tablet">
                    <img style="width: 288px; height: 432px" v-lazy="actorInfor.imagePath" alt="" />
                    <p class="text-infor-title" style="font-size: 16px">Thông tin cá nhân</p>
                    <div class="column-element">
                        <p class="para-actor text-infor-title">Giới tính</p>
                        <p class="text-infor-detail">{{ actorInfor.genderName }}</p>
                    </div>
                    <div class="column-element">
                        <p class="para-actor text-infor-title">Ngày sinh</p>
                        <p class="text-infor-detail">{{ actorInfor.dateOfBirth }}</p>
                    </div>
                    <div class="column-element">
                        <p class="para-actor text-infor-title">Nơi sinh</p>
                        <p class="text-infor-detail">{{ actorInfor.country }}</p>
                    </div>
                </div>
            </div>
            <div class="col-md-8 col-sm-12 col-xs-12">
                <div class="column-element main-actor">
                    <p class="para-actor text-infor-title" style="font-size: 36px">{{ actorInfor.fullname }}</p>
                    <p class="para-actor text-infor-title" style="font-size: 16px">Tiểu sử</p>
                    <p>
                        {{ actorInfor.biography }}
                    </p>
                    <p class="para-actor text-infor-title" style="font-size: 16px">Các phim đã tham gia</p>
                    <div class="tab">
                        <div class="flex-wrap-movielist">
                            <template v-for="(movieItemGrid, indexN) in movieListByActor" :key="movieItemGrid">
                                <li
                                    class="movie-item-style-2 movie-item-style-1"
                                    @mouseover="showButtonPlay(indexN)"
                                    @mouseout="hideButtonPlay"
                                    @click="showMovieSingle"
                                >
                                    <div @click="btnShowMovieSingle(movieItemGrid.movieId)">
                                        <span class="label">{{ movieItemGrid.newestEpisode.episodeName }}</span>
                                        <span class="watcher"
                                            ><font-awesome-icon :icon="['fas', 'eye']" style="margin-right: 2px" />{{
                                                movieItemGrid.movieReview
                                            }}</span
                                        >
                                        <img
                                            class="img-film"
                                            :title="movieItemGrid.movieName"
                                            :alt="movieItemGrid.movieName"
                                            v-lazy="movieItemGrid.imagePath"
                                            style="
                                                height: 260.96px;
                                                width: 100% !important;
                                                margin-bottom: 0;
                                                border-radius: 0;
                                            "
                                        />
                                        <div class="name">
                                            <span
                                                ><a>{{ movieItemGrid.movieName }}</a></span
                                            >
                                        </div>
                                        <a
                                            id="play-video"
                                            class="video-play-button"
                                            :class="{ 'show-button-play': enableShowBtnPlay['index'] === indexN }"
                                            ref="btnPlay"
                                        >
                                            <span></span>
                                        </a>
                                    </div>
                                </li>
                            </template>
                        </div>
                        <div class="noData" v-if="noData">Không có dữ liệu</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <MLoadingClient v-if="showLoadingClient"></MLoadingClient>
</template>

<script>
import axios from 'axios';

import commonJS from '@/js/common';
export default {
    name: 'ActorSingle',
    watch: {
        movieListByActor: function (value) {
            if (value.length < 1) {
                this.noData = true;
            } else {
                this.noData = false;
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

        btnShowMovieSingle(idMovie) {
            this.$router.push({ name: 'MovieSingle', params: { id: idMovie } });
        },
    },
    created() {
        setTimeout(() => {
            this.showLoadingClient = false;
            axios
                .get(`${this.$MResource.API}/Actors/GetActorById?actorId=${this.$route.params.id}`)
                .then((response) => {
                    this.actorInfor = response.data;

                    if (this.actorInfor.imgByte != null && this.actorInfor.imgByte.length > 0) {
                        this.actorInfor.imagePath = 'data:image/png;base64,' + this.actorInfor.imgByte;
                        // this.file = 'data:image/png;base64,' + this.movieInfor.imgByte;
                    }

                    if (this.actorInfor.dateOfBirth != null || this.actorInfor.dateOfBirth != undefined) {
                        this.actorInfor.dateOfBirth = commonJS.formatDate(this.actorInfor.dateOfBirth);
                    }
                })
                .catch((err) => {
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        messageToast: err,
                        showToastMessage: true,
                        typeToast: 'errorToast',
                    });
                });
            axios
                .get(
                    `${this.$MResource.API}/Movies/GetAllMovieByTypeAndFilter?pageNumber=1&pageSize=1000&categoryId=${this.$route.params.id}&columnFilter=5`,
                )
                .then((response) => {
                    this.movieListByActor = response.data.data;

                    this.movieListByActor.forEach((movieRelatedItem) => {
                        if (movieRelatedItem.imgByte != null && movieRelatedItem.imgByte.length > 0) {
                            movieRelatedItem.imagePath = 'data:image/png;base64,' + movieRelatedItem.imgByte;
                            // this.file = 'data:image/png;base64,' + this.movieInfor.imgByte;
                        }
                        if (
                            movieRelatedItem.dateOfManufactor != null ||
                            movieRelatedItem.dateOfManufactor != undefined
                        ) {
                            movieRelatedItem.dateOfManufactor = commonJS.formatDate(movieRelatedItem.dateOfManufactor);
                        }
                    });
                })
                .catch((err) => {
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        messageToast: err,
                        showToastMessage: true,
                        typeToast: 'errorToast',
                    });
                    console.log('err: ', err);
                });
        }, 1000);
    },
    data() {
        return {
            actorInfor: {},
            movieListByActor: [],

            enableShowBtnPlay: {},

            showLoadingClient: true,
        };
    },
};
</script>

<style scoped>
@import url('./actor.css');

.noData {
    margin-bottom: 30px;
    /* font-size: 14px; */
    color: #abb7c4;
}
</style>
