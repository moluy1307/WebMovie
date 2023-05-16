<template>
    <div class="body-header">
        <div class="body-title">Thống kê</div>
    </div>
    <div class="body-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="row">
                    <div class="col-xxl-4 col-md-3">
                        <div class="card info-card money">
                            <div class="card-body">
                                <h5 class="card-title">Tổng doanh thu</h5>

                                <div class="d-flex align-items-center">
                                    <div
                                        class="card-icon rounded-circle d-flex align-items-center justify-content-center"
                                    >
                                        <font-awesome-icon :icon="['fas', 'dollar-sign']" />
                                    </div>
                                    <div class="ps-3">
                                        <h6>{{ totalRevenue }}</h6>
                                        <!-- <span class="text-success small pt-1 fw-bold">20000</span>

                                        <span class="text-muted small pt-2 ps-1">Xu</span> -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xxl-4 col-md-3">
                        <div class="card info-card user-total">
                            <div class="card-body">
                                <h5 class="card-title">Số lượng người dùng</h5>

                                <div class="d-flex align-items-center">
                                    <div
                                        class="card-icon rounded-circle d-flex align-items-center justify-content-center"
                                    >
                                        <font-awesome-icon :icon="['fas', 'user-group']" />
                                    </div>
                                    <div class="ps-3">
                                        <h6>{{ totalAccount.length }}</h6>
                                        <!-- <span class="text-success small pt-1 fw-bold">20000</span>

                                        <span class="text-muted small pt-2 ps-1">Xu</span> -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xxl-4 col-md-3">
                        <div class="card info-card movie-total">
                            <div class="card-body">
                                <h5 class="card-title">Tổng số phim</h5>

                                <div class="d-flex align-items-center">
                                    <div
                                        class="card-icon rounded-circle d-flex align-items-center justify-content-center"
                                    >
                                        <font-awesome-icon :icon="['fas', 'video']" />
                                    </div>
                                    <div class="ps-3">
                                        <h6>{{ totalMovie }}</h6>
                                        <!-- <span class="text-success small pt-1 fw-bold">20000</span>

                                        <span class="text-muted small pt-2 ps-1">Xu</span> -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xxl-4 col-md-3">
                        <div class="card info-card movie-review">
                            <div class="card-body">
                                <h5 class="card-title">Tổng số lượt xem</h5>

                                <div class="d-flex align-items-center">
                                    <div
                                        class="card-icon rounded-circle d-flex align-items-center justify-content-center"
                                    >
                                        <font-awesome-icon :icon="['fas', 'eye']" />
                                    </div>
                                    <div class="ps-3">
                                        <h6>{{ totalView }}</h6>
                                        <!-- <span class="text-success small pt-1 fw-bold">20000</span>

                                        <span class="text-muted small pt-2 ps-1">Xu</span> -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-8">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <!-- <div class="filter">
                                <a class="icon show" href="#" data-bs-toggle="dropdown" aria-expanded="true">
                                    <font-awesome-icon :icon="['fas', 'ellipsis']"
                                /></a>
                                <ul
                                    class="dropdown-menu dropdown-menu-end dropdown-menu-arrow show"
                                    data-popper-placement="bottom-end"
                                    style="
                                        position: absolute;
                                        inset: 0px 0px auto auto;
                                        margin: 0px;
                                        transform: translate3d(0px, 29.6px, 0px);
                                    "
                                >
                                    <li class="dropdown-header text-start">
                                        <h6>Filter</h6>
                                    </li>

                                    <li><a class="dropdown-item" href="#">Today</a></li>
                                    <li><a class="dropdown-item" href="#">This Month</a></li>
                                    <li><a class="dropdown-item" href="#">This Year</a></li>
                                </ul>
                            </div> -->

                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-10">
                                        <h5 class="card-title">Doanh thu theo tháng trong một năm</h5>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="group-ip" style="padding: 20px 0 15px 0">
                                            <select v-model="sortByYear" style="padding: 6px 12px 6px 10px">
                                                <option
                                                    v-for="itemYear in yearSelect"
                                                    :key="itemYear"
                                                    :value="itemYear.valueYear"
                                                >
                                                    {{ itemYear.titleYear }}
                                                </option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="report-chart" style="max-height: 310px">
                                    <div class="row">
                                        <div class="col-md-9">
                                            <MChartNew
                                                id="my-chart-id"
                                                :chartOptions="chartOptions"
                                                :dataChart="dataRevenue"
                                                :key="chartBarKey"
                                            ></MChartNew>
                                        </div>
                                        <!-- <Bar id="my-chart-id" :options="chartOptions" :data="chartData"></Bar> -->
                                        <div class="col-md-3 d-flex align-items-center justify-content-center">
                                            <div style="padding-top: 120px">
                                                <p style="font-size: 16px; color: #012970">
                                                    Doanh thu năm {{ sortByYear }}
                                                </p>
                                                <p style="font-size: 28px; color: #012970; font-weight: 700">
                                                    {{ totalRevenueInAYear }}
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="reload-chart" v-if="showLoadingChart">
                                        <!-- <div id="status" style="width: auto; height: auto; right: 0; top: -30px">
                                            <span style="border: 7px solid #6c757dc4; width: 50px; height: 50px"></span>
                                        </div> -->

                                        <div class="lds-ring">
                                            <div></div>
                                            <div></div>
                                            <div></div>
                                            <div></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">Tỉ lệ người dùng đã xem phim trả phí</h5>
                                <div class="report-chart" style="height: 315px; display: flex">
                                    <!-- <Doughnut :data="dataDoughnut" :options="optionsDoughnut" /> -->
                                    <MChartDoughnut
                                        :totalHasNotBuyMovie="totalHasNotBuyMovie"
                                        :userIdList="userIdList"
                                        :optionsDoughnut="optionsDoughnut"
                                        :key="chartDoughnutKey"
                                    ></MChartDoughnut>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import MChartNew from '@/components/chart/MChartNew.vue';
import MChartDoughnut from '@/components/chart/MChartDoughnut.vue';

// import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
// import { Doughnut } from 'vue-chartjs';

// ChartJS.register(ArcElement, Tooltip, Legend);

import axios from 'axios';

import commonJS from '@/js/common';
export default {
    name: 'OverviewAdmin',
    components: { MChartNew, MChartDoughnut },
    watch: {
        sortByYear: function (value) {
            if (value) {
                this.showLoadingChart = true;
                setTimeout(() => {
                    this.showLoadingChart = false;
                    this.callApiGetRevenueForChart();
                }, 1000);
            }
        },
    },
    methods: {
        callApiGetRevenue() {
            axios
                .get(`${this.$MResource.API}/OrderUsers/GetOrderRevenue`)
                .then((response) => {
                    this.orderRevenueList = response.data;
                    const calcTotalValue = this.orderRevenueList.reduce(
                        (accumulator, currentValue) => accumulator + currentValue.recharge,
                        0,
                    );

                    this.totalRevenue = commonJS.formatCurrencyCorrect(calcTotalValue * 100);
                    //                 this.intervalId = setInterval(() => {
                    //   if (this.totalRevenue < numberVnd) {
                    //     this.count++;
                    //   } else {
                    //     clearInterval(this.intervalId);
                    //   }
                    // }, 10)
                })
                .catch((err) => {
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        messageToast: err,
                        showToastMessage: true,
                        typeToast: 'errorToast',
                    });
                    console.log('err: ', err);
                });
        },
        async callApiGetRevenueForChart() {
            await axios
                .get(`${this.$MResource.API}/OrderUsers/GetOrderRevenue?sortByYear=${this.sortByYear}`)
                .then((response) => {
                    this.dataRevenue = response.data;
                    const calcTotalValue = this.dataRevenue.reduce(
                        (accumulator, currentValue) => accumulator + currentValue.recharge,
                        0,
                    );

                    this.totalRevenueInAYear = commonJS.formatCurrencyCorrect(calcTotalValue * 100);
                    this.chartBarKey++;
                    // const groupedData = this.dataRevenue.reduce((result, data) => {
                    //     // Lấy ra tháng từ ngày
                    //     const month = new Date(data.createdDate).getMonth() + 1;
                    //     const key = `Th ${month}`;

                    //     if (!result[key]) {
                    //         result[key] = 0;
                    //     }

                    //     result[key] += data.recharge * 100;

                    //     return result;
                    // }, {});

                    // this.chartData = {
                    //     labels: Object.keys(groupedData),
                    //     datasets: [
                    //         {
                    //             label: 'Doanh thu',
                    //             backgroundColor: '#4154f1',
                    //             data: this.dataFake,
                    //             // data: revenueByMonth,
                    //         },
                    //     ],
                    // };
                })
                .catch((err) => {
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        messageToast: err,
                        showToastMessage: true,
                        typeToast: 'errorToast',
                    });
                    console.log('err: ', err);
                });
        },

        callApiGetTotalUser() {
            axios
                .get(`${this.$MResource.API}/Users/filter?pageNumber=1&pageSize=10000`)
                .then((response) => {
                    this.accountList = response.data.data;
                    this.totalAccount = this.accountList.filter((accountItem) => accountItem.roleName == 'User');
                })
                .catch((err) => {
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        messageToast: err,
                        showToastMessage: true,
                        typeToast: 'errorToast',
                    });
                });
        },

        callApiGetTotalMovie() {
            axios
                .get(`${this.$MResource.API}/Movies/filter?pageNumber=1&pageSize=10000`)
                .then((response) => {
                    this.movieList = response.data.data;
                    this.totalMovie = commonJS.formatCurrency(response.data.totalRecords);
                    const calcTotalValue = this.movieList.reduce(
                        (accumulator, currentValue) => accumulator + currentValue.movieReview,
                        0,
                    );
                    this.totalView = commonJS.formatCurrency(calcTotalValue);
                })
                .catch((err) => {
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        messageToast: err,
                        showToastMessage: true,
                        typeToast: 'errorToast',
                    });
                    console.log('err: ', err);
                });
        },

        callApiGetUserIdBoughtMovie() {
            axios
                .get(`${this.$MResource.API}/OrderUsers/GetUserIdBoughtMovie`)
                .then((response) => {
                    this.userIdList = response.data;
                    axios
                        .get(`${this.$MResource.API}/Users/filter?pageNumber=1&pageSize=10000`)
                        .then((response) => {
                            this.accountListHasNotBuyMovie = response.data.data;
                            this.totalHasNotBuyMovie = this.accountListHasNotBuyMovie.filter(
                                (accountItem) => accountItem.roleName == 'User',
                            );

                            this.chartDoughnutKey++;
                        })
                        .catch((err) => {
                            this.$MToastMessage({
                                titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                                messageToast: err,
                                showToastMessage: true,
                                typeToast: 'errorToast',
                            });
                        });
                })
                .catch((err) => {
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                        messageToast: err,
                        showToastMessage: true,
                        typeToast: 'errorToast',
                    });
                });
        },
    },
    async created() {
        this.sortByYear = 2023;
        //Gọi api tính tổng doanh thu
        this.callApiGetRevenue();

        //Gọi Api tính tổng tài khoản
        this.callApiGetTotalUser();

        //Gọi Api tính tổng tổng số phim
        this.callApiGetTotalMovie();

        //Gọi Api truyền dữ liệu cho biểu đồ cột thống kê doanh thu theo tháng trong 1 năm
        this.callApiGetRevenueForChart();

        //Gọi api tính tỉ lệ người mua phim
        this.callApiGetUserIdBoughtMovie();
    },
    data() {
        return {
            orderRevenueList: [],
            totalRevenue: 0,
            // intervalId: null,

            accountList: [],
            totalAccount: 0,

            movieList: [],
            totalMovie: 0,
            totalView: 0,

            userIdList: [],
            accountListHasNotBuyMovie: [],
            totalHasNotBuyMovie: 0,

            yearSelect: [
                { valueYear: 2020, titleYear: 'Năm 2020' },
                { valueYear: 2021, titleYear: 'Năm 2021' },
                { valueYear: 2022, titleYear: 'Năm 2022' },
                { valueYear: 2023, titleYear: 'Năm 2023' },
            ],
            sortByYear: 2023,

            dataRevenue: [],
            totalRevenueInAYear: 0,
            chartData: [],
            chartBarKey: 0,

            chartOptions: {
                responsive: true,
            },

            optionsDoughnut: {
                responsive: true,
                maintainAspectRatio: false,
            },

            dataDoughnut: {},
            chartDoughnutKey: 0,

            showLoadingChart: false,
        };
    },
};
</script>

<style scoped>
@import url('./overviewAdmin.css');

/* .counter {
  font-size: 24px;
  font-weight: bold;
  animation: count-up 1s ease-in-out;
}

@keyframes count-up {
  from {
    transform: scale(1);
  }
  to {
    transform: scale(1.2);
  }
} */
</style>
