<template>
    <div id="moviesrelated" class="tab">
        <div class="row">
            <div class="topbar-filter">
                <p>
                    Tổng số: <span>{{ totalRecord }} phim</span>
                </p>
                <div style="display: flex; align-items: center; column-gap: 12px">
                    <label>Sắp xếp theo:</label>
                    <select>
                        <option value="popularity">Mới nhất</option>
                        <option value="popularity">Cũ nhất</option>
                    </select>
                </div>
            </div>
            <template v-for="(movieRelatedItem, indexRelatedItem) in movieRelatedList" :key="indexRelatedItem">
                <div class="movie-item-style-2">
                    <img :src="movieRelatedItem.imagePath" alt="" style="max-width: 170px" />
                    <div class="mv-item-infor">
                        <h6>
                            <a href="#"
                                >{{ movieRelatedItem.movieName }} &nbsp;
                                <span
                                    ><font-awesome-icon :icon="['fas', 'coins']" />&nbsp;{{
                                        movieRelatedItem.amount
                                    }}
                                    xu</span
                                ></a
                            >
                        </h6>
                        <p class="rate">
                            <font-awesome-icon
                                :icon="['fas', 'star']"
                                style="color: #f5b50a; font-size: 18px"
                            /><span>{{ movieRelatedItem.mediumScore }}</span>
                            /10
                        </p>
                        <p class="describe" v-if="movieRelatedItem.typeMovie != 4">
                            {{ movieRelatedItem.movieInfor }}
                        </p>
                        <p class="run-time">
                            Thời lượng: {{ movieRelatedItem.movieTime }} .
                            <span>Quốc gia: {{ movieRelatedItem.movieArea }} </span> .
                            <span>Ngày sản xuất: {{ movieRelatedItem.dateOfManufactor }}</span>
                        </p>
                        <p>
                            Đạo diễn: <a href="#">{{ movieRelatedItem.movieDirector }}</a>
                        </p>
                        <p>
                            Diễn viên: <a>{{ movieRelatedItem.actorMovie }}</a>
                        </p>
                    </div>
                </div>
            </template>
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
                        <MButton v-if="page.number === 0" :label="page.number" :class="{ hidePage: true }"> </MButton>
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
</template>

<script>
import axios from 'axios';

import commonJS from '@/js/common';
export default {
    name: 'MovieRelated',
    watch: {
        perPageValue: function (value) {
            if (value) {
                this.callApiPaginationReview();
            }
        },
        currentPage: function (value) {
            if (value) {
                this.callApiPaginationReview();
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
    },
    created() {
        var me = this;
        this.perPageValue = 5;
        axios
            .get(
                `${this.$MResource.API}/Movies/GetSimilarMovieByFilter?pageNumber=${this.currentPage}&pageSize=${this.perPageValue}&movieId=${this.$route.params.id}`,
            )
            .then((response) => {
                me.movieRelatedList = response.data.data;
                this.totalPages = response.data.totalPages;
                this.totalRecord = response.data.totalRecords;

                this.movieRelatedList.forEach((movieRelatedItem) => {
                    if (movieRelatedItem.imgByte != null && movieRelatedItem.imgByte.length > 0) {
                        movieRelatedItem.imagePath = 'data:image/png;base64,' + movieRelatedItem.imgByte;
                        // this.file = 'data:image/png;base64,' + this.movieInfor.imgByte;
                    }
                    if (movieRelatedItem.dateOfManufactor != null || movieRelatedItem.dateOfManufactor != undefined) {
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
            });
        console.log('id overview: ', this.$route.params.id);
    },
    data() {
        return {
            movieRelatedList: [],

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
            // file: null,
        };
    },
};
</script>

<style scoped>
/* .infor-movie-related {
    max-height: 75px;
    white-space: nowrap;
    text-overflow: ellipsis;

    overflow: hidden;
    vertical-align: middle !important;
} */
</style>
