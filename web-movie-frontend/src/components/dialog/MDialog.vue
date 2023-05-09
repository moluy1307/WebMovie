<template>
    <div class="modal">
        <div class="dialog">
            <div class="dialog-header">
                <div class="title-dialog">{{ titleDialog }}</div>
                <div @click="btnCloseOnClick" class="icon-size" style="cursor: pointer">
                    <svg
                        width="12.01"
                        height="12.01"
                        viewBox="0 0 18 18"
                        fill="none"
                        xmlns="http://www.w3.org/2000/svg"
                    >
                        <path
                            d="M17 1L1 17"
                            stroke="#6B6C72"
                            stroke-width="2"
                            stroke-miterlimit="10"
                            stroke-linecap="round"
                        />
                        <path
                            d="M17 17L1 1"
                            stroke="#6B6C72"
                            stroke-width="2"
                            stroke-miterlimit="10"
                            stroke-linecap="round"
                        />
                    </svg>
                </div>
            </div>
            <div class="dialog-content">
                <div class="dialog-icon">
                    <MIcon
                        v-if="typeDialog === 'errorDialog'"
                        nameIcon="error"
                        newStyle="position: relative; width: 36px; height: 36px; background-color: transparent;"
                    ></MIcon>
                    <MIcon
                        v-else-if="typeDialog === 'warningDialog'"
                        nameIcon="warning"
                        newStyle="position: relative; width: 36px; height: 36px;background-color: transparent;"
                    ></MIcon>
                    <MIcon
                        v-else-if="typeDialog === 'questionDialog'"
                        nameIcon="question"
                        newStyle="position: relative; width: 36px; height: 36px;background-color: transparent;"
                    ></MIcon>
                </div>
                <div class="dialog-title">{{ depict }}</div>
            </div>
            <div class="btn-dialog">
                <button
                    ref="btnFirstDialog"
                    :class="hasCancelButton"
                    @click="btnCancelButton"
                    class="btn-custom-default btn-bg"
                >
                    Hủy
                </button>
                <div class="btn-dialog-right">
                    <button v-if="hasCloseButton" @click="btnCloseOnClick" class="btn-custom-default btn-bg">
                        Không
                    </button>
                    <button
                        ref="btnLastInDialog"
                        v-if="dialogConfirm === 'false'"
                        @click="btnCloseOnClick"
                        class="btn-custom-default"
                    >
                        {{ titleButton }}
                    </button>
                    <button
                        ref="btnLastInDialog"
                        v-else-if="dialogConfirm === 'true'"
                        @click="btnActionOnClick"
                        class="btn-custom-default"
                        @keydown="focusFirst"
                    >
                        {{ titleButton }}
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: 'MDialog',
    props: ['depict', 'dialogConfirm', 'hasCloseButton', 'hasCancelButton', 'typeDialog', 'titleButton', 'titleDialog'],
    methods: {
        /**
         * Gọi đến sự kiện đóng dialog
         * CreatedBy: huynq (3/1/2023)
         */
        btnCloseOnClick() {
            try {
                this.$emit('close-dialog');
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
         * Gọi đến methods thực hiện nghiệp vụ riêng
         * CreatedBy: huynq (18/1/2023)
         */
        btnActionOnClick() {
            try {
                this.$emit('click-action');
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
         * Gọi đến sự kiện đóng dialog, giữ lại form ban đầu
         * CreatedBy: huynq (18/1/2023)
         */
        btnCancelButton() {
            try {
                this.$emit('cancel-dialog');
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
         * Focus về input đầu tiên trong form khi đã tab đến button cuối cùng
         * @author huynq 18-2-2023
         * @param {*} event
         */
        focusFirst(event) {
            try {
                event.preventDefault();
                if (event.keyCode == this.$MEnum.KEY_CODE.TAB) {
                    this.$refs.btnFirstDialog.focus();
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

        // focusNext(event) {

        // }
    },

    mounted() {
        this.$refs.btnLastInDialog.focus();
    },
};
</script>
<style>
@import url('./dialog.css');

.modal {
    position: fixed;
    background-color: rgba(0, 0, 0, 0.4);
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    /* display: none; */
    z-index: 909;
}
</style>
