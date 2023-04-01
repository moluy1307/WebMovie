<template>
    <div class="combobox" style="min-width: 32px; height: 24px" ref="myDropdown">
        <MIcon
            ref="test"
            nameIcon="arrow-blue"
            @click="btnShowBoxFunction"
            :keyboard="inputOnKeyDown"
            :anim="{ 'svg-arrow-click': showBoxFunction }"
            :newClass="newClassIcon"
        ></MIcon>
        <div
            v-click-away="hideCombobox"
            ref="dialog"
            v-if="showBoxFunction"
            class="ddl-function"
            :class="[isTop ? 'ddl-function-top' : 'ddl-function-bottom']"
            style="z-index: 13"
            :newClass="{ 'button-function': true }"
        >
            <div class="cbo-item" @click="btnReplicationOnClick">
                <div class="content-item">{{ this.$MResource.VI.REPLICATE_DATA }}</div>
            </div>
            <!-- <div v-if="isTreeTable == true" class="cbo-item" @click="btnDeleteTree">
                <div class="content-item">{{ this.$MResource.VI.DELETE_DATA }}</div>
            </div> -->
            <div class="cbo-item" @click="btnShowConfirmDialog">
                <div class="content-item">{{ this.$MResource.VI.DELETE_DATA }}</div>
            </div>
            <div class="cbo-item" @click="btnChangeStatus">
                <div class="content-item">{{ enableActive }}</div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: 'MBoxFunction',
    props: [
        'position',
        'newClassIcon',
        'tableContentRect',
        'isTreeTable',
        'codeDelete',
        'entityDelete',
        'enableActive',
    ],
    methods: {
        /**
         * Hiển thị dialog xác nhận xóa dữ liệu nhân viên
         * CreatedBy: huynq (18/1/2023)
         */
        btnShowConfirmDialog() {
            try {
                this.showBoxFunction = false;
                this.$emit('itemDelete');
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
         * Hiển thị dialog xác nhận xóa dữ liệu nhân viên
         * CreatedBy: huynq (18/1/2023)
         */
        btnDeleteTree() {
            try {
                this.showBoxFunction = false;
                console.log('cac gia tri nhan duoc: ', this.entityDelete, this.codeDelete);
                this.$emit('itemDelete', this.entityDelete, this.codeDelete);
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
         * Thực hiện nhân bản dữ liệu nhân viên
         * CreatedBy: huynq (18/1/2023)
         */
        btnReplicationOnClick() {
            try {
                this.showBoxFunction = false;
                this.$emit('getIdReplicate');
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
         * Thay đổi trạng thái bản ghi (Đang sử dụng thành ngừng sử dụng và ngược lại)
         * CreatedBy: huynq (24/3/2023)
         */
        btnChangeStatus() {
            try {
                this.showBoxFunction = false;
                this.isActive = !this.isActive;
                // console.log('gia tri status: ', this.isActive);
                this.$emit('valueStatus');
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
         * Ẩn/hiện box chức năng
         * CreatedBy: huynq (18/1/2023)
         */
        btnShowBoxFunction(e) {
            try {
                const rect = e.target.getBoundingClientRect();
                // const tableDataRect =
                //     e.target.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.getBoundingClientRect();
                // const tableContentHeight = this.tableContentRect.bottom;
                // console.log(tableContentHeight);
                // this.isTop = tableContentHeight - rect.bottom < 120;
                const screenHeight = window.innerHeight;
                this.isTop = screenHeight - rect.bottom < 120;
                this.showBoxFunction = !this.showBoxFunction;
            } catch (err) {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            }
        },

        // /**
        //  * Đóng combobox khi click bên ngoài
        //  * CreatedBy: huynq - (9/1/2023)
        //  * @param {*} event
        //  */
        // handleClickOutside(event) {
        //     try {
        //         if (!this.$refs.myDropdown.contains(event.target)) {
        //             this.showBoxFunction = false;
        //         }
        //     } catch (err) {
        //         this.$MToastMessage({
        //             titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
        //             messageToast: err,
        //             showToastMessage: true,
        //             typeToast: 'errorToast',
        //         });
        //     }
        // },

        // /**
        //  * Đóng combobox khi click bên ngoài
        //  * CreatedBy: huynq - (9/1/2023)
        //  * @param {*} event
        //  */
        hideCombobox() {
            try {
                this.showBoxFunction = false;
                // const dropDown = document.querySelectorAll('.br-none');
                // dropDown.forEach((item) => {
                //     if (item.classList.contains('rewrite-td')) {
                //         item.classList.remove('rewrite-td');
                //     }
                // });
            } catch (err) {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            }
        },
    },
    // mounted() {
    //     document.addEventListener('click', this.handleClickOutside);
    // },

    // unmounted() {
    //     document.removeEventListener('click', this.handleClickOutside);
    // },
    // created() {
    //     this.isActive = this.enableActive;
    // },
    data() {
        return {
            showBoxFunction: false,
            isTop: false,

            isActive: true,
        };
    },
};
</script>

<style>
@import url('../combobox/combobox.css');
</style>
