<template>
    <div id="reviews" class="tab review">
        <div class="row">
            <div class="rv-hd">
                <div class="div">
                    <h2>{{ movieInfor.movieName }}</h2>
                </div>
                <a @click="showFormReview = true" class="redbtn" style="cursor: pointer">Viết bình luận</a>
            </div>
            <div class="topbar-filter">
                <p>
                    <span>{{ totalRecord }}</span> bình luận
                </p>
                <label>Sắp xếp theo:</label>
                <select>
                    <option value="popularity">Mới nhất</option>
                    <option value="popularity">Cũ nhất</option>
                </select>
            </div>
            <template v-for="(commentItem, indexComment) in commentList" :key="indexComment">
                <div class="mv-user-review-item">
                    <div class="user-infor">
                        <img src="../../../assets/img/userProf.jpg" alt="" style="width: 42px; height: 42px" />
                        <div>
                            <h3>{{ commentItem.username }}</h3>
                            <div class="no-star">
                                <font-awesome-icon
                                    id="evaluate"
                                    v-for="n in 10"
                                    :key="n"
                                    :icon="['fas', 'star']"
                                    :class="[n <= commentItem.rating ? 'star-selected' : 'star-icon']"
                                />
                            </div>
                            <a class="time"> Bình luận</a><a> &nbsp;{{ descriptionTime }} ago</a>
                        </div>
                    </div>
                    <p>
                        {{ commentItem.commentContent }}
                    </p>
                </div>
            </template>
            <div class="topbar-filter">
                <label>Số bình luận trên một trang:</label>
                <select v-model="perPageValue">
                    <option v-for="itemCbo in dataCombobox" :key="itemCbo" :value="itemCbo.valueCombobox">
                        {{ itemCbo.title }}
                    </option>
                </select>
                <!-- <div class="pagination2">
                    <a class="active" href="#">1</a>
                    <a href="#">2</a>
                    <a href="#">3</a>
                    <a href="#">4</a>
                    <a href="#">5</a>
                    <a href="#">6</a>
                    <a href="#"><i class="ion-arrow-right-b"></i></a>
                </div> -->
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
    <MovieWriteReview
        :nameOfMovie="movieInfor.movieName"
        v-if="showFormReview"
        @onClose="showFormReview = false"
        :idMovie="movieInfor.movieId"
    ></MovieWriteReview>
</template>

<script>
import axios from 'axios';
import moment from 'moment';

import commonJS from '@/js/common';

import MovieWriteReview from './MovieWriteReview.vue';
export default {
    name: 'MovieReview',
    components: { MovieWriteReview },
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
        callApiPaginationReview() {
            axios
                .get(
                    `${this.$MResource.API}/Comments/filter?keyword=${this.$route.params.id}&pageNumber=${this.currentPage}&pageSize=${this.perPageValue}`,
                )
                .then((response) => {
                    this.commentList = response.data.data;
                    // this.entities = data;
                    this.totalPages = response.data.totalPages;
                    this.totalRecord = response.data.totalRecords;
                    for (var item in this.commentList) {
                        const now = moment(); // Lấy thời điểm hiện tại
                        const date = moment(this.commentList[item].createdDate); // Lấy thời điểm từ chuỗi

                        // Tính toán khoảng thời gian từ thời điểm đó đến hiện tại
                        const duration = moment.duration(now.diff(date));

                        // Xuất ra chuỗi mô tả khoảng thời gian
                        this.descriptionTime = duration.humanize();

                        this.selectedRating = this.commentList[item].rating;
                    }

                    console.log('gia trio: ', this.commentList);
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

        this.perPageValue = 5;
        this.callApiPaginationReview();
        console.log('id review: ', this.$route.params.id);
    },
    data() {
        return {
            movieInfor: {},
            actorList: [],

            showFormReview: false,

            currentPage: 1,
            perPageValue: 5,
            commentList: [],
            totalPages: 0,
            totalRecord: 0,

            descriptionTime: null,

            dataCombobox: [
                {
                    valueCombobox: 5,
                    title: '5 bình luận',
                },
                {
                    valueCombobox: 10,
                    title: '10 bình luận',
                },
                {
                    valueCombobox: 20,
                    title: '20 bình luận',
                },
                {
                    valueCombobox: 50,
                    title: '50 bình luận',
                },
            ],

            selectedRating: 0,

            // file: null,
        };
    },
};
</script>

<style scoped>
.star-selected {
    color: #f5b50a;
    font-size: 14px;
}

.star-icon {
    color: #ccc;
    font-size: 14px;
}
</style>
