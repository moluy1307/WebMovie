<template>
    <tr @dblclick="btnEditOnClick(itemRoot)" :class="{ 'is-row-root': isFolder }" class="row-table">
        <td
            v-if="hasCheckbox"
            style="padding-left: 20px; padding-bottom: 20px"
            ref="columnCheckbox"
            :class="{ 'focus-row': checkedItem[itemRoot[propValue]] }"
            @dblclick.stop.prevent
        >
            <label class="chkCustom">
                <input
                    type="checkbox"
                    ref="rowCheckbox"
                    :value="itemRoot[propValue]"
                    v-model="checkRows"
                    v-on:change="(event) => eventCheckedCheckbox(event, itemRoot[propValue])"
                />
                <span class="chkCheckmark"></span>
            </label>
        </td>
        <template v-for="(col, indexCol) in titleTable" :key="indexCol">
            <td v-if="col.hasExtend" :class="col.className">
                <div class="column-extend" :style="{ 'margin-left': 35 * itemRoot[levelObject] + 'px' }">
                    <div v-if="isFolder" @click="expandchild">
                        <MIcon
                            v-if="!isOpenChild"
                            nameIcon="extend"
                            newStyle="position: relative;width: 18px; height: 18px;"
                        ></MIcon>
                        <MIcon
                            v-else
                            nameIcon="collapse"
                            newStyle="position: relative;width: 18px; height: 18px;"
                        ></MIcon>
                    </div>
                    <div ref="textcontent">{{ getFormatByData(col.formatType, itemRoot[col.dataField]) }}</div>
                </div>
            </td>
            <td v-else :class="col.className">
                {{ getFormatByData(col.formatType, itemRoot[col.dataField]) }}
            </td>
        </template>
        <td class="br-none column-120-imp" ref="cellFunction" v-if="hasWiget" @dblclick.stop.prevent>
            <div class="text-function-center">
                <button class="btn-action btn-edit-form" @click="btnEditOnClick(itemRoot)">Sửa</button>

                <MBoxFunction
                    @getIdReplicate="transmissionIdReplicate(itemRoot)"
                    @itemDelete="transmissionInfoDelete(itemRoot)"
                    @valueStatus="transmissionStatus(itemRoot)"
                    @click.prevent="recieveCombobox"
                    :newClassIcon="{
                        'btn-arrow-function': true,
                    }"
                    :enableActive="textStatus"
                ></MBoxFunction>
            </div>
        </td>
    </tr>
    <tr v-show="isOpenChild && shouldDisplayChildren" v-if="isFolder" class="row-child">
        <td colspan="999" style="padding: 0; border: 0">
            <MTreeRowTable
                :showAllNode="showAllNode"
                v-for="(child, index) in itemRoot.children"
                :key="index"
                :itemRoot="child"
                :titleTable="titleDataChild"
                :hasCheckbox="hasCheckbox"
                :hasWiget="true"
                :levelObject="levelObject"
                :propValue="propValue"
                :propValueCode="propValueCode"
                :propChildren="propChildren"
                @getEntityEditId="btnEditOnClick"
                @getValuesAction="transmissionInfoDelete"
                @idReplicationTree="transmissionIdReplicate"
                @statusRecord="transmissionStatus"
                :enableActive="textStatus"
            ></MTreeRowTable>
        </td>
    </tr>
</template>

<script>
import MBoxFunction from '../other/MBoxFunction.vue';
import commonJS from '@/js/common';
export default {
    name: 'MTreeRowTable',
    components: { MBoxFunction },
    props: {
        hasCheckbox: {
            type: Boolean,
        },
        hasWiget: {
            type: Boolean,
        },
        itemRoot: {
            type: Object,
        },
        titleTable: {
            type: Array,
        },

        levelObject: {
            type: String,
        },
        propValue: {
            type: String,
        },
        propValueCode: {
            type: String,
        },
        isTreeTable: {
            type: Boolean,
        },
        showAllNode: {
            type: Boolean,
        },
        propChildren: {
            type: String,
        },
        enableActive: {
            type: String,
        },
        // propParentId: {
        //     type: String,
        // },
    },
    watch: {
        showAllNode: function (value) {
            // if (value) {
            //     this.isOpenChild = true;
            //     this.shouldDisplayChildren = true;
            // }
            this.shouldDisplayChildren = value;
            this.isOpenChild = value;
        },
    },
    computed: {
        /**
         * Tính toán Object item có phải là parent không
         * CreatedBy: huynq (20/3/2023)
         */
        isFolder: function () {
            return this.itemRoot.children && this.itemRoot.children.length;
        },
    },
    methods: {
        /**
         * Đóng/Mở tài khoản con
         * CreatedBy: huynq (20/3/2023)
         */
        expandchild() {
            try {
                if (this.isFolder) {
                    if (!this.showAllNode) {
                        if (!this.isOpenChild) {
                            this.shouldDisplayChildren = true;
                            this.isOpenChild = true;
                        } else {
                            this.isOpenChild = false;
                            this.shouldDisplayChildren = false;
                        }
                    } else {
                        this.isOpenChild = !this.isOpenChild;
                        this.shouldDisplayChildren = this.showChildren;

                        // if (!this.shouldDisplayChildren) {
                        //     // Mở rộng tất cả các nút con
                        //     this.itemRoot[this.propChildren].forEach((child) => {
                        //         child.shouldDisplayChildren = true;
                        //     });
                        //     this.shouldDisplayChildren = true;
                        // } else {
                        //     // Thu gọn tất cả các nút con
                        //     this.itemRoot[this.propChildren].forEach((child) => {
                        //         child.shouldDisplayChildren = false;
                        //     });
                        //     this.shouldDisplayChildren = false;

                        //     // Kiểm tra các nút con của một nút cha có đang được ẩn không, nếu đang được ẩn thì showAllNode = false
                        //     let allHidden = this.itemRoot[this.propChildren].every((child) => {
                        //         return !child.isOpenChild;
                        //     });

                        //     console.log('dasd', allHidden);
                        //     if (allHidden) {
                        //         // this.showAllNode = false;
                        //         console.log('value thay doi ');
                        //     }
                        // }
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
         * Hàm trả về định dạng của dữ liệu
         * @author huynq 09.02.2023
         */
        getFormatByData(formatType, value) {
            try {
                switch (formatType) {
                    case 'Date':
                        return commonJS.formatDate(value);
                    case 'Enum': {
                        return commonJS.formatGender(value);
                    }
                    default:
                        return value;
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
         * Mở box function đúng định dạng
         * Truyền id và mã nhân viên cần xóa
         * @author huyhq 16-2-2023
         */
        recieveCombobox() {
            try {
                // console.log('dropdown: ', this.dropdown);
                // this.entityDelete = this.itemRoot[this.propValue];
                // this.codeDelete = this.itemRoot[this.propValueCode];
                // console.log('id xoa: ', this.entityDelete);

                // this.$emit('getValuesAction', this.entityDelete, this.codeDelete);
                this.textStatus = this.itemRoot.status;
                const dropDown = document.querySelectorAll('.br-none');
                let myContent = null;

                dropDown.forEach((item) => {
                    if (this.$refs.cellFunction.contains(item)) {
                        myContent = item;
                    } else {
                        item.classList.remove('rewrite-td');
                    }
                });

                if (myContent) myContent.classList.toggle('rewrite-td');
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
         * Hiển thị form thông tin đối tượng ứng với mã được chọn
         * <br/>
         * CreatedBy: huynq (5/1/2023)
         * @param {*} item
         */
        btnEditOnClick(item) {
            try {
                // this.entityEdit = item;
                // this.entityEdit = item[this.propValue];
                this.$emit('getEntityEditId', item);
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
         * Truyền thông tin đối tượng muốn xóa đến nghiệp vụ
         * @author huynq 16-2-2023
         */
        transmissionInfoDelete(item) {
            try {
                console.log('id truyen di: ', item);
                this.$emit('getValuesAction', item);
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
         * Truyền id nhân bản đến nghiệp vụ
         * CreatedBy: huynq (18/2/2023)
         */
        transmissionIdReplicate(item) {
            try {
                this.$emit('idReplicationTree', item);
                console.log(this.entityEdit);
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
         * Truyền trạng thái bản ghi đến nghiệp vụ
         * CreatedBy: huynq (18/2/2023)
         */
        transmissionStatus(item) {
            try {
                this.$emit('statusRecord', item);
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
            isOpenChild: false,
            shouldDisplayChildren: true,

            // entityEdit: null,
            entityDelete: null,
            codeDelete: null,

            textStatus: '',

            titleDataChild: [
                {
                    dataField: 'accountNumber',
                    title: this.$MResource.VI.DATA_TABLE.ACCOUNT_SYSTEM.ACCOUNT_NUMBER,
                    className: 'column-150-imp text-left',
                    classTitle: 'column-150 text-left',
                    formatType: 'Text',
                    checkIsEnableSort: 'true',
                    hasFilter: 'true',
                    hasExtend: true,
                    isFieldMain: true,
                    // styleElement: 'element-col-150',
                },
                {
                    dataField: 'accountName',
                    title: this.$MResource.VI.DATA_TABLE.ACCOUNT_SYSTEM.ACCOUNT_NAME,
                    className: 'column-230-imp text-left',
                    classTitle: 'column-230 text-left',
                    formatType: 'Text',
                    checkIsEnableSort: 'true',
                    hasFilter: 'true',
                    // styleElement: 'element-col-230',
                },
                {
                    dataField: 'natureName',
                    title: this.$MResource.VI.DATA_TABLE.ACCOUNT_SYSTEM.NATURE_NAME,
                    className: 'column-150-imp text-left',
                    classTitle: 'column-150 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'accountNameEn',
                    title: this.$MResource.VI.DATA_TABLE.ACCOUNT_SYSTEM.ACCOUNT_NAME_EN,
                    className: 'column-230-imp text-left',
                    classTitle: 'column-230 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'explain',
                    title: this.$MResource.VI.DATA_TABLE.ACCOUNT_SYSTEM.EXPLAIN,
                    className: 'col-explain text-left',
                    classTitle: 'column-230 text-left',
                    formatType: 'Text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
                {
                    dataField: 'status',
                    title: this.$MResource.VI.DATA_TABLE.ACCOUNT_SYSTEM.STATUS,
                    className: 'column-150-imp text-left',
                    classTitle: 'column-150 text-left',
                    formatType: 'text',
                    checkIsEnableSort: 'false',
                    // styleElement: 'column-150',
                },
            ],
        };
    },
};
</script>

<style>
@import url('./table.css');
.table-child {
    overflow: auto;
}

.level-1 {
    margin-left: 20px;
}

.boxFunctionTree {
    position: absolute;
}
</style>
