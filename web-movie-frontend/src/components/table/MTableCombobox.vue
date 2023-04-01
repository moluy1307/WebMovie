<template>
    <table id="m-table" class="tbl-cbo" ref="tableData">
        <thead>
            <tr style="height: 32px !important">
                <template v-for="(item, index) in titleTable" :key="index">
                    <th :class="item.classTitle" :style="item.styleCss" :ref="item.hasFilter" style="border: none">
                        <div style="position: relative" :class="item.styleElement">{{ item.title }}</div>
                    </th>
                </template>
                <!-- <td class="column-150-imp">333333</td>
                <td class="column-230-imp">2222</td> -->
            </tr>
        </thead>
        <tbody class="main-table-cbo" ref="scrollContainer">
            <!-- <template v-for="(item, index) in entities" :key="index"> -->
            <tr
                ref="rowItem"
                class="row-table-cbo"
                v-for="(item, index) in entities"
                :key="index"
                :class="{ 'is-row-root': isFolder.includes(item[propValue]), 'item-select': index == indexItemSelect }"
                @click="selectValueRow(item)"
            >
                <template v-for="(col, indexCol) in titleTable" :key="indexCol">
                    <td v-if="col.hasExtend" :class="col.className" style="border: none">
                        <div :style="{ 'margin-left': 12 * item[levelObject] + 'px' }">
                            {{ item[col.dataField] }}
                        </div>
                    </td>
                    <td v-else :class="col.className">
                        {{ item[col.dataField] }}
                    </td>
                </template>
            </tr>
            <!-- <tr v-if="noData == true" style="display: block">
                <td :colspan="titleTable.length" class="column-no-data">Không có dữ liệu hiển thị</td>
            </tr> -->
            <!-- </template> -->
        </tbody>
    </table>
</template>

<script>
export default {
    name: 'MTableCombobox',
    props: {
        titleTable: {
            type: Array,
        },
        entities: {
            type: Array,
        },
        levelObject: {
            type: String,
        },
        isTreeStructure: {
            type: Boolean,
        },
        propParentValue: {
            type: String,
        },
        propValue: {
            type: String,
        },
        indexItemSelect: {
            type: Number,
        },
        noData: {
            type: Boolean,
        },
    },
    watch: {
        indexItemSelect: function (value) {
            const followScroll = this.$refs.rowItem[value].clientHeight;
            this.$refs.scrollContainer.scrollTop = followScroll * value;
        },
    },
    methods: {
        /**
         * Truyền giá trị chọn được khi lựa chọn vào 1 hàng bất kì trong table combobox
         * CreatedBy: huyhq (20/3/2023)
         * @param {*} item
         */
        selectValueRow(item) {
            try {
                this.$emit('valueSelected', item);
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
        const followScroll = this.$refs.rowItem[this.indexItemSelect].clientHeight;
        this.$refs.scrollContainer.scrollTop = followScroll * this.indexItemSelect;
    },
    created() {
        if (this.isTreeStructure) {
            for (var i = 0; i < this.entities.length; i++) {
                for (var j = i + 1; j < this.entities.length; j++) {
                    if (this.entities[i][this.propValue] == this.entities[j][this.propParentValue]) {
                        this.isFolder.push(this.entities[i][this.propValue]);
                    }
                }
            }
        }
    },
    data() {
        return {
            isFolder: [],
        };
    },
};
</script>

<style></style>
