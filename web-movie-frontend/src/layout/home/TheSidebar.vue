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
            <router-link class="menu-item" to="/account">
                <font-awesome-icon :icon="['fas', 'users']" style="font-size: 16px; color: #e0e0e0" />
                <div class="menu-text" style="font-size: 16px">Tài khoản</div>
            </router-link>
            <router-link to="/movieList" class="menu-item">
                <font-awesome-icon :icon="['fas', 'clapperboard']" style="font-size: 16px; color: #e0e0e0" />
                <div class="menu-text" style="font-size: 16px">Quản lý phim</div>
            </router-link>
            <router-link to="/actor" class="menu-item">
                <font-awesome-icon :icon="['fas', 'blog']" style="font-size: 16px; color: #e0e0e0" />
                <div class="menu-text" style="font-size: 16px">Quản lý diễn viên</div>
            </router-link>
            <router-link to="/orderUserList" class="menu-item">
                <font-awesome-icon :icon="['fas', 'masks-theater']" style="font-size: 16px; color: #e0e0e0" />
                <div class="menu-text" style="font-size: 16px">Yêu cầu</div>
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
