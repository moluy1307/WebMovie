<template>
    <div class="modal" @click.self="$emit('onClose')" @keydown="inputOnKeyDown" @keyup="inputOnKeyUp">
        <div class="form">
            <div class="form-header">
                <div class="frm-header-left">
                    <div class="frm-title">Thông tin tài khoản</div>
                </div>
                <div class="content-gap-12">
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
                            @click="$emit('onClose')"
                            :class="{ 'icon-size': true }"
                            newStyle="position: relative"
                        ></MIcon>
                    </div>
                </div>
            </div>
            <div class="frm-content">
                <div class="content-flex">
                    <div style="display: flex; flex-direction: column; row-gap: 22px; width: 405px">
                        <div class="content-element">
                            <div class="frm-manual">
                                <MInput
                                    labelInput="Tên đăng nhập"
                                    v-model="userInformation.username"
                                    :newClass="!errorField.username ? '' : 'input-error'"
                                    ref="refUsername"
                                    compulsory="*"
                                    styleCompulsory="margin-left: 5px;"
                                    :tooltipError="errorField.username"
                                    errorMessage="Tên đăng nhập không được để trống"
                                    @keydown.shift.tab="focusLast"
                                ></MInput>
                            </div>
                            <div class="frm-cal">
                                <MInput
                                    labelInput="Mật khẩu"
                                    :isInputPassword="true"
                                    v-model="userInformation.password"
                                    propTabindex="0"
                                    :newClass="!errorField.password ? '' : 'input-error'"
                                    ref="refPassword"
                                    :tooltipError="errorField.password"
                                    errorMessage="Mật khẩu không được để trống"
                                ></MInput>
                            </div>
                        </div>
                        <MInput
                            labelInput="Xác nhận mật khẩu"
                            :isInputPassword="true"
                            v-model="txtConfirmPass"
                            propTabindex="0"
                            :newClass="!errorField.confirm ? '' : 'input-error'"
                            ref="refTextConfirm"
                            :tooltipError="errorField.confirm"
                            errorMessage="Nhập mật khẩu xác nhận"
                        ></MInput>
                        <div class="infor">
                            <MDropdownList
                                typeCbo="dropdown"
                                id="cboUnit"
                                label="Chức danh"
                                propValue="roleId"
                                propName="roleNameVi"
                                v-model="userInformation.roleId"
                                api="https://localhost:7112/api/v1/RoleUsers"
                                :warning="!errorField.role ? '' : 'input-error'"
                                :key="reloadDropdown"
                                tabIndexDropdown="0"
                                compulsory="*"
                                :tooltipError="errorField.role"
                                errorMessage="Chức danh không được để trống"
                                ref="dropdownRole"
                                :idObject="userInformation.roleId"
                            ></MDropdownList>
                        </div>
                    </div>

                    <div style="display: flex; flex-direction: column; row-gap: 22px; flex: 1">
                        <div class="content-element">
                            <div class="frm-manual">
                                <div class="infor">
                                    <label for="txtDate">Ngày sinh</label>
                                    <div class="m-row">
                                        <v-date-picker
                                            v-model="userInformation.dateOfBirth"
                                            color="green"
                                            title-position="left"
                                        >
                                            <template #default="{ inputValue, inputEvents }">
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
                                                    />
                                                    <div class="icon-date"><div class="icon-date-content"></div></div>
                                                </div>
                                            </template>
                                        </v-date-picker>
                                    </div>
                                </div>
                            </div>
                            <div class="frm-cal">
                                <div class="infor">
                                    <div class="frm-cal">
                                        <MInput
                                            labelInput="Tên đầy đủ"
                                            v-model="userInformation.fullName"
                                            :newClass="!errorField.name ? '' : 'input-error'"
                                            propTabindex="0"
                                            :tooltipError="errorField.name"
                                            :errorMessage="this.$MResource.VI.MSG.NAME_NOTNULL"
                                            ref="fullNameInput"
                                        ></MInput>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="content-element">
                            <div class="frm-manual">
                                <MInputMoney label="Số tiền" v-model="userInformation.amountMoney"></MInputMoney>
                            </div>
                            <div class="frm-cal">
                                <div class="infor">
                                    <MInput
                                        labelInput="Số điện thoại"
                                        v-model="userInformation.phoneNumber"
                                        propTabindex="0"
                                    ></MInput>
                                </div>
                            </div>
                        </div>

                        <div class="frm-cal">
                            <MInput
                                labelInput="Email"
                                v-model="userInformation.email"
                                propTabindex="0"
                                compulsory="*"
                                styleCompulsory="margin-left: 5px;"
                                :newClass="!errorField.email ? '' : 'input-error'"
                                ref="refEmail"
                                :tooltipError="errorField.email"
                                errorMessage="Địa chỉ Email không được để trống"
                            ></MInput>
                        </div>
                    </div>
                </div>

                <div class="content-element" style="margin-bottom: 22px">
                    <div class="frm-cal">
                        <MInput labelInput="Địa chỉ" v-model="userInformation.address" propTabindex="0"></MInput>
                    </div>
                </div>
                <div class="btn-group">
                    <!-- <button @click="btnCloseOnClick" class="btn btn-bg">Hủy</button> -->
                    <MButton
                        @click="btnCloseOnClick"
                        :class="{ 'btn-custom-default': true, 'btn-bg': true }"
                        label="Hủy"
                    ></MButton>
                    <div class="btn-left">
                        <!-- <button class="btn btn-bg" @click.prevent="btnSaveOnClick">Cất</button> -->
                        <MButton
                            @click="btnSaveOnClick"
                            :class="{ 'btn-custom-default': true, 'btn-bg': true }"
                            label="Cất"
                            ref="btnSave"
                        ></MButton>
                        <!-- <button class="btn" onclick="keepadd()">Cất và Thêm</button> -->
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
    <div v-else-if="showDialogDataChange">
        <MDialog
            @close-dialog="btnCloseDialogAndForm"
            :depict="this.$MResource.VI.DATA_CHANGED"
            dialogConfirm="true"
            @click-action="btnActionEmployeeById"
            :hasCloseButton="true"
            :hasCancelButton="{ 'btn-dialog-left': false }"
            @cancel-dialog="cancelDialog"
            typeDialog="questionDialog"
            titleDialog="Dữ liệu thay đổi"
            titleButton="Có"
        ></MDialog>
    </div>
</template>

<script>
import axios from 'axios';

import commonJS from '@/js/common';
// import { uuid } from 'vue-uuid';
export default {
    name: 'AccountDetail',
    props: ['id'],
    computed: {
        isAdd() {
            if (this.id) {
                return false;
            } else {
                return true;
            }
        },
    },

    watch: {
        'userInformation.username': function (value) {
            if (value.length <= 0) {
                this.errorField.username = true;
                this.$refs.refUsername.$refs.txtInput.classList.add('input-error');
                // this.$refs.refUsername.$refs.txtInput.focus();
            } else {
                this.$refs.refUsername.$refs.txtInput.classList.remove('input-error');
                this.errorField.username = false;
            }
        },
        'userInformation.password': function (value) {
            if (value.length <= 0) {
                this.errorField.password = true;
                this.$refs.refPassword.$refs.txtInput.classList.add('input-error');
                // this.$refs.refPassword.$refs.txtInput.focus();
            } else {
                this.$refs.refPassword.$refs.txtInput.classList.remove('input-error');
                this.errorField.password = false;
            }
        },

        'userInformation.email': function (value) {
            if (value.length <= 0) {
                this.errorField.email = true;
                this.$refs.refEmail.$refs.txtInput.classList.add('input-error');
                // this.$refs.refEmail.$refs.txtInput.focus();
            } else {
                this.$refs.refEmail.$refs.txtInput.classList.remove('input-error');
                this.errorField.email = false;
            }
        },

        txtConfirmPass: function (value) {
            if (value.length <= 0) {
                this.errorField.confirm = true;
                this.$refs.refTextConfirm.$refs.txtInput.classList.add('input-error');
                // this.$refs.refTextConfirm.$refs.txtInput.focus();
            } else {
                this.$refs.refTextConfirm.$refs.txtInput.classList.remove('input-error');
                this.errorField.confirm = false;
            }
        },

        'userInformation.roleId': function (value) {
            console.log('gia tri chuc danh thay doi: ', value.length);
            if (value.length <= 0) {
                this.errorField.role = true;
                this.$refs.dropdownRole.$refs.inputDropdown.classList.add('input-error');
                // this.$refs.firstInput.$refs.txtInput.focus();
            } else {
                this.$refs.dropdownRole.$refs.inputDropdown.classList.remove('input-error');
                this.errorField.role = false;
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

                if (this.isAdd) {
                    if (this.txtConfirmPass == null) {
                        isValid = false;
                        this.error.push('Nhập mật khẩu xác nhận');
                        this.showDialog = true;
                        this.errorField.confirm = true;
                    }
                }
                if (this.userInformation.password != null && this.txtConfirmPass != null) {
                    if (this.userInformation.password != this.txtConfirmPass) {
                        isValid = false;
                        this.error.push('Mật khẩu xác nhận chưa đúng');
                        this.showDialog = true;
                    }
                }

                if (!this.userInformation.password) {
                    isValid = false;
                    this.error.push('Mật khẩu không được để trống');
                    this.showDialog = true;
                    this.errorField.password = true;
                }

                if (!this.userInformation.email) {
                    isValid = false;
                    this.error.push('Địa chỉ Email không được để trống');
                    this.showDialog = true;
                    this.errorField.email = true;
                    // this.$refs.fullNameInput.$refs.txtInput.focus();
                }

                if (!this.userInformation.username) {
                    isValid = false;
                    this.error.push('Tên đăng nhập không được để trống');
                    this.showDialog = true;
                    this.errorField.username = true;
                }

                if (!this.userInformation.roleId) {
                    isValid = false;
                    this.error.push('Chức danh không được để trống');
                    this.showDialog = true;
                    this.errorField.role = true;
                }
                //Validate Email không đúng định dạng
                if (this.userInformation.email != null) {
                    if (!/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4})*$/.test(this.userInformation.email)) {
                        isValid = false;
                        this.error.push('Email không đúng định dạng');
                        this.showDialog = true;
                    }
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
            if (me.isAdd) {
                axios
                    .post(`${this.$MResource.API}/Users`, me.userInformation)
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
                        console.log(err);
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
            } else {
                axios
                    .put(`${this.$MResource.API}/Users/${me.id}`, me.userInformation)
                    .then(() => {
                        this.$MToastMessage({
                            titleToast: this.$MResource.VI.TOAST.TITLE_SUCCESS,
                            messageToast: 'Sửa thông tin tài khoản thành công',
                            showToastMessage: true,
                            typeToast: 'successToast',
                        });
                        me.$emit('onClose');
                        me.$emit('dataRecovery');
                    })
                    .catch((err) => {
                        console.log(err);
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
            }
        },
    },
    created() {
        var me = this;
        if (me.id) {
            axios
                .get(`${this.$MResource.API}/Users/recordId?recordId=${me.id}`)
                .then((response) => {
                    me.userInformation = response.data;

                    if (me.userInformation.dateOfBirth != null || me.userInformation.dateOfBirth != undefined) {
                        me.userInformation.dateOfBirth = commonJS.formatDate(me.userInformation.dateOfBirth);
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
            me.userInformation = {};
        }
    },
    data() {
        return {
            userInformation: {},

            error: [],
            showDialog: false,
            errorField: { username: false, email: false, password: false, confirm: false, role: false },
            txtConfirmPass: null,

            showDialogWarning: false,
            textWarning: '',
            // uuid: uuid.v1(),
        };
    },
};
</script>

<style scoped>
@import url('./account.css');
</style>
