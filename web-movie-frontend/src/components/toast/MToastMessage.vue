<template>
    <div class="toast-box" v-if="showToast">
        <div class="toast-item toast-error">
            <div class="toast-icon">
                <svg
                    v-if="typeToast === 'successToast'"
                    width="20"
                    height="20"
                    viewBox="0 0 36 36"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                >
                    <path
                        d="M18 36C27.9411 36 36 27.9411 36 18C36 8.05887 27.9411 0 18 0C8.05887 0 0 8.05887 0 18C0 27.9411 8.05887 36 18 36Z"
                        fill="#2CA01C"
                    />
                    <path
                        d="M11 18L16 23L25 13"
                        stroke="white"
                        stroke-width="2"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    />
                </svg>

                <svg
                    v-else-if="typeToast === 'errorToast'"
                    width="20"
                    height="20"
                    viewBox="0 0 20 20"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                >
                    <path
                        d="M10 20C15.5228 20 20 15.5228 20 10C20 4.47715 15.5228 0 10 0C4.47715 0 0 4.47715 0 10C0 15.5228 4.47715 20 10 20Z"
                        fill="#FF7777"
                    />
                    <path
                        d="M10 10.5V6.5"
                        stroke="white"
                        stroke-width="2"
                        stroke-miterlimit="10"
                        stroke-linecap="round"
                    />
                    <path
                        d="M10 15C10.5523 15 11 14.5523 11 14C11 13.4477 10.5523 13 10 13C9.44772 13 9 13.4477 9 14C9 14.5523 9.44772 15 10 15Z"
                        fill="white"
                    />
                </svg>
            </div>
            <div class="toast-text">
                <span v-if="typeToast === 'errorToast'" style="color: #de3618">{{ titleToast }}!</span>
                <span v-else-if="typeToast === 'successToast'" style="color: #50b83c">{{ titleToast }}!</span>
                &nbsp;{{ messageToast }}
            </div>
            <div class="toast-close">
                <div class="undo" @click="btnCloseOnClick">Hoàn tác</div>
                <svg
                    @click="btnCloseOnClick"
                    name="close-icon"
                    width="10"
                    height="10"
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
    </div>
</template>

<script>
export default {
    name: 'MToastMessage',
    props: {
        titleToast: {
            type: String,
            required: true,
        },
        messageToast: {
            type: String,
            required: true,
        },
        showToastMessage: {
            type: Boolean,
            default: false,
        },
        typeToast: {
            type: String,
        },
    },

    methods: {
        /**
         * Hàm đóng toast mesage
         * @author huynq 11-2-2023
         */
        btnCloseOnClick() {
            try {
                this.showToast = false;
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

    mounted() {
        if (this.showToastMessage == true) {
            setTimeout(() => {
                this.showToast = !this.showToastMessage;
            }, 5000);
        }
    },

    created() {
        this.showToast = this.showToastMessage;
    },

    data() {
        return {
            toast: {
                icon: '',
                title: '',
                message: '',
                bgColor: '',
            },
            timeout: '',
            showToast: false,
        };
    },
};
</script>

<style>
.toast-box {
    position: fixed;
    top: 10px;
    right: 0;
    z-index: 9999;
}

.toast-box .toast-item {
    height: 48px;
    border-radius: 4px;
    box-shadow: 0px 4px 40px rgba(0, 0, 0, 0.078);
    background-color: #fff;
    margin-bottom: 8px;
    margin-right: 8px;
    display: flex;
    align-items: center;
    padding: 16px;
}

.toast-box .toast-item .toast-icon {
    font-size: 24px;
    display: flex;
    justify-content: center;
    align-items: center;
}

.toast-box .toast-item .toast-text {
    color: var(--black);
    margin: 0 24px 0 8px;
}

.toast-box .toast-item .toast-close {
    text-align: center;
    cursor: pointer;
    display: flex;
    justify-content: center;
    align-items: flex-start;
}

.undo {
    margin-right: 16px;
    text-decoration: underline;
    color: #1f1f1f;
    font-weight: 500;
}
</style>
