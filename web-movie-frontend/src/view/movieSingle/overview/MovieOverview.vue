<template>
    <div id="overview" class="tab active">
        <div class="row">
            <div class="col-md-8 col-sm-12 col-xs-12">
                <p>{{ movieInfor.movieInfor }}</p>
                <!-- <div class="title-hd-sm">
                                                    <h4>Videos & Photos</h4>
                                                    <a href="#" class="time"
                                                        >All 5 Videos & 245 Photos <i class="ion-ios-arrow-right"></i
                                                    ></a>
                                                </div>
                                                <div class="mvsingle-item ov-item">
                                                    <a
                                                        class="img-lightbox"
                                                        data-fancybox-group="gallery"
                                                        href="images/uploads/image11.jpg"
                                                        ><img src="images/uploads/image1.jpg" alt=""
                                                    /></a>
                                                    <a
                                                        class="img-lightbox"
                                                        data-fancybox-group="gallery"
                                                        href="images/uploads/image21.jpg"
                                                        ><img src="images/uploads/image2.jpg" alt=""
                                                    /></a>
                                                    <a
                                                        class="img-lightbox"
                                                        data-fancybox-group="gallery"
                                                        href="images/uploads/image31.jpg"
                                                        ><img src="images/uploads/image3.jpg" alt=""
                                                    /></a>
                                                    <div class="vd-it">
                                                        <img class="vd-img" src="images/uploads/image4.jpg" alt="" />
                                                        <a
                                                            class="fancybox-media hvr-grow"
                                                            href="https://www.youtube.com/embed/o-0hcF97wy0"
                                                            ><img src="images/uploads/play-vd.png" alt=""
                                                        /></a>
                                                    </div>
                                                </div> -->
                <div class="title-hd-sm">
                    <h4>Diễn viên</h4>
                    <!-- <a href="#" class="time"
                                                        >Full Cast & Crew <i class="ion-ios-arrow-right"></i
                                                    ></a> -->
                </div>
                <!-- movie cast -->
                <div class="mvcast-item">
                    <template v-for="(actorItem, indexActor) in actorList" :key="indexActor">
                        <div class="cast-it">
                            <div class="cast-left">
                                <img :src="actorItem.imagePath" alt="" style="width: 40px; height: 40px" />
                                <a>{{ actorItem.fullname }}</a>
                            </div>
                            <!-- <p>... Robert Downey Jr.</p> -->
                        </div>
                    </template>
                    <!-- <div class="cast-it">
                        <div class="cast-left">
                            <img
                                src="https://image.tmdb.org/t/p/w300_and_h450_bestv2/8eTtJ7XVXY0BnEeUaSiTAraTIXd.jpg"
                                alt=""
                                style="width: 40px; height: 40px"
                            />
                            <a href="#">Robert Downey Jr.</a>
                        </div>
                        <p>... Robert Downey Jr.</p>
                    </div>
                    <div class="cast-it">
                        <div class="cast-left">
                            <img
                                src="https://image.tmdb.org/t/p/w300_and_h450_bestv2/8eTtJ7XVXY0BnEeUaSiTAraTIXd.jpg"
                                alt=""
                                style="width: 40px; height: 40px"
                            />
                            <a href="#">Robert Downey Jr.</a>
                        </div>
                        <p>... Robert Downey Jr.</p>
                    </div> -->
                </div>
            </div>
            <div class="col-md-4 col-xs-12 col-sm-12">
                <div class="sb-it">
                    <h6>Đạo diễn:</h6>
                    <p>
                        <a>{{ movieInfor.movieDirector }}</a>
                    </p>
                </div>

                <div class="sb-it">
                    <h6>Thể loại:</h6>
                    <p>
                        <a>{{ movieInfor.genreNames }}</a>
                    </p>
                </div>
                <div class="sb-it">
                    <h6>Ngày sản xuất</h6>
                    <p>{{ movieInfor.dateOfManufactor }}</p>
                </div>
                <div class="sb-it">
                    <h6>Thời lượng:</h6>
                    <p>{{ movieInfor.movieTime }}</p>
                </div>
                <div class="sb-it">
                    <h6>Quốc gia:</h6>
                    <p>{{ movieInfor.movieArea }}</p>
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

<style></style>
