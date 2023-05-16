<template>
    <div id="overview" class="tab active">
        <div class="row">
            <div class="col-md-8 col-sm-12 col-xs-12">
                <p>{{ movieInfor.movieInfor }}</p>

                <div class="title-hd-sm">
                    <h4>Diễn viên</h4>
                </div>
                <!-- movie cast -->
                <div class="mvcast-item">
                    <template v-for="(actorItem, indexActor) in actorList" :key="indexActor">
                        <div class="cast-it" @click="btnShowActorSingle(actorItem.value)" style="cursor: pointer">
                            <div class="cast-left">
                                <img v-lazy="actorItem.imagePath" alt="" style="width: 40px; height: 40px" />
                                <a>{{ actorItem.fullname }}</a>
                            </div>
                        </div>
                    </template>
                </div>
            </div>
            <div class="col-md-4 col-xs-12 col-sm-12">
                <div class="sb-it">
                    <h6>Đạo diễn:</h6>
                    <p>
                        <a class="text-overview">{{ movieInfor.movieDirector }}</a>
                    </p>
                </div>

                <div class="sb-it">
                    <h6>Thể loại:</h6>
                    <p>
                        <a class="text-overview">{{ movieInfor.genreNames }}</a>
                    </p>
                </div>
                <div class="sb-it">
                    <h6>Ngày sản xuất</h6>
                    <p class="text-overview">{{ movieInfor.dateOfManufactor }}</p>
                </div>
                <div class="sb-it">
                    <h6>Thời lượng:</h6>
                    <p class="text-overview">{{ movieInfor.movieTime }}</p>
                </div>
                <div class="sb-it">
                    <h6>Quốc gia:</h6>
                    <p class="text-overview">{{ movieInfor.movieArea }}</p>
                </div>

                <!-- <div class="ads">
                                                    <img src="images/uploads/ads1.png" alt="" />
                                                </div> -->
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

import commonJS from '@/js/common';
export default {
    name: 'MovieOverview',
    methods: {
        btnShowActorSingle(idActor) {
            this.$router.push({ name: 'actorsingle', params: { id: idActor } });
        },
    },
    created() {
        var me = this;
        axios
            .get(`${this.$MResource.API}/Movies/GetMovieById?movieId=${this.$route.params.id}`)
            .then((response) => {
                me.movieInfor = response.data;

                if (me.movieInfor.dateOfManufactor != null || me.movieInfor.dateOfManufactor != undefined) {
                    me.movieInfor.dateOfManufactor = commonJS.formatDate(me.movieInfor.dateOfManufactor);
                }
                if (this.movieInfor.imgByte != null && this.movieInfor.imgByte.length > 0) {
                    this.movieInfor.imagePath = 'data:image/png;base64,' + this.movieInfor.imgByte;
                    // this.file = 'data:image/png;base64,' + this.movieInfor.imgByte;
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
            .get(`${this.$MResource.API}/Actors/GetActorByMovieId?movieId=${this.$route.params.id}`)
            .then((response) => {
                me.actorList = response.data;

                this.actorList.forEach((actorItem) => {
                    if (actorItem.imgByte != null && actorItem.imgByte.length > 0) {
                        actorItem.imagePath = 'data:image/png;base64,' + actorItem.imgByte;
                        // this.file = 'data:image/png;base64,' + this.movieInfor.imgByte;
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
            });
        console.log('id overview: ', this.$route.params.id);
    },
    data() {
        return {
            movieInfor: {},
            actorList: [],
            // file: null,
        };
    },
};
</script>

<style scoped>
.text-overview {
    color: #4280bf;
}
.text-overview:hover {
    color: #4280bf;
}
</style>
