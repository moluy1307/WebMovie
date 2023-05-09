<template>
    <div class="infor">
        <label for="" v-tippy="tooltip" :style="styleLabel"
            >{{ labelInput
            }}<span :style="styleCompulsory" style="color: rgb(230, 0, 0); margin-bottom: 8px">{{
                compulsory
            }}</span></label
        >
        <div class="m-row" :style="styleBoxInput" @mouseover="showTooltip" @mouseleave="show = false">
            <input
                v-if="isInputPassword == true"
                type="password"
                :value="modelValue"
                :class="newClass"
                :style="newStyle"
                class="txtNhap"
                @input="$emit('update:modelValue', $event.target.value)"
                autocomplete="off"
                :placeholder="placeHolder"
                ref="txtInput"
                :tabindex="propTabindex"
            />
            <input
                v-else
                type="text"
                :value="modelValue"
                :class="newClass"
                :style="newStyle"
                class="txtNhap"
                @input="$emit('update:modelValue', $event.target.value)"
                autocomplete="off"
                :placeholder="placeHolder"
                ref="txtInput"
                :tabindex="propTabindex"
            />

            <div v-show="show" class="type-tooltip" :style="styleTooltip">
                <div class="arrow-tooltip"></div>
                <div class="tooltip-input-error">
                    {{ errorMessage }}
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import { TippyDirective } from 'tippy.vue';
export default {
    name: 'MInput',
    props: [
        'labelInput',
        'modelValue',
        'newClass',
        'newStyle',
        'eventChange',
        'txtInput',
        'placeHolder',
        'propTabindex',
        'compulsory',
        'styleCompulsory',
        'errorMessage',
        'tooltip',
        'tooltipError',
        'styleBoxInput',
        'styleLabel',
        'styleTooltip',
        'isInputPassword',
    ],
    emits: ['update:modelValue'],
    watch: {
        value: function (newValue) {
            this.$emit('update:modelValue', newValue);
        },
    },
    directives: {
        tippy: TippyDirective,
    },
    methods: {
        /**
         * Hiện tooltip khi hover vào ô input
         * @author huynq 18-2-2023
         */
        showTooltip() {
            try {
                if (this.tooltipError) {
                    this.show = true;
                } else {
                    this.show = false;
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
    },
    created() {
        this.value = this.modelValue;
    },
    data() {
        return {
            value: null,
            show: false,
        };
    },
};
</script>
<style scoped>
@import '~tippy.js/dist/tippy.css';
@import url('./input.css');
.m-row {
    display: flex;
    flex-direction: column;
    justify-content: center;
    position: relative;
}
</style>
