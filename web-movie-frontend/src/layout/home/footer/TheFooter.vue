<template>
    <!-- footer section-->
    <footer class="ht-footer">
        <div class="container">
            <div class="flex-parent-ft">
                <div class="flex-child-ft item1">
                    <a href="index-2.html"><img class="logo" src="../../../assets/img/logo1.png" alt="" /></a>
                    <p>
                        Đại học Công Nghiệp<br />
                        Bắc Từ LIêm, Hà Nội
                    </p>
                    <p>Liên hệ: <a href="#">0123456789</a></p>
                </div>
                <div class="flex-child-ft item2">
                    <h4>Về Block Buster</h4>
                    <ul>
                        <li><a href="#">Trang chủ</a></li>
                        <li><a href="#">Thể loại</a></li>
                        <li><a href="#">Phim bộ</a></li>
                        <li><a href="#">Phim lẻ</a></li>
                        <li><a href="#">Bảng xếp hạng</a></li>
                    </ul>
                </div>
                <div class="flex-child-ft item3">
                    <h4>Quy định</h4>
                    <ul>
                        <li><a href="#">Điều khoản chung</a></li>
                        <li><a href="#">Chính sách riêng tư</a></li>
                    </ul>
                </div>
                <!-- <div class="flex-child-ft item4">
                    <h4>Account</h4>
                    <ul>
                        <li><a href="#">My Account</a></li>
                        <li><a href="#">Watchlist</a></li>
                        <li><a href="#">Collections</a></li>
                        <li><a href="#">User Guide</a></li>
                    </ul>
                </div> -->
                <div class="flex-child-ft item5" style="max-width: 250px">
                    <h4>Bản tin</h4>
                    <p>Theo dõi hệ thống bản tin của chúng tôi ngay bây giờ để nhận tin tức mới nhất từ chúng tôi.</p>
                    <form action="#">
                        <input type="text" placeholder="Enter your email..." />
                        <MIcon nameIcon="email" :newClass="{ 'input-email': true }"></MIcon>
                    </form>
                    <a href="#" class="btn"
                        >Theo dõi ngay <MIcon nameIcon="arrow-next" :newClass="{ 'arrow-next': true }"></MIcon
                    ></a>
                </div>
            </div>
        </div>
    </footer>
    <!-- end of footer section-->
</template>

<script>
export default {
    name: 'TheFooter',
};
</script>

<style scoped>
.flex-child-ft form {
    position: relative;
}
.input-email {
    position: absolute;
    top: 0;
    right: 4px;
    background-color: transparent !important;
}

.flex-child-ft a.btn {
    display: flex;
}
.arrow-next {
    position: relative !important;
    width: 7px !important;
    height: 12px !important;
    margin: 4px 0 0 4px;
    background-color: transparent !important;
    color: #dd003f;
}
</style>
