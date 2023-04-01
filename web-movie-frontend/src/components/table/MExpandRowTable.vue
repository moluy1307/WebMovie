<template>
    <tr
        v-for="(itemChild, index) in entitiesChildren"
        :key="index"
        @dblclick="btnEditOnClick(itemChild)"
        ref="rowTable"
    >
        <template v-for="(col, indexCol) in titleTable" :key="indexCol">
            <td :class="col.className">
                <div
                    v-if="col.isFieldMain == true"
                    :style="{ 'margin-left': marginLeft + 'px' }"
                    @click="showChild(itemChild[propValue])"
                >
                    {{ getFormatByData(col.formatType, itemChild[col.dataField]) }}
                </div>
                <div v-else>{{ getFormatByData(col.formatType, itemChild[col.dataField]) }}</div>
            </td>
        </template>

        <td class="br-none" ref="cellFunction" v-if="hasWiget" @dblclick.stop.prevent>
            <div class="text-function-center">
                <button class="btn-action btn-edit-form" @click="btnEditOnClick(itemChild)">Sửa</button>

                <MBoxFunction
                    :employeeDelete="entityDelete"
                    :codeDelete="codeDelete"
                    @getIdReplicate="transmissionIdReplicate"
                    @itemDelete="transmissionInfoDelete"
                    @click="($event) => recieveCombobox($event, itemChild)"
                    :newClassIcon="{
                        'btn-arrow-function': true,
                    }"
                ></MBoxFunction>
            </div>
        </td>
    </tr>
</template>

<script>
import MBoxFunction from '../other/MBoxFunction.vue';
import commonJS from '@/js/common';
import axios from 'axios';

export default {
    name: 'MExpandRowTable',
    components: { MBoxFunction },
    props: {
        titleTable: {
            type: Array,
        },
        typeObject: {
            type: String,
        },
        propParentId: {
            type: String,
        },
        propValue: {
            type: String,
        },
        rootId: {
            type: String,
        },
        marginLeft: {
            type: Number,
        },
        isFieldMain: {
            type: Boolean,
        },
        hasWiget: {
            type: Boolean,
        },
    },
    watch: {
        rootId: function (value) {
            console.log('valurRootId: ', value);
        },
    },
    methods: {
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
        showChild(id) {
            console.log('rrererer: ', id);
            this.$emit('rootEntityId', id);
            // axios
            //     .get(`${this.$MResource.LOCALHOST}/${this.typeObject}/${this.propParentId}?${this.propValue}=${id}`)
            //     .then((response) => {
            //         this.entitiesChildren = response.data;
            //         console.log('entites: ', this.entitiesChildren);
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
    },
    created() {
        axios
            .get(
                `${this.$MResource.LOCALHOST}/${this.typeObject}/${this.propParentId}?${this.propValue}=${this.rootId}`,
            )
            .then((response) => {
                this.entitiesChildren = response.data;
                console.log('entites: ', this.entitiesChildren);
            })
            .catch((err) => {
                this.$MToastMessage({
                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                    messageToast: err,
                    showToastMessage: true,
                    typeToast: 'errorToast',
                });
            });
    },
    data() {
        return {
            entitiesChildren: [],
        };
    },
};
</script>

<style>
@import url('./table.css');
</style>
