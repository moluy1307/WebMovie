<template>
    <div class="hero user-hero">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="hero-ct">
                        <h1>Thông tin tài khoản</h1>
                        <ul class="breadcumb">
                            <li class="active"><a href="#">Trang chủ</a></li>
                            <li>
                                <span><font-awesome-icon :icon="['fas', 'angle-right']" /></span>Lịch sử xem phim
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="page-single">
        <div class="container">
            <div class="row ipad-width">
                <div class="col-md-3 col-sm-12 col-xs-12">
                    <div class="user-information">
                        <div class="user-img">
                            <a><img src="../../assets/img/user-img.png" alt="" /><br /></a>
                            <!-- <a href="#" class="redbtn">Change avatar</a> -->
                        </div>
                        <div class="user-fav">
                            <p>Chi tiết tài khoản</p>
                            <ul>
                                <!-- <li class="active"><a href="userprofile.html">Profile</a></li> -->
                                <li style="color: #dcf836">
                                    <font-awesome-icon :icon="['fas', 'coins']" />&nbsp;{{
                                        userInformation.amountMoney == null ? '0' : userInformation.amountMoney
                                    }}
                                    xu
                                </li>
                                <li style="cursor: pointer" @click="btnShowFormInstruction"><a>Nạp tiền</a></li>
                            </ul>
                        </div>
                        <div class="user-fav">
                            <p>Khác</p>
                            <ul>
                                <li @click="btnGotoChangePass" style="cursor: pointer"><a>Thay đổi mật khẩu</a></li>
                                <li @click="btnLogout" style="cursor: pointer"><a>Đăng xuất</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-md-9 col-sm-12 col-xs-12">
                    <div class="topbar-filter">
                        <p>
                            Tổng số: <span>{{ totalRecord }} phim</span>
                        </p>
                        <div style="display: flex; align-items: center; column-gap: 12px">
                            <label style="margin-bottom: 0">Sắp xếp theo:</label>
                            <select v-model="columnSort">
                                <option v-for="itemSort in dataSort" :key="itemSort" :value="itemSort.idSort">
                                    {{ itemSort.titleSort }}
                                </option>
                            </select>
                        </div>
                    </div>
                    <div class="tab">
                        <div class="flex-wrap-movielist">
                            <template v-for="(movieItemGrid, indexN) in movieListGrid" :key="indexN">
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
                    </div>
                    <div class="noData" v-if="noData">Không có dữ liệu</div>
                    <div class="topbar-filter">
                        <label>Số phim trên một trang:</label>
                        <select v-model="perPageValue">
                            <option v-for="itemCbo in dataCombobox" :key="itemCbo" :value="itemCbo.valueCombobox">
                                {{ itemCbo.title }}
                            </option>
                        </select>
                        <div class="page-number">
                            <!-- <button class="btn-page-number btn-none">Trước</button> -->
                            <MButton
                                label="Trước"
                                :class="{ 'btn-page-number': true }"
                                @click="onClickPreviousPage"
                                :disable="isInFirstPage"
                            ></MButton>
                            <div class="btn-number" v-for="(page, index) in pages" :key="index">
                                <MButton v-if="page.number === 0" :label="page.number" :class="{ hidePage: true }">
                                </MButton>
                                <MButton
                                    :label="page.number"
                                    @click="onClickPage(page.number, index)"
                                    :disabled="page.isDisabled"
                                    :class="{ 'btn-page-number': true, 'btn-clicked': isPageActive(page.number) }"
                                >
                                </MButton>
                            </div>
                            <MButton
                                label="Sau"
                                :class="{ 'btn-page-number': true }"
                                @click="onClickNextPage"
                                :disabled="isInLastPage"
                            ></MButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <RechargeMoneyVue v-if="showFormInstruction" @onClose="showFormInstruction = false"></RechargeMoneyVue>
    <MLoadingClient v-if="showLoadingClient"></MLoadingClient>
</template>

<script>
import axios from 'axios';

import commonJS from '@/js/common';

import RechargeMoneyVue from './RechargeMoney.vue';
export default {
    name: 'UserMovieViewed',

    components: { RechargeMoneyVue },
    watch: {
        perPageValue: function (value) {
            if (value) {
                this.callApiPaginationListMovieGrid();
            }
        },
        currentPage: function (value) {
            if (value) {
                this.callApiPaginationListMovieGrid();
            }
        },
        columnSort: function (value) {
            if (value) {
                this.callApiPaginationListMovieGrid();
            }
        },
        '$route.params.id': function (value) {
            if (value) {
                location.reload();
            }
        },
        movieListGrid: function (value) {
            if (value.length < 1) {
                this.noData = true;
            } else {
                this.noData = false;
            }
        },
    },
    computed: {
        /**
         * Tính toán hiển thị trang
         * CreatedBy: huynq (16/1/2023)
         */
        pages() {
            const range = [];

            for (let i = 1; i <= this.totalPages; i++) {
                if (i == 1 || i == this.totalPages || (i <= this.currentPage + 2 && i >= this.currentPage - 2)) {
                    range.push({
                        number: i,
                        isDisabled: i === this.currentPage,
                    });
                } else if (i == this.currentPage + 3 || i == this.currentPage - 3) {
                    range.push({
                        number: '...',
                        isDisabled: i === this.currentPage,
                    });
                }
            }

            return range;
        },

        /**
         * Trang đầu tiên đang được chọn
         * CreatedBy: huynq (16/1/2023)
         */
        isInFirstPage() {
            return this.currentPage === 1;
        },

        /**
         * Trang cuối cùng đang được chọn
         * CreatedBy: huynq (16/1/2023)
         */
        isInLastPage() {
            return this.currentPage === this.totalPages;
        },
    },
    methods: {
        btnShowFormInstruction() {
            this.showFormInstruction = !this.showFormInstruction;
        },

        showButtonPlay(index) {
            // this.$refs.btnPlay[index].classList;
            this.enableShowBtnPlay = { ...this.enableShowBtnPlay, index };
        },
        hideButtonPlay() {
            this.enableShowBtnPlay = {};
        },

        /**
         * Click button "Trước" trong phần paging
         * CreatedBy: huynq (16/1/2023)
         */
        onClickPreviousPage() {
            try {
                if (this.currentPage > 1) {
                    this.currentPage = this.currentPage - 1;
                }
            } catch (err) {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            }
        },

        /**
         * Click các button số trang trong phần paging
         * CreatedBy: huynq (16/1/2023)
         */
        onClickPage(page) {
            try {
                if (page != '...') {
                    this.currentPage = page;
                }
            } catch (err) {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            }
        },

        /**
         * Click button "Sau" trong phần paging
         * CreatedBy: huynq (16/1/2023)
         */
        onClickNextPage() {
            try {
                if (this.currentPage < this.totalPages) {
                    this.currentPage = this.currentPage + 1;
                }
            } catch (err) {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            }
        },

        /**
         * Thay đổi trạng thái button số trang đang được chọn
         * CreatedBy: huynq (16/1/2023)
         */
        isPageActive(page) {
            return this.currentPage === page;
        },

        callApiPaginationListMovieGrid() {
            axios
                .get(
                    `${this.$MResource.API}/Movies/GetAllMovieByTypeAndFilter?pageNumber=${this.currentPage}&pageSize=${this.perPageValue}&categoryId=${this.userInformation.userId}&columnFilter=6&columnSort=${this.columnSort}`,
                )
                .then((response) => {
                    this.movieListGrid = response.data.data;
                    this.totalPages = response.data.totalPages;
                    this.totalRecord = response.data.totalRecords;

                    this.movieListGrid.forEach((movieRelatedItem) => {
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
        },

        btnShowMovieSingle(idMovie) {
            this.$router.push({ name: 'MovieSingle', params: { id: idMovie } });
        },

        btnLogout() {
            this.userInformation = {};
            localStorage.removeItem('userInfor');
            this.$router.push('/');
            setTimeout(() => {
                location.reload();
            }, 100);
        },

        btnGotoChangePass() {
            this.$router.push({ name: 'userprofile', params: { id: this.userInformation.userId } });
        },
    },
    created() {
        setTimeout(() => {
            this.showLoadingClient = false;
            this.perPageValue = 5;
            this.columnSort = 1;
            const storage = localStorage.getItem('userInfor');
            if (storage !== null) {
                this.userInformation = JSON.parse(storage);
                this.callApiPaginationListMovieGrid();
            }
        }, 1000);
    },
    data() {
        return {
            showFormInstruction: false,
            userInformation: {},
            showLoadingClient: true,

            dataSort: [
                {
                    idSort: 1,
                    titleSort: 'Ngày đăng phim',
                },
                {
                    idSort: 2,
                    titleSort: 'Ngày phát hành',
                },
                {
                    idSort: 3,
                    titleSort: 'Lượt xem',
                },
                {
                    idSort: 4,
                    titleSort: 'Điểm đánh giá',
                },
            ],

            movieListGrid: [],
            enableShowBtnPlay: {},

            currentPage: 1,
            perPageValue: 5,
            totalPages: 0,
            totalRecord: 0,
            dataCombobox: [
                {
                    valueCombobox: 5,
                    title: '5 phim',
                },
                {
                    valueCombobox: 10,
                    title: '10 phim',
                },
                {
                    valueCombobox: 20,
                    title: '20 phim',
                },
                {
                    valueCombobox: 50,
                    title: '50 phim',
                },
            ],
            noData: false,
            columnSort: 1,
        };
    },
};
</script>

<style></style>
