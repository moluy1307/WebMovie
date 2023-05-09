<template>
    <div class="overlay openform" @click.self="btnCloseForm">
        <div class="login-wrapper" id="login-content">
            <div class="login-content">
                <a href="#" class="close">x</a>
                <h3>{{ isFormSignup ? 'Đăng ký' : 'Đăng nhập' }}</h3>
                <form method="post" action="#">
                    <div class="row" v-if="isFormSignup == false">
                        <label for="username">
                            Tên đăng nhập:
                            <input
                                v-model="txtUsername"
                                type="text"
                                name="username"
                                id="username"
                                placeholder="Tài khoản đăng nhập"
                            />
                        </label>
                    </div>

                    <div class="row" v-if="isFormSignup == true">
                        <MInput
                            labelInput="Tên đăng nhập:"
                            v-model="insertUser.username"
                            :newClass="!errorField.username ? '' : 'input-error'"
                            ref="refUsername"
                            :tooltipError="errorField.username"
                            errorMessage="Tên đăng nhập không được để trống"
                            styleLabel="margin-bottom: 8px !important"
                            newStyle="height: 42px !important;"
                            styleBoxInput="height: 42px !important; margin-bottom: 25px"
                            styleTooltip="top: 42px !important"
                        ></MInput>
                    </div>

                    <div class="row" v-if="isFormSignup == true">
                        <MInput
                            labelInput="Địa chỉ Email:"
                            v-model="insertUser.email"
                            :newClass="!errorField.email ? '' : 'input-error'"
                            ref="refEmail"
                            :tooltipError="errorField.email"
                            errorMessage="Địa chỉ Email không được để trống"
                            styleLabel="margin-bottom: 8px !important"
                            newStyle="height: 42px !important;"
                            styleBoxInput="height: 42px !important; margin-bottom: 25px"
                            styleTooltip="top: 42px !important"
                        ></MInput>
                    </div>

                    <div class="row" v-if="isFormSignup == false">
                        <label for="password">
                            Mật khẩu:
                            <input
                                v-model="txtPassword"
                                type="password"
                                name="password"
                                id="password"
                                placeholder="Mật khẩu"
                            />
                        </label>
                    </div>

                    <div class="row" v-if="isFormSignup == true">
                        <MInput
                            :isInputPassword="true"
                            labelInput="Mật khẩu:"
                            v-model="insertUser.password"
                            :newClass="!errorField.password ? '' : 'input-error'"
                            ref="refPassword"
                            :tooltipError="errorField.password"
                            errorMessage="Mật khẩu không được để trống"
                            styleLabel="margin-bottom: 8px !important"
                            newStyle="height: 42px !important;"
                            styleBoxInput="height: 42px !important; margin-bottom: 25px"
                            styleTooltip="top: 42px !important"
                        ></MInput>
                    </div>

                    <div class="row" v-if="isFormSignup == true">
                        <MInput
                            :isInputPassword="true"
                            labelInput="Nhập lại mật khẩu:"
                            v-model="txtConfirmPass"
                            :newClass="!errorField.confirm ? '' : 'input-error'"
                            ref="refTextConfirm"
                            :tooltipError="errorField.confirm"
                            errorMessage="Nhập mật khẩu xác nhận"
                            styleLabel="margin-bottom: 8px !important"
                            newStyle="height: 42px !important;"
                            styleBoxInput="height: 42px !important; margin-bottom: 25px"
                            styleTooltip="top: 42px !important"
                        ></MInput>
                    </div>
                    <div class="row" v-if="isFormSignup == false">
                        <div class="remember">
                            <div>
                                <!-- <input type="checkbox" name="remember" value="Remember me" /><span>Remember me</span> -->
                            </div>
                            <a style="cursor: pointer" @click="btnShowFormForgot">Quên mật khẩu ?</a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="btn-login" v-if="isFormSignup == false" @click="btnLogin">Đăng nhập</div>
                        <div class="btn-login" v-else-if="isFormSignup == true" @click="btnSignUp">Đăng ký</div>
                    </div>
                </form>
                <!-- <div class="row">
                    <p>Or via social</p>
                    <div class="social-btn-2">
                        <a class="fb" href="#"><i class="ion-social-facebook"></i>Facebook</a>
                        <a class="tw" href="#"><i class="ion-social-twitter"></i>twitter</a>
                    </div>
                </div> -->
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

export default {
    name: 'UserLogin',
    props: ['isFormSignup'],

    watch: {
        'insertUser.username': function (value) {
            if (value.length <= 0) {
                this.errorField.username = true;
                this.$refs.refUsername.$refs.txtInput.classList.add('input-error');
                // this.$refs.refUsername.$refs.txtInput.focus();
            } else {
                this.$refs.refUsername.$refs.txtInput.classList.remove('input-error');
                this.errorField.username = false;
            }
        },
        'insertUser.password': function (value) {
            if (value.length <= 0) {
                this.errorField.password = true;
                this.$refs.refPassword.$refs.txtInput.classList.add('input-error');
                // this.$refs.refPassword.$refs.txtInput.focus();
            } else {
                this.$refs.refPassword.$refs.txtInput.classList.remove('input-error');
                this.errorField.password = false;
            }
        },

        'insertUser.email': function (value) {
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
    },

    methods: {
        btnCloseForm() {
            this.$emit('close-form');
        },
        btnLogin() {
            console.log('dang nhap: ', this.$MResource.API);
            axios
                .get(`${this.$MResource.API}/Users/Login?username=${this.txtUsername}&password=${this.txtPassword}`)
                .then((response) => {
                    this.userInfor = response.data;
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_SUCCESS,
                        messageToast: this.$MResource.VI.TOAST.LOGIN_SUCCESS,
                        showToastMessage: true,
                        typeToast: 'successToast',
                    });
                    this.$emit('close-form');
                    // this.$router.push({ name: 'home', params: { id: this.userInfor.userId } });
                    if (this.userInfor.roleName == 'User') {
                        // this.$emit('passUserInfor', this.userInfor);
                        location.reload();
                        localStorage.setItem('userInfor', JSON.stringify(this.userInfor));
                    } else if (this.userInfor.roleName == 'Admin') {
                        localStorage.setItem('adminInfor', JSON.stringify(this.userInfor));
                        location.reload();
                        // this.$router.push({ name: 'dashboard', params: { id: this.userInfor.userId } });
                    }
                })
                .catch((err) => {
                    let response = err.response;
                    switch (response.status) {
                        case 500:
                            if (response.data['errorCode'] === 1) {
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
        },

        btnShowFormForgot() {
            this.$emit('close-form');
            this.$emit('open-form-forgotPass');
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

                if (this.txtConfirmPass == null) {
                    isValid = false;
                    this.error.push('Nhập mật khẩu xác nhận');
                    this.showDialog = true;
                    this.errorField.confirm = true;
                }

                if (!this.insertUser.password) {
                    isValid = false;
                    this.error.push('Mật khẩu không được để trống');
                    this.showDialog = true;
                    this.errorField.password = true;
                }

                if (!this.insertUser.email) {
                    isValid = false;
                    this.error.push('Địa chỉ Email không được để trống');
                    this.showDialog = true;
                    this.errorField.email = true;
                    // this.$refs.fullNameInput.$refs.txtInput.focus();
                }

                if (!this.insertUser.username) {
                    isValid = false;
                    this.error.push('Tên đăng nhập không được để trống');
                    this.showDialog = true;
                    this.errorField.username = true;
                }

                if (
                    this.insertUser.username &&
                    this.insertUser.email &&
                    this.insertUser.password &&
                    this.txtConfirmPass != null
                ) {
                    if (this.insertUser.password != this.txtConfirmPass) {
                        isValid = false;
                        this.error.push('Mật khẩu xác nhận chưa đúng');
                        this.showDialog = true;
                    }
                    //Validate Email không đúng định dạng
                    if (this.insertUser.email != null) {
                        if (!/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4})*$/.test(this.insertUser.email)) {
                            isValid = false;
                            this.error.push('Email không đúng định dạng');
                            this.showDialog = true;
                        }
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

        btnSignUp() {
            var me = this;

            var isValid = me.checkForm();
            if (!isValid) {
                return;
            }
            axios
                .post(`${this.$MResource.API}/Users`, me.insertUser)
                .then(() => {
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_SUCCESS,
                        messageToast: this.$MResource.VI.TOAST.ADD_SUCCESS,
                        showToastMessage: true,
                        typeToast: 'successToast',
                    });
                    this.$emit('close-form');
                    location.reload();
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
                });
        },
    },
    data() {
        return {
            txtUsername: '',
            txtPassword: '',
            userInfor: {},

            txtConfirmPass: null,
            insertUser: { username: '', password: '', email: '', roleId: '142cb08f-7c31-21fa-8e90-67245e8b283e' },

            error: [],
            showDialog: false,
            errorField: { username: false, email: false, password: false, confirm: false },

            showDialogWarning: false,
            textWarning: '',
        };
    },
};
</script>

<style scoped>
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
