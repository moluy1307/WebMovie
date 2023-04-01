<template>
    <button
        :class="newClass"
        @click="eventOnClick"
        v-tippy="tooltip"
        :disabled="disable"
        :style="newStyle"
        ref="refBtn"
    >
        {{ label }}
        <!-- <MIcon nameIcon="iconElement"></MIcon> -->
    </button>
</template>

<script>
export default {
    name: 'MButton',
    props: ['label', 'newClass', 'eventOnClick', 'tooltip', 'icon', 'disable', 'newStyle', 'refBtn'],
    watch: {
        iconElement: function (value) {
            console.log(value);
        },
    },
    created() {
        if (this.icon) {
            this.iconElement = this.icon;
        }
    },
    data() {
        return {
            iconElement: null,
        };
    },
};
</script>

<style scoped>
@import url('./button.css');
</style>
