<template>
    <div class="body-header">
        <div class="body-title">Danh sách phim</div>
    </div>
    <div class="master-detail">
        <div class="body-content-top">
            <div class="top-left">
                <div class="search-box">
                    <input type="text" v-model="keyword" placeHolder="Tìm kiếm theo số, tên tài khoản" />
                    <MIcon
                        nameIcon="search"
                        newStyle="width: 20px !important; height: 20px !important; top: 5px;
    right: 8px;"
                    ></MIcon>
                </div>
            </div>
            <div class="top-right">
                <MIcon
                    nameIcon="reload"
                    :class="{ 'icon-size': true }"
                    @click="onUpdate"
                    :tooltip="{
                        content: this.$MResource.VI.ACRONYM.RELOAD_DATA,
                        placement: 'bottom',
                        theme: 'dark',
                        arrow: false,
                    }"
                ></MIcon>
                <MButton label="Thêm mới" :class="{ 'btn-custom-default': true }" @click="btnAddOnClick"></MButton>
            </div>
        </div>
        <div class="master-section">
            <MTable
                propValue="movieId"
                propValueCode="movieCode"
                typeObject="Movies"
                :table="table"
                :hasWiget="true"
                :hasCheckbox="false"
                :titleTable="titleTableData"
                :api="`${this.$MResource.API}/Movies/filter?keyword=${keyword}&pageNumber=${currentPage}&pageSize=${perPageValue}`"
                :key="tableReload"
                @totalNumberPages="totalPages"
                @totalRecords="totalRecords"
                @getEmployeeId="getId"
                :checkedRows="paymentDel"
                @getMasterId="getMovieId"
                styleColumnFunction="width: 135px"
                @getIdDeleteRecord="getIdDel"
            ></MTable>
            <MPaging
                v-if="!noData"
                :totalPages="allPage"
                :total="allRecord"
                @perPage="getPerPage"
                :currentPage="currentPage"
                @pagechanged="onPageChange"
                :perPageTitle="perPageValue"
            ></MPaging>
        </div>
        <div class="divider-section">
            <MIcon nameIcon="arrow" newStyle="" :class="{ 'icon-extend-table': true }"></MIcon>
        </div>
        <div class="tab-title"><div class="tab-item">Chi tiết tập phim</div></div>
        <div class="detail-section" :key="tableReload">
            <MTable
                propValue="movieId"
                propValueCode="movieCode"
                typeObject="Accounts"
                propParentId="accountParentId"
                :table="table"
                :hasWiget="false"
                :hasCheckbox="false"
                :titleTable="titleTableDataDetail"
                :key="tableReload"
                @totalNumberPages="totalPages"
                @totalRecords="totalRecords"
                :detailEntity="true"
                :arraydetailEntity="arrayElement"
            ></MTable>
            <MPaging
                v-if="!noData"
                :totalPages="allPageDetail"
                :total="allRecordDetail"
                @perPage="getPerPageDetai"
                :currentPage="currentPageDetail"
                @pagechanged="onPageChangeDetai"
                :perPageTitle="perPageValueDetail"
            ></MPaging>
        </div>
    </div>
    <!-- <ul v-for="movie in movieList" :key="movie.movieId">
        <li>{{ movie.movieCode }}</li>
        <li @click="btnShowEdit(movie.movieId)">{{ movie.movieName }}</li>
    </ul> -->
    <MovieDetail
        @dataRecovery="onUpdate()"
        v-if="showMovieDetail"
        :id="movieEdit"
        @onClose="showMovieDetail = false"
    ></MovieDetail>

    <MDialog
        :depict="`Bạn có thực sự muốn xóa phim có mã <${codeDelete}> không? Hành động này sẽ xóa tất cả các tập phim và trailer của phim.`"
        v-if="showConfirmDel"
        dialogConfirm="true"
        :hasCloseButton="true"
        :hasCancelButton="{ 'btn-dialog-left': true }"
        typeDialog="warningDialog"
        @close-dialog="showConfirmDel = false"
        @click-action="btnDeleteOrderById"
        titleDialog="Xóa yêu cầu"
        titleButton="Có"
    ></MDialog>
</template>

<script>
import axios from 'axios';
import MovieDetail from './MovieDetail.vue';
export default {
    name: 'MovieList',
    components: { MovieDetail },
    watch: {
        currentPage: function (value) {
            if (value) {
                this.tableReload++;
            }
        },
        perPageValue: function (value) {
            console.log('Thay doi pageSize: ', value);
            this.onUpdate();
        },
        keyword: function (value) {
            this.tableReload++;
            if (value.length <= 1) {
                this.tableReload++;
            }
        },

        currentPageDetail: function (value) {
            if (value) {
                this.callApiMovieDetail(this.movieForeignId, value, this.currentPageDetail);
            }
        },
        perPageValueDetail: function (value) {
            if (value) {
                this.callApiMovieDetail(this.movieForeignId, this.currentPageDetail, value);
            }
        },
    },
    methods: {
        btnAddOnClick() {
            this.movieEdit = null;
            this.showMovieDetail = true;
        },
        btnShowEdit(id) {
            this.movieEdit = id;
            this.showMovieDetail = true;
        },

        /**
         * Sự kiện các trang thay đổi trong phần paging
         * CreatedBy: huynq (16/1/2023)
         * @param {*} page
         */
        onPageChange(page) {
            try {
                this.currentPage = page;
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
         * Trả về số bản ghi khi chọn trong combobox paging
         * CreatedBy: huynq (30/1/2023)
         * @param {*} event
         */
        getPerPage(event) {
            try {
                this.perPageValue = event;
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
         * Trả về tổng số trang sau khi lựa chọn số bản ghi xuất hiện trong 1 trang
         * CreatedBy: huynq (30/1/2023)
         * @param {*} event
         */
        totalPages(event) {
            try {
                this.allPage = event;
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
         * Trả về tổng số bản ghi có trong api
         * CreatedBy: huynq (30/1/2023)
         * @param {*} event
         */
        totalRecords(event) {
            try {
                this.allRecord = event;
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
         * Load lại dữ liệu table
         * CreatedBy: huynq (13/1/2022)
         */
        onUpdate() {
            try {
                this.tableReload++;
                this.currentPage = 1;
                this.keyword = '';
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
         * Nhận lại id để truyền dữ liệu và hiển thị lên form edit
         * CreatedBy: huynq (2/2/2023)
         * @param {*} event
         */
        getId(event) {
            try {
                this.movieEdit = event;
                this.showMovieDetail = true;
            } catch (err) {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            }
        },

        getMovieId(masterId) {
            this.movieForeignId = masterId;
            console.log('idn nhan duoc: ', this.movieForeignId);
            this.callApiMovieDetail(masterId, this.currentPageDetail, this.perPageValueDetail);
            // axios
            //     .get(
            //         `${this.$MResource.API}/Episodes/GetEpisodeByMovieId?movieId=${masterId}&pageNumber=${this.currentPageDetail}&pageSize=${this.perPageValueDetail}`,
            //     )
            //     .then((response) => {
            //         this.arraydetailEntity = response.data;
            //         this.arrayElement = this.arraydetailEntity.data;
            //         this.allPageDetail = this.arraydetailEntity.totalPages;
            //         this.allRecordDetail = this.arraydetailEntity.totalRecords;
            //     })
            //     .catch((err) => {
            //         this.$MToastMessage({
            //             titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
            //             messageToast: err,
            //             showToastMessage: true,
            //             typeToast: 'errorToast',
            //         });
            //     });
        },

        callApiMovieDetail(idMaster, curr, per) {
            axios
                .get(
                    `${this.$MResource.API}/Episodes/GetEpisodeByMovieId?movieId=${idMaster}&pageNumber=${curr}&pageSize=${per}`,
                )
                .then((response) => {
                    this.arraydetailEntity = response.data;
                    this.arrayElement = this.arraydetailEntity.data;
                    this.allPageDetail = this.arraydetailEntity.totalPages;
                    this.allRecordDetail = this.arraydetailEntity.totalRecords;
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
         * Sự kiện các trang thay đổi trong phần paging detail
         * CreatedBy: huynq (16/1/2023)
         * @param {*} page
         */
        onPageChangeDetai(page) {
            try {
                this.currentPageDetail = page;
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
         * Trả về số bản ghi khi chọn trong combobox paging detail
         * CreatedBy: huynq (30/1/2023)
         * @param {*} event
         */
        getPerPageDetai(event) {
            try {
                this.perPageValueDetail = event;
            } catch (err) {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            }
        },

        getIdDel(idDel, nameRecord) {
            this.idDelete = idDel;
            this.codeDelete = nameRecord;
            this.showConfirmDel = true;
            console.log('ma xoa: ', idDel);
        },

        btnDeleteOrderById() {
            try {
                const me = this;
                axios.delete(`${this.$MResource.API}/Movies/DeleteMovie?movieId=${me.idDelete}`).then(() => {
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_SUCCESS,
                        messageToast: this.$MResource.VI.TOAST.DELETE_SUCCESS,
                        showToastMessage: true,
                        typeToast: 'successToast',
                    });
                    this.showConfirmDel = false;
                    me.onUpdate();
                });
            } catch (err) {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            }
        },
    },
    created() {
        axios
            .get(`${this.$MResource.API}/Movies/GetAllMovie`)
            .then((response) => {
                this.movieList = response.data;
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
    data() {
        return {
            showMovieDetail: false,
            movieList: [],
            movieEdit: null,

            tableReload: 0,
            reloadForm: 0,
            currentPage: 1,
            perPageValue: 10,
            allPage: 1,
            allRecord: 0,

            keyword: '',

            showForm: false,

            paymentDel: [],

            paymentWatch: null,

            movieForeignId: null,
            arraydetailEntity: [],
            arrayElement: [],
            currentPageDetail: 1,
            perPageValueDetail: 10,
            allPageDetail: 1,
            allRecordDetail: 0,

            titleTableData: [
                {
                    dataField: 'movieCode',
                    title: 'Mã phim',
                    className: 'text-left text-center',
                    classTitle: 'column-150-imp  text-center',
                    formatType: 'text',
                    checkIsEnableSort: 'true',
                    hasFilter: 'true',
                    // styleElement: 'element-col-150',
                },
                {
                    dataField: 'movieName',
                    title: 'Tên phim',
                    className: 'text-left ',
                    classTitle: ' column-230-imp  text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'movieInfor',
                    title: 'Nội dung',
                    className: 'text-left ',
                    classTitle: ' column-230-imp  text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'movieDirector',
                    title: 'Đạo diễn',
                    className: 'column-150-imp  text-left',
                    classTitle: 'column-150-imp  text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'movieReview',
                    title: 'Lượt xem',
                    className: 'column-150-imp  text-right',
                    classTitle: 'column-150-imp  text-right',
                    formatType: 'Number',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'movieTime',
                    title: 'Thời lượng',
                    className: 'column-150-imp  text-left',
                    classTitle: 'column-150-imp  text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'movieArea',
                    title: 'Quốc gia',
                    className: 'column-150-imp  text-left',
                    classTitle: 'column-150-imp  text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'dateOfManufactor',
                    title: 'Ngày sản xuất',
                    className: 'text-left text-center',
                    classTitle: 'column-150-imp  text-center',
                    formatType: 'Date',
                    checkIsEnableSort: 'true',
                    hasFilter: 'true',
                    // styleElement: 'element-col-150',
                },
                {
                    dataField: 'genreNames',
                    title: 'Thể loại',
                    className: 'text-left',
                    classTitle: 'column-230-imp  text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'actorNames',
                    title: 'Diễn viên',
                    className: 'text-left',
                    classTitle: 'column-230-imp  text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
            ],
            titleTableDataDetail: [
                {
                    dataField: 'serial',
                    title: '#',
                    className: 'column-64',
                    classTitle: 'column-64',
                    formatType: 'text',
                    isColumnSerial: 'true',
                    // styleElement: 'element-col-150',
                },
                {
                    dataField: 'episodeName',
                    title: 'Tên bộ phim',
                    className: 'text-left',
                    classTitle: 'column-150 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },

                {
                    dataField: 'episodeNumber',
                    title: 'Số tập',
                    className: 'column-150 text-right',
                    classTitle: 'column-150 text-right',
                    formatType: 'Number',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'episodeUrl',
                    title: 'Đường dẫn video',
                    className: 'text-left ',
                    classTitle: ' column-230 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
            ],

            idDelete: null,
            codeDelete: null,
            showConfirmDel: false,
        };
    },
};
</script>

<style scoped>
@import url('./movie.css');
</style>
