<template>
    <div id="moviesrelated" class="tab">
        <div class="row">
            <h3>Phim tương tự</h3>
            <h2 style="margin-bottom: 15px">Thanh Gươm Diệt Quỷ: Phần Làng Rèn Kiếm</h2>
            <div class="topbar-filter">
                <p>Tổng số: <span>12 phim</span></p>
                <label>Sắp xếp theo:</label>
                <select>
                    <option value="popularity">Mới nhất</option>
                    <option value="popularity">Cũ nhất</option>
                </select>
            </div>
            <template v-for="n in 3" :key="n">
                <div class="movie-item-style-2">
                    <img src="https://image.tmdb.org/t/p/w342/A3ZbZsmsvNGdprRi2lKgGEeVLEH.jpg" alt="" />
                    <div class="mv-item-infor">
                        <h6>
                            <a href="#">Shazam <span>(2023)</span></a>
                        </h6>
                        <p class="rate">
                            <font-awesome-icon :icon="['fas', 'star']" style="color: #f5b50a; font-size: 18px" /><span
                                >8.1</span
                            >
                            /10
                        </p>
                        <p class="describe">
                            Sau khi tìm thấy được cây quyền trượng sở hữu sức mạnh đặc biệt, những người con gái của
                            thần Atlas lên kế hoạch tìm các Shazam nhằm lấy lại nguồn phép thuật của các vị thần Hy Lạp
                            cổ đại mà họ cho là đã bị những đứa trẻ loài người đánh cắp. Sau khi tìm thấy được cây quyền
                            trượng sở hữu sức mạnh đặc biệt, những người con gái của thần Atlas lên kế hoạch tìm các
                            Shazam nhằm lấy lại nguồn phép thuật của các vị thần Hy Lạp cổ đại mà họ cho là đã bị những
                            đứa trẻ loài người đánh cắp.
                        </p>
                        <p class="run-time">
                            Thời lượng: 2h21’ . <span>Quốc gia: Mỹ </span> .
                            <span>Ngày sản xuất: 15/3/2023</span>
                        </p>
                        <p>Đạo diễn: <a href="#">Joss Whedon</a></p>
                        <p>
                            Diễn viên: <a href="#">Robert Downey Jr.,</a>
                            <a href="#">Chris Evans,</a>
                            <a href="#"> Chris Hemsworth</a>
                        </p>
                    </div>
                </div>
            </template>
            <div class="topbar-filter">
                <label>Số phim trên một trang:</label>
                <select>
                    <option value="range">5 phim</option>
                    <option value="saab">10 phim</option>
                </select>
                <div class="pagination2">
                    <a class="active" href="#">1</a>
                    <a href="#">2</a>
                    <a href="#"><i class="ion-arrow-right-b"></i></a>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'MovieRelated',
};
</script>

<style></style>
