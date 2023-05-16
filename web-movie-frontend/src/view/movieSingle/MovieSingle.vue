<template>
    <!-- <TheHeader></TheHeader> -->
    <div class="page-single movie-single movie_single">
        <div class="container">
            <div class="row ipad-width2">
                <div class="col-md-4 col-sm-12 col-xs-12">
                    <div class="movie-img sticky-sb">
                        <img v-lazy="movieInfor.imagePath" alt="" />
                        <div class="movie-btn">
                            <div class="btn-transform transform-vertical red">
                                <div>
                                    <a class="item item-1 redbtn">
                                        <font-awesome-icon :icon="['fas', 'trailer']" />
                                        <span style="margin-left: 4px">Xem trailer</span>
                                    </a>
                                </div>
                                <div>
                                    <router-link
                                        class="item item-2 redbtn fancybox-media hvr-grow"
                                        :to="{
                                            name: 'moviemedia',
                                            params: { id: this.$route.params.id },
                                        }"
                                    >
                                        <font-awesome-icon :icon="['fas', 'trailer']" />
                                    </router-link>
                                    <!-- <a
                                        href="https://www.youtube.com/embed/o-0hcF97wy0"
                                        class="item item-2 redbtn fancybox-media hvr-grow"
                                        ><font-awesome-icon :icon="['fas', 'trailer']"
                                    /></a> -->
                                </div>
                            </div>
                            <div @click="btnWatchMovie" class="btn-transform transform-vertical">
                                <div>
                                    <a class="item item-1 yellowbtn"
                                        ><font-awesome-icon :icon="['fas', 'play']" /><span style="margin-left: 8px"
                                            >Xem phim</span
                                        ></a
                                    >
                                </div>
                                <div>
                                    <div class="item item-2 yellowbtn">
                                        <font-awesome-icon :icon="['fas', 'play']" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-8 col-sm-12 col-xs-12">
                    <div class="movie-single-ct main-content">
                        <h1 class="bd-hd">
                            {{ movieInfor.movieName }}
                            <span> <font-awesome-icon :icon="['fas', 'coins']" />&nbsp;{{ movieInfor.amount }} xu</span>
                        </h1>
                        <!-- <div class="social-btn">
                            <a href="#" class="parent-btn"><i class="ion-heart"></i> Add to Favorite</a>
                            <div class="hover-bnt">
                                <a href="#" class="parent-btn"><i class="ion-android-share-alt"></i>share</a>
                                <div class="hvr-item">
                                    <a href="#" class="hvr-grow"><i class="ion-social-facebook"></i></a>
                                    <a href="#" class="hvr-grow"><i class="ion-social-twitter"></i></a>
                                    <a href="#" class="hvr-grow"><i class="ion-social-googleplus"></i></a>
                                    <a href="#" class="hvr-grow"><i class="ion-social-youtube"></i></a>
                                </div>
                            </div>
                        </div> -->
                        <div class="movie-rate">
                            <div class="rate">
                                <font-awesome-icon :icon="['fas', 'star']" style="color: #f5b50a; font-size: 24px" />
                                <p>
                                    <span>{{ movieInfor.mediumScore == null ? 0 : movieInfor.mediumScore }}</span>
                                    /10<br />
                                    <!-- <span>{{ this.mediumScore.toFixed(1) }}</span> /10<br /> -->
                                    <span class="rv">{{ countRating }} lượt đánh giá</span>
                                </p>
                            </div>
                            <div class="rate-star">
                                <p>Đánh giá:</p>
                                <!-- <template v-for="n in 10" :key="n"> -->
                                <font-awesome-icon
                                    v-for="n in 10"
                                    :key="n"
                                    @click="selectRating(n)"
                                    :icon="['fas', 'star']"
                                    :class="[n <= movieInfor.mediumScore ? 'star-selected' : 'star-icon']"
                                />
                                <!-- </template> -->
                            </div>
                        </div>
                        <div class="movie-tabs">
                            <div class="tabs">
                                <ul class="tab-links tabs-mv">
                                    <li>
                                        <router-link
                                            :to="{
                                                name: 'MovieDescript',
                                                params: { id: this.$route.params.id },
                                            }"
                                            >Tổng quan</router-link
                                        >
                                    </li>
                                    <li>
                                        <router-link
                                            :to="{
                                                name: 'moviereview',
                                                params: { id: this.$route.params.id },
                                            }"
                                            >Bình luận</router-link
                                        >
                                    </li>
                                    <li>
                                        <router-link
                                            :to="{
                                                name: 'moviemedia',
                                                params: { id: this.$route.params.id },
                                            }"
                                            >Trailers</router-link
                                        >
                                    </li>
                                    <li>
                                        <router-link
                                            :to="{
                                                name: 'movierelated',
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
        </div>
    </div>
    <!-- <TheFooter></TheFooter> -->
    <div v-if="hasAccount">
        <MDialog
            @close-dialog="hasAccount = false"
            :depict="textWarning"
            dialogConfirm="false"
            :hasCloseButton="false"
            :hasCancelButton="{ 'btn-dialog-left': true }"
            typeDialog="warningDialog"
            titleDialog="Thông báo"
            titleButton="Đồng ý"
        ></MDialog>
    </div>
    <MLoadingClient v-if="showLoadingClient"></MLoadingClient>
</template>

<script>
// import TheFooter from '@/layout/home/footer/TheFooter.vue';
// import TheHeader from '../header/TheHeader.vue';

import axios from 'axios';

import commonJS from '@/js/common';
export default {
    name: 'MovieSingle',
    // components: {
    //     TheFooter,
    // },
    watch: {
        '$route.params.id': function (value) {
            if (value) {
                location.reload();
            }
        },
    },
    methods: {
        // selectRating(rating) {
        //     this.selectedRating = rating;
        //     console.log('diem phimL: ', rating);
        // },

        callApiPaginationReview() {
            axios
                .get(
                    `${this.$MResource.API}/Comments/filter?keyword=${this.$route.params.id}&pageNumber=1&pageSize=1000`,
                )
                .then((response) => {
                    this.commentList = response.data.data;
                    this.commentList.forEach((element) => {
                        if (element.rating != null) {
                            this.countRating++;
                            this.totalRating += element.rating;
                        }
                        console.log('cacs daxnh gia: ', element.rating);
                    });
                    console.log('tong dieng: ', this.totalRating / this.countRating);
                    // console.log('tong dieng: ', this.totalRating);
                    // if (this.totalRating == 0 || this.totalRating == null || this.totalRating == undefined) {
                    //     this.mediumScore = 0;
                    // } else {
                    //     this.mediumScore = this.totalRating / this.countRating;
                    // }

                    // axios
                    //     .put(
                    //         `${this.$MResource.API}/Comments/filter?keyword=${this.$route.params.id}&pageNumber=1&pageSize=1000`,
                    //     )
                    //     .then((response) => {
                    //         this.commentList = response.data.data;
                    //         this.commentList.forEach((element) => {
                    //             if (element.rating != null) {
                    //                 this.countRating++;
                    //                 this.totalRating += element.rating;
                    //             }
                    //         });
                    //         console.log('tong dieng: ', this.totalRating);
                    //         if (this.totalRating == 0 || this.totalRating == null || this.totalRating == undefined) {
                    //             this.mediumScore = 0;
                    //         } else {
                    //             this.mediumScore = this.totalRating / this.countRating;
                    //         }
                    //     })
                    //     .catch((err) => {
                    //         this.$MToastMessage({
                    //             titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    //             messageToast: err,
                    //             showToastMessage: true,
                    //             typeToast: 'errorToast',
                    //         });
                    //     });
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

        checkBuyOrWatchMovie() {
            if (this.userInformation.amountMoney < this.movieInfor.amount) {
                this.hasAccount = true;
                this.textWarning =
                    'Bạn không đủ tiền để có thể xem phim. Vui lòng nạp tiền vào tài khoản để có thể trải nghiệm xem phim trực tuyến';
            } else {
                axios
                    .put(
                        `${this.$MResource.API}/Users/MinusMoneyUser?userId=${
                            this.userInformation.userId
                        }&amountMoneyMovie=${parseFloat(this.movieInfor.amount)}`,
                    )
                    .then(() => {
                        this.orderInfor.orderName = 'Yêu cầu mua phim';
                        this.orderInfor.userId = this.userInformation.userId;
                        this.orderInfor.movieId = this.movieInfor.movieId;
                        axios
                            .post(`${this.$MResource.API}/OrderUsers`, this.orderInfor)
                            .then(() => {})
                            .catch((err) => {
                                let response = err.response;
                                switch (response.status) {
                                    case 500:
                                        if (response.data['errorCode'] === 5) {
                                            this.$MToastMessage({
                                                titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                                                messageToast: response.data['userMsg'],
                                                showToastMessage: true,
                                                typeToast: 'errorToast',
                                            });
                                        } else {
                                            this.$MToastMessage({
                                                titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                                                messageToast: response.data['devMsg'],
                                                showToastMessage: true,
                                                typeToast: 'errorToast',
                                            });
                                        }
                                        break;
                                    case 400:
                                        var userMsg = response.data['moreInfo'];
                                        userMsg.forEach((element) => {
                                            this.showDialogWarning = true;
                                            this.textWarning = element;
                                        });

                                        break;
                                    default:
                                        break;
                                }
                            });
                        this.$router.push({
                            name: 'watchmovie',
                            params: { id: this.movieInfor.movieId },
                        });

                        //Thông tin user mới
                        axios
                            .get(`${this.$MResource.API}/Users/recordId?recordId=${this.userInformation.userId}`)
                            .then((response) => {
                                this.newUserInfor = response.data;
                                localStorage.setItem('userInfor', JSON.stringify(this.newUserInfor));
                            })
                            .catch((err) => {
                                this.$MToastMessage({
                                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                                    messageToast: err,
                                    showToastMessage: true,
                                    typeToast: 'errorToast',
                                });
                            });

                        //Cập nhật thêm lượt xem
                        axios
                            .put(`${this.$MResource.API}/Movies/UpdateMovieReview?movieId=${this.movieInfor.movieId}`)
                            .then(() => {})
                            .catch((err) => {
                                this.$MToastMessage({
                                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                                    messageToast: err,
                                    showToastMessage: true,
                                    typeToast: 'errorToast',
                                });
                            });
                    });
            }
        },

        btnWatchMovie() {
            var me = this;
            const storage = localStorage.getItem('userInfor');
            if (storage !== null) {
                this.userInformation = JSON.parse(storage);
                axios
                    .get(`${this.$MResource.API}/OrderUsers/GetByUserId?userId=${this.userInformation.userId}`)
                    .then((response) => {
                        me.orderInforByUserId = response.data;
                        console.log('orderInforByUserId', me.orderInforByUserId);
                        if (me.orderInforByUserId.length < 1) {
                            console.log('them');
                            this.checkBuyOrWatchMovie();
                        } else {
                            me.orderInforByUserId.forEach((element) => {
                                console.log('element', element);
                                if (element.movieId != null && element.movieId == this.movieInfor.movieId) {
                                    //Cập nhật thêm lượt xem
                                    axios
                                        .put(
                                            `${this.$MResource.API}/Movies/UpdateMovieReview?movieId=${this.movieInfor.movieId}`,
                                        )
                                        .then(() => {})
                                        .catch((err) => {
                                            this.$MToastMessage({
                                                titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                                                messageToast: err,
                                                showToastMessage: true,
                                                typeToast: 'errorToast',
                                            });
                                        });
                                    this.$router.push({ name: 'watchmovie', params: { id: this.movieInfor.movieId } });
                                } else {
                                    console.log('them moi');
                                    this.checkBuyOrWatchMovie();
                                }
                            });
                        }
                    })
                    .catch((err) => {
                        let response = err.response;
                        switch (response.status) {
                            case 500:
                                if (response.data['errorCode'] === 5) {
                                    this.$MToastMessage({
                                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                                        messageToast: response.data['userMsg'],
                                        showToastMessage: true,
                                        typeToast: 'errorToast',
                                    });
                                }
                                break;
                            case 400:
                                var userMsg = response.data['moreInfo'];
                                userMsg.forEach((element) => {
                                    this.showDialogWarning = true;
                                    this.textWarning = element;
                                });

                                break;
                            default:
                                break;
                        }
                    });
            } else {
                this.hasAccount = true;
                this.textWarning = 'Bạn cần phải đăng nhập tài khoản để có thể xem phim';
            }
        },
    },
    created() {
        setTimeout(() => {
            var me = this;
            axios
                .get(`${this.$MResource.API}/Movies/GetMovieById?movieId=${this.$route.params.id}`)
                .then((response) => {
                    this.showLoadingClient = false;
                    me.movieInfor = response.data;

                    if (me.movieInfor.dateOfBirth != null || me.movieInfor.dateOfBirth != undefined) {
                        me.movieInfor.dateOfBirth = commonJS.bindingFormatDate(me.movieInfor.dateOfBirth);
                    }
                    if (this.movieInfor.imgByte != null && this.movieInfor.imgByte.length > 0) {
                        this.movieInfor.imagePath = 'data:image/png;base64,' + this.movieInfor.imgByte;
                        // this.file = 'data:image/png;base64,' + this.movieInfor.imgByte;
                    }
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

            this.callApiPaginationReview();
        }, 1000);
    },

    data() {
        return {
            movieInfor: {},

            hasAccount: false,
            textWarning: null,

            orderInfor: { orderName: '', recharge: 0, userId: '', movieId: '', enable: 0 },

            orderInforByUserId: {},

            newUserInfor: {},

            selectedRating: 0,

            commentList: [],

            countRating: 0,
            totalRating: 0,
            mediumScore: 0,

            showLoadingClient: true,
            // file: null,
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

.star-selected {
    color: #f5b50a;
    font-size: 24px;
}

.star-icon {
    color: #ccc;
    font-size: 24px;
}
</style>
