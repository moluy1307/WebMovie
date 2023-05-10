<template>
    <div class="overlay openform" @click.self="$emit('onClose')">
        <div class="login-wrapper" id="login-content">
            <div class="login-content">
                <a @click="$emit('onClose')" class="close">x</a>
                <h3>Bình luận phim {{ nameOfMovie }}</h3>
                <form method="post" action="#">
                    <div class="row">
                        <label for="evaluate">
                            Đánh giá:
                            <font-awesome-icon
                                id="evaluate"
                                v-for="n in 10"
                                :key="n"
                                @click="selectRating(n)"
                                :icon="['fas', 'star']"
                                :class="[n <= selectedRating ? 'star-selected' : 'star-icon']"
                            />
                        </label>
                    </div>

                    <div class="row">
                        <label for="comment-fill">
                            Viết bình luận:
                            <textarea
                                v-model="commentInfor.commentContent"
                                type="text"
                                name="password"
                                id="comment-fill"
                                style="min-height: 100px"
                                class="text-comment"
                            ></textarea>
                        </label>
                        <!-- <MInput
                            labelInput="Viết bình luận"
                            styleLabel="margin-bottom: 8px !important"
                            styleBoxInput="height: 42px !important; margin-bottom: 25px"
                            newStyle="height: 42px !important;"
                        ></MInput> -->
                    </div>

                    <div class="row">
                        <div class="btn-login" @click="btnSendComment">Gửi</div>
                    </div>
                </form>
                <!-- <div class="row">
                    <p>Or via social</p>
                    <div class="social-btn-2">
                        <a class="fb" href="#"><i class="ion-social-facebook"></i>Facebook</a>
                        <a class="tw" href="#"><i class="ion-social-twitter"></i>twitter</a>
                    </div>
                </div> -->
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
export default {
    props: ['nameOfMovie', 'idMovie'],
    name: 'MovieWriteReview',

    methods: {
        selectRating(rating) {
            this.selectedRating = rating;
            this.commentInfor.rating = parseInt(this.selectedRating);
        },

        updateMediumScoreMovie() {
            axios
                .get(`${this.$MResource.API}/Comments/filter?keyword=${this.idMovie}&pageNumber=1&pageSize=1000`)
                .then((response) => {
                    this.commentList = response.data.data;
                    this.commentList.forEach((element) => {
                        if (element.rating != null) {
                            this.countRating++;
                            this.totalRating += element.rating;
                        }
                    });
                    if (this.totalRating == 0 || this.totalRating == null || this.totalRating == undefined) {
                        this.mediumScore = 0;
                    } else {
                        this.mediumScore = this.totalRating / this.countRating;
                    }
                    console.log('da co diem : ', this.mediumScore);

                    //Call Api cập nhật điểm trung bình
                    axios
                        .put(
                            `${this.$MResource.API}/Movies/UpdateMediumScore?movieId=${this.idMovie}&mediumScore=${this.mediumScore}`,
                        )
                        .then(() => {})
                        .catch((err) => {
                            this.$MToastMessage({
                                titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                                messageToast: err,
                                showToastMessage: true,
                                typeToast: 'errorToast',
                            });
                        });
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

        btnSendComment() {
            var me = this;
            me.commentInfor.movieId = this.idMovie;
            const storage = localStorage.getItem('userInfor');
            if (storage !== null) {
                this.userInformation = JSON.parse(storage);
                me.commentInfor.userId = this.userInformation.userId;

                axios
                    .post(`${this.$MResource.API}/Comments`, me.commentInfor)
                    .then(() => {
                        this.$MToastMessage({
                            titleToast: this.$MResource.VI.TOAST.TITLE_SUCCESS,
                            messageToast: this.$MResource.VI.TOAST.ADD_SUCCESS,
                            showToastMessage: true,
                            typeToast: 'successToast',
                        });
                        this.updateMediumScoreMovie();
                        me.$emit('onClose');
                        location.reload();
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
    },

    data() {
        return {
            selectedRating: 0,

            commentInfor: {
                commentContent: '',
                userId: '',
                movieId: '',
                rating: 0,
            },
            userInformation: {},

            countRating: 0,
            totalRating: 0,
            mediumScore: 0,
            commentList: [],
        };
    },
};
</script>

<style scoped>
.star-selected {
    color: #f5b50a;
    font-size: 20px;
}

.star-icon {
    color: #ccc;
    font-size: 20px;
}
</style>
