<template>
    <div class="infor">
        <label for="" v-tippy="tooltip" :style="styleLabel">{{ label }}</label>
        <div class="m-row">
            <input type="text" v-model="inputValue" @input="formatInput" @keydown="deleteValue" class="txtNhap" />
        </div>
    </div>
</template>

<script>
export default {
    name: 'MInputTime',
    props: ['label', 'modelValue', 'styleLabel'],
    emits: ['update:modelValue'],
    watch: {
        modelValue(newVal) {
            this.inputValue = newVal;
        },
    },
    methods: {
        formatInput() {
            if (this.inputValue == undefined) {
                return;
            }
            const input = this.inputValue.replace(/[^\d]/g, '');
            let minutes;
            let seconds;
            if (input.length == 3) {
                minutes = input.substr(0, 1);
                seconds = input.substr(1, 2);
            } else {
                minutes = input.substr(0, 2);
                seconds = input.substr(2, 2);
            }
            this.inputValue = `${minutes}:${seconds}`;
            this.$emit('update:modelValue', this.inputValue);
        },

        deleteValue(event) {
            if (event.keyCode === 8) {
                event.preventDefault();
                const input = this.inputValue.replace(/[^\d]/g, '');
                const newInput = input.slice(0, -1);
                const minutes = newInput.substr(0, 2);
                const seconds = newInput.substr(2, 2);
                this.inputValue = `${minutes}:${seconds}`;
                this.$emit('update:modelValue', this.inputValue);
            }
        },
    },
    mounted() {
        console.log('gia tri:', this.modelValue);
    },
    created() {
        console.log('gia tri: ', this.modelValue);
        this.inputValue = this.modelValue;
    },
    data() {
        return {
            value: '',
        };
    },
};
</script>

<style></style>
