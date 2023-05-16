<template>
    <div id="media" class="tab">
        <div class="row">
            <!-- <div class="rv-hd">
                <div>
                    <h3>Trailer phim</h3>
                    <h2>Thanh Gươm Diệt Quỷ: Phần Làng Rèn Kiếm</h2>
                </div>
            </div> -->
            <div class="title-hd-sm">
                <h4>
                    Videos <span>({{ trailerList.length }})</span>
                </h4>
            </div>
            <div class="mvsingle-item media-item">
                <template v-for="trailerItem in trailerList" :key="trailerItem">
                    <div class="vd-item">
                        <div class="vd-it">
                            <!-- <iframe
                                class="item-video"
                                v-lazy="trailerItem.trailerUrl"
                                style="width: 170px; height: 111px"
                                allowpaymentrequest
                            ></iframe> -->
                            <div v-lazyload-youtube="trailerItem.trailerUrl" style="width: 170px; height: 111px"></div>
                        </div>
                        <div class="vd-infor">
                            <h6>
                                <a>Trailer: {{ trailerItem.trailerName }}</a>
                            </h6>
                            <p class="time">{{ trailerItem.trailerTime }}</p>
                        </div>
                    </div>
                </template>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
export default {
    name: 'MovieMedia',
    created() {
        axios
            .get(`${this.$MResource.API}/Trailers/GetAllTrailerByMovieId?movieId=${this.$route.params.id}`)
            .then((response) => {
                this.trailerList = response.data;
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
    data() {
        return {
            trailerList: [],
        };
    },
};
</script>

<style></style>
