<template>
    <div class="hero common-hero" style="height: 275px">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="hero-ct">
                        <h1>Danh sách phim</h1>
                        <ul class="breadcumb">
                            <li class="active"><a href="#">Trang chủ</a></li>
                            <li v-if="this.$route.params.id == 1">
                                <span class="ion-ios-arrow-right"
                                    ><font-awesome-icon :icon="['fas', 'angle-right']" /></span
                                >Phim bộ
                            </li>
                            <li v-else-if="this.$route.params.id == 2">
                                <span class="ion-ios-arrow-right"
                                    ><font-awesome-icon :icon="['fas', 'angle-right']"
                                /></span>
                                Phim lẻ
                            </li>
                            <li v-else-if="this.$route.params.id.length > 1">
                                <span class="ion-ios-arrow-right"
                                    ><font-awesome-icon :icon="['fas', 'angle-right']"
                                /></span>
                                Thể loại
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
                <div class="col-md-8 col-sm-12 col-xs-12">
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
                <div class="col-md-4 col-sm-12 col-xs-12">
                    <div class="sidebar">
                        <div class="searh-form">
                            <h4 class="sb-title">Tìm kiếm nâng cao</h4>
                            <form class="form-style-1" action="#" :key="formReload">
                                <div class="row">
                                    <div class="col-md-12 form-it">
                                        <label>Tên phim</label>
                                        <input type="text" v-model="keyword" placeholder="Nhập tên phim" />
                                    </div>
                                    <div class="col-md-12 form-it">
                                        <label>Thể loại</label>
                                        <div class="group-ip">
                                            <select
                                                name="skills"
                                                class="ui fluid dropdown"
                                                v-model="categorySearchImproveId"
                                            >
                                                <option
                                                    v-for="itemCbo in categoryList"
                                                    :key="itemCbo"
                                                    :value="itemCbo.value"
                                                >
                                                    {{ itemCbo.categoryName }}
                                                </option>
                                            </select>
                                            <font-awesome-icon
                                                style="position: absolute; right: 25px; color: #abb7c4"
                                                :icon="['fas', 'chevron-down']"
                                            />
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-it">
                                        <label>Năm phát hành</label>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="group-ip">
                                                    <select v-model="startYear">
                                                        <option value="range">Từ</option>
                                                        <option
                                                            v-for="itemYear in yearSelect"
                                                            :key="itemYear"
                                                            :value="itemYear"
                                                        >
                                                            {{ itemYear }}
                                                        </option>
                                                    </select>
                                                    <font-awesome-icon
                                                        style="position: absolute; right: 25px; color: #abb7c4"
                                                        :icon="['fas', 'chevron-down']"
                                                    />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="group-ip">
                                                    <select v-model="endYear">
                                                        <option value="range">Đến</option>
                                                        <option
                                                            v-for="itemYear in yearSelect"
                                                            :key="itemYear"
                                                            :value="itemYear"
                                                        >
                                                            {{ itemYear }}
                                                        </option>
                                                    </select>
                                                    <font-awesome-icon
                                                        style="position: absolute; right: 25px; color: #abb7c4"
                                                        :icon="['fas', 'chevron-down']"
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="submit" @click="btnFilterImproveMovie">Lọc phim</div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div v-if="showDialogWarning">
        <MDialog
            @close-dialog="showDialogWarning = false"
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
import axios from 'axios';

import commonJS from '@/js/common';

export default {
    name: 'MovieGrid',

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
            if (this.$route.params.id.length == 1) {
                axios
                    .get(
                        `${this.$MResource.API}/Movies/GetAllMovieByTypeAndFilter?pageNumber=${this.currentPage}&pageSize=${this.perPageValue}&typeMovie=${this.$route.params.id}&columnFilter=2&keyword=${this.keyword}&categorySearchImproveId=${this.categorySearchImproveId}&startYear=${this.startYear}&endYear=${this.endYear}&columnSort=${this.columnSort}`,
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
                                movieRelatedItem.dateOfManufactor = commonJS.formatDate(
                                    movieRelatedItem.dateOfManufactor,
                                );
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
            } else {
                axios
                    .get(
                        `${this.$MResource.API}/Movies/GetAllMovieByTypeAndFilter?pageNumber=${this.currentPage}&pageSize=${this.perPageValue}&categoryId=${this.$route.params.id}&columnFilter=1&keyword=${this.keyword}&categorySearchImproveId=${this.categorySearchImproveId}&startYear=${this.startYear}&endYear=${this.endYear}&columnSort=${this.columnSort}`,
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
                                movieRelatedItem.dateOfManufactor = commonJS.formatDate(
                                    movieRelatedItem.dateOfManufactor,
                                );
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
            }
        },

        btnShowMovieSingle(idMovie) {
            this.$router.push({ name: 'MovieSingle', params: { id: idMovie } });
        },

        checkFormSearchImprove() {
            var isValid = true;
            if (this.startYear > this.endYear) {
                isValid = false;
                this.textWarning = 'Ngày bắt đầu không được lớn hơn ngày kết thúc';
                this.showDialogWarning = true;
            }
            return isValid;
        },

        btnFilterImproveMovie() {
            if (this.startYear === 'range' || this.endYear === 'range') {
                return;
            }

            var isValid = this.checkFormSearchImprove();
            if (!isValid) {
                return;
            }

            this.callApiPaginationListMovieGrid();
            this.keyword = '';
            this.categorySearchImproveId = '';
            this.startYear = '';
            this.endYear = '';
            this.columnSort = '';
            // location.reload();
        },
    },

    created() {
        setTimeout(() => {
            this.showLoadingClient = false;
            this.perPageValue = 5;
            this.columnSort = 1;
            this.callApiPaginationListMovieGrid();

            axios
                .get(`${this.$MResource.API}/Categories`)
                .then((response) => {
                    this.categoryList = response.data;
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
        }, 1000);
    },
    data() {
        return {
            enableShowBtnPlay: {},

            movieListGrid: [],

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

            categoryList: [],
            yearSelect: [2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023],
            keyword: '',
            categorySearchImproveId: '',
            startYear: '',
            endYear: '',
            columnSort: 1,

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
            formReload: 0,

            noData: false,
            showDialogWarning: false,
            textWarning: '',

            showLoadingClient: true,
        };
    },
};
</script>

<style scoped>
.noData {
    margin-bottom: 30px;
    /* font-size: 14px; */
    color: #abb7c4;
}
</style>
