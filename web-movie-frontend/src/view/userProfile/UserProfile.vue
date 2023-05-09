<template>
    <div class="hero user-hero">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="hero-ct">
                        <h1>Thông tin tài khoản</h1>
                        <ul class="breadcumb">
                            <li class="active"><a href="#">Trang chủ</a></li>
                            <li>
                                <span><font-awesome-icon :icon="['fas', 'angle-right']" /></span>Hồ sơ
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="page-single">
        <div class="container">
            <div class="row ipad-width">
                <div class="col-md-3 col-sm-12 col-xs-12">
                    <div class="user-information">
                        <div class="user-img">
                            <a href="#"><img src="../../assets/img/user-img.png" alt="" /><br /></a>
                            <!-- <a href="#" class="redbtn">Change avatar</a> -->
                        </div>
                        <div class="user-fav">
                            <p>Chi tiết tài khoản</p>
                            <ul>
                                <!-- <li class="active"><a href="userprofile.html">Profile</a></li> -->
                                <li style="color: #dcf836">
                                    <font-awesome-icon :icon="['fas', 'coins']" />&nbsp;{{
                                        userInformation.amountMoney == null ? '0' : userInformation.amountMoney
                                    }}
                                    xu
                                </li>
                                <li style="cursor: pointer" @click="btnShowFormInstruction"><a>Nạp tiền</a></li>
                            </ul>
                        </div>
                        <div class="user-fav">
                            <p>Khác</p>
                            <ul>
                                <li><a href="#">Thay đổi mật khẩu</a></li>
                                <li @click="btnLogout"><a>Đăng xuất</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-md-9 col-sm-12 col-xs-12">
                    <div class="form-style-1 user-pro" action="#">
                        <form action="#" class="user">
                            <h4>01. Hồ sơ chi tiết</h4>
                            <div class="row">
                                <div class="col-md-6 form-it">
                                    <label>Tên đăng nhập</label>
                                    <input autocomplete="off" type="text" v-model="userInformation.username" />
                                </div>
                                <div class="col-md-6 form-it">
                                    <label>Email</label>
                                    <input autocomplete="off" type="text" v-model="userInformation.email" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 form-it">
                                    <label>Tên đầy đủ</label>
                                    <input autocomplete="off" type="text" v-model="userInformation.fullName" />
                                </div>
                                <div class="col-md-6 form-it">
                                    <label>Địa chỉ</label>
                                    <input autocomplete="off" type="text" v-model="userInformation.address" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 form-it">
                                    <label>Số điện thoại</label>
                                    <input autocomplete="off" type="text" v-model="userInformation.phoneNumber" />
                                </div>
                                <div class="col-md-6 form-it">
                                    <label>Ngày sinh</label>
                                    <v-date-picker
                                        color="green"
                                        title-position="left"
                                        v-model="userInformation.dateOfBirth"
                                    >
                                        <template #default="{ inputValue, inputEvents }">
                                            <!-- <input autocomplete="off" class="px-3 py-1 border rounded" :value="inputValue" @focusin="togglePopover()" /> -->
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
                                                    autocomplete="off"
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
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="submit" @click="btnChangeInforUser">Lưu</div>
                                </div>
                            </div>
                        </form>
                        <form action="#" class="password">
                            <h4>02. Thay đổi mật khẩu</h4>
                            <div class="row">
                                <div class="col-md-6 form-it">
                                    <label>Mật khẩu cũ</label>
                                    <input autocomplete="off" type="password" v-model="txtOldPassword" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 form-it">
                                    <label>Mật khẩu mới</label>
                                    <input autocomplete="off" type="password" v-model="txtNewPassword" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 form-it">
                                    <label>Xác nhận mật khẩu mới</label>
                                    <input autocomplete="off" type="password" v-model="txtReNewPassword" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="submit" @click="btnChangePassword">Sửa</div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <RechargeMoneyVue v-if="showFormInstruction" @onClose="showFormInstruction = false"></RechargeMoneyVue>
    <div v-if="showConfirmNewPassword">
        <MDialog
            @close-dialog="showConfirmNewPassword = false"
            depict="Bạn cần điền đúng thông tin xác nhận mật khẩu mới"
            dialogConfirm="false"
            :hasCloseButton="false"
            :hasCancelButton="{ 'btn-dialog-left': true }"
            typeDialog="warningDialog"
            titleDialog="Thông báo"
            titleButton="Đóng"
        ></MDialog>
    </div>
    <div v-else-if="showRequestFill">
        <MDialog
            @close-dialog="showRequestFill = false"
            depict="Bạn cần điền thông tin mật khẩu cũ"
            dialogConfirm="false"
            :hasCloseButton="false"
            :hasCancelButton="{ 'btn-dialog-left': true }"
            typeDialog="warningDialog"
            titleDialog="Thông báo"
            titleButton="Đóng"
        ></MDialog>
    </div>
</template>

<script>
import axios from 'axios';
import RechargeMoneyVue from './RechargeMoney.vue';
export default {
    name: 'UserProfile',
    components: { RechargeMoneyVue },
    methods: {
        btnLogout() {
            this.userInformation = {};
            localStorage.removeItem('userInfor');
            this.$router.push('/');
            setTimeout(() => {
                location.reload();
            }, 1000);
        },

        btnShowFormInstruction() {
            this.showFormInstruction = !this.showFormInstruction;
        },

        btnChangePassword() {
            if (this.txtOldPassword != null || this.txtOldPassword != undefined) {
                axios
                    .get(
                        `${this.$MResource.API}/Users/Login?username=${this.userInformation.username}&password=${this.txtOldPassword}`,
                    )
                    .then(() => {
                        if (this.txtNewPassword != this.txtReNewPassword) {
                            this.showConfirmNewPassword = true;
                        } else {
                            axios
                                .put(
                                    `${this.$MResource.API}/Users/changePassword?userId=${this.userInformation.userId}&password=${this.txtReNewPassword}`,
                                )
                                .then(() => {
                                    this.$MToastMessage({
                                        titleToast: this.$MResource.VI.TOAST.TITLE_SUCCESS,
                                        messageToast: 'Thay đổi mật khẩu thành công',
                                        showToastMessage: true,
                                        typeToast: 'successToast',
                                    });
                                    this.userInformation = {};
                                    localStorage.removeItem('userInfor');
                                    this.$router.push('/');
                                    setTimeout(() => {
                                        location.reload();
                                    }, 1000);
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
                    })
                    .catch((err) => {
                        let response = err.response;
                        switch (response.status) {
                            case 500:
                                if (response.data['errorCode'] === 1) {
                                    // this.$router.push({ name: 'userprofile', params: { id: this.userInformation.userId } });
                                    this.$MToastMessage({
                                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                                        messageToast: 'Mật khẩu cũ không chính xác',
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
                this.showRequestFill = true;
            }
        },

        btnChangeInforUser() {
            var me = this;
            axios
                .put(`${this.$MResource.API}/Users/${this.userInformation.userId}`, me.userInformation)
                .then(() => {
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_SUCCESS,
                        messageToast: 'Tài khoản của bạn đã được cập nhật thành công',
                        showToastMessage: true,
                        typeToast: 'successToast',
                    });
                    localStorage.setItem('userInfor', JSON.stringify(me.userInformation));
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
                    console.log(err);
                });
        },
    },
    created() {
        // console.log('router user prof: ', this.$route.params.id);
        const storage = localStorage.getItem('userInfor');
        if (storage !== null) {
            this.userInformation = JSON.parse(storage);
        }
    },
    data() {
        return {
            showFormInstruction: false,
            userInformation: {},
            txtOldPassword: null,
            txtReNewPassword: '',
            txtNewPassword: '',
            showConfirmNewPassword: false,
            showRequestFill: false,
        };
    },
};
</script>

<style></style>
