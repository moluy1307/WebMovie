<template>
    <div class="modal" @click.self="$emit('onClose')" @keydown="inputOnKeyDown" @keyup="inputOnKeyUp">
        <div class="form" style="width: 635px">
            <div class="form-header">
                <div class="frm-header-left">
                    <div class="frm-title">Thông tin diễn viên</div>
                </div>
                <div class="content-gap-12">
                    <!-- <div
                        v-tippy="{
                            content: this.$MResource.VI.ACRONYM.HELP,
                            placement: 'bottom',
                            theme: 'dark',
                            arrow: true,
                        }"
                        class="icon-size"
                        style="cursor: pointer"
                    >
                        <MIcon nameIcon="help" :class="{ 'icon-size': true }" newStyle="position: relative;"></MIcon>
                    </div> -->
                    <div
                        v-tippy="{
                            content: this.$MResource.VI.ACRONYM.CLOSE,
                            placement: 'bottom',
                            theme: 'dark',
                            arrow: true,
                        }"
                        @click="$emit('onClose')"
                        class="icon-size"
                        style="cursor: pointer"
                    >
                        <MIcon nameIcon="close" :class="{ 'icon-size': true }" newStyle="position: relative"></MIcon>
                    </div>
                </div>
            </div>
            <div class="frm-content">
                <div class="content-flex">
                    <div style="display: flex; flex-direction: column; row-gap: 22px; width: 405px">
                        <div class="content-element">
                            <div class="frm-manual">
                                <MInput
                                    labelInput="Mã"
                                    ref="refActorCode"
                                    compulsory="*"
                                    styleCompulsory="margin-left: 5px;"
                                    v-model="actorInfor.actorCode"
                                    :newClass="!errorField.code ? '' : 'input-error'"
                                    :tooltipError="errorField.code"
                                    errorMessage="Mã không được để trống"
                                ></MInput>
                            </div>
                            <div class="frm-cal">
                                <MInput
                                    labelInput="Tên"
                                    propTabindex="0"
                                    compulsory="*"
                                    styleCompulsory="margin-left: 5px;"
                                    ref="reffullname"
                                    v-model="actorInfor.fullname"
                                    :newClass="!errorField.fullname ? '' : 'input-error'"
                                    :tooltipError="errorField.fullname"
                                    errorMessage="Tên không được để trống"
                                ></MInput>
                            </div>
                        </div>
                        <div class="infor">
                            <MInput
                                labelInput="Quê quán"
                                v-model="actorInfor.country"
                                propTabindex="0"
                                ref="fullnameInput"
                            ></MInput>
                        </div>
                        <div class="content-element">
                            <div class="frm-manual">
                                <label for="txtDate">Ngày sinh</label>
                                <v-date-picker v-model="actorInfor.dateOfBirth" color="green" title-position="left">
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
                                            <div class="icon-date"><div class="icon-date-content"></div></div>
                                        </div>
                                    </template>
                                </v-date-picker>
                            </div>
                            <div class="frm-cal">
                                <div class="infor">
                                    <label for="txtGender" style="padding-left: 8px">Giới tính</label>
                                    <div class="m-row" style="padding-left: 8px">
                                        <label class="txtGender"
                                            >Nam
                                            <input
                                                v-model="actorInfor.gender"
                                                type="radio"
                                                name="radio"
                                                :value="0"
                                                checked
                                                tabindex="0"
                                            />
                                            <span class="checkmark"></span>
                                        </label>
                                        <label class="txtGender"
                                            >Nữ
                                            <input
                                                v-model="actorInfor.gender"
                                                type="radio"
                                                name="radio"
                                                :value="1"
                                                tabindex="0"
                                            />
                                            <span class="checkmark"></span>
                                        </label>
                                        <label class="txtGender"
                                            >Khác
                                            <input
                                                v-model="actorInfor.gender"
                                                type="radio"
                                                name="radio"
                                                :value="2"
                                                tabindex="0"
                                            />
                                            <span class="checkmark"></span>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div style="display: flex; flex-direction: column; row-gap: 22px; flex: 1">
                        <div class="content-element">
                            <div style="display: flex; flex-direction: column; row-gap: 22px">
                                <img
                                    :src="actorInfor.imagePath"
                                    alt="Ảnh minh họa"
                                    style="width: 150px; height: 150px"
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
                </div>

                <div class="content-element" style="margin-bottom: 22px">
                    <div class="frm-cal">
                        <MInput labelInput="Tiểu sử" v-model="actorInfor.biography" propTabindex="0"></MInput>
                    </div>
                </div>

                <div class="btn-group">
                    <!-- <button @click="btnCloseOnClick" class="btn btn-bg">Hủy</button> -->
                    <MButton
                        @click="$emit('onClose')"
                        :class="{ 'btn-custom-default': true, 'btn-bg': true }"
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

import commonJS from '@/js/common';
export default {
    name: 'ActorDetail',
    props: ['id'],
    watch: {
        'actorInfor.actorCode': function (value) {
            if (value.length <= 0) {
                this.errorField.code = true;
                this.$refs.refActorCode.$refs.txtInput.classList.add('input-error');
                // this.$refs.refUsername.$refs.txtInput.focus();
            } else {
                this.$refs.refActorCode.$refs.txtInput.classList.remove('input-error');
                this.errorField.code = false;
            }
        },
        'actorInfor.fullname': function (value) {
            if (value.length <= 0) {
                this.errorField.fullname = true;
                this.$refs.reffullname.$refs.txtInput.classList.add('input-error');
                // this.$refs.reffullname.$refs.txtInput.focus();
            } else {
                this.$refs.reffullname.$refs.txtInput.classList.remove('input-error');
                this.errorField.fullname = false;
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
    },

    methods: {
        /**
         * Kiểm tra dữ liệu bắt buộc nhập
         * <br/>
         * CreatedBy: huynq (5/1/2023)
         */
        checkForm() {
            try {
                this.error = [];
                var isValid = true;

                if (!this.actorInfor.fullname) {
                    isValid = false;
                    this.error.push('Tên diễn viên không được để trống');
                    this.showDialog = true;
                    this.errorField.fullname = true;
                    // this.$refs.fullnameInput.$refs.txtInput.focus();
                }

                if (!this.actorInfor.actorCode) {
                    isValid = false;
                    this.error.push('Mã diễn viên không được để trống');
                    this.showDialog = true;
                    this.errorField.code = true;
                }

                if (this.actorInfor.actorCode && this.actorInfor.fullname) {
                    isValid = true;
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

        onFileChange(event) {
            this.file = event.target.files[0];
            this.actorInfor.imagePath = URL.createObjectURL(this.file);
        },
        btnSaveOnClick() {
            var me = this;

            var isValid = me.checkForm();
            if (!isValid) {
                return;
            }

            const formData = new FormData();
            // for (var property in this.actorInfor) {
            //     formData.append(property, this.actorInfor[property]);
            // }

            formData.append('actorCode', this.actorInfor.actorCode);
            formData.append('fullname', this.actorInfor.fullname);
            if (this.actorInfor.dateOfBirth != null || this.actorInfor.dateOfBirth != undefined) {
                this.actorInfor.dateOfBirth = commonJS.bindingFormatDate(this.actorInfor.dateOfBirth);
                formData.append('dateOfBirth', this.actorInfor.dateOfBirth);
            }
            formData.append('gender', this.actorInfor.gender);
            formData.append('country', this.actorInfor.country);
            formData.append('biography', this.actorInfor.biography);

            if (this.$refs.fileInput.files.length > 0) {
                console.log('da co duong dan link image', this.$refs.fileInput.files[0]);
                // formData.append('image', this.$refs.fileInput.files[0]);
                formData.append('image', this.file);
            }
            if (me.isAdd) {
                axios
                    .post('https://localhost:7112/api/v1/Actors/InsertActor', formData, {
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
                        this.$MToastMessage({
                            titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                            messageToast: err,
                            showToastMessage: true,
                            typeToast: 'errorToast',
                        });
                    });
            }
        },
    },

    created() {
        var me = this;
        if (me.id) {
            axios
                .get(`${this.$MResource.API}/Actors/GetActorById?actorId=${me.id}`)
                .then((response) => {
                    me.actorInfor = response.data;

                    if (me.actorInfor.dateOfBirth != null || me.actorInfor.dateOfBirth != undefined) {
                        me.actorInfor.dateOfBirth = commonJS.bindingFormatDate(me.actorInfor.dateOfBirth);
                    }
                    if (this.actorInfor.imgByte != null && this.actorInfor.imgByte.length > 0) {
                        this.actorInfor.imagePath = 'data:image/png;base64,' + this.actorInfor.imgByte;
                        this.file = 'data:image/png;base64,' + this.actorInfor.imgByte;
                    }
                    if (this.actorInfor.categories != null) {
                        this.actorInfor.categories.forEach((element) => {
                            this.valueCategory.push(element.value);
                        });
                    }
                    console.log('chi tiet nhan duoc: ', this.movieInfor);
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
            me.actorInfor = {
                actorCode: '',
                fullname: '',
                dateOfBirth: '',
                gender: 0,
                country: '',
                biography: '',
            };
        }
    },

    data() {
        return {
            actorInfor: {
                actorCode: '',
                fullname: '',
                dateOfBirth: '',
                gender: 0,
                country: '',
                biography: '',
            },
            error: [],
            showDialog: false,
            errorField: { code: false, fullname: false },

            file: null,

            showDialogWarning: false,
            textWarning: '',
        };
    },
};
</script>

<style scoped>
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
