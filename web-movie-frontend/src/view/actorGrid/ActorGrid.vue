<template>
    <div class="hero common-hero" style="height: 275px">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="hero-ct">
                        <h1>Danh sách diễn viên</h1>
                        <ul class="breadcumb">
                            <li class="active"><a href="#">Trang chủ</a></li>
                            <li>
                                <span class="ion-ios-arrow-right"
                                    ><font-awesome-icon :icon="['fas', 'angle-right']"
                                /></span>
                                Diễn viên
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- celebrity grid v1 section-->
    <div class="page-single">
        <div class="container">
            <div class="row ipad-width2">
                <div class="col-md-9 col-sm-12 col-xs-12">
                    <div class="topbar-filter">
                        <p>
                            Tổng số: <span>{{ totalRecord }} diễn viên</span>
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
                    <div class="celebrity-items">
                        <template v-for="(actorItem, indexItem) in actorListGrid" :key="indexItem">
                            <div class="ceb-item" @click="btnShowActorSingle(actorItem.value)">
                                <a><img v-lazy="actorItem.imagePath" alt="" style="width: 270px; height: 400px" /></a>
                                <div class="ceb-infor">
                                    <h2>
                                        <a>{{ actorItem.fullname }}</a>
                                    </h2>
                                    <span>{{ actorItem.country }}</span>
                                </div>
                            </div>
                        </template>
                    </div>
                    <div class="noData" v-if="noData">Không có dữ liệu</div>
                    <div class="topbar-filter">
                        <label>Số diễn viên trên một trang:</label>
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
                <div class="col-md-3 col-xs-12 col-sm-12">
                    <div class="sidebar">
                        <div class="searh-form">
                            <h4 class="sb-title">Tìm kiếm năng cao</h4>
                            <form class="form-style-1 celebrity-form" action="#">
                                <div class="row">
                                    <div class="col-md-12 form-it">
                                        <label>Tên diễn viên</label>
                                        <input type="text" placeholder="Nhập từ khóa" />
                                    </div>
                                    <div class="col-md-12 form-it">
                                        <label>Giới tính</label>
                                        <div class="group-ip">
                                            <select name="skills" class="ui fluid dropdown" v-model="gender">
                                                <option
                                                    v-for="itemCbo in genderData"
                                                    :key="itemCbo"
                                                    :value="itemCbo.idGender"
                                                >
                                                    {{ itemCbo.genderName }}
                                                </option>
                                            </select>
                                            <font-awesome-icon
                                                style="position: absolute; right: 25px; color: #abb7c4"
                                                :icon="['fas', 'chevron-down']"
                                            />
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-it">
                                        <label>Năm sinh</label>
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
                                        <div class="submit" @click="btnFilterImproveActor">Lọc diễn viên</div>
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
    name: 'ActorGrid',

    watch: {
        perPageValue: function (value) {
            if (value) {
                this.callApiPaginationListActorGrid();
            }
        },
        currentPage: function (value) {
            if (value) {
                this.callApiPaginationListActorGrid();
            }
        },
        columnSort: function (value) {
            if (value) {
                this.callApiPaginationListActorGrid();
            }
        },
        actorListGrid: function (value) {
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

        callApiPaginationListActorGrid() {
            axios
                .get(
                    `${this.$MResource.API}/Actors/GetActorBySearchingImprove?pageNumber=${this.currentPage}&pageSize=${this.perPageValue}&keyword=${this.keyword}&gender=${this.gender}&startYear=${this.startYear}&endYear=${this.endYear}&columnSort=${this.columnSort}`,
                )
                .then((response) => {
                    this.actorListGrid = response.data.data;
                    this.totalPages = response.data.totalPages;
                    this.totalRecord = response.data.totalRecords;

                    this.actorListGrid.forEach((actorItem) => {
                        if (actorItem.imgByte != null && actorItem.imgByte.length > 0) {
                            actorItem.imagePath = 'data:image/png;base64,' + actorItem.imgByte;
                            // this.file = 'data:image/png;base64,' + this.movieInfor.imgByte;
                        }
                        if (actorItem.dateOfBirth != null || actorItem.dateOfBirth != undefined) {
                            actorItem.dateOfBirth = commonJS.formatDate(actorItem.dateOfBirth);
                        }
                    });
                })
                .catch((err) => {
                    // this.$MToastMessage({
                    //     titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    //     messageToast: err,
                    //     showToastMessage: true,
                    //     typeToast: 'errorToast',
                    // });
                    console.log('err: ', err);
                });
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

        btnFilterImproveActor() {
            if (this.startYear === 'range' || this.endYear === 'range') {
                return;
            }

            var isValid = this.checkFormSearchImprove();
            if (!isValid) {
                return;
            }

            this.callApiPaginationListActorGrid();
            this.keyword = '';
            this.gender = '';
            this.startYear = '';
            this.endYear = '';
            this.columnSort = '';
            // location.reload();
        },

        btnShowActorSingle(idActor) {
            this.$router.push({ name: 'actorsingle', params: { id: idActor } });
        },
    },
    created() {
        setTimeout(() => {
            this.showLoadingClient = false;
            this.perPageValue = 9;
            this.columnSort = 1;
            this.callApiPaginationListActorGrid();
        }, 1000);
    },

    data() {
        return {
            actorListGrid: [],

            currentPage: 1,
            perPageValue: 9,
            totalPages: 0,
            totalRecord: 0,
            dataCombobox: [
                {
                    valueCombobox: 9,
                    title: '9 diễn viên',
                },
                {
                    valueCombobox: 15,
                    title: '15 diễn viên',
                },
                {
                    valueCombobox: 30,
                    title: '30 diễn viên',
                },
            ],

            categoryList: [],
            yearSelect: [1950, 1960, 1970, 1980, 1990, 2000, 2010, 2020, 2023],
            keyword: '',
            gender: '',
            startYear: '',
            endYear: '',
            columnSort: 1,

            dataSort: [
                {
                    idSort: 1,
                    titleSort: 'Ngày sinh',
                },
                {
                    idSort: 2,
                    titleSort: 'Tên diễn viên',
                },
            ],

            genderData: [
                {
                    idGender: 0,
                    genderName: 'Nam',
                },
                {
                    idGender: 1,
                    genderName: 'Nữ',
                },
            ],

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
