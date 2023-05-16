<template>
    <Doughnut :data="dataDoughnut" :options="optionsDoughnut" />
</template>

<script>
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Doughnut } from 'vue-chartjs';

ChartJS.register(ArcElement, Tooltip, Legend);
export default {
    name: 'MChartDoughnut',
    components: { Doughnut },
    props: ['optionsDoughnut', 'totalHasNotBuyMovie', 'userIdList'],
    created() {
        this.dataDoughnut = {
            labels: ['Người chưa xem', 'Người đã xem'],
            datasets: [
                {
                    backgroundColor: ['#41B883', '#E46651'],
                    data: [this.totalHasNotBuyMovie.length - this.userIdList.length, this.userIdList.length],
                },
            ],
        };
    },
    data() {
        return {
            dataDoughnut: [],
        };
    },
};
</script>

<style></style>
