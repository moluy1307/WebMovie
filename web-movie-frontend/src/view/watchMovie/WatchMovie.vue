<template>
    <ArtplayerMovie @get-instance="getInstance" :option="option" :style="style" :key="watchVideo" />
    <div class="container">
        <div class="movie-single">
            <div class="movie-single-ct main-content">
                <div class="rate-star">
                    <p style="font-size: 18px">Tổng hợp</p>
                </div>
                <div class="movie-rate box-list-episode">
                    <!-- <div class="rate">
                        <font-awesome-icon :icon="['fas', 'star']" style="color: #f5b50a; font-size: 24px" />
                        <p>
                            <span>8.1</span> /10<br />
                            <span class="rv">56 lượt đánh giá</span>
                        </p>
                    </div>
                    <div class="rate-star">
                        <p>Đánh giá:</p>
                        <template v-for="n in 10" :key="n"
                            ><font-awesome-icon :icon="['fas', 'star']" style="color: #f5b50a; font-size: 24px"
                        /></template>
                    </div> -->
                    <template v-for="(itemEp, indexEp) in movieInfor.episodes" :key="indexEp">
                        <div class="list-episode" @click="btnChooseEpisode(itemEp.episodeUrl)">
                            Tập {{ itemEp.episodeNumber }}
                        </div>
                    </template>
                </div>
                <div class="movie-tabs">
                    <div class="tabs">
                        <ul class="tab-links tabs-mv">
                            <li>
                                <router-link
                                    :to="{
                                        name: 'detailescript',
                                        params: { id: this.$route.params.id },
                                    }"
                                    >Tổng quan</router-link
                                >
                            </li>
                            <li>
                                <router-link
                                    :to="{
                                        name: 'detailreview',
                                        params: { id: this.$route.params.id },
                                    }"
                                    >Bình luận</router-link
                                >
                            </li>
                            <li>
                                <router-link
                                    :to="{
                                        name: 'detailmedia',
                                        params: { id: this.$route.params.id },
                                    }"
                                    >Trailers</router-link
                                >
                            </li>
                            <li>
                                <router-link
                                    :to="{
                                        name: 'detailrelated',
                                        params: { id: this.$route.params.id },
                                    }"
                                    >Phim tương tự</router-link
                                >
                            </li>
                        </ul>
                        <div class="tab-content">
                            <router-view></router-view>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import ArtplayerMovie from './Artplayer.vue';

import axios from 'axios';
export default {
    name: 'WatchMovie',
    components: {
        ArtplayerMovie,
    },
    watch: {
        'option.url': function (value) {
            if (value) {
                this.watchVideo++;
            }
        },
    },
    methods: {
        getInstance(art) {
            console.info(art);
        },

        btnChooseEpisode(urlEpisode) {
            this.option.url = urlEpisode;
        },
    },
    created() {
        var me = this;
        axios
            .get(`${this.$MResource.API}/Movies/GetMovieById?movieId=${this.$route.params.id}`)
            .then((response) => {
                me.movieInfor = response.data;
                console.log('chi tiet: ', this.movieInfor);
            })
            .catch((err) => {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            });

        console.log('router: ', this.$route.params.id);
    },
    data() {
        return {
            option: {
                url: '',
                volume: 0.5,
                isLive: false,
                muted: false,
                autoplay: false,
                pip: true,
                autoSize: true,
                // autoMini: true,
                screenshot: true,
                setting: true,
                loop: true,
                playbackRate: true,
                aspectRatio: true,
                fullscreen: true,
                fullscreenWeb: true,
                subtitleOffset: true,
                miniProgressBar: true,
                mutex: true,
                backdrop: true,
                playsInline: true,
                autoPlayback: true,
                airplay: true,
                lang: navigator.language.toLowerCase(),
                whitelist: ['*'],
                moreVideoAttr: {
                    crossOrigin: 'anonymous',
                },
                // quality: [
                //     {
                //         default: true,
                //         html: 'SD 480P',
                //         url: '/assets/sample/video.mp4',
                //     },
                //     {
                //         html: 'HD 720P',
                //         url: '/assets/sample/video.mp4',
                //     },
                // ],
            },
            style: {
                width: '600px',
                height: '400px',
                margin: '60px auto 0',
            },

            movieInfor: {},
            watchVideo: 0,
        };
    },
};
</script>

<style scoped>
.router-link-exact-active {
    border-bottom: 3px solid #dcf836 !important;
    color: #dcf836 !important;
    background-color: transparent;
}

.list-episode {
    float: left;
    margin: 0 5px 15px 5px;
    padding: 7px;
    background: #3a3a3a;
    color: #fdfdfd;
    border-radius: 3px;
    font-weight: bold;
    font-size: 15px;
    cursor: pointer;
}

.box-list-episode {
    display: flex;
    flex-wrap: wrap;
    padding-top: 15px;
}
</style>
