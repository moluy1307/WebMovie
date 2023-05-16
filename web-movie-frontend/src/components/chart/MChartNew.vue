<template>
    <Bar id="my-chart-id" :options="chartOptions" :data="chartData"></Bar>
</template>

<script>
import { Bar } from 'vue-chartjs';
import { Chart as ChartJS, Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale } from 'chart.js';

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale);
export default {
    name: 'MChartNew',
    components: { Bar },
    props: ['chartOptions', 'dataChart'],
    mounted() {
        console.log('du lieu duoc di:  ', this.dataChart);
    },
    async created() {
        // // Lọc các dữ liệu theo yêu cầu "yeu cau mua hang" và enable là true
        // const filteredData = this.dataChart.filter((item) => item.request === 'yeu cau mua hang' && item.enable);
        // // Lấy dữ liệu của năm hiện tại
        // const currentYear = new Date().getFullYear();
        // const currentYearData = filteredData.filter((item) => new Date(item.date).getFullYear() === currentYear);
        // // Tính tổng doanh thu của từng tháng trong năm
        // const revenueByMonth = Array(12).fill(0);
        // currentYearData.forEach((item) => {
        //     const month = new Date(item.date).getMonth();
        //     revenueByMonth[month] += item.revenue;
        // });
        const groupedData = this.dataChart.reduce((result, data) => {
            // Lấy ra tháng từ ngày
            const month = new Date(data.createdDate).getMonth() + 1;
            const key = `Th ${month}`;
            if (!result[key]) {
                result[key] = 0;
            }
            result[key] += data.recharge * 100;
            return result;
        }, {});
        this.chartData = {
            labels: Object.keys(groupedData),
            datasets: [
                {
                    label: 'Doanh thu',
                    backgroundColor: '#4154f1',
                    data: Object.values(groupedData),
                    // data: revenueByMonth,
                },
            ],
        };
    },
    data() {
        return {
            chartData: [],
        };
    },
};
</script>

<style></style>
