<template>
    <div class="paging" :style="newStyle">
        <div class="total">
            Tổng số: <span>{{ total }}</span> bản ghi
        </div>
        <div class="paging-list">
            <MDropdownList
                typeCbo="dropdown"
                propValue="valueCombobox"
                propName="title"
                v-model="dataCombobox.valueCombobox"
                :dataCombobox="dataCombobox"
                :newClassBox="{ 'cbo-page-list': true }"
                :hasValueDefault="true"
                :IndexValueDefault="0"
            ></MDropdownList>
            <!-- <MCombobox
                :addClass="{ combobox: true, 'combobox-paging': true }"
                :classBox="{ 'cbo-list': true, 'cbo-page-list': true }"
                :items="{
                    10: { title: '10 bản ghi trên 1 trang' },
                    20: { title: '20 bản ghi trên 1 trang' },
                    50: { title: '50 bản ghi trên 1 trang' },
                    100: { title: '100 bản ghi trên 1 trang' },
                }"
                @unitArray="showUnit"
            ></MCombobox> -->

            <div class="page-number">
                <!-- <button class="btn-page-number btn-none">Trước</button> -->
                <MButton
                    label="Trước"
                    :class="{ 'btn-page-number': true }"
                    :eventOnClick="onClickPreviousPage"
                    :disable="isInFirstPage"
                ></MButton>
                <div class="btn-number" v-for="(page, index) in pages" :key="index">
                    <MButton v-if="page.number === 0" :label="page.number" :class="{ hidePage: true }"> </MButton>
                    <MButton
                        :label="page.number"
                        @click="onClickPage(page.number, index)"
                        :disabled="page.isDisabled"
                        :class="{ 'btn-page-number': true, 'btn-clicked': isPageActive(page.number) }"
                    >
                    </MButton>
                </div>
                <!-- <div class="btn-number" v-for="(page, index) in pages" :key="index">
                    <!- <MButton v-if="page.number === 0" :label="page.number" :class="{ hidePage: true }"> </MButton> ->
                    <!- <MButton
                        v-if="page == '...' && page == this.currentPage + 3"
                        :label="page.number"
                        @click="onClickMore(page.number)"
                        :disabled="page.isDisabled"
                        :class="{ 'btn-page-number': true, 'btn-clicked': isPageActive(page.number) }"
                    >
                    </MButton> ->
                    <MButton
                        :label="page.number"
                        @click="onClickPage(page.number)"
                        :disabled="page.isDisabled"
                        :class="{ 'btn-page-number': true, 'btn-clicked': isPageActive(page.number) }"
                    >
                    </MButton>
                </div> -->
                <MButton
                    label="Sau"
                    :class="{ 'btn-page-number': true }"
                    :eventOnClick="onClickNextPage"
                    :disabled="isInLastPage"
                ></MButton>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'MPaging',
    props: {
        maxVisibleButtons: {
            type: Number,
            required: false,
            default: 5,
        },
        totalPages: {
            type: Number,
            required: true,
        },
        total: {
            type: Number,
            required: true,
        },
        perPage: {
            type: Number,
            required: true,
        },
        currentPage: {
            type: Number,
            required: true,
        },
        perPageTitle: {
            type: Number,
        },
        newStyle: {
            type: String,
        },
    },
    watch: {
        'dataCombobox.valueCombobox': function (value) {
            console.log('thay doi cua combobox: ', value);
            this.$emit('perPage', value);
        },
    },
    computed: {
        /**
         * Tính toán hiển thị trang
         * CreatedBy: huynq (16/1/2023)
         */
        pages() {
            const range = [];

            for (let i = 1; i <= this.totalPages; i++) {
                if (i == 1 || i == this.totalPages || (i <= this.currentPage + 2 && i >= this.currentPage - 2)) {
                    range.push({
                        number: i,
                        isDisabled: i === this.currentPage,
                    });
                } else if (i == this.currentPage + 3 || i == this.currentPage - 3) {
                    range.push({
                        number: '...',
                        isDisabled: i === this.currentPage,
                    });
                }
            }

            return range;
        },

        /**
         * Trang đầu tiên đang được chọn
         * CreatedBy: huynq (16/1/2023)
         */
        isInFirstPage() {
            return this.currentPage === 1;
        },

        /**
         * Trang cuối cùng đang được chọn
         * CreatedBy: huynq (16/1/2023)
         */
        isInLastPage() {
            return this.currentPage === this.totalPages;
        },
    },
    methods: {
        /**
         * Click button "Trước" trong phần paging
         * CreatedBy: huynq (16/1/2023)
         */
        onClickPreviousPage() {
            try {
                if (this.currentPage > 1) {
                    this.$emit('pagechanged', this.currentPage - 1);
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

        onClickMore(page) {
            console.log('kich 3 cham: ', page);
        },

        /**
         * Click các button số trang trong phần paging
         * CreatedBy: huynq (16/1/2023)
         */
        onClickPage(page) {
            try {
                if (page != '...') {
                    this.$emit('pagechanged', page);
                } else {
                    // if (page == this.currentPage + 3) {
                    //     this.$emit('pagechanged', this.currentPage + 3);
                    // } else if (this.currentPage < this.totalPages) {
                    //     this.$emit('pagechanged', this.currentPage - 3);
                    // }
                    // console.log('vi tri page ba gio: ', page);
                    console.log('mang cac button : ', this.pages);
                    for (var i in this.pages) {
                        console.log('cac phan tu con: ', this.pages[i].number);
                    }
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
         * Click button "Sau" trong phần paging
         * CreatedBy: huynq (16/1/2023)
         */
        onClickNextPage() {
            try {
                if (this.currentPage < this.totalPages) {
                    this.$emit('pagechanged', this.currentPage + 1);
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
         * Thay đổi trạng thái button số trang đang được chọn
         * CreatedBy: huynq (16/1/2023)
         */
        isPageActive(page) {
            return this.currentPage === page;
        },
    },

    created() {
        this.dataCombobox.valueCombobox = this.perPageTitle;
    },
    data() {
        return {
            dataCombobox: [
                {
                    valueCombobox: 10,
                    title: '10 bản ghi trên 1 trang',
                },
                {
                    valueCombobox: 20,
                    title: '20 bản ghi trên 1 trang',
                },
                {
                    valueCombobox: 50,
                    title: '50 bản ghi trên 1 trang',
                },
                {
                    valueCombobox: 100,
                    title: '100 bản ghi trên 1 trang',
                },
            ],
        };
    },
};
</script>

<style scoped>
@import url('./paging.css');
.hidePage {
    display: none;
    width: 0;
    height: 0;
    padding: 0;
    margin: 0;
}
</style>
