<template>
    <tr v-for="index in 10" :key="index">
        <!-- <td class="column-16" style="background-color: #fff !important"></td> -->
        <td v-if="hasCheckbox">
            <!-- <Skeleton width="18" height="18" /> -->
            <div class="loading"></div>
        </td>
        <template v-for="(col, indexCol) in titleTable" :key="indexCol">
            <td :class="col.className"><div class="loading"></div></td>
        </template>
        <td class="br-none" v-if="hasWiget">
            <div class="loading"></div>
        </td>
    </tr>
</template>

<script>
// import { Skeleton } from 'vue-loading-skeleton';
export default {
    name: 'MLoading',
    // components: { Skeleton },
    props: {
        hasWiget: {
            type: Boolean,
        },
        hasCheckbox: {
            type: Boolean,
        },
        titleTable: {
            type: Array,
        },
    },
};
</script>

<style>
@import url('./loading.css');
@import url('../table/table.css');
</style>
