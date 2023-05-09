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
                        nameIcon="successDialog"
                        newStyle="position: relative; width: 36px; height: 36px; background-color: transparent;"
                    ></MIcon>
                </div>
                <div class="dialog-title">
                    {{ depict }} <span style="color: #1565c0; font-weight: 700">{{ secondsRemaining }} giây</span>
                </div>
            </div>
            <!-- <div class="btn-dialog">
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
            </div> -->
        </div>
    </div>
</template>

<script>
export default {
    name: 'MDialogTimeCount',
    props: ['depict', 'titleDialog'],
    mounted() {
        this.intervalId = setInterval(() => {
            // Giảm giá trị số giây còn lại đi 1 mỗi giây
            this.secondsRemaining--;

            // Dừng đếm ngược khi số giây còn lại bằng 0
            if (this.secondsRemaining <= 0) {
                clearInterval(this.intervalId);
                this.$emit('close-dialog');
                const storage = localStorage.getItem('userInfor');
                if (storage !== null) {
                    localStorage.removeItem('userInfor');
                }
                this.$router.push('/');
            }
        }, 1000);
    },
    beforeUnmount() {
        // Hủy bỏ interval khi component bị xoá khỏi DOM
        clearInterval(this.intervalId);
    },
    data() {
        return {
            secondsRemaining: 5,
            intervalId: null,
        };
    },
};
</script>

<style scoped>
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
