<template>
    <tr>
        <td class="text-center">{{ orderNumber }}</td>
        <template v-for="(propVal, indexVal) in titleTableData" :key="indexVal">
            <td>
                <input type="text" :value="orderNumber" />
            </td>
        </template>
        <!-- <td><MInput></MInput></td>
        <td><MInput></MInput></td>
        <td><MInput></MInput></td>
        <td><MInput></MInput></td>
        <td></td> -->
        <td>
            <MIcon
                nameIcon="trash"
                newStyle="width: 32px; height: 32px;margin-right: 4px"
                :class="{ 'icon-size': true }"
            ></MIcon>
        </td>
    </tr>
</template>

<script>
export default {
    name: 'MRowDetail',
    props: {
        orderNumber: {
            type: Number,
        },
        objectDetail: {
            type: Object,
        },
        titleTableData: {
            type: Array,
        },
    },
    created() {
        console.log('object nhan duoc: ', this.objectDetail);
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

<style></style>
