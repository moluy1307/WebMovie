<template>
    <label for="id" v-if="label != undefined"
        >{{ label }}<span style="color: rgb(230, 0, 0); margin-left: 5px">{{ compulsory }}</span></label
    >
    <div
        class="combobox"
        :style="styleCbo"
        :class="classCbo"
        ref="myDropdown"
        @mouseover="showTooltip"
        @mouseleave="enableShowTooltip = false"
    >
        <input
            type="text"
            name="unit"
            id="id"
            v-model="textSelected"
            class="txtInputCbo"
            :tabindex="tabIndexDropdown"
            autocomplete="off"
            @input="onSearchItem"
            @keydown="inputOnKeyDown"
            :class="warning"
            ref="inputDropdown"
        />
        <MIcon
            nameIcon="arrow"
            :eventOnClick="btnShowCombobox"
            :keyboard="inputOnKeyDown"
            :anim="{ 'svg-arrow-click': showCombobox }"
        ></MIcon>
        <!-- <div v-show="enableShowTooltip" class="tooltip-input-error tooltip-dropdown">
            {{ errorMessage }}
        </div> -->
        <div v-show="enableShowTooltip" class="type-tooltip tooltip-dropdown">
            <div class="arrow-tooltip"></div>
            <div class="tooltip-input-error">
                {{ errorMessage }}
            </div>
        </div>
        <div
            v-click-away="hideCombobox"
            class="cbo-list"
            :class="newClassBox"
            v-if="showCombobox"
            ref="scrollContainer"
        >
            <template v-if="isTableCbo">
                <MTableCombobox
                    :titleTable="titleTable"
                    :entities="entitySearch"
                    :levelObject="levelObject"
                    :propParentValue="propParentValue"
                    :propValue="propValue"
                    @valueSelected="itemOnSelect"
                    :indexItemSelect="indexItemSelect"
                    :noData="noData"
                ></MTableCombobox>
            </template>
            <template v-else>
                <div
                    class="cbo-item"
                    ref="options"
                    :class="{ 'item-select': index == indexItemSelect }"
                    v-for="(item, index) in entitySearch"
                    :value="item[propValue]"
                    :key="index"
                    @click="itemOnSelect(item)"
                >
                    {{ item[propName] }}
                </div>
            </template>
        </div>
        <div class="disabled-cbo" v-if="isDisabled">
            <MIcon nameIcon="arrow" newStyle="background-color: #ebebeb;"></MIcon>
        </div>
    </div>
</template>
<script>
import MTableCombobox from '../table/MTableCombobox.vue';
import axios from 'axios';
export default {
    name: 'MDropdownList',
    components: { MTableCombobox },
    props: [
        'id',
        'label',
        'api',
        'propName',
        'propValue',
        'modelValue',
        'idObject',
        'warning',
        'tabIndexDropdown',
        'compulsory',
        'errorMessage',
        'tooltipError',
        'dataCombobox',
        'newClassBox',
        'value',
        'styleCbo',
        'isDisabled',
        'classCbo',
        'isTableCbo',
        'titleTable',
        'levelObject',
        'propParentValue',
        'hasValueDefault',
        'valueDefault',
    ],
    watch: {
        modelValue(newVal) {
            this.setItemSelected(newVal);
        },
        isDisabled: function (value) {
            console.log('gia tri checkbox thay doi: ', value);
        },
    },
    emits: ['update:modelValue'],
    computed: {
        //Tìm index của item đã được chọn trong danh sách tìm kiếm
        findIndexSelected: function () {
            var me = this;
            //Tính toán lại index của item đã được chọn
            let itemAfterSearch = this.entitySearch.findIndex((e) => e[me.propValue] == me.itemSelected[me.propValue]);
            return itemAfterSearch;
        },
    },
    methods: {
        /**
         * Ẩn/ hiện combobox
         * CreatedBy: huynq (9/1/2023)
         */
        btnShowCombobox() {
            try {
                this.showCombobox = !this.showCombobox;
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
            this.showCombobox = false;
            console.log('dasdasvavav');
        },

        /**
         *Lấy giá trị combobox hiển thị lên input text
         * CreatedBy: huynq (9/1/2023)
         */
        itemOnSelect(item) {
            try {
                var me = this;

                //reset lại danh sách
                this.entitySearch = this.entities;
                this.itemSelected = item;

                //Set index của item được chọn
                this.indexItemSelect = me.findIndexSelected;

                this.textSelected = item[this.propName];
                this.showCombobox = false;
                this.$emit('update:modelValue', item[this.propValue]);
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
         * Lấy tên đơn vị của nhân viên được chọn hiển thị lên input field và vị trí của tên đó trong danh sách dropdown
         * CreatedBy: huynq (9/1/2023)
         */
        setItemSelected(val) {
            try {
                var me = this;
                //Tìm item tương ứng với modelValue truyền từ ngoài vào
                let entitySelected = this.entities.find((item) => item[me.propValue] == (val ?? me.modelValue));
                if (entitySelected) {
                    this.textSelected = entitySelected[this.propName];
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
         * Lọc giá trị item
         * CreatedBy: huynq (9/1/2023)
         */
        onSearchItem() {
            try {
                var me = this;
                //Tìm item tương ứng với modelValue truyền từ ngoài vào
                this.entitySearch = this.entities.filter((item) =>
                    item[me.propName].toLowerCase().includes(me.textSelected.toLowerCase()),
                );
                if (this.entitySearch == 0) {
                    this.noData = true;
                }
                this.showCombobox = true;
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
         * CreatedBy: huynq (9/1/2023)
         * @param {*} event
         */
        inputOnKeyDown(event) {
            try {
                const key = event.keyCode;
                switch (key) {
                    case this.$MEnum.KEY_CODE.ENTER:
                        //xác định item đang chọn dựa vào index
                        // eslint-disable-next-line no-case-declarations
                        const item = this.entitySearch[this.indexItemSelect];
                        this.itemOnSelect(item);
                        break;
                    case this.$MEnum.KEY_CODE.ROW_UP:
                        this.showCombobox = true;
                        if (this.indexItemSelect > 0) {
                            this.indexItemSelect--;
                            // this.fixScrolling();
                        }
                        break;
                    case this.$MEnum.KEY_CODE.ROW_DOWN:
                        this.showCombobox = true;
                        // eslint-disable-next-line no-case-declarations
                        let maxLength = this.entitySearch.length;

                        if (this.indexItemSelect < maxLength - 1) {
                            this.indexItemSelect++;
                            // this.fixScrolling();
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
        // fixScrolling() {
        //     try {
        //         if (this.$refs.options[this.indexItemSelect]) {
        //             const followScroll = this.$refs.options[this.indexItemSelect].clientHeight;
        //             this.$refs.scrollContainer.scrollTop = followScroll * this.indexItemSelect;
        //         }
        //     } catch (err) {
        //         this.$MToastMessage({
        //     titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
        //     messageToast: err,
        //     showToastMessage: true,
        //     typeToast: 'errorToast',
        // });
        //     }
        // },

        // /**
        //  * Đóng combobox khi click bên ngoài
        //  * CreatedBy: huynq - (9/1/2023)
        //  * @param {*} event
        //  */
        // handleClickOutside(event) {
        //     try {
        //         if (!this.$refs.myDropdown.contains(event.target)) {
        //             this.showCombobox = false;
        //         }
        //     } catch (err) {
        //         this.$MToastMessage({
        //             titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
        //             messageToast: err,
        //             showToastMessage: true,
        //             typeToast: 'errorToast',
        //         });
        //     }
        //},

        /**
         * Hiện tooltip khi hover vào ô input
         * @author huynq 18-2-2023
         */
        showTooltip() {
            try {
                if (this.tooltipError) {
                    this.enableShowTooltip = true;
                } else {
                    this.enableShowTooltip = false;
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
    },

    created() {
        if (this.api) {
            axios
                .get(this.api)
                .then((data) => {
                    this.entities = data.data;
                    this.entitySearch = data.data;

                    //Set item combobox khi entityId được truyền vào
                    if (this.idObject) {
                        this.indexItemSelect = this.entitySearch.findIndex((entity) => {
                            return entity[this.propValue] == this.idObject;
                        });
                        console.log('index: ', this.idObject, this.entitySearch);
                    }

                    //Set giá trị mặc định nếu được yêu cầu
                    if (this.hasValueDefault == true && this.valueDefault != undefined) {
                        var myIndex = this.entitySearch.findIndex((entity) => {
                            return entity[this.propValue] == this.valueDefault;
                        });
                        this.indexItemSelect = myIndex;
                        this.textSelected = this.entitySearch[myIndex][this.propName];
                    }
                    this.setItemSelected();
                })
                .catch((res) => {
                    alert(res);
                });
        } else {
            this.entities = this.dataCombobox;
            this.entitySearch = this.dataCombobox;

            //Set item combobox khi entityId được truyền vào
            if (this.idObject) {
                this.indexItemSelect = this.entitySearch.findIndex((entity) => {
                    return entity[this.propValue] == this.idObject;
                });
                console.log('index: ', this.idObject, this.entitySearch);
            }

            //Set giá trị mặc định nếu được yêu cầu
            if (this.hasValueDefault == true && this.valueDefault != undefined) {
                var myIndex = this.entitySearch.findIndex((entity) => {
                    return entity[this.propValue] == this.valueDefault;
                });
                this.indexItemSelect = myIndex;
                this.textSelected = this.entitySearch[myIndex][this.propName];
            }

            this.setItemSelected();
        }
    },
    data() {
        return {
            showCombobox: false,
            entities: [],
            entitySearch: [],
            itemSelected: null,
            textSelected: null,
            indexItemSelect: 0,
            enableShowTooltip: false,

            noData: false,
        };
    },
};
</script>
<style>
@import url('./combobox.css');
.tooltip-dropdown {
    transform: translate(65%, 0);
}
</style>
