<template>
    <div :class="addClass" :style="styleCombobox">
        <input
            type="text"
            autocomplete="off"
            @input="onChange"
            v-model="search"
            @keydown="inputOnKeyDown"
            :class="classInput"
            :style="newStyle"
            :readonly="enable"
        />
        <MIcon
            nameIcon="arrow"
            :eventOnClick="showAll"
            :anim="{ 'svg-arrow-click': isOpen }"
            :newClass="classFunction"
        ></MIcon>
        <div v-show="isOpen" ref="scrollContainer" :class="classBox">
            <!-- <div class="loading" v-if="isLoading">Loading</div> -->
            <div style="position: relative">
                <div
                    class="cbo-item"
                    style="display: block"
                    ref="options"
                    v-for="(result, i) in Object.keys(items)"
                    :key="i"
                    @click="setResult(result, i)"
                    :class="{ 'item-select': i === arrowCounter }"
                >
                    {{ items[result].title }}
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: 'MDropdownList',
    props: {
        items: {
            type: Array,
            required: false,
            // default: () =>
            //     Array(30)
            //         .fill()
            //         .map((_, i) => `${i + 1} bản ghi trên 1 trang`),
        },
        isAsync: {
            type: Boolean,
            required: false,
            default: false,
        },
        addClass: {
            type: String,
            required: true,
        },
        classInput: {
            type: String,
            required: false,
        },
        classBox: {
            type: String,
            required: true,
        },
        newStyle: {
            type: String,
            required: false,
        },
        enable: {
            type: Boolean,
            required: false,
        },
        styleCombobox: {
            type: String,
        },
        classFunction: {
            type: String,
        },

        checkIsBoxFunction: {
            type: Boolean,
        },
    },

    watch: {
        // items: function (val, oldValue) {
        //     if (val.length !== oldValue.length) {
        //         this.results = val;
        //         this.isLoading = false;
        //     }
        // },
    },

    methods: {
        /**
         * Kiểm tra sự thay đổi của input field
         * CreatedBy: huynq - (9/1/2023)
         */
        onChange() {
            try {
                this.$emit('input', this.search);
                this.$emit('unitArray', this.search);

                if (this.isAsync) {
                    this.isLoading = true;
                } else {
                    this.filterResults();
                    this.isOpen = true;
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

        /**
         * Lọc các item tìm được khi input thay đổi
         * CreatedBy: huynq - (9/1/2023)
         */
        filterResults() {
            try {
                this.results = Object.values(this.items).filter((item) => {
                    // return item.toLowerCase().includes(this.search.toLowerCase());
                    return item.toLowerCase().indexOf(this.search.toLowerCase()) > -1;
                });
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
         * Hiển thị kết quả được chọn trong combobox list lên input field
         * CreatedBy: huynq - (9/1/2023)
         * @param {*} result
         * @param {*} i
         */
        setResult(result, i) {
            try {
                this.arrowCounter = i;
                this.search = this.items[result].title;

                this.value = result;
                this.isOpen = false;
                // if (this.checkIsBoxFunction == true) {
                //     this.value = 1;
                // }
                this.$emit('input', this.search);
                this.$emit('unitArray', this.value);
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
         * Các sự kiện với bàn phím
         * CreatedBy: huynq - (9/1/2023)
         * @param {*} event
         */
        inputOnKeyDown(event) {
            try {
                const key = event.keyCode;
                switch (key) {
                    case this.$MEnum.KEY_CODE.ENTER:
                        // this.search = this.results[this.arrowCounter];
                        this.search = this.results[this.arrowCounter].title;
                        this.value = Object.keys(this.items)[this.arrowCounter];
                        this.isOpen = false;
                        break;
                    case this.$MEnum.KEY_CODE.ROW_UP:
                        this.isOpen = true;
                        if (this.arrowCounter > 0) {
                            this.arrowCounter--;
                            this.fixScrolling();
                        }
                        break;
                    case this.$MEnum.KEY_CODE.ROW_DOWN:
                        this.isOpen = true;
                        if (this.arrowCounter < this.results.length - 1) {
                            this.arrowCounter++;
                            this.fixScrolling();
                        }
                        break;
                    default:
                        break;
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

        /**
         * Scroll follow theo nút lên/xuống của bàn phím
         * CreatedBy: huynq - (9/1/2023)
         * @author
         */
        fixScrolling() {
            try {
                const followScroll = this.$refs.options[this.arrowCounter].clientHeight;
                this.$refs.scrollContainer.scrollTop = followScroll * this.arrowCounter;
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
         * Ẩn/hiện combobox
         * CreatedBy: huynq - (9/1/2023)
         */
        showAll(event) {
            try {
                this.isOpen = !this.isOpen;

                this.isOpen ? (this.results = Object.values(this.items)) : (this.results = []);
                this.$emit('setColumn', event);
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
        //         if (!this.$el.contains(event.target)) {
        //             this.isOpen = false;
        //             this.arrowCounter = -1;
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
    },

    // mounted() {
    //     document.addEventListener('click', this.handleClickOutside);
    // },
    // unmounted() {
    //     document.removeEventListener('click', this.handleClickOutside);
    // },

    created() {
        this.results = Object.values(this.items);
        // this.results = this.items;

        this.search = this.results[0].title;

        this.value = Object.keys(this.items)[0];
    },

    data() {
        return {
            isOpen: false,
            results: [],
            search: '',
            isLoading: false,
            arrowCounter: 0,

            value: 0,
        };
    },
};
</script>
<style>
@import url('./combobox.css');
</style>
