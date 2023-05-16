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
                        <li class="dropdown first" @click="btnGoHome">
                            <a class="btn btn-default dropdown-toggle lv1" data-toggle="dropdown"> Trang chủ </a>
                        </li>
                        <li
                            class="dropdown first"
                            @mouseover="showcategoryList = true"
                            @mouseleave="showcategoryList = false"
                        >
                            <a class="btn btn-default dropdown-toggle lv1" data-toggle="dropdown" data-hover="dropdown">
                                Thể loại<MIcon nameIcon="arrow-dropdown" :newClass="{ 'arrow-dropdown': true }"></MIcon>
                            </a>
                            <!-- <ul class="dropdown-menu level1">
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
                            </ul> -->
                            <ul class="sub-menu" v-if="showcategoryList">
                                <template v-for="(catItem, indexCat) in categoryList" :key="indexCat"
                                    ><li>
                                        <a
                                            :class="{ 'category-selected': catItem.value == checkedSelectedCategory }"
                                            @click="btnFilterMovieByCat(catItem.value)"
                                            >{{ catItem.categoryName }}</a
                                        >
                                    </li></template
                                >
                            </ul>
                        </li>
                        <li class="dropdown first" @click="btnShowSeriesMovie">
                            <a class="btn btn-default dropdown-toggle lv1" data-toggle="dropdown" data-hover="dropdown">
                                Phim bộ <i class="fa fa-angle-down" aria-hidden="true"></i>
                            </a>
                        </li>
                        <li class="dropdown first" @click="btnShowOddMovie">
                            <a class="btn btn-default dropdown-toggle lv1" data-toggle="dropdown" data-hover="dropdown">
                                Phim lẻ <i class="fa fa-angle-down" aria-hidden="true"></i>
                            </a>
                        </li>
                        <li class="dropdown first" @click="btnShowListActor">
                            <a class="btn btn-default dropdown-toggle lv1" data-toggle="dropdown" data-hover="dropdown">
                                Diễn viên <i class="fa fa-angle-down" aria-hidden="true"></i>
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
                            <div class="top-search" style="width: 230px">
                                <input
                                    type="text"
                                    v-model="textSelected"
                                    @input="onSearchItem"
                                    autocomplete="off"
                                    placeholder="Tìm kiếm phim..."
                                    @keydown="inputOnKeyDown"
                                />
                                <MIcon nameIcon="search" :newClass="{ 'search-icon': true }"></MIcon>
                                <div
                                    v-click-away="hideCombobox"
                                    class="cbo-list cbo-search"
                                    :class="newClassBox"
                                    v-if="showCombobox"
                                    ref="scrollContainer"
                                >
                                    <div
                                        class="cbo-item cbo-item-search"
                                        ref="rowItem"
                                        v-for="(item, index) in entities"
                                        :class="{ 'item-search-select': index == indexItemSelect }"
                                        :value="item.movieId"
                                        :key="index"
                                        @click="itemOnSelect(item, index)"
                                    >
                                        <img v-lazy="item.imagePath" style="width: 40px; height: 100%" />
                                        <div class="search-infor">
                                            <div class="movie-name">{{ item.movieName }}</div>
                                            <!-- <p class="rate rate-movie-search">
                                                <font-awesome-icon
                                                    :icon="['fas', 'star']"
                                                    style="color: #f5b50a; font-size: 13px"
                                                /><span></span>
                                                /10
                                            </p> -->
                                            <p
                                                class="number-episode"
                                                :class="{ 'number-episode-select': index == indexItemSelect }"
                                            >
                                                {{
                                                    item.newestEpisode == null
                                                        ? item.genreNames
                                                        : item.newestEpisode.episodeName
                                                }}
                                            </p>
                                        </div>
                                    </div>
                                    <div class="cbo-item" ref="options" v-if="noData">khong co du lieu</div>
                                </div>
                            </div>
                        </li>
                        <li class="loginLink" @click="btnShowFormLogin"><a>Đăng nhập</a></li>
                        <li class="btn signupLink" @click="btnShowFormSignup"><a>Đăng ký</a></li>
                    </ul>
                    <div class="user-header" v-if="hasAccount == true">
                        <div class="top-search" style="width: 230px; margin-right: 12px">
                            <input
                                type="text"
                                v-model="textSelected"
                                @input="onSearchItem"
                                autocomplete="off"
                                placeholder="Tìm kiếm phim..."
                                @keydown="inputOnKeyDown"
                            />
                            <MIcon nameIcon="search" :newClass="{ 'search-icon': true }"></MIcon>
                            <div
                                v-click-away="hideCombobox"
                                class="cbo-list cbo-search"
                                :class="newClassBox"
                                v-if="showCombobox"
                                ref="scrollContainer"
                            >
                                <div
                                    class="cbo-item cbo-item-search"
                                    ref="rowItem"
                                    v-for="(item, index) in entities"
                                    :class="{ 'item-search-select': index == indexItemSelect }"
                                    :value="item.movieId"
                                    :key="index"
                                    @click="itemOnSelect(item, index)"
                                >
                                    <img v-lazy="item.imagePath" style="width: 40px; height: 100%" />
                                    <div class="search-infor">
                                        <div class="movie-name">{{ item.movieName }}</div>
                                        <!-- <p class="rate rate-movie-search">
                                                <font-awesome-icon
                                                    :icon="['fas', 'star']"
                                                    style="color: #f5b50a; font-size: 13px"
                                                /><span></span>
                                                /10
                                            </p> -->
                                        <p
                                            class="number-episode"
                                            :class="{ 'number-episode-select': index == indexItemSelect }"
                                        >
                                            {{
                                                item.newestEpisode == null
                                                    ? item.genreNames
                                                    : item.newestEpisode.episodeName
                                            }}
                                        </p>
                                    </div>
                                </div>
                                <div
                                    class="cbo-item cbo-item-search"
                                    style="justify-content: center"
                                    ref="options"
                                    v-if="noData"
                                >
                                    Không có dữ liệu
                                </div>
                            </div>
                        </div>
                        <div :class="{ 'box-icon-user': true }" @click="showBoxUser = !showBoxUser">
                            <MIcon nameIcon="user" :class="{ 'icon-user': true }"></MIcon>
                        </div>
                        <div class="user-profile" @click="showBoxUser = !showBoxUser">
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
                                <li>
                                    <router-link
                                        class="fa-film"
                                        :to="{ name: 'userhistory', params: { id: userInformation.userId } }"
                                    >
                                        <font-awesome-icon :icon="['fas', 'film']" style="padding-right: 10px" />
                                        Lịch Sử Xem Phim
                                    </router-link>
                                </li>
                                <!-- <li>
                                    <a class="fa-film">Hộp Phim</a>
                                </li>
                                
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

import axios from 'axios';
export default {
    name: 'TheHeader',
    props: ['detectScroll'],
    components: { UserLogin, ForgotPassword },
    watch: {
        textSelected: function (value) {
            if (value.length >= 1) {
                this.showCombobox = true;
                //Lấy danh sách phim tìm kiếm
                axios
                    .get(
                        `${this.$MResource.API}/Movies/GetAllMovieByTypeAndFilter?pageNumber=1&pageSize=1000&columnFilter=3&keyword=${value}`,
                    )
                    .then((response) => {
                        this.entities = response.data.data;
                        console.log('du lieu: ', this.entities);
                        // if (me.movieInfor.dateOfManufactor != null || me.movieInfor.dateOfManufactor != undefined) {
                        //     me.movieInfor.dateOfManufactor = commonJS.bindingFormatDate(me.movieInfor.dateOfManufactor);
                        // }
                        this.entities.forEach((movieItem) => {
                            if (movieItem.imgByte != null && movieItem.imgByte.length > 0) {
                                movieItem.imagePath = 'data:image/png;base64,' + movieItem.imgByte;
                            }
                        });
                        if (this.entities.length < 1) {
                            this.noData = true;
                        } else {
                            this.noData = false;
                        }
                        // if (this.movieInfor.categories != null) {
                        //     this.movieInfor.categories.forEach((element) => {
                        //         this.valueCategory.push(element.value);
                        //     });
                        // }
                    })
                    .catch((err) => {
                        this.$MToastMessage({
                            titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                            messageToast: err,
                            showToastMessage: true,
                            typeToast: 'errorToast',
                        });
                        console.log(err);
                    });
            } else {
                this.showCombobox = false;
            }
        },
        indexItemSelect: function (value) {
            const followScroll = this.$refs.rowItem[value].clientHeight;
            this.$refs.scrollContainer.scrollTop = followScroll * value;
        },
    },

    // computed: {
    //     filters() {
    //         return this.entities.filter((itemEntity) => {
    //             return this.removeVietnameseTones(itemEntity.movieName)
    //                 .toLowerCase()
    //                 .includes(this.removeVietnameseTones(this.textSelected).toLowerCase());
    //         });
    //     },
    // },
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
            // setTimeout(() => {
            //     location.reload();
            // }, 10);
        },

        showDialogSuccess() {
            this.showFormForgotPass = false;
            this.dialogSuccess = true;
            this.textNotif = `Mật khẩu mới đã được gửi đến email đăng ký tài khoản của bạn.
            Vui lòng kiểm tra và đăng nhập lại tài khoản. Bạn sẽ được chuyển hướng về trang chủ sau `;
        },

        /**
         * Hàm chuẩn hóa chuỗi tiếng việt có dấu thành không dấu
         * @param {*} str
         */
        // removeVietnameseTones(str) {
        //     str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, 'a');
        //     str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, 'e');
        //     str = str.replace(/ì|í|ị|ỉ|ĩ/g, 'i');
        //     str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, 'o');
        //     str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, 'u');
        //     str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, 'y');
        //     str = str.replace(/đ/g, 'd');
        //     str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, 'A');
        //     str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, 'E');
        //     str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, 'I');
        //     str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, 'O');
        //     str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, 'U');
        //     str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, 'Y');
        //     str = str.replace(/Đ/g, 'D');

        //     // Some system encode vietnamese combining accent as individual utf-8 characters
        //     // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
        //     str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ''); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
        //     str = str.replace(/\u02C6|\u0306|\u031B/g, ''); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư

        //     // Remove punctuations
        //     // Bỏ dấu câu, kí tự đặc biệt
        //     str = str.replace(/!|@|%|\^|\*|\(|\)|\+|=|<|>|\?|\/|,|\.|:|;|'|"|&|#|\[|\]|~|\$|_|`|-|{|}|\||\\/g, '');

        //     // Remove extra spaces
        //     // Bỏ các khoảng trắng liền nhau
        //     str = str.replace(/ + /g, ' ');
        //     str = str.trim();

        //     return str;
        // },

        /**
         * Lọc giá trị item khi có từ khóa tìm kiếm được nhập vào
         * CreatedBy: huynq (9/1/2023)
         */
        // onSearchItem() {
        //     if (this.filters.length < 1) {
        //         this.noData = true;
        //     } else {
        //         this.noData = false;
        //     }
        // },

        /**
         * Các sự kiện với bàn phím
         * CreatedBy: huynq (9/1/2023)
         * @param {*} event
         */
        inputOnKeyDown(event) {
            try {
                const key = event.keyCode;
                switch (key) {
                    case this.$MEnum.KEY_CODE.ENTER:
                        //xác định item đang chọn dựa vào index
                        //// eslint-disable-next-line no-case-declarations
                        // const item = this.filters[this.indexItemSelect];

                        // eslint-disable-next-line no-case-declarations
                        const item = this.entities[this.indexItemSelect];
                        this.itemOnSelect(item, this.indexItemSelect);
                        this.showCombobox = false;
                        this.textSelected = '';
                        break;
                    case this.$MEnum.KEY_CODE.ROW_UP:
                        // this.showCombobox = true;
                        if (this.showCombobox == true) {
                            if (this.indexItemSelect > 0) {
                                this.indexItemSelect--;
                                // this.fixScrolling();
                            }
                        } else return;
                        break;
                    case this.$MEnum.KEY_CODE.ROW_DOWN:
                        // this.showCombobox = true;

                        if (this.showCombobox == true) {
                            // // eslint-disable-next-line no-case-declarations
                            // let maxLength = this.filters.length;

                            // eslint-disable-next-line no-case-declarations
                            let maxLength = this.entities.length;

                            if (this.indexItemSelect < maxLength - 1) {
                                this.indexItemSelect++;
                                // this.fixScrolling();
                            }
                        } else return;
                        break;
                    default:
                        break;
                }
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
         * Kết quả tìm kiếm
         * @param {*} item
         */
        itemOnSelect(item, index) {
            try {
                console.log('id lay duoc: ', item.movieName);
                this.$router.push({ name: 'MovieSingle', params: { id: item.movieId } });
                // location.reload();
                this.indexItemSelect = index;
            } catch (err) {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            }
        },

        btnShowSeriesMovie() {
            this.$router.push({ name: 'moviegrid', params: { id: 1 } });
        },

        btnShowOddMovie() {
            this.$router.push({ name: 'moviegrid', params: { id: 2 } });
        },

        btnFilterMovieByCat(idCate) {
            this.$router.push({ name: 'moviegrid', params: { id: idCate } });
            this.checkedSelectedCategory = idCate;
        },

        btnShowListActor() {
            this.$router.push({ name: 'actorgrid' });
        },

        btnGoHome() {
            this.$router.push('/');
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

        //Lấy danh sách phim tìm kiếm
        axios
            .get(`${this.$MResource.API}/Movies/GetAllMovieByTypeAndFilter?pageNumber=1&pageSize=1000&columnFilter=3`)
            .then((response) => {
                this.entities = response.data.data;

                // if (me.movieInfor.dateOfManufactor != null || me.movieInfor.dateOfManufactor != undefined) {
                //     me.movieInfor.dateOfManufactor = commonJS.bindingFormatDate(me.movieInfor.dateOfManufactor);
                // }
                this.entities.forEach((movieItem) => {
                    if (movieItem.imgByte != null && movieItem.imgByte.length > 0) {
                        movieItem.imagePath = 'data:image/png;base64,' + movieItem.imgByte;
                    }
                });
                // if (this.movieInfor.categories != null) {
                //     this.movieInfor.categories.forEach((element) => {
                //         this.valueCategory.push(element.value);
                //     });
                // }
            })
            .catch((err) => {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            });

        //Lây danh sách thể loại
        axios
            .get(`${this.$MResource.API}/Categories`)
            .then((response) => {
                this.categoryList = response.data;
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
            showFormLogin: false,
            isFormSignup: false,
            hasAccount: false,
            userInformation: {},
            showBoxUser: false,

            showFormForgotPass: false,

            dialogSuccess: false,
            textNotif: null,

            textSelected: '',
            showCombobox: false,
            entities: [],
            itemSelected: null,
            indexItemSelect: -1,

            noData: false,

            categoryList: [],
            showcategoryList: false,

            checkedSelectedCategory: '',
        };
    },
};
</script>

<style scoped>
@import url('./header.css');

.cbo-search {
    padding: 0;
}

.cbo-item-search {
    height: 70px;
    justify-content: flex-start;
    column-gap: 8px;
    background-color: #475569;
    color: #abb7c4;
    max-height: 350px !important;
}

/* .search-infor {
    display: flex;
    flex-direction: column;
    column-gap: 2px;
} */

.movie-name {
    font-weight: 700;
}

/* .rate-movie-search {
    font-size: 13px;
    margin-bottom: 0;
    line-height: 1.5rem !important;
} */

.number-episode {
    margin-bottom: 0;
}

.item-search-select {
    background-color: #cbd5e1;
    color: #1e293b;
}

.number-episode-select {
    color: #1e293b;
}

.category-selected {
    color: #dcf836;
}

.router-link-exact-active {
    color: #dcf836 !important;
}
</style>
