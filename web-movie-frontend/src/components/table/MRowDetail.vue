<template>
    <tr>
        <td>{{ orderNumber }}</td>
        <td><MInput></MInput></td>
        <td>
            <MDropdownList
                typeCbo="dropdown"
                propValue="valueCombobox"
                propName="title"
                v-model="dataCombobox.valueCombobox"
                :dataCombobox="dataCombobox"
            ></MDropdownList>
        </td>
        <td>
            <MDropdownList
                typeCbo="dropdown"
                propValue="valueCombobox"
                propName="title"
                v-model="dataCombobox.valueCombobox"
                :dataCombobox="dataCombobox"
            ></MDropdownList>
        </td>
        <td><MInput></MInput></td>
        <td><MInput></MInput></td>
        <td></td>
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
