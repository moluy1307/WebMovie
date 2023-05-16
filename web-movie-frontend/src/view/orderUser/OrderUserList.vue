<template>
    <div class="body-header">
        <div class="body-title">Danh sách yêu cầu</div>
    </div>
    <div class="body-content">
        <div class="body-content-top">
            <div class="top-left">
                <!-- <div style="display: flex; flex-direction: column">
                    <div style="margin-left: 16px" :class="{ isDisplay: displayInforFilter }">
                        {{ nameColumnFilter }}: {{ nameOperator }} "{{ otherKeyword }}" &nbsp;&nbsp;
                        <MIcon
                            nameIcon="closeFilter"
                            newStyle="position: relative; display: inline; width: 16px; height: 16px; background-color: transparent;"
                            :eventOnClick="unfilteredDataDetail"
                        ></MIcon>
                    </div>
                    <div style="max-width: 300px; margin-left: 16px" :class="{ isDisplay: displayCounterChecked }">
                        {{ this.$MResource.VI.IS_CHECKED }}
                        <span style="font-weight: 700">{{ numberEmployeeDelete }}</span>
                        <span style="color: #e61d1d; margin-left: 16px; cursor: pointer" @click="unCheckedOnClick">{{
                            this.$MResource.VI.UN_CHECKED
                        }}</span>
                    </div>
                </div>

                <div class="btns-action" :class="{ isDisplay: displayCounterChecked }">
                    <div class="btn-action-multiple" @click="btnShowDialogCOnfirm">
                        <MIcon
                            nameIcon="trash"
                            newStyle="width: 32px; height: 32px;margin-right: 4px"
                            :class="{ 'icon-size': true }"
                        ></MIcon>
                        <div class="content-btn">{{ this.$MResource.VI.DELETE_MULTIPLE }}</div>
                    </div>

                    <div class="btn-action-multiple" @click="btnExportById">
                        <MIcon
                            nameIcon="excelid"
                            newStyle="width: 32px; height: 32px; margin-right: 4px"
                            :class="{ 'icon-size': true }"
                        ></MIcon>
                        <div class="content-btn">{{ this.$MResource.VI.ACRONYM.EXPORT_TO_EXCEL }}</div>
                    </div>

                </div> -->
            </div>
            <div class="top-right">
                <div class="search-box">
                    <input type="text" v-model="keyword" placeHolder="Tìm kiếm theo tên tài khoản" />
                    <MIcon
                        nameIcon="search"
                        newStyle="width: 20px !important; height: 20px !important; top: 5px;
    right: 8px;"
                    ></MIcon>
                </div>

                <MIcon
                    nameIcon="reload"
                    :class="{ 'icon-size': true }"
                    :eventOnClick="onUpdate"
                    :tooltip="{
                        content: this.$MResource.VI.ACRONYM.RELOAD_DATA,
                        placement: 'bottom',
                        theme: 'dark',
                        arrow: false,
                    }"
                ></MIcon>

                <!-- <MIcon
                    nameIcon="excel"
                    :class="{ 'icon-size': true }"
                    :eventOnClick="onExport"
                    :tooltip="{
                        content: this.$MResource.VI.ACRONYM.EXPORT_TO_EXCEL,
                        placement: 'bottom',
                        theme: 'dark',
                        arrow: false,
                    }"
                ></MIcon> -->
            </div>
        </div>
        <div class="body-content-bottom" :key="tableReload">
            <MTable
                propValue="orderId"
                propValueCode="username"
                typeObject="Movies"
                propValueStatus="enable"
                :table="table"
                :hasWiget="true"
                :hasCheckbox="false"
                :titleTable="titleTableData"
                :api="`${this.$MResource.API}/OrderUsers/filter?keyword=${keyword}&pageNumber=${currentPage}&pageSize=${perPageValue}`"
                :key="tableReload"
                @totalNumberPages="totalPages"
                @totalRecords="totalRecords"
                @getEmployeeId="getId"
                :checkedRows="paymentDel"
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
        <div v-if="noData" class="body-no-data">
            <div class="content-no-data" style="text-align: center; margin-top: 150px">Không có dữ liệu</div>
        </div>
    </div>
    <MDialog
        :depict="`Bạn có thực sự muốn xóa yêu cầu của <${orderOfUser}> không?`"
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
export default {
    name: 'OrderUserList',
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
    },
    methods: {
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
                this.orderIdEdit = event;
                axios
                    .get(`${this.$MResource.API}/OrderUsers/UpdateMoney?orderId=${this.orderIdEdit}`)
                    .then(() => {
                        this.$MToastMessage({
                            titleToast: this.$MResource.VI.TOAST.TITLE_SUCCESS,
                            messageToast: 'Tài khoản của bạn đã được cập nhật thành công',
                            showToastMessage: true,
                            typeToast: 'successToast',
                        });
                        this.onUpdate();
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
                        console.log(err);
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

        getIdDel(idDel, nameRecord) {
            this.idDelete = idDel;
            this.orderOfUser = nameRecord;
            this.showConfirmDel = true;
        },

        btnDeleteOrderById() {
            try {
                const me = this;
                axios.delete(`${this.$MResource.API}/OrderUsers/DeleteOrder?recordId=${me.idDelete}`).then(() => {
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
    data() {
        return {
            tableReload: 0,
            reloadForm: 0,
            currentPage: 1,
            perPageValue: 10,
            allPage: 1,
            allRecord: 0,

            keyword: '',

            titleTableData: [
                {
                    dataField: 'orderName',
                    title: 'Tên yêu cầu',
                    className: 'text-left',
                    classTitle: 'column-230 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'true',
                    hasFilter: 'true',
                    // styleElement: 'element-col-150',
                },
                {
                    dataField: 'recharge',
                    title: 'Tiền nạp',
                    className: 'text-right ',
                    classTitle: ' column-150 text-right',
                    formatType: 'Number',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'username',
                    title: 'Tên người dùng',
                    className: 'text-left ',
                    classTitle: ' column-150 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'email',
                    title: 'Email',
                    className: 'column-230 text-left',
                    classTitle: 'column-230 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'fullName',
                    title: 'Tên khách hàng',
                    className: 'column-230 text-left',
                    classTitle: 'column-230 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'enable',
                    title: 'Trạng thái',
                    className: 'column-150 text-left',
                    classTitle: 'column-150 text-left',
                    formatType: 'Status',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
            ],

            orderIdEdit: null,

            idDelete: null,
            orderOfUser: null,
            showConfirmDel: false,
        };
    },
};
</script>

<style></style>
