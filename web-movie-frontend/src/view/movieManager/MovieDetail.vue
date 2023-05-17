<template>
    <div class="modal-pay-money" @click.self="$emit('onClose')">
        <div class="form-pay">
            <div class="form-header form-pay-header">
                <div class="frm-header-left" style="column-gap: 12px">
                    <div class="frm-title name-payment">
                        <font-awesome-icon :icon="['fas', 'file-video']" />
                        Thông tin phim
                    </div>
                    <div class="w-350">
                        <MDropdownList
                            typeCbo="dropdown"
                            propValue="valueCombobox"
                            propName="title"
                            v-model="movieInfor.typeMovie"
                            :dataCombobox="dataComboboxReasonType"
                            :newClassBox="{ 'dropdown-reason': true }"
                            :hasValueDefault="true"
                            :valueDefault="dataComboboxReasonType[0].valueCombobox"
                        ></MDropdownList>
                    </div>
                </div>
                <div class="top-right">
                    <!-- <div class="instruction">
                        <MIcon
                            nameIcon="instruction"
                            newStyle="position: relative; background-color: transparent"
                        ></MIcon>
                        <div class="title-instruction">{{ this.$MResource.VI.BUTTONS.INSTRUCTION }}</div>
                    </div>
                    <div
                        v-tippy="{
                            content: this.$MResource.VI.ACRONYM.CUSTOM_UI,
                            placement: 'bottom',
                            theme: 'dark',
                            arrow: true,
                        }"
                        class="icon-size"
                        style="cursor: pointer"
                    >
                        <MIcon
                            nameIcon="setting"
                            :class="{ 'icon-size': true }"
                            newStyle="position: relative; background-color: transparent"
                        ></MIcon>
                    </div>
                    <div
                        v-tippy="{
                            content: this.$MResource.VI.ACRONYM.HELP,
                            placement: 'bottom',
                            theme: 'dark',
                            arrow: true,
                        }"
                        class="icon-size"
                        style="cursor: pointer"
                    >
                        <MIcon
                            nameIcon="help"
                            :class="{ 'icon-size': true }"
                            newStyle="position: relative; background-color: transparent"
                        ></MIcon>
                    </div> -->
                    <div
                        v-tippy="{
                            content: this.$MResource.VI.ACRONYM.CLOSE,
                            placement: 'bottom',
                            theme: 'dark',
                            arrow: true,
                        }"
                        @click="btnCloseOrChangeData"
                        class="icon-size"
                        style="cursor: pointer"
                    >
                        <MIcon
                            nameIcon="close"
                            :class="{ 'icon-size': true }"
                            newStyle="position: relative; background-color: transparent"
                        ></MIcon>
                    </div>
                </div>
            </div>
            <div class="main-information">
                <div class="basic-information">
                    <div class="right-information">
                        <div class="right-line-information">
                            <div class="fields-information">
                                <div class="w-350">
                                    <!-- <MDropdownList
                                        :isTableCbo="true"
                                        :label="this.$MResource.VI.TAB_PROCEDURE.PAYMENT.OBJECT_CODE"
                                        typeCbo="dropdown"
                                        propValue="employeeId"
                                        propName="employeeCode"
                                        :newClassBox="{ 'table-cbo': true, 'combobox-object': true }"
                                        v-model="paymentInfor.employeeId"
                                        styleCbo="width: 100%"
                                        :titleTable="titleTableObject"
                                        :api="`${this.$MResource.LOCALHOST}/Employees`"
                                        ref="comboboxObject"
                                        :idObject="paymentInfor.employeeId"
                                    ></MDropdownList> -->
                                    <!-- <MInput
                                        ref="firstInput"
                                        v-model="paymentInfor.employeeCode"
                                        :labelInput="this.$MResource.VI.TAB_PROCEDURE.PAYMENT.OBJECT_CODE"
                                    ></MInput> -->
                                    <MInput
                                        labelInput="Mã phim"
                                        v-model="movieInfor.movieCode"
                                        propTabindex="0"
                                        ref="refMovieCode"
                                        compulsory="*"
                                        styleCompulsory="margin-left: 5px;"
                                        :newClass="!errorField.code ? '' : 'input-error'"
                                        :tooltipError="errorField.code"
                                        errorMessage="Mã không được để trống"
                                    ></MInput>
                                </div>
                                <div class="w-350">
                                    <MInput
                                        labelInput="Tên phim"
                                        v-model="movieInfor.movieName"
                                        propTabindex="0"
                                        compulsory="*"
                                        styleCompulsory="margin-left: 5px;"
                                        ref="refMovieName"
                                        :newClass="!errorField.name ? '' : 'input-error'"
                                        :tooltipError="errorField.name"
                                        errorMessage="Tên không được để trống"
                                    ></MInput>
                                </div>
                                <div class="field-flex">
                                    <!-- <MInput
                                        labelInput="Thể loại"
                                        v-model="movieInfor.movieCode"
                                        propTabindex="0"
                                    ></MInput> -->
                                    <label>Thể loại</label>
                                    <Multiselect
                                        :close-on-select="false"
                                        :searchable="true"
                                        v-model="valueCategory"
                                        mode="tags"
                                        :options="categoryList"
                                        label="categoryName"
                                        trackBy="categoryName"
                                    />
                                </div>
                            </div>
                            <div class="fields-information">
                                <div class="w-350">
                                    <MInput
                                        labelInput="Đạo diễn"
                                        v-model="movieInfor.movieDirector"
                                        propTabindex="0"
                                    ></MInput>
                                </div>
                                <div class="w-350">
                                    <MInput
                                        labelInput="Thời lượng"
                                        v-model="movieInfor.movieTime"
                                        propTabindex="0"
                                    ></MInput>
                                </div>
                                <div class="field-flex">
                                    <label>Diễn viên</label>
                                    <Multiselect
                                        :close-on-select="false"
                                        :searchable="true"
                                        v-model="valueActor"
                                        mode="tags"
                                        :options="actorList"
                                        label="fullname"
                                        trackBy="fullname"
                                    />
                                </div>
                            </div>
                            <div class="fields-information">
                                <div class="field-flex">
                                    <MInput
                                        labelInput="Nội dung phim"
                                        v-model="movieInfor.movieInfor"
                                        propTabindex="0"
                                    ></MInput>
                                </div>
                            </div>
                            <div class="fields-information">
                                <div class="w-350">
                                    <!-- <MDropdownList
                                        :isTableCbo="true"
                                        :label="this.$MResource.VI.MENU.EMPLOYEE"
                                        typeCbo="dropdown"
                                        propValue="employeeId"
                                        propName="fullName"
                                        :newClassBox="{ 'table-cbo': true, 'combobox-object': true }"
                                        v-model="paymentInfor.employeeId"
                                        styleCbo="width: 100%"
                                        :titleTable="titleTableEmployee"
                                        :api="`${this.$MResource.LOCALHOST}/Employees`"
                                        ref="comboboxObject"
                                        :idObject="paymentInfor.employeeId"
                                    ></MDropdownList> -->
                                    <!-- <MInput
                                        v-model="paymentInfor.fullName"
                                        :labelInput="this.$MResource.VI.MENU.EMPLOYEE"
                                    ></MInput> -->
                                    <MInput
                                        labelInput="Quốc gia"
                                        v-model="movieInfor.movieArea"
                                        propTabindex="0"
                                    ></MInput>
                                </div>
                                <div class="w-350">
                                    <MInput labelInput="Số tiền" propTabindex="0" v-model="movieInfor.amount"></MInput>
                                </div>
                                <div class="field-flex">
                                    <div class="infor">
                                        <label for="txtDate">Ngày sản xuất</label>
                                        <div class="m-row">
                                            <v-date-picker
                                                color="green"
                                                title-position="left"
                                                v-model="movieInfor.dateOfManufactor"
                                            >
                                                <template #default="{ inputValue, inputEvents }">
                                                    <!-- <input class="px-3 py-1 border rounded" :value="inputValue" @focusin="togglePopover()" /> -->
                                                    <div
                                                        style="
                                                            position: relative;
                                                            width: 150px;
                                                            height: 32px;
                                                            display: flex;
                                                            align-items: center;
                                                        "
                                                    >
                                                        <input
                                                            class="input-date"
                                                            :value="inputValue"
                                                            v-on="inputEvents"
                                                            placeholder="DD/MM/YYYY"
                                                            tabindex="0"
                                                        />
                                                        <div class="icon-date">
                                                            <div class="icon-date-content"></div>
                                                        </div>
                                                    </div>
                                                </template>
                                            </v-date-picker>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="left-line-information">
                            <div style="display: flex; flex-direction: column; row-gap: 22px; flex: 1">
                                <img
                                    v-lazy="movieInfor.imagePath"
                                    alt="Ảnh minh họa"
                                    style="width: 220px; height: 200px"
                                />

                                <div class="button-wrapper">
                                    <span class="label"> Chọn tệp </span>

                                    <input
                                        type="file"
                                        ref="fileInput"
                                        @change="onFileChange($event)"
                                        id="upload"
                                        class="upload-box"
                                        placeholder="Upload File"
                                    />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="total-money">
                        <div class="title-total">Lượt xem</div>
                        <div class="value-money">{{ movieInfor.movieReview }}</div>
                    </div>
                </div>
            </div>
            <div class="body-detail">
                <div class="body-content box-episode" style="height: auto">
                    <div class="body-content-top text-title" style="padding-left: 20px; z-index: 1">
                        Danh sách tập phim
                    </div>
                    <div class="body-content-bottom body-table-episode" :key="tableReload">
                        <MTable
                            :payDetail="true"
                            :hasWiget="false"
                            :hasCheckbox="false"
                            :titleTable="titleTableData"
                            :orderNumber="orderNumber"
                            :arrayRowTable="movieInfor.episodes"
                            @indexDelRow="delRowTable"
                            @newValueEp="arrayEpisodes"
                        ></MTable>
                    </div>
                    <div class="control-row-table">
                        <MButton
                            @click="btnAddRowTable"
                            :class="{ 'btn-custom-default': true }"
                            :label="this.$MResource.VI.BUTTONS.ADD_ROW"
                            ref="btnSave"
                            style="margin-right: 8px"
                        ></MButton
                        ><MButton
                            @click="btnDelAllRow"
                            :class="{ 'btn-custom-default': true, 'btn-bg': true }"
                            :label="this.$MResource.VI.BUTTONS.DELETE_ALL_ROW"
                            ref="btnSave"
                        ></MButton>
                    </div>
                </div>
                <div class="body-content box-trailer" style="height: auto">
                    <div class="body-content-top text-title" style="padding-left: 20px; z-index: 1">
                        Danh sách trailer
                    </div>
                    <div class="body-content-bottom body-table-trailer" :key="tableReload">
                        <MTable
                            :payDetail="true"
                            :hasWiget="false"
                            :hasCheckbox="false"
                            :titleTable="titleTableDataTrailer"
                            :orderNumber="orderNumber"
                            :arrayRowTable="movieInfor.trailers"
                            @indexDelRow="delRowTableTrailer"
                            @newValueEp="arrayTrailers"
                        ></MTable>
                    </div>
                    <div class="control-row-table">
                        <MButton
                            @click="btnAddRowTableTrailer"
                            :class="{ 'btn-custom-default': true }"
                            :label="this.$MResource.VI.BUTTONS.ADD_ROW"
                            ref="btnSave"
                            style="margin-right: 8px"
                        ></MButton
                        ><MButton
                            @click="btnDelAllRowTrailer"
                            :class="{ 'btn-custom-default': true, 'btn-bg': true }"
                            :label="this.$MResource.VI.BUTTONS.DELETE_ALL_ROW"
                            ref="btnSave"
                        ></MButton>
                    </div>
                </div>
            </div>
            <div class="btn-group form-pay-footer">
                <!-- <button @click="btnCloseOnClick" class="btn btn-bg">Hủy</button> -->
                <MButton
                    @click="btnCloseOnClick"
                    :class="{ 'btn-custom-default': true, 'btn-bg': true, 'btn-form-pay ': true }"
                    label="Hủy"
                ></MButton>
                <div class="btn-left">
                    <!-- <button class="btn btn-bg" @click.prevent="btnSaveOnClick">Cất</button> -->
                    <MButton
                        @click="btnSaveOnClick"
                        :class="{ 'btn-custom-default': true }"
                        label="Cất"
                        ref="btnSave"
                    ></MButton>
                    <!-- <button class="btn" onclick="keepadd()">Cất và Thêm</button> -->
                    <!-- <MButton
                        :class="{ btn: true }"
                        @click="btnAddAndSaveOnClick"
                        label="Cất và Thêm"
                        ref="btnLastFocus"
                        @keydown.tab="focusFirst"
                        @keydown.shift.tab="focusPerButton"
                    ></MButton> -->
                </div>
            </div>
        </div>
    </div>
    <div v-if="showDialog">
        <MDialog
            @close-dialog="showDialog = false"
            v-for="(item, index) in error"
            :key="index"
            :depict="item"
            dialogConfirm="false"
            :hasCloseButton="false"
            :hasCancelButton="{ 'btn-dialog-left': true }"
            typeDialog="errorDialog"
            titleDialog="Thông báo"
            titleButton="Đóng"
        ></MDialog>
    </div>
    <div v-else-if="showDialogWarning">
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
</template>

<script>
import axios from 'axios';
import Multiselect from '@vueform/multiselect';
import commonJS from '@/js/common';

// import moment from 'moment';
export default {
    name: 'MovieDetail',
    props: ['id'],
    components: {
        Multiselect,
    },
    watch: {
        'movieInfor.movieCode': function (value) {
            if (value.length <= 0) {
                this.errorField.code = true;
                this.$refs.refMovieCode.$refs.txtInput.classList.add('input-error');
                // this.$refs.refUsername.$refs.txtInput.focus();
            } else {
                this.$refs.refMovieCode.$refs.txtInput.classList.remove('input-error');
                this.errorField.code = false;
            }
        },
        'movieInfor.movieName': function (value) {
            if (value.length <= 0) {
                this.errorField.name = true;
                this.$refs.refMovieName.$refs.txtInput.classList.add('input-error');
                // this.$refs.reffullname.$refs.txtInput.focus();
            } else {
                this.$refs.refMovieName.$refs.txtInput.classList.remove('input-error');
                this.errorField.name = false;
            }
        },
    },
    computed: {
        isAdd() {
            if (this.id) {
                return false;
            } else {
                return true;
            }
        },

        // formattedValue() {
        //     // if (!this.inputValue) {
        //     //     return '';
        //     // }
        //     // const value = parseInt(this.inputValue);
        //     // const minutes = Math.floor(value / 60);
        //     // const seconds = value % 60;
        //     // return `${minutes}:${seconds < 10 ? '0' : ''}${seconds}`;

        //     // const inputValue = this.inputValue.trim();
        //     // if (!inputValue) {
        //     //     return '';
        //     // }
        //     // const seconds = parseInt(inputValue);
        //     // const minutes = Math.floor(seconds / 60);
        //     // const remainingSeconds = seconds % 60;
        //     // return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;

        //     // if (this.inputValue !== null) {
        //     //     const seconds = parseInt(this.inputValue);
        //     //     return moment().startOf('day').seconds(seconds).format('m:ss');
        //     // }
        //     // return '';

        //     // if (this.inputValue.length === 3) {
        //     //     const minutes = this.inputValue.charAt(0);
        //     //     const seconds = this.inputValue.substr(1);
        //     //     return `${minutes}:${seconds}`;
        //     // }
        //     // return this.inputValue;

        //     const input = this.inputValue.replace(/[^\d]/g, '');
        //     const minutes = input.substr(0, 2);
        //     const seconds = input.substr(2, 2);
        //     return `${minutes}:${seconds}`;
        // },
    },

    methods: {
        formatInput() {
            const input = this.inputValue.replace(/[^\d]/g, '');
            const minutes = input.substr(0, 2);
            const seconds = input.substr(2, 2);
            this.inputValue = `${minutes}:${seconds}`;
        },

        deleteValue(event) {
            if (event.keyCode === 8) {
                event.preventDefault();
                const input = this.inputValue.replace(/[^\d]/g, '');
                const newInput = input.slice(0, -1);
                const minutes = newInput.substr(0, 2);
                const seconds = newInput.substr(2, 2);
                this.inputValue = `${minutes}:${seconds}`;
            }
        },

        btnAddAndSaveOnClick() {
            // console.log('movie category: ', this.movieInfor.categories);
            // for (let test of this.movieInfor.categories) {
            //     console.log('gia tri value lay duoc: ', test);
            //     this.addValue.push({ value: test });
            //     console.log('gia tri push: ', this.addValue);
            // }
            for (let test of this.valueCategory) {
                console.log('gia tri value lay duoc: ', test);
                this.movieInfor.categories.push({ value: test });
                console.log('gia tri duoc push : ', this.movieInfor.categories);
            }
        },
        onFileChange(event) {
            this.file = event.target.files[0];
            this.movieInfor.imagePath = URL.createObjectURL(this.file);
        },

        /**
         * Kiểm tra dữ liệu bắt buộc nhập
         * <br/>
         * CreatedBy: huynq (5/1/2023)
         */
        checkForm() {
            try {
                this.error = [];
                var isValid = true;

                if (!this.movieInfor.movieName) {
                    isValid = false;
                    this.error.push('Tên phim không được để trống');
                    this.showDialog = true;
                    this.errorField.name = true;
                    // this.$refs.fullnameInput.$refs.txtInput.focus();
                }

                if (!this.movieInfor.movieCode) {
                    isValid = false;
                    this.error.push('Mã phim không được để trống');
                    this.showDialog = true;
                    this.errorField.code = true;
                }

                return isValid;
            } catch (err) {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            }
        },
        btnSaveOnClick() {
            var me = this;

            var isValid = me.checkForm();
            if (!isValid) {
                return;
            }

            const formData = new FormData();
            me.movieInfor.categories = [];
            me.movieInfor.actors = [];
            for (let test of this.valueCategory) {
                // console.log('gia tri value lay duoc: ', test);
                this.movieInfor.categories.push({ value: test });
                // console.log('gia tri duoc push : ', JSON.stringify(this.movieInfor.categories));
            }

            for (let test of this.valueActor) {
                // console.log('gia tri value lay duoc: ', test);
                this.movieInfor.actors.push({ value: test });
                // console.log('gia tri duoc push : ', JSON.stringify(this.movieInfor.categories));
            }

            formData.append('movieCode', this.movieInfor.movieCode);
            formData.append('movieName', this.movieInfor.movieName);
            formData.append('movieInfor', this.movieInfor.movieInfor);
            formData.append('movieDirector', this.movieInfor.movieDirector);
            if (this.movieInfor.movieReview != null) {
                formData.append('movieReview', this.movieInfor.movieReview);
            } else {
                formData.append('movieReview', 0);
            }
            if (this.movieInfor.dateOfManufactor != null && this.movieInfor.dateOfManufactor != undefined) {
                // const formattedDate = commonJS.bindingFormatDate(this.movieInfor.dateOfManufactor);
                // if (isNaN(Date.parse(formattedDate)) == true) {
                //     this.movieInfor.dateOfManufactor = '';
                // } else {
                //     this.movieInfor.dateOfManufactor = formattedDate;
                // }

                this.movieInfor.dateOfManufactor = commonJS.bindingFormatDate(this.movieInfor.dateOfManufactor);

                formData.append('dateOfManufactor', this.movieInfor.dateOfManufactor);
            }

            formData.append('movieArea', this.movieInfor.movieArea);
            formData.append('movieTime', this.movieInfor.movieTime);
            formData.append('amount', this.movieInfor.amount);
            formData.append('typeMovie', this.movieInfor.typeMovie);

            formData.append('cateMovie', JSON.stringify(this.movieInfor.categories));
            formData.append('actorMovie', JSON.stringify(this.movieInfor.actors));
            formData.append('episodeMovie', JSON.stringify(this.movieInfor.episodes));
            formData.append('trailerMovie', JSON.stringify(this.movieInfor.trailers));

            if (this.$refs.fileInput.files.length > 0) {
                console.log('da co duong dan link image', this.$refs.fileInput.files[0]);
                // formData.append('image', this.$refs.fileInput.files[0]);
                formData.append('image', this.file);
            }
            if (me.isAdd) {
                axios
                    .post('https://localhost:7112/api/v1/Movies/PostMovie', formData, {
                        headers: { 'Content-Type': 'multipart/form-data' },
                    })
                    .then(() => {
                        this.$MToastMessage({
                            titleToast: this.$MResource.VI.TOAST.TITLE_SUCCESS,
                            messageToast: this.$MResource.VI.TOAST.ADD_SUCCESS,
                            showToastMessage: true,
                            typeToast: 'successToast',
                        });
                        me.$emit('onClose');
                        me.$emit('dataRecovery');
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
                                if (userMsg != null || userMsg != undefined) {
                                    userMsg.forEach((element) => {
                                        this.showDialogWarning = true;
                                        this.textWarning = element;
                                    });
                                }

                                break;
                            default:
                                break;
                        }
                        console.log(err);
                    });
            } else {
                axios
                    .put(`${this.$MResource.API}/Movies/UpdateMovie?movieId=${me.id}`, formData, {
                        headers: { 'Content-Type': 'multipart/form-data' },
                    })
                    .then(() => {
                        this.$MToastMessage({
                            titleToast: this.$MResource.VI.TOAST.TITLE_SUCCESS,
                            messageToast: 'Sửa thông tin phim thành công',
                            showToastMessage: true,
                            typeToast: 'successToast',
                        });
                        me.$emit('onClose');
                        me.$emit('dataRecovery');
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
            }
        },

        /**
         * Thêm hàng mới vào bảng chi tiết
         */
        btnAddRowTable() {
            this.movieInfor.episodes.push({});
            console.log('btn them dong');
        },

        /**
         * Xóa 1 hàng
         * @param {*} indexDel
         */
        delRowTable(indexDel) {
            this.movieInfor.episodes.splice(indexDel, 1);
        },

        /**
         * Xóa tất cả các dòng
         */
        btnDelAllRow() {
            this.movieInfor.episodes = [{}];
        },

        /**
         * Nhận mảng các bộ phim đã được thay đổi
         * @param {*} arr
         */
        arrayEpisodes(arr) {
            this.movieInfor.episodes = arr;
        },

        //Table trailer
        /**
         * Thêm hàng mới vào bảng chi tiết trailer
         */
        btnAddRowTableTrailer() {
            this.movieInfor.trailers.push({});
        },

        /**
         * Xóa 1 hàng trailer
         * @param {*} indexDel
         */
        delRowTableTrailer(indexDel) {
            this.movieInfor.trailers.splice(indexDel, 1);
        },

        /**
         * Xóa tất cả các dòng trailer
         */
        btnDelAllRowTrailer() {
            this.movieInfor.trailers = [{}];
        },

        /**
         * Nhận mảng các bộ phim đã được thay đổi trailer
         * @param {*} arr
         */
        arrayTrailers(arr) {
            this.movieInfor.trailers = arr;
        },

        btnCloseOrChangeData() {
            this.$emit('onClose');
        },
    },

    mounted() {
        axios.get(`${this.$MResource.API}/Categories`).then((response) => {
            this.categoryList = response.data;
        });
        axios.get(`${this.$MResource.API}/Actors`).then((response) => {
            this.actorList = response.data;
        });
    },
    created() {
        var me = this;
        if (me.id) {
            axios
                .get(`${this.$MResource.API}/Movies/GetMovieById?movieId=${me.id}`)
                .then((response) => {
                    me.movieInfor = response.data;
                    // if (this.movieInfor.imagePath) {
                    //     console.log('co hinh anh');
                    //     const blob = fetch(`data:image/png;base64,${this.movieInfor.imagePath}`).then((res) =>
                    //         res.blob(),
                    //     );
                    //     this.movieInfor.imagePath = URL.createObjectURL(blob);
                    // }
                    // me.movieInfor.imagePath = `@Url.Content("~/Images/${response.data.imagePath}")`;
                    if (me.movieInfor.dateOfManufactor != null || me.movieInfor.dateOfManufactor != undefined) {
                        me.movieInfor.dateOfManufactor = commonJS.bindingFormatDate(me.movieInfor.dateOfManufactor);
                    }
                    if (this.movieInfor.imgByte != null && this.movieInfor.imgByte.length > 0) {
                        this.movieInfor.imagePath = 'data:image/png;base64,' + this.movieInfor.imgByte;
                        this.file = 'data:image/png;base64,' + this.movieInfor.imgByte;
                    }
                    if (this.movieInfor.categories != null) {
                        this.movieInfor.categories.forEach((element) => {
                            this.valueCategory.push(element.value);
                        });
                    }
                    if (this.movieInfor.actors != null) {
                        this.movieInfor.actors.forEach((element) => {
                            this.valueActor.push(element.value);
                        });
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
        } else {
            me.movieInfor = {
                movieCode: '',
                movieName: '',
                movieInfor: '',
                movieDirector: '',
                movieReview: 0,
                movieTime: '',
                dateOfManufactor: '',
                movieArea: '',
                amount: 0,
                categories: [],
                cateMovie: '',
                actorMovie: '',
                actors: [],
                typeMovie: 1,
                episodes: [{ episodeName: '', episodeNumber: '', episodeUrl: '' }],
                episodeMovie: '',
                trailers: [{ trailerName: '', trailerUrl: '', trailerTime: '' }],
                trailerMovie: '',
            };
        }
    },
    data() {
        return {
            inputValue: '',
            movieInfor: {
                movieCode: '',
                movieName: '',
                movieInfor: '',
                movieDirector: '',
                movieReview: 0,
                movieTime: '',
                dateOfManufactor: '',
                movieArea: '',
                amount: 0,
                categories: [],
                cateMovie: '',
                actorMovie: '',
                actors: [],
                typeMovie: 0,
                episodes: [{ episodeName: '', episodeNumber: '', episodeUrl: '' }],
                episodeMovie: '',
                trailers: [{ trailerName: '', trailerUrl: '', trailerTime: '' }],
                trailerMovie: '',
            },
            file: null,

            valueCategory: [],
            categoryList: [],

            valueActor: [],
            actorList: [],

            titleTableData: [
                {
                    dataField: 'episodeUrl',
                    title: 'Đường dẫn video',
                    className: 'text-left',
                    classTitle: 'column-230 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'true',
                    hasFilter: 'true',
                    // styleElement: 'element-col-150',
                },
                {
                    dataField: 'episodeName',
                    title: 'Tên tập phim',
                    className: 'text-left',
                    classTitle: 'column-150 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'true',
                    hasFilter: 'true',
                    // styleElement: 'element-col-230',
                },
                {
                    dataField: 'episodeNumber',
                    title: 'Số tập',
                    className: 'text-left ',
                    classTitle: ' column-230 text-left',
                    formatType: 'Number',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
            ],

            titleTableDataTrailer: [
                {
                    dataField: 'trailerUrl',
                    title: 'Đường dẫn video',
                    className: 'text-left',
                    classTitle: 'column-230 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'true',
                    hasFilter: 'true',
                    // styleElement: 'element-col-150',
                },
                {
                    dataField: 'trailerName',
                    title: 'Tên trailer',
                    className: 'text-left',
                    classTitle: 'column-150 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'true',
                    hasFilter: 'true',
                    // styleElement: 'element-col-230',
                },
                {
                    dataField: 'trailerTime',
                    title: 'Thời lượng',
                    className: 'text-left ',
                    classTitle: ' column-230 text-left',
                    formatType: 'Number',
                    checkIsEnableSort: 'false',
                    hasInputTime: 'true',
                    // styleElement: 'column-150',
                },
            ],
            dataComboboxReasonType: [
                {
                    valueCombobox: 1,
                    title: 'Phim bộ',
                },
                {
                    valueCombobox: 2,
                    title: 'Phim lẻ',
                },
                // {
                //     valueCombobox: 3,
                //     title: 'Phim chiếu rạp',
                // },
                {
                    valueCombobox: 4,
                    title: 'Phim hot',
                },
            ],
            showDialogWarning: false,
            textWarning: '',

            error: [],
            showDialog: false,
            errorField: { code: false, name: false },
        };
    },
};
</script>

<style scoped>
@import url('./movie.css');
@import url('@vueform/multiselect/themes/default.css');
/* .vc-popover-caret.direction-bottom.align-left {
    display: none;
}

.vc-svg-icon {
    width: 24px;
    height: 24px;
}

.vc-weeks {
    padding: 12px 24px !important;
}

.vc-pane {
    box-shadow: 0px 2px 3px rgba(0, 0, 0, 0.051) !important;
}

.vc-header {
    padding: 24px 24px 0 !important;
}

.vc-arrows-container {
    padding: 24px 24px 0 !important;
}

.vc-container {
    margin-top: 4px !important;
    font-family: Roboto !important;
    font-size: 1.4rem !important;
}

.vc-weekday {
    color: #000 !important;
    font-weight: 500 !important;
}

.input-date {
    border: 1px solid #e0e1e4;
    width: 100%;
    height: 100%;
    padding: 10px 12px;
    border-radius: 4px;
    font-size: 1.4rem;
    font-family: Roboto;
}
.input-date:focus {
    border: 1px solid #50b83c;
}

.vc-highlight {
    background-color: #50b83c !important;
}

.icon-date {
    width: 24px;
    height: 24px;
    position: absolute;
    right: 0;
    margin-right: 8px;
    display: flex;
    justify-content: center;
    align-items: center;
}
.icon-date-content {
    width: 16px;
    height: 16px;
    background-image: url('../../assets/icon/calendar.png');
} */
</style>
