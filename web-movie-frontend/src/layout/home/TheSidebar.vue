<template>
    <div class="menu">
        <div class="menu-top">
            <div class="icon-size">
                <svg width="24" height="24" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M0 0H4V4H0V0Z" fill="white"></path>
                    <path d="M6 0H10V4H6V0Z" fill="white"></path>
                    <path d="M12 0H16V4H12V0Z" fill="white"></path>
                    <path d="M12 6H16V10H12V6Z" fill="white"></path>
                    <path d="M12 12H16V16H12V12Z" fill="white"></path>
                    <path d="M6 6H10V10H6V6Z" fill="white"></path>
                    <path d="M6 12H10V16H6V12Z" fill="white"></path>
                    <path d="M0 6H4V10H0V6Z" fill="white"></path>
                    <path d="M0 12H4V16H0V12Z" fill="white"></path>
                </svg>
            </div>
            <div class="logo" style="font-size: 20px; font-weight: 700; color: #fff">DASHBOARD</div>
        </div>
        <div class="menu-content">
            <router-link class="menu-item" to="/overviewAdmin">
                <svg width="24" height="24" viewBox="0 0 18 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path
                        d="M13.56 9.17003L8.36 11.8199L8.56 6.16002V5.03001C7.12689 4.92246 5.6954 5.25857 4.45996 5.99278C3.22451 6.72699 2.245 7.82369 1.65444 9.13389C1.06389 10.4441 0.89092 11.9044 1.15903 13.3163C1.42713 14.7282 2.12331 16.0233 3.15303 17.0259C4.18274 18.0284 5.49608 18.6897 6.91466 18.9199C8.33324 19.1502 9.78831 18.9382 11.0822 18.3128C12.3762 17.6875 13.4463 16.6789 14.1472 15.4243C14.8481 14.1697 15.1458 12.7298 15 11.3C14.9124 10.4372 14.6612 9.59882 14.26 8.82994L13.56 9.17003Z"
                        stroke="#A8A8A8"
                        stroke-width="1.5"
                        stroke-miterlimit="10"
                    />
                    <path
                        d="M11.06 7.64001L16.25 4.98999L16.93 4.64001C16.3891 3.58133 15.5875 2.67786 14.6007 2.01477C13.614 1.35168 12.4746 0.950782 11.29 0.849976V1.97998L11.06 7.64001Z"
                        stroke="#A8A8A8"
                        stroke-width="1.5"
                        stroke-miterlimit="10"
                    />
                </svg>
                <div class="menu-text" style="font-size: 16px">Tổng quan</div>
            </router-link>
            <router-link class="menu-item" to="/account">
                <font-awesome-icon :icon="['fas', 'users']" style="font-size: 16px; color: #e0e0e0" />
                <div class="menu-text" style="font-size: 15.7px">Quản lý tài khoản</div>
            </router-link>
            <router-link to="/movieList" class="menu-item">
                <font-awesome-icon :icon="['fas', 'clapperboard']" style="font-size: 16px; color: #e0e0e0" />
                <div class="menu-text" style="font-size: 16px">Quản lý phim</div>
            </router-link>
            <router-link to="/actor" class="menu-item">
                <font-awesome-icon :icon="['fas', 'masks-theater']" style="font-size: 16px; color: #e0e0e0" />
                <div class="menu-text" style="font-size: 16px">Quản lý diễn viên</div>
            </router-link>
            <router-link to="/orderUserList" class="menu-item">
                <svg width="24" height="24" viewBox="0 0 14 17" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path
                        d="M13 4.28004V15C13 15.2652 12.8946 15.5196 12.7071 15.7072C12.5196 15.8947 12.2652 16 12 16H2C1.73478 16 1.48043 15.8947 1.29289 15.7072C1.10536 15.5196 1 15.2652 1 15V2.00001C1 1.73479 1.10536 1.48039 1.29289 1.29286C1.48043 1.10532 1.73478 1.00001 2 1.00001H9.63C9.76047 0.999225 9.88608 1.04948 9.98001 1.14002L12.85 3.92005C12.9423 4.0177 12.9957 4.14574 13 4.28004V4.28004Z"
                        stroke="#A8A8A8"
                        stroke-width="1.5"
                        stroke-miterlimit="10"
                    />
                    <path
                        d="M9 1V4.72998C9 4.80159 9.02844 4.87026 9.07907 4.9209C9.12971 4.97153 9.19838 5 9.26999 5H13"
                        stroke="#A8A8A8"
                        stroke-width="1.5"
                        stroke-miterlimit="10"
                    />
                    <path
                        d="M9.25 7.75H4.75C4.33579 7.75 4 8.08579 4 8.5C4 8.91421 4.33579 9.25 4.75 9.25H9.25C9.66421 9.25 10 8.91421 10 8.5C10 8.08579 9.66421 7.75 9.25 7.75Z"
                        fill="#A8A8A8"
                    />
                    <path
                        d="M9.25 10.75H4.75C4.33579 10.75 4 11.0858 4 11.5C4 11.9142 4.33579 12.25 4.75 12.25H9.25C9.66421 12.25 10 11.9142 10 11.5C10 11.0858 9.66421 10.75 9.25 10.75Z"
                        fill="#A8A8A8"
                    />
                </svg>
                <div class="menu-text" style="font-size: 16px">Quản lý yêu cầu</div>
            </router-link>
        </div>
    </div>
</template>
<script>
export default {
    name: 'TheSidebar',
};
</script>
<style>
.menu-content .router-link-active,
.router-link-exact-active {
    background-color: #616161;
}
.separation-line {
    margin: 5px 20px;
    border-top: 0.5px solid #fff;
    opacity: 0.3;
}
</style>
