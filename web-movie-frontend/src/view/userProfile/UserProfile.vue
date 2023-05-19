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
                            <a><img src="../../assets/img/user-img.png" alt="" /><br /></a>
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
                                <li style="cursor: pointer"><a>Thay đổi mật khẩu</a></li>
                                <li @click="btnLogout" style="cursor: pointer"><a>Đăng xuất</a></li>
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

    <MLoadingClient v-if="showLoadingClient"></MLoadingClient>
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
            }, 100);
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
        setTimeout(() => {
            this.showLoadingClient = false;
            const storage = localStorage.getItem('userInfor');
            if (storage !== null) {
                this.userInformation = JSON.parse(storage);
            }
        }, 1000);
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

            showDialogWarning: false,
            textWarning: '',

            showLoadingClient: true,
        };
    },
};
</script>

<style>
.modal-instruction {
    position: fixed;
    background-color: rgba(0, 0, 0, 0.4);
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    /* display: none; */
    z-index: 99999;
}

.instructure-recharge {
    width: 80%;
    margin: 0 auto;
    padding: 20px;
    border-radius: 10px;
    background-color: #fff;
}

.instructure-recharge h2 {
    margin-bottom: 12px;
}
.amount {
    border: 2px solid red;
    border-radius: 10px;
    padding: 10px;
    font-size: 20px;
}

.denomination {
    display: flex;
    flex-direction: row;
}

.provider-item.item-price {
    display: block !important;
    padding: 10px 10px 2px;
}

.provider-item {
    display: block;
    border: 1px solid #d3d3d3;
    border-radius: 8px;
    padding: 4px;
    min-height: 60px !important;
    max-width: 130px !important;
    cursor: pointer;
    position: relative;
}

.provider-item.item-price.selected {
    display: block !important;
    padding: 9px 9px 1px;
}

.provider-item.selected {
    border: 2px solid #f25922;
    padding: 3px;
}

.provider-item.item-price {
    display: block !important;
    padding: 10px 10px 2px;
}

.provider-item.item-price.selected .price-info {
    color: #f25922 !important;
    padding-bottom: 15px;
    padding-top: 15px;
}

.provider-item .price-info {
    color: #0a0a0a !important;
    display: block;
    padding-bottom: 15px;
    padding-top: 15px;
    font-size: 18px;
    text-align: center;
    font-weight: 500;
}

.provider-item .price-discount {
    display: flex;
    border-top: 1px dotted #333;
    padding-top: 4px;
    margin: 0;
}

.provider-item .price-discount .discount-text {
    float: left;
    font-size: 12px;
    color: #0a0a0a;
    width: 49%;
}

.provider-item .price-discount .discount-value {
    float: right;
    font-size: 13px;
    color: #002bff;
    width: 49%;
    text-align: right;
    font-weight: 500;
}
</style>
