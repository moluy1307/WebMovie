<template>
    <header class="ht-header" :class="{ sticky: detectScroll }" ref="headerRef">
        <div class="container">
            <nav class="navbar navbar-default navbar-custom">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header logo">
                    <div class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                        <span class="sr-only">Toggle navigation</span>
                        <div id="nav-icon1">
                            <span></span>
                            <span></span>
                            <span></span>
                        </div>
                    </div>
                    <a href="index-2.html"
                        ><img class="logo" src="../../../assets/img/logo1.png" alt="" width="119" height="58"
                    /></a>
                </div>
                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse flex-parent" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav flex-child-menu menu-left">
                        <li class="hidden">
                            <a href="#page-top"></a>
                        </li>
                        <li class="dropdown first">
                            <a class="btn btn-default dropdown-toggle lv1" data-toggle="dropdown"> Trang chủ </a>
                        </li>
                        <li class="dropdown first">
                            <a class="btn btn-default dropdown-toggle lv1" data-toggle="dropdown" data-hover="dropdown">
                                Thể loại<MIcon nameIcon="arrow-dropdown" :newClass="{ 'arrow-dropdown': true }"></MIcon>
                            </a>
                            <ul class="dropdown-menu level1">
                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown"
                                        >Movie grid<i class="ion-ios-arrow-forward"></i
                                    ></a>
                                    <ul class="dropdown-menu level2">
                                        <li><a href="moviegrid.html">Movie grid</a></li>
                                        <li><a href="moviegridfw.html">movie grid full width</a></li>
                                    </ul>
                                </li>
                                <li><a href="movielist.html">Movie list</a></li>
                                <li><a href="moviesingle.html">Movie single</a></li>
                                <li class="it-last"><a href="seriessingle.html">Series single</a></li>
                            </ul>
                        </li>
                        <li class="dropdown first">
                            <a class="btn btn-default dropdown-toggle lv1" data-toggle="dropdown" data-hover="dropdown">
                                Phim bộ <i class="fa fa-angle-down" aria-hidden="true"></i>
                            </a>
                        </li>
                        <li class="dropdown first">
                            <a class="btn btn-default dropdown-toggle lv1" data-toggle="dropdown" data-hover="dropdown">
                                Phim lẻ <i class="fa fa-angle-down" aria-hidden="true"></i>
                            </a>
                        </li>
                        <li class="dropdown first">
                            <a class="btn btn-default dropdown-toggle lv1" data-toggle="dropdown" data-hover="dropdown">
                                Bảng xếp hạng <i class="fa fa-angle-down" aria-hidden="true"></i>
                            </a>
                            <ul class="dropdown-menu level1">
                                <li><a href="userfavoritegrid.html">user favorite grid</a></li>
                                <li><a href="userfavoritelist.html">user favorite list</a></li>
                                <li><a href="userprofile.html">user profile</a></li>
                                <li class="it-last"><a href="userrate.html">user rate</a></li>
                            </ul>
                        </li>
                    </ul>
                    <ul class="nav navbar-nav flex-child-menu menu-right" v-if="hasAccount == false">
                        <li class="button-search">
                            <!-- <a style="padding: 10px 5px"
                                ><font-awesome-icon :icon="['fas', 'magnifying-glass']" style="font-size: 20px"
                            /></a> -->
                            <div class="top-search">
                                <input type="text" placeholder="Tìm kiếm phim..." />
                                <MIcon nameIcon="search" :newClass="{ 'search-icon': true }"></MIcon>
                            </div>
                        </li>
                        <li class="loginLink" @click="btnShowFormLogin"><a>Đăng nhập</a></li>
                        <li class="btn signupLink" @click="btnShowFormSignup"><a>Đăng ký</a></li>
                    </ul>
                    <div class="user-header" v-if="hasAccount == true" @click="showBoxUser = !showBoxUser">
                        <div :class="{ 'box-icon-user': true }">
                            <MIcon nameIcon="user" :class="{ 'icon-user': true }"></MIcon>
                        </div>
                        <div class="user-profile">
                            <div class="user-name">
                                {{ userInformation.username }}
                                <span>
                                    <MIcon
                                        nameIcon="arrow-dropdown"
                                        newStyle="position: static;display: inherit;"
                                        :newClass="{ 'arrow-dropdown': true }"
                                    ></MIcon>
                                </span>
                            </div>
                            <ul v-if="showBoxUser" v-click-away="() => (showBoxUser = false)">
                                <li>
                                    <router-link
                                        class="fa-user"
                                        :to="{ name: 'userprofile', params: { id: userInformation.userId } }"
                                    >
                                        <font-awesome-icon :icon="['fas', 'user']" style="padding-right: 10px" />Thông
                                        tin tài khoản
                                    </router-link>
                                </li>
                                <!-- <li>
                                    <a class="fa-film">Hộp Phim</a>
                                </li>
                                <li><a class="fa-film">Lịch Sử Xem Anime</a></li>
                                <li>
                                    <a class="fa-film">Các Tập Đang Lưu</a>
                                </li>
                                <li>
                                    <a class="fa-lock">Thay đổi thông tin</a>
                                </li> -->

                                <li @click="btnLogout">
                                    <a id="logout" class="fa-power-off"
                                        ><font-awesome-icon
                                            :icon="['fas', 'arrow-right-from-bracket']"
                                            style="padding-right: 10px"
                                        />Đăng xuất</a
                                    >
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </nav>

            <!-- top search form -->
            <!-- <div class="top-search" v-if="!detectScroll">
                <input type="text" placeholder="Tìm kiếm phim..." />
                <MIcon nameIcon="search" :newClass="{ 'search-icon': true }"></MIcon>
            </div> -->
        </div>
    </header>
    <!-- END | Header -->
    <UserLogin
        v-if="showFormLogin"
        :isFormSignup="isFormSignup"
        @close-form="showFormLogin = false"
        @open-form-forgotPass="showFormForgotPass = true"
    ></UserLogin>
    <ForgotPassword
        v-if="showFormForgotPass"
        @on-close="showFormForgotPass = false"
        @on-success="showDialogSuccess"
    ></ForgotPassword>
    <div v-if="dialogSuccess">
        <MDialogTimeCount
            @close-dialog="dialogSuccess = false"
            :depict="textNotif"
            titleDialog="Thông báo"
        ></MDialogTimeCount>
    </div>
</template>

<script>
import UserLogin from '@/view/login/UserLogin.vue';
import ForgotPassword from '@/view/login/ForgotPassword.vue';
export default {
    name: 'TheHeader',
    props: ['detectScroll'],
    components: { UserLogin, ForgotPassword },
    methods: {
        btnShowFormLogin() {
            this.showFormLogin = true;
            this.isFormSignup = false;
        },
        btnShowFormSignup() {
            this.showFormLogin = true;
            this.isFormSignup = true;
        },
        // getUserInfor(user) {
        //     this.hasAccount = true;
        //     this.userInfor = user;
        // },
        btnLogout() {
            this.userInformation = {};
            localStorage.removeItem('userInfor');
            this.hasAccount = false;
            this.$router.push('/');
            setTimeout(() => {
                location.reload();
            }, 1000);
        },

        showDialogSuccess() {
            this.showFormForgotPass = false;
            this.dialogSuccess = true;
            this.textNotif = `Mật khẩu mới đã được gửi đến email đăng ký tài khoản của bạn. 
            Vui lòng kiểm tra và đăng nhập lại tài khoản. Bạn sẽ được chuyển hướng về trang chủ sau `;
        },
    },
    created() {
        const storage = localStorage.getItem('userInfor');
        if (storage !== null) {
            this.userInformation = JSON.parse(storage);
            this.hasAccount = true;
        } else {
            this.hasAccount = false;
        }
    },
    data() {
        return {
            showFormLogin: false,
            isFormSignup: false,
            hasAccount: false,
            userInformation: {},
            showBoxUser: false,

            showFormForgotPass: false,

            dialogSuccess: false,
            textNotif: null,
        };
    },
};
</script>

<style>
@import url('./header.css');
</style>
