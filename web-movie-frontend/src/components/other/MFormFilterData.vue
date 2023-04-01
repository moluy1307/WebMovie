<template>
    <div
        v-click-away="hideCombobox"
        class="box"
        :style="{ left: positionFormFilter.left + 'px', top: positionFormFilter.top + 'px' }"
    >
        <div class="selection">
            <div class="name-column-filter">Lọc {{ columnFilter }}</div>
            <MCombobox
                :addClass="{ combobox: true, 'combobox-paging': true }"
                :classBox="{ 'cbo-list': true }"
                :items="operatorFilter"
                @unitArray="showUnit"
                :enable="true"
                styleCombobox="min-width:120px"
                newStyle="width:90px"
            ></MCombobox>
        </div>
        <MInput placeHolder="Nhập giá trị lọc" v-model="textSearch"></MInput>
        <div class="selection">
            <MButton
                :class="{ btn: true, 'btn-bg': true }"
                label="Bỏ lọc"
                :eventOnClick="btnCloseFormFilterOnClick"
            ></MButton>
            <MButton :class="{ btn: true }" :eventOnClick="btnFilterOnClick" label="Lọc"></MButton>
        </div>
    </div>
</template>

<script>
export default {
    name: 'MFormFilterData',
    props: {
        columnFilter: {
            type: String,
        },
        positionFormFilter: {
            type: Object,
        },
        typeColumnFilter: {
            type: String,
        },
    },
    watch: {},
    methods: {
        /**
         * Truyền giá trị chọn phép toán để lọc
         * CreatedBy: huynq (16/2/2023)
         * @param {*} event
         */
        showUnit(event) {
            try {
                this.valueOperator = event;
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
         * Thực hiện lọc dữ liệu theo giá trị và phép toán đã lựa chọn
         * @author huynq 16-2-2023
         */
        btnFilterOnClick() {
            try {
                this.$emit(
                    'getValueFilter',
                    this.textSearch,
                    this.valueOperator,
                    this.columnFilter,
                    this.typeColumnFilter,
                    Object.values(this.operatorFilter)[this.valueOperator].title,
                );
                console.log(
                    'gia tri gui di de loc: ',
                    this.textSearch,
                    this.valueOperator,
                    this.columnFilter,
                    this.typeColumnFilter,
                    Object.values(this.operatorFilter)[this.valueOperator].title,
                );
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
         * Bỏ lọc
         * CreatedBy: huynq (16-2-2023)
         */
        btnCloseFormFilterOnClick() {
            try {
                this.$emit('unfiltered');
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
        hideCombobox() {
            try {
                this.$emit('close-box');
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
    data() {
        return {
            textSearch: '',
            valueOperator: 0,
            operatorFilter: {
                0: { title: 'Chứa' },
                1: { title: 'Bắt đầu với' },
                2: { title: 'Kết thúc với' },
                3: { title: 'Không chứa' },
                4: { title: 'Bằng' },
                5: { title: 'Khác' },
            },
        };
    },
};
</script>

<style>
.box {
    position: absolute;
    padding: 22px 17px;
    border: 1px solid #babec5;
    z-index: 999;
    background: #fff;
    min-width: 350px;
    max-width: 350px;
    border-radius: 4px;
    box-shadow: 3px 3px 6px #ddd;
    display: flex;
    flex-direction: column;
    row-gap: 20px;
}

.selection {
    display: flex;
    justify-content: space-between;
    align-items: center;
}
</style>
