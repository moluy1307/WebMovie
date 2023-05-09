<template>
    <div id="media" class="tab">
        <div class="row">
            <div class="rv-hd">
                <div>
                    <h3>Trailer phim</h3>
                    <h2>Thanh Gươm Diệt Quỷ: Phần Làng Rèn Kiếm</h2>
                </div>
            </div>
            <div class="title-hd-sm">
                <h4>Videos <span>(8)</span></h4>
            </div>
            <div class="mvsingle-item media-item">
                <template v-for="n in 10" :key="n">
                    <div class="vd-item">
                        <div class="vd-it">
                            <iframe
                                class="item-video"
                                src="https://www.youtube.com/embed/o-0hcF97wy0"
                                style="width: 170px; height: 111px"
                            ></iframe>
                        </div>
                        <div class="vd-infor">
                            <h6><a href="#">Trailer: Watch New Scenes</a></h6>
                            <p class="time">1: 31</p>
                        </div>
                    </div>
                </template>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'MovieMedia',
};
</script>

<style></style>
