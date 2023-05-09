<template>
    <div class="overlay openform" @click.self="$emit('on-close')">
        <div class="login-wrapper" id="login-content">
            <div class="login-content">
                <a href="#" class="close">x</a>
                <h3>Quên mật khẩu ?</h3>
                <form method="post" action="#">
                    <div class="row">
                        <label for="email-2" v-if="formConfirm == false">
                            email:
                            <input
                                type="text"
                                name="email"
                                id="email-2"
                                placeholder="Nhập địa chỉ email đã đăng ký"
                                pattern="(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$"
                                required="required"
                                v-model="emailUser"
                            />
                        </label>
                    </div>

                    <div class="row" v-if="formConfirm">
                        <div style="font-size: 14px; font-weight: 550; margin-bottom: 20px">
                            Một mã xác thực đã được gửi đến email đăng ký tài khoản của bạn. Vui lòng nhập mã xác thực
                            đó!
                        </div>
                    </div>

                    <div class="row" v-if="formConfirm">
                        <label for="email-2">
                            Mã xác thực:
                            <input
                                type="text"
                                name="email"
                                id="email-2"
                                placeholder="Nhập mã xác thực"
                                required="required"
                                v-model="confirmCode"
                            />
                        </label>
                    </div>

                    <div class="row">
                        <div class="btn-login" @click="btnContinue" v-if="formConfirm == false">Tiếp tục</div>
                        <div class="btn-login" @click="btnSuccessRestore" v-if="formConfirm">Tiếp tục</div>
                    </div>
                </form>
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
</template>

<script>
import axios from 'axios';
export default {
    name: 'ForgotPassword',
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
                //Validate Email không đúng định dạng
                if (this.emailUser != null) {
                    if (!/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4})*$/.test(this.emailUser)) {
                        isValid = false;
                        this.error.push('Email không đúng định dạng');
                        this.showDialog = true;
                    }
                } else {
                    this.error.push('Bạn cần điền đầy đủ thông tin email đã đăng ký tài khoản');
                    this.showDialog = true;
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

        btnContinue() {
            var me = this;
            var isValid = me.checkForm();
            if (!isValid) {
                return;
            }

            axios
                .post(`${this.$MResource.API}/Users/SendConfirmCode?emailMess=${me.emailUser}`)
                .then(() => {
                    this.formConfirm = true;
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

        btnSuccessRestore() {
            axios
                .get(`${this.$MResource.API}/Users/GetUserByEmail?emailUser=${this.emailUser}`)
                .then((response) => {
                    this.userInfor = response.data;
                    if (this.userInfor.confirmCode == this.confirmCode) {
                        axios
                            .post(`${this.$MResource.API}/Users/SendNewPassword?emailMess=${this.emailUser}`)
                            .then(() => {
                                this.$emit('on-success');
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
                        this.error = [];
                        this.error.push('Mã xác thực không chính xác! Vui lòng điền lại mã xác thực');
                        this.showDialog = true;
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
        },
    },
    data() {
        return {
            showDialog: false,
            error: [],
            emailUser: null,

            formConfirm: false,

            userInfor: {},
            confirmCode: null,
        };
    },
};
</script>

<style>
.btn-login {
    font-size: 14px;
    color: #ffffff;
    font-weight: bold;
    text-transform: uppercase;
    border: none;
    background-color: #dd003f;
    height: 42px;
    width: 100%;
    cursor: pointer;
    display: flex;
    justify-content: center;
    align-items: center;
}
</style>
