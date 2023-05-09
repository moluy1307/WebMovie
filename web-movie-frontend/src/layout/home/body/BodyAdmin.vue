<template>
    <div class="body">
        <router-view name="routerAdmin"></router-view>
        <!-- <EmployeeList></EmployeeList> -->
    </div>
</template>
<script>
// import EmployeeList from '@/views/employee/EmployeeList.vue';
export default {
    name: 'BodyAdmin',
    // components: { EmployeeList },
};
</script>
<style>
@import url('./body.css');
</style>
