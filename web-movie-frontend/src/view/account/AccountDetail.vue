<template>
    <div>chi tiet tai khoan</div>
</template>

<script>
import axios from 'axios';
// import { uuid } from 'vue-uuid';
export default {
    name: 'AccountDetail',
    props: ['id'],
    computed: {
        isAdd() {
            if (this.id) {
                return false;
            } else {
                return true;
            }
        },
    },

    methods: {
        onFileChange(event) {
            this.file = event.target.files[0];
            this.movieInfor.imagePath = URL.createObjectURL(this.file);
        },
        btnSaveOnClick() {
            var me = this;
            const formData = new FormData();
            // formData.append('movieId', crypto.randomUUID(movieInfor.movieName"));
            formData.append('movieCode', this.movieInfor.movieCode);
            formData.append('movieName', this.movieInfor.movieName);
            // if (this.file) {
            //     formData.append('imagePath', this.file);
            // }
            if (this.$refs.fileInput.files.length > 0) {
                console.log('da co duong dan link image', this.$refs.fileInput.files[0]);
                // formData.append('image', this.$refs.fileInput.files[0]);
                formData.append('image', this.file);
            }
            if (me.isAdd) {
                axios
                    .post('https://localhost:7112/api/v1/Movies/PostMovie', formData, {
                        headers: { 'Content-Type': 'multipart/form-data' },
                    })
                    .then((response) => {
                        // this.$MToastMessage({
                        //     titleToast: this.$MResource.VI.TOAST.TITLE_SUCCESS,
                        //     messageToast: this.$MResource.VI.TOAST.ADD_SUCCESS,
                        //     showToastMessage: true,
                        //     typeToast: 'successToast',
                        // });
                        me.$emit('onClose');
                        // me.$emit('dataRecovery');
                        console.log('gia tri tra ve khi da them tham cong: ', response.data);
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
    created() {
        var me = this;
        if (me.id) {
            axios
                .get(`${this.$MResource.API}/Movies/GetMovieById?movieId=${me.id}`)
                .then((response) => {
                    me.movieInfor = response.data;
                    // if (this.movieInfor.imagePath) {
                    //     console.log('co hinh anh');
                    //     const blob = fetch(`data:image/png;base64,${this.movieInfor.imagePath}`).then((res) =>
                    //         res.blob(),
                    //     );
                    //     this.movieInfor.imagePath = URL.createObjectURL(blob);
                    // }
                    // me.movieInfor.imagePath = `@Url.Content("~/Images/${response.data.imagePath}")`;
                    if (this.movieInfor.imgByte != null && this.movieInfor.imgByte.length > 0) {
                        this.movieInfor.imagePath = 'data:image/png;base64,' + this.movieInfor.imgByte;
                        this.file = 'data:image/png;base64,' + this.movieInfor.imgByte;
                        console.log('duong dan image: ', this.file);
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
        } else {
            me.movieInfor = { movieCode: '', movieName: '' };
        }
    },
    data() {
        return {
            movieInfor: {
                movieCode: '',
                movieName: '',
            },
            file: null,
            // uuid: uuid.v1(),
        };
    },
};
</script>

<style scoped>
@import url('./account.css');
</style>
