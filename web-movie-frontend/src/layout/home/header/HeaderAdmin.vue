<template>
    <div class="header">
        <div class="header-left"></div>
        <div class="header-right">
            <div class="notification" @click="btnShowNotification">
                <svg width="24" height="25" viewBox="0 0 24 25" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path
                        d="M19 11V8C19 6.14348 18.2625 4.36301 16.9498 3.05025C15.637 1.7375 13.8565 1 12 1C10.1435 1 8.36299 1.7375 7.05023 3.05025C5.73748 4.36301 5 6.14348 5 8V11C5 14.3 2 15.1 2 17C2 18.7 5.9 20 12 20C18.1 20 22 18.7 22 17C22 15.1 19 14.3 19 11Z"
                        stroke="#757575"
                        stroke-width="2"
                        stroke-miterlimit="10"
                        stroke-linecap="square"
                    ></path>
                    <path
                        d="M12 22C11.0323 22.0166 10.0643 21.9832 9.09998 21.9C9.2139 22.2836 9.40342 22.6405 9.65735 22.9497C9.91128 23.259 10.2245 23.5143 10.5786 23.7006C10.9327 23.887 11.3205 24.0006 11.7192 24.0349C12.1178 24.0691 12.5193 24.0233 12.9 23.9C13.3693 23.7514 13.796 23.4921 14.144 23.144C14.4921 22.796 14.7514 22.3693 14.9 21.9C13.9358 21.985 12.9678 22.0184 12 22Z"
                        fill="#757575"
                    ></path>
                </svg>
                <span class="notif" :class="{ isDisplayNotif: isShowedNotif }">{{ orderList.length }}</span>
                <ul
                    class="dropdown-menu dropdown-menu-end dropdown-menu-arrow notifications show"
                    data-popper-placement="bottom-end"
                    style="
                        position: absolute;
                        inset: 0px 0px auto auto;
                        margin: 0px;
                        transform: translate3d(7.2px, 23.2px, 0px);
                        max-height: 500px;
                    "
                    v-if="showBoxNotif"
                    v-click-away="() => (showBoxNotif = false)"
                >
                    <li class="dropdown-header">Bạn có {{ orderList.length }} yêu cầu</li>
                    <template v-for="(orderItem, indexOrder) in orderList" :key="indexOrder">
                        <li>
                            <hr class="dropdown-divider" />
                        </li>

                        <li class="notification-item">
                            <font-awesome-icon
                                style="margin: 0 15px 0 10px; font-size: 24px"
                                :icon="['fas', 'money-bill-trend-up']"
                            />
                            <div>
                                <h4>{{ orderItem.orderName }}</h4>
                                <p>{{ orderItem.username }}</p>
                                <p>{{ descriptionTime }} ago</p>
                            </div>
                        </li>
                    </template>
                </ul>
            </div>

            <div class="user-header" @click="showBoxUser = !showBoxUser">
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
                        <!-- <li>
                            <a class="fa-user"
                                ><font-awesome-icon :icon="['fas', 'user']" style="padding-right: 10px" />Thông tin tài
                                khoản</a
                            >
                        </li> -->
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
    </div>
</template>
<script>
import axios from 'axios';
import moment from 'moment';
import 'moment/locale/vi';
moment.locale('vi');
export default {
    name: 'HeaderAdmin',
    methods: {
        btnLogout() {
            this.userInformation = {};
            localStorage.removeItem('adminInfor');

            this.$router.push('/');
            setTimeout(() => {
                location.reload();
            }, 1000);
        },

        btnShowNotification() {
            this.showBoxNotif = !this.showBoxNotif;
            if (!this.isShowedNotif) {
                this.isShowedNotif = true;
            }
        },
    },
    created() {
        const storage = localStorage.getItem('adminInfor');
        if (storage !== null) {
            this.userInformation = JSON.parse(storage);
            // this.accountAdmin = true;
            axios
                .get(`${this.$MResource.API}/OrderUsers`)
                .then((response) => {
                    this.orderList = response.data;
                    console.log('cac yeu cau: ', this.orderList);
                    for (var item in this.orderList) {
                        const now = moment(); // Lấy thời điểm hiện tại
                        const date = moment(this.orderList[item].createdDate); // Lấy thời điểm từ chuỗi

                        // Tính toán khoảng thời gian từ thời điểm đó đến hiện tại
                        const duration = moment.duration(now.diff(date));

                        // Xuất ra chuỗi mô tả khoảng thời gian
                        this.descriptionTime = duration.humanize();
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
        }
    },

    data() {
        return {
            // accountAdmin: false,
            userInformation: {},
            showBoxUser: false,
            orderList: [],

            descriptionTime: null,

            showBoxNotif: false,

            isShowedNotif: false,
        };
    },
};
</script>
<style>
/* @import url(./header.css); */
/* Css header admin */
.header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 0 16px;
}

.header-left {
    display: flex;
    align-items: center;
    column-gap: 24px;
}

.name-company select {
    background-color: transparent;
    border: none;
    outline: none;
    color: #212121;
    font-weight: 500;
}

.header-right {
    display: flex;
    align-items: center;
    column-gap: 24px;
}

.isDisplayNotif {
    visibility: visible;
    opacity: 0;
    display: none;
}
</style>
