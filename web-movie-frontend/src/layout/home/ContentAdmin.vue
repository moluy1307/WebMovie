<template>
    <div class="content">
        <HeaderAdmin></HeaderAdmin>
        <BodyAdmin></BodyAdmin>
    </div>
</template>

<script>
import HeaderAdmin from './header/HeaderAdmin.vue';
import BodyAdmin from './body/BodyAdmin.vue';
export default {
    name: 'ContentAdmin',
    components: { HeaderAdmin, BodyAdmin },
};
</script>

<style></style>
