<template>
    <div class="infor">
        <label for="" v-tippy="tooltip">{{ label }}</label>
        <div class="m-row">
            <input
                type="text"
                @keypress="onlyNumber"
                class="txtNhap"
                autocomplete="off"
                :value="money"
                @input="changeValue"
            />
        </div>
    </div>
</template>

<script>
export default {
    name: 'MInputMoney',
    props: ['label', 'modelValue'],
    emits: ['update:modelValue'],
    watch: {
        modelValue: function (newValue) {
            this.value = newValue;
        },
    },
    computed: {
        money: function () {
            if (this.value) {
                var money = new Intl.NumberFormat().format(this.value);
                return money;
            } else {
                return '';
            }
        },
    },
    methods: {
        /**
         * Thay đổi định dạng số khi thực hiện nhập
         * @param {*} event
         * CreatedBy: huynq (16/1/2023)
         */
        changeValue(event) {
            var rootValue = event.target.value.replaceAll('.', '');
            // console.log('gia tri tien ban dau thay doi : ', rootValue);
            var value = parseInt(rootValue);
            // console.log('gia tri tien hien tai: ', rootValue);
            this.$emit('update:modelValue', value);
            console.log('event tien : ', value);
        },

        /**
         * Chỉ cho phép nhập số
         */
        onlyNumber($event) {
            //console.log($event.keyCode);
            let keyCode = $event.keyCode ? $event.keyCode : $event.which;
            if ((keyCode < 48 || keyCode > 57) && keyCode !== 46) {
                $event.preventDefault();
            }
        },
    },
    created() {
        this.value = this.modelValue;
    },
    data() {
        return {
            value: null,
        };
    },
};
</script>

<style>
@import url('./input.css');
</style>
