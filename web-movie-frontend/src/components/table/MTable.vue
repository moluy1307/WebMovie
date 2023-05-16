<template>
    <MFormFilterData
        :positionFormFilter="positionFormFilter"
        v-if="isShowFormFilter"
        :columnFilter="columnFilter"
        :typeColumnFilter="typeColumnFilter"
        @getValueFilter="textFilter"
        @unfiltered="unfilterData"
        @close-box="isShowFormFilter = false"
    ></MFormFilterData>
    <table id="m-table" ref="tableData">
        <thead>
            <tr>
                <th class="text-center" style="width: 48px !important" v-if="payDetail">#</th>
                <th v-if="hasCheckbox" class="text-center" style="padding: 0 16px 20px 20px">
                    <label class="chkCustom">
                        <input
                            type="checkbox"
                            ref="titleCheckbox"
                            @click="checkedCheckbox"
                            v-model="checkAllRows"
                            v-on:change="titleCheckbox"
                        />
                        <span class="chkCheckmark"></span>
                    </label>
                </th>
                <th
                    v-for="(item, index) in titleTable"
                    :key="index"
                    :class="item.classTitle"
                    :style="item.styleCss"
                    :ref="item.hasFilter"
                    @mouseover="(event) => showButtonFilter(event, item.hasFilter)"
                    @mouseleave="hideButtonFilter"
                >
                    <div style="position: relative">
                        <div
                            :class="item.styleElement"
                            v-if="item.checkAcronym == true"
                            v-tippy="{
                                content: item.content,
                                placement: 'bottom',
                                theme: 'dark',
                                arrow: false,
                            }"
                        >
                            {{ item.title }}
                        </div>
                        <div
                            @click="(event) => btnSortOnClick(event, item.checkIsEnableSort, item.dataField)"
                            v-else
                            :class="item.styleElement"
                        >
                            {{ item.title }}
                        </div>

                        <slot v-if="item.hasFilter == 'true'">
                            <MIcon
                                nameIcon="arrowUp"
                                :newClass="{ 'btn-arrow-function': true, 'btn-show-form-filter': true }"
                                newStyle="position:absolute;width:14px;height:14px;background-color: transparent; left: 105px; color: #666666; margin-left: 2px"
                            ></MIcon>
                            <MIcon
                                nameIcon="arrowDown"
                                :newClass="{ 'btn-arrow-function': true, 'btn-show-form-filter': true }"
                                newStyle="position:absolute;width:14px;height:14px;background-color: transparent; left: 105px; color: #666666; margin-left: 2px"
                            ></MIcon>
                            <MIcon
                                nameIcon="filterTable"
                                :eventOnClick="() => showBoxFilter(index, item.title, item.dataField)"
                                :anim="{ 'svg-arrow-click': isOpen }"
                                :newClass="{ 'btn-arrow-function': true, 'btn-show-form-filter': true }"
                                newStyle="position:absolute;width:14px;height:14px;right: -12px"
                                ref="btnDetailFilter"
                            ></MIcon>
                        </slot>
                    </div>
                </th>
                <th class="title-function text-center" :style="styleColumnFunction" v-if="hasWiget" @click="testshow">
                    Chức năng
                </th>
                <th class="text-center" style="width: 48px" v-if="payDetail"></th>
            </tr>
        </thead>
        <tbody ref="bodyTable">
            <MLoading
                v-if="showSkeleton"
                :hasCheckbox="hasCheckbox"
                :hasWiget="hasWiget"
                :titleTable="titleTable"
            ></MLoading>
            <template v-if="isTreeTable">
                <template v-for="(item, index) in entities" :key="index">
                    <MTreeRowTable
                        :isTreeTable="isTreeTable"
                        :itemRoot="item"
                        :hasCheckbox="hasCheckbox"
                        :hasWiget="hasWiget"
                        :titleTable="titleTable"
                        :levelObject="levelObject"
                        :propValue="propValue"
                        @getEntityEditId="btnEditOnClick"
                        :propParentId="propParentId"
                        @getValuesAction="transmissionInforDel"
                        :propValueCode="propValueCode"
                        :showAllNode="showAllNode"
                        :propChildren="propChildren"
                        @idReplicationTree="transmissIdRep"
                        @statusRecord="transmissionStatus"
                        :enableActive="enableActive"
                    ></MTreeRowTable>
                </template>
            </template>
            <template v-else-if="payDetail">
                <template v-for="(itemAccounting, indexAccounting) in arrayRowTable" :key="indexAccounting">
                    <!-- <MRowDetail
                        :orderNumber="indexAccounting + 1"
                        :objectDetail="itemAccounting"
                        :titleTableData="titleTableData"
                    ></MRowDetail> -->
                    <tr>
                        <td class="text-center">{{ indexAccounting + 1 }}</td>
                        <template v-for="(col, indexCol) in titleTable" :key="indexCol">
                            <td>
                                <slot v-if="col.hasInputTime == 'true'">
                                    <MInputTime
                                        styleLabel="display: none !important"
                                        v-model="itemAccounting[col.dataField]"
                                    ></MInputTime>
                                </slot>
                                <slot v-else>
                                    <MInput
                                        styleLabel="display: none !important"
                                        v-model="itemAccounting[col.dataField]"
                                    ></MInput>
                                </slot>
                            </td>
                        </template>
                        <td
                            class="text-center"
                            style="display: flex; justify-content: center; align-items: center; height: 48px"
                        >
                            <MIcon
                                @click="btnDeleteRow(indexAccounting)"
                                nameIcon="trash"
                                newStyle="width: 32px; height: 32px;margin-right: 4px"
                                :class="{ 'icon-size': true }"
                            ></MIcon>
                        </td>
                    </tr>
                </template>
            </template>
            <template v-else-if="detailEntity">
                <template v-for="(itemDetail, indexDetail) in arraydetailEntity" :key="indexDetail">
                    <!-- <MRowDetail
                        :orderNumber="indexAccounting + 1"
                        :objectDetail="itemAccounting"
                        :titleTableData="titleTableData"
                    ></MRowDetail> -->
                    <tr>
                        <template v-for="(col, indexCol) in titleTable" :key="indexCol">
                            <td :class="col.className">
                                {{
                                    col.isColumnSerial == 'true'
                                        ? indexDetail + 1
                                        : getFormatByData(col.formatType, itemDetail[col.dataField])
                                }}
                            </td>
                        </template>
                    </tr>
                </template>
            </template>
            <template v-else>
                <tr
                    v-for="(item, index) in entities"
                    :key="index"
                    @dblclick="btnEditOnClick(item)"
                    ref="rowTable"
                    :class="{ 'focus-row': checkedItem[item[propValue]], 'row-table': true }"
                    @click="btnGetIdMaster(item)"
                >
                    <!-- <td class="column-16" style="background-color: #fff !important"></td> -->
                    <td
                        v-if="hasCheckbox"
                        style="padding-left: 20px; padding-bottom: 20px"
                        ref="columnCheckbox"
                        :class="{ 'focus-row': checkedItem[item[propValue]] }"
                        @dblclick.stop.prevent
                    >
                        <label class="chkCustom">
                            <input
                                type="checkbox"
                                ref="rowCheckbox"
                                :value="item[propValue]"
                                v-model="checkRows"
                                v-on:change="(event) => eventCheckedCheckbox(event, item[propValue])"
                            />
                            <span class="chkCheckmark"></span>
                        </label>
                    </td>
                    <template v-for="(col, indexCol) in titleTable" :key="indexCol">
                        <td :class="col.className">
                            {{ getFormatByData(col.formatType, item[col.dataField]) }}
                        </td>
                    </template>

                    <td
                        class="br-none"
                        ref="cellFunction"
                        :class="{ 'focus-row': checkedItem[item[propValue]] }"
                        v-if="hasWiget"
                        @dblclick.stop.prevent
                    >
                        <div class="text-function-center">
                            <div
                                @click="btnEditOnClick(item)"
                                v-if="item[propValueStatus] == '0' || !item[propValueStatus]"
                            >
                                <slot v-if="item[propValueStatus] != null"
                                    ><font-awesome-icon
                                        :icon="['far', 'square-check']"
                                        style="color: #1565c0; margin-right: 2px"
                                /></slot>
                                <slot v-else
                                    ><font-awesome-icon :icon="['fas', 'pen-to-square']" style="color: #1565c0"
                                /></slot>
                                <button class="btn-action btn-edit-form">
                                    {{ item[propValueStatus] == null ? 'Sửa' : 'Xác thực' }}
                                </button>
                            </div>
                            <!-- <MBoxFunction
                                @getIdReplicate="transmissionIdReplicate"
                                @itemDelete="transmissionInfoDelete"
                                @click="($event) => recieveCombobox($event, item)"
                                :newClassIcon="{
                                    'btn-arrow-function': true,
                                    'focus-row': checkedItem[item[propValue]],
                                }"
                            ></MBoxFunction> -->
                            <div @click="btnDeleteOnClick(item)">
                                <font-awesome-icon :icon="['fas', 'eraser']" style="color: #eb3333" />
                                <button style="color: #eb3333" class="btn-action btn-edit-form">Xóa</button>
                            </div>
                            <!-- <button v-if="item[propValueStatus] == '0'">Xac nhan</button> -->
                        </div>
                    </td>
                </tr>
            </template>
        </tbody>
    </table>
    <!-- <MPaging
            :totalPages="totalPage"
            :total="totalRecord"
            @perPage="getPerPage"
            :currentPage="currentPage"
            @pagechanged="onPageChange"
        ></MPaging>
    </div> -->
</template>

<script>
import MLoading from '@/components/loading/MLoading.vue';
// import MBoxFunction from '../other/MBoxFunction.vue';
import MFormFilterData from '../other/MFormFilterData.vue';
// import MExpandRowTableVue from './MExpandRowTable.vue';
import MTreeRowTable from './MTreeRowTable.vue';
// import MRowDetail from './MRowDetail.vue';

// import axios from 'axios';

import commonJS from '@/js/common';

export default {
    name: 'MTable',
    props: {
        enableActive: {
            type: String,
        },
        textStatus: {
            type: String,
        },
        propChildren: {
            type: String,
        },
        isTreeTable: {
            type: Boolean,
        },
        hasWiget: {
            type: Boolean,
        },
        hasCheckbox: {
            type: Boolean,
        },
        hasExtend: {
            type: Boolean,
        },
        enableCheckChild: {
            type: Boolean,
        },
        isFieldMain: {
            type: Boolean,
        },
        titleTable: {
            // type: Object,
            type: Array,
            required: true,
        },

        columnTable: {
            type: Array,
            required: true,
        },
        typeObject: {
            type: String,
        },
        api: {
            type: String,
            required: true,
        },
        propValue: {
            type: String,
        },
        propValueCode: {
            type: String,
        },
        propParentId: {
            type: String,
        },
        comboboxReload: {
            type: Boolean,
        },

        modelValue: {
            type: Array,
        },
        checkedRows: {
            type: Array,
        },
        numberRecord: {
            type: Number,
        },
        sortDirection: {
            type: String,
        },
        isAsc: {
            type: String,
        },
        isDesc: {
            type: String,
        },

        levelObject: {
            type: String,
        },
        showAllNode: {
            type: Boolean,
        },
        payDetail: {
            type: Boolean,
        },
        orderNumber: {
            type: Number,
        },
        arrayRowTable: {
            type: Array,
        },
        detailEntity: {
            type: Boolean,
        },
        arraydetailEntity: {
            type: Array,
        },
        styleColumnFunction: {
            type: String,
        },

        propValueStatus: {
            type: String,
        },
    },
    components: {
        MLoading,
        // MBoxFunction,
        MFormFilterData,
        MTreeRowTable,
        // MRowDetail
    },
    watch: {
        checkRows: function (value) {
            console.log('dasdas', value);
            if (value?.length === this.entities.length) {
                this.checkAllRows = true;
            } else if (value?.length < this.entities.length) {
                this.checkAllRows = false;
            }
            if (value?.length > 0) {
                this.enableDeleteMultiple = true;
            } else {
                this.enableDeleteMultiple = false;
            }
            this.$emit('employeeIds', Object.values(value), this.enableDeleteMultiple);
        },
        entities: function (value) {
            if (this.hasCheckbox == true) {
                this.checkAllRows = true;
                for (var i in value) {
                    if (!this.checkRows.includes(this.entities[i][this.propValue])) {
                        this.checkAllRows = false;
                    }
                }
                if (value.length <= 0) {
                    this.checkAllRows = false;
                }
            }
        },
        arrayRowTable: {
            handler: function (value, old) {
                // old.map((index) => {
                //     console.log('indexOld', index);
                //     // this.entitiesChildren = [...this.entitiesChildren, index];
                // });
                // value.map((index) => {
                //     this.entitiesChildren = [index];
                //     // console.log('indexNew', index);
                // });
                console.log('oldValue: ', old);
                console.log('newValue: ', value);
                this.$emit('newValueEp', value);
            },
            deep: true,
        },

        arraydetailEntity: function (value) {
            if (value) {
                this.showSkeleton = false;
                console.log('mang array thay doi: ', value);
            }
        },
        // entitiesChildren: {
        //     handler: function (value, old) {
        //         // old.map((index) => {
        //         //     console.log('indexOld', index);
        //         //     // this.entitiesChildren = [...this.entitiesChildren, index];
        //         // });
        //         // value.map((index) => {
        //         //     this.entitiesChildren = [index];
        //         //     // console.log('indexNew', index);
        //         // });
        //         console.log('oldValue: ', old);
        //         console.log('newValue: ', value);
        //         this.lastEntitiesChildren = [...Object.values(old), ...Object.values(value)];
        //         console.log('dasdsad', this.lastEntitiesChildren);
        //     },
        //     deep: true,
        // },
    },

    // computed: {
    //     filteredTreeData() {
    //         if (this.showAllNode == true) {
    //             return this.entities;
    //         } else {
    //             return this.entities.filter((node) => !node.parent);
    //         }
    //     },
    // },
    methods: {
        /**
         * Mở box function đúng định dạng
         * Truyền id và mã nhân viên cần xóa
         * @author huyhq 16-2-2023
         * @param {*} event
         * @param {*} item
         */
        recieveCombobox(event, item) {
            try {
                // console.log(this.$refs.cellFunction);
                // event.target.parentElement.parentElement.parentElement.classList.toggle('rewrite-td');

                this.entityDelete = item[this.propValue];
                this.codeDelete = item[this.propValueCode];

                this.$emit('getValuesDelete', this.entityDelete, this.codeDelete);

                const dropDown = this.$refs.cellFunction;
                const main = event.currentTarget;
                const className = 'rewrite-td';
                let myContent = null;

                dropDown.forEach((item) => {
                    if (main.parentElement.parentElement.contains(item)) {
                        myContent = item;
                    } else {
                        item.classList.remove(className);
                    }
                });

                // Chuyển đổi ẩn hiện khi được nhấp
                if (myContent) myContent.classList.toggle(className);
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
                    case 'Currency':
                        return commonJS.formatCurrency(value);
                    case 'Status': {
                        return commonJS.formatStatus(value);
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
         * Hiển thị form thông tin đối tượng ứng với mã được chọn
         * <br/>
         * CreatedBy: huynq (5/1/2023)
         * @param {*} item
         */
        btnEditOnClick(item) {
            try {
                // this.entityEdit = item;
                this.entityEdit = item[this.propValue];
                this.$emit('getEmployeeId', this.entityEdit, item[this.propParentId]);
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
         * Truyền thông tin xóa
         * <br/>
         * CreatedBy: huynq (5/1/2023)
         * @param {*} item
         */
        btnDeleteOnClick(item) {
            try {
                this.$emit('getIdDeleteRecord', item[this.propValue], item[this.propValueCode]);
            } catch (err) {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            }
        },

        btnGetIdMaster(item) {
            try {
                // this.entityEdit = item;
                this.entityMaster = item[this.propValue];
                this.$emit('getMasterId', this.entityMaster);
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
         * Chọn tất cả checkbox có trong table khi click vào ô checkbox title của table
         * CreatedBy: huynq (15/1/2023)
         */
        checkedCheckbox() {
            try {
                // this.checkRows = [];
                // const newcheckedItem = { ...this.checkedItem };
                this.checkedItem = {};
                // if (!this.checkAllRows) {
                //     for (var i in this.entities) {
                //         this.checkRows.push(this.entities[i][this.propValue]);
                //         // newcheckedItem[this.entities[i][this.propValue]] = event.target.checked;
                //         this.checkedItem = {
                //             ...this.checkedItem,
                //             [this.entities[i][this.propValue]]: event.target.checked,
                //         };
                //     }
                // }

                if (!this.checkAllRows) {
                    for (let i in this.entities) {
                        if (!this.checkRows.includes(this.entities[i][this.propValue])) {
                            this.checkRows.push(this.entities[i][this.propValue]);
                            // newcheckedItem[this.entities[i][this.propValue]] = event.target.checked;
                            this.checkedItem = {
                                ...this.checkedItem,
                                [this.entities[i][this.propValue]]: event.target.checked,
                            };
                        }
                    }
                } else {
                    for (let i in this.entities) {
                        let valueCheck = this.checkRows.indexOf(this.entities[i][this.propValue]);
                        if (valueCheck != -1) {
                            this.checkRows.splice(valueCheck, 1);
                        }
                    }
                }
                if (this.checkRows.length > 0) {
                    this.enableDeleteMultiple = true;
                } else {
                    this.enableDeleteMultiple = false;
                }
                this.checkAllRows = !this.checkAllRows;
                this.$emit('employeeIds', this.checkRows, this.enableDeleteMultiple);
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
         * Hàm kiểm tra cột dữ liệu nào được sắp xếp
         * @author huynq 12-2-2023
         * @param {*} check
         * @param {*} sortColumn
         */
        btnSortOnClick(event, check, sortColumn) {
            console.log('sortOrderDirection', this.sortOrderDirection);
            try {
                if (check === 'true') {
                    switch (this.sortOrderDirection) {
                        case '': {
                            this.sortOrderDirection = 'desc';
                            // console.log('case rong', this.isDesc, event,);
                            //         for (var i in this.$refs.true) {
                            //     if (event.currentTarget.contains(this.$refs.btnDetailFilter[i].$refs.btnIcon)) {
                            //         this.$refs.btnDetailFilter[i].$refs.btnIcon.classList.remove('btn-show-form-filter');
                            //     } else {
                            //         this.$refs.btnDetailFilter[i].$refs.btnIcon.classList.add('btn-show-form-filter');
                            //     }
                            // }
                            break;
                        }
                        case 'desc': {
                            this.sortOrderDirection = 'asc';
                            console.log('case desc', this.isDesc);
                            break;
                        }
                        case 'asc': {
                            console.log('case asc');
                            this.sortOrderDirection = '';
                            break;
                        }
                        default: {
                            break;
                        }
                    }
                    this.$emit('getvalueSortOrder', sortColumn, this.sortOrderDirection);
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
         * Hiện button khi hover ở các cột cho phép được lọc
         * @author huyhq 16-02-2023
         * @param {*} event
         * @param {*} check
         */
        showButtonFilter(event, check) {
            try {
                // console.log('gsdfg', this.$refs.btnDetailFilter[1].$refs.btnIcon);
                // debugger;
                if (check == 'true') {
                    for (var i in this.$refs.true) {
                        if (event.currentTarget.contains(this.$refs.btnDetailFilter[i].$refs.btnIcon)) {
                            this.$refs.btnDetailFilter[i].$refs.btnIcon.classList.remove('btn-show-form-filter');
                        } else {
                            this.$refs.btnDetailFilter[i].$refs.btnIcon.classList.add('btn-show-form-filter');
                        }
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
         * Ẩn button khi không hover vào các cột cho phép được lọc
         * @author huyhq 16-02-2023
         */
        hideButtonFilter() {
            try {
                for (var i in this.$refs.true) {
                    this.$refs.btnDetailFilter[i].$refs.btnIcon.classList.add('btn-show-form-filter');
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
         * Hàm ẩn/hiện form lọc chi tiết
         * @author huynq 16-2-2023
         * @param {*} index
         * @param {*} title
         * @param {*} typeColumn
         */
        showBoxFilter(index, title, typeColumn) {
            try {
                this.positionFormFilter = { ...this.positionFormFilter, left: 5 + 180 * index };
                this.columnFilter = title;
                this.typeColumnFilter = typeColumn;
                this.isShowFormFilter = !this.isShowFormFilter;
                console.log('title: ', title);
                console.log('index: ', index);
                console.log('typeColumnFilter: ', this.typeColumnFilter);
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
         * Hàm truyền giá trị loại phép toán lọc nhận được từ form lọc chi tiết đến nghiệp vụ chính
         * @author huyhq 16-2-2023
         * @param {*} value
         */
        operatorFilter(value) {
            try {
                this.$emit('getOperator', value);
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
         * Hàm truyền giá trị text nhận được từ form lọc chi tiết đến nghiệp vụ chính
         * @author huyhq 16-2-2023
         * @param {*} value
         * @param {*} operatorFilter
         * @param {*} nameColumnFilter
         * @param {*} nameOperator
         */
        textFilter(value, operatorFilter, nameColumnFilter, typeOfColumnFilter, nameOperator) {
            try {
                this.$emit('getFilterData', value, operatorFilter, nameColumnFilter, typeOfColumnFilter, nameOperator);
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
        transmissionInfoDelete() {
            try {
                this.$emit('infoDelete', this.entityDelete, this.codeDelete);
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
        transmissionIdReplicate() {
            try {
                this.$emit('idReplication', this.entityDelete);
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
         * Thay đổi màu sắc row table khi click vào hàng đó hoặc checkbox trong hàng
         * @author huynq 20-2-2023
         * @param {*} event
         * @param {*} item
         */
        eventCheckedCheckbox(event, item) {
            // for (var i in this.$refs.rowTable) {
            //     if (event.currentTarget.parentElement.parentElement.parentElement.contains(this.$refs.rowCheckbox[i])) {
            //         if (event.target.checked == true) {
            //             this.$refs.rowTable[i].classList.add('focus-row');
            //             this.$refs.columnCheckbox[i].classList.add('focus-row');
            //             this.$refs.cellFunction[i].classList.add('focus-row');
            //             this.$refs.buttonBoxFunction[i].$refs.btnIcon.classList.add('focus-row');
            //         } else {
            //             this.$refs.rowTable[i].classList.remove('focus-row');
            //             this.$refs.columnCheckbox[i].classList.remove('focus-row');
            //             this.$refs.cellFunction[i].classList.remove('focus-row');
            //             this.$refs.buttonBoxFunction[i].$refs.btnIcon.classList.remove('focus-row');
            //         }
            //     }
            // }

            try {
                this.checkedItem = { ...this.checkedItem, [item]: event.target.checked };
                console.log('cac checkbox da duoc check: ', this.checkedItem);
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
         * Bỏ lọc dữ liệu
         * CreatedBy: huynq (16-2-2023)
         */
        unfilterData() {
            try {
                this.isShowFormFilter = false;
                this.$emit('unfilteredDataTable');
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
         * Truyền dữ liệu cần thiết để xóa một bản ghi trong bảng có cấu trúc dạng cây
         * CreatedBy: huynq (20/3/2023)
         * @param {*} item
         */
        transmissionInforDel(item) {
            try {
                this.$emit('infoDelete', item[this.propValue], item[this.propValueCode]);
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
         * Lấy các đối tượng con dựa của cha dựa vào id của đối tượng cha
         * CreatdBy: huynq (11/3/2023)
         * @param {*} rootId
         */
        extendRootEntity(rootId, indexRow) {
            const index = this.extension.indexOf(rootId);
            console.log('fsdf', this.$refs.textcontent[indexRow]);
            var widthText = this.$refs.textcontent[indexRow].clientWidth;
            console.log('gdfg', widthText);
            this.parentId = rootId;
            this.positionTextChidlNumber = 26 + widthText;
            // axios
            //     .get(`${this.$MResource.LOCALHOST}/${this.typeObject}/${this.propParentId}?${this.propValue}=${rootId}`)
            //     .then((response) => {
            //         this.entitiesChildren = response.data;
            if (index > -1) {
                this.extension.splice(index, 1);
            } else {
                this.extension.push(rootId);
            }
            //         // this.lastEntitiesChildren = [...this.entitiesChildren];

            //         // console.log('lastEntitiesChildren', this.lastEntitiesChildren);
            //     })
            //     .catch((err) => {
            //         this.$MToastMessage({
            //             titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
            //             messageToast: err,
            //             showToastMessage: true,
            //             typeToast: 'errorToast',
            //         });
            //     });
        },

        /**
         * Hiển thị form thông tin đối tượng ứng với mã được chọn
         * <br/>
         * CreatedBy: huynq (5/1/2023)
         * @param {*} item
         */
        transmissIdRep(item) {
            try {
                // this.entityEdit = item;
                this.entityEdit = item[this.propValue];

                this.$emit('getIdReplication', this.entityEdit, item[this.propParentId]);
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
         * CreatedBy: huynq (27/3/2023)
         */
        transmissionStatus(item) {
            try {
                this.$emit('setStatus', item, item[this.textStatus]);
                console.log('gia tri text rtuye di: ', item[this.textStatus]);
            } catch (err) {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            }
        },

        btnDeleteRow(indexDel) {
            this.$emit('indexDelRow', indexDel);
        },
    },

    created() {
        // if (this.hasCheckbox == true) {
        //     this.checkRows = this.checkedRows;
        //         this.checkedRows.forEach((item) => {
        //             this.checkedItem = { ...this.checkedItem, [item]: true };
        //         });
        // }
        if (this.api) {
            this.sortOrderDirection = this.sortDirection;

            setTimeout(() => {
                // Lấy dữ liệu
                fetch(this.api)
                    .then((res) => res.json())
                    .then((data) => {
                        this.showSkeleton = false;
                        this.entities = data.data;
                        // this.entities = data;
                        this.totalPage = data.totalPages;
                        this.totalRecord = data.totalRecords;
                        this.$emit('totalNumberPages', this.totalPage);
                        this.$emit('totalRecords', this.totalRecord);

                        if (this.totalRecord <= 0) {
                            this.$emit('checkData', true);
                            this.checkAllRows = false;
                        } else {
                            this.$emit('checkData', false);
                        }
                        // if (this.enableCheckChild == true) {
                        //     axios
                        //         .get(`${this.$MResource.LOCALHOST}/${this.typeObject}`)
                        //         .then((response) => {
                        //             this.allEntities = response.data;
                        //             this.allEntities.map((index) => {
                        //                 this.entities.map((indexEntities) => {
                        //                     if (index[this.propParentId] == indexEntities[this.propValue]) {
                        //                         this.hasButtonExtend = [...this.hasButtonExtend, index[this.propParentId]];
                        //                     }
                        //                 });
                        //             });
                        //             //Lọc các giá trị trùng lặp
                        //             this.lastValuesButtonExtend = Array.from(new Set(this.hasButtonExtend));
                        //         })
                        //         .catch((err) => {
                        //             this.$MToastMessage({
                        //                 titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        //                 messageToast: err,
                        //                 showToastMessage: true,
                        //                 typeToast: 'errorToast',
                        //             });
                        //         });
                        // }
                    })
                    .catch((err) => {
                        this.showSkeleton = false;
                        this.$MToastMessage({
                            titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                            messageToast: err,
                            showToastMessage: true,
                            typeToast: 'errorToast',
                        });
                        console.log(err);
                    });

                // if (this.hasCheckbox == true) {
                //     if (this.checkRows != undefined) {
                //         if (this.checkRows.length === this.numberRecord) {
                //             this.checkAllRows = true;
                //         }
                //     }
                // }
            }, 1500);
        } else if (this.payDetail == true) {
            this.showSkeleton = false;
        }
    },

    data() {
        return {
            entities: [],

            showSkeleton: true,

            entityDelete: null,
            codeDelete: null,

            entityEdit: null,
            entityMaster: null,

            totalPage: 1,
            totalRecord: 0,

            currentPage: 1,

            checkRows: [],

            checkAllRows: false,

            sortOrderDirection: '',

            enableDeleteMultiple: false,

            positionFormFilter: { left: 5, top: 104 },
            isShowFormFilter: false,
            columnFilter: '',

            checkedItem: {},

            hasChildren: false,
            // entitiesChildren: [],
            // lastEntitiesChildren: [],
            extension: [],
            allEntities: [],
            hasButtonExtend: [],
            lastValuesButtonExtend: [],
            parentId: '',
            positionTextChidlNumber: 0,

            accounting: [{}],
        };
    },
};
</script>

<style>
@import url('./table.css');
.rewrite-box-function {
    width: 132px;
    left: -95px;
}

.btn-show-form-filter {
    visibility: visible;
    opacity: 0;
}

.focus-row {
    background-color: #f1ffef !important;
}

table tbody tr:hover .focus-row {
    background-color: #f2f2f2 !important;
}

.no-data {
    display: flex;
    justify-content: center;
    align-items: center;
}
</style>
