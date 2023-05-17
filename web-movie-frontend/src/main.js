import { createApp } from 'vue';
import App from './App.vue';
import { createRouter, createWebHashHistory } from 'vue-router';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
library.add(fas);
library.add(far);

import MDropdownList from './components/combobox/MDropdownList.vue';
import MCombobox from './components/combobox/MCombobox.vue';
import MInput from './components/input/MInput.vue';
import MInputMoney from './components/input/MInputMoney.vue';
import MInputTime from './components/input/MInputTime.vue';
import MButton from './components/button/MButton.vue';
import MDialog from './components/dialog/MDialog.vue';
import MDialogTimeCount from './components/dialog/MDialogTimeCount.vue';
import MIcon from './components/icon/MIcon.vue';
import MPaging from './components/paging/MPaging.vue';
import MTable from './components/table/MTable.vue';
import MToastMessage from './components/toast/MToastMessage.vue';
import MLoadingClient from './components/loading/MLoadingClient.vue';

// import HomeMovie from './view/home/HomeMovie.vue';
// import MovieSingle from './view/movieSingle/MovieSingle.vue';
// import MovieOverview from './view/movieSingle/overview/MovieOverview.vue';
// import MovieReview from './view/movieSingle/review/MovieReview.vue';
// import MovieMedia from './view/movieSingle/media/MovieMedia.vue';
// import MovieRelated from './view/movieSingle/related/MovieRelated.vue';
// import WatchMovie from './view/watchMovie/WatchMovie.vue';
// import ContentAdmin from './layout/home/ContentAdmin.vue';
// import AccountList from './view/account/AccountList.vue';
// import MovieList from './view/movieManager/MovieList.vue';
// import OrderUserList from './view/orderUser/OrderUserList.vue';
// import UserProfile from './view/userProfile/UserProfile.vue';
// import UserMovieViewed from './view/userProfile/UserMovieViewed.vue';
// import ActorList from './view/actorManager/ActorList.vue';
// import MovieGrid from './view/movieGrid/MovieGrid.vue';
// import ActorGrid from './view/actorGrid/ActorGrid.vue';
// import ActorSingle from './view/actorGrid/ActorSingle.vue';
// import OverviewAdmin from './view/overviewAdmin/OverviewAdmin.vue';

import ToastPlugin from './components/toast/ToastPlugin';
import MEnum from './js/enum';
import MResource from './js/resource';

import { TippyPlugin } from 'tippy.vue';
import VueClickAway from 'vue3-click-away';
import VCalendar from 'v-calendar';
import 'v-calendar/dist/style.css';
import VueLazyLoad from 'vue3-lazyload';
import lazyload from './js/lazyload.js';

const app = createApp(App);

app.component('MIcon', MIcon);
app.component('MPaging', MPaging);
app.component('MButton', MButton);
app.component('MDropdownList', MDropdownList);
app.component('MCombobox', MCombobox);
app.component('MInput', MInput);
app.component('MInputMoney', MInputMoney);
app.component('MInputTime', MInputTime);
app.component('MTable', MTable);
app.component('MDialog', MDialog);
app.component('MDialogTimeCount', MDialogTimeCount);
app.component('MToastMessage', MToastMessage);
app.component('MLoadingClient', MLoadingClient);

app.component('font-awesome-icon', FontAwesomeIcon);

app.config.globalProperties.$MEnum = MEnum;
app.config.globalProperties.$MResource = MResource;

const routes = [
    { path: '/', component: () => import('./view/home/HomeMovie.vue') },
    {
        path: '/moviesingle/:id',
        name: 'MovieSingle',
        redirect: { name: 'MovieDescript' },
        component: () => import('./view/movieSingle/MovieSingle.vue'),
        children: [
            {
                path: '',
                name: 'MovieDescript',
                component: () => import('./view/movieSingle/overview/MovieOverview.vue'),
                props: true,
            },
            {
                path: 'moviereview',
                name: 'moviereview',
                component: () => import('./view/movieSingle/review/MovieReview.vue'),
                props: true,
            },
            {
                path: 'moviemedia',
                name: 'moviemedia',
                component: () => import('./view/movieSingle/media/MovieMedia.vue'),
                props: true,
            },
            {
                path: 'movierelated',
                name: 'movierelated',
                component: () => import('./view/movieSingle/related/MovieRelated.vue'),
                props: true,
            },
        ],
    },
    {
        path: '/moviesingle/:id/watchmovie',
        name: 'watchmovie',
        redirect: { name: 'detailescript' },
        component: () => import('./view/watchMovie/WatchMovie.vue'),
        children: [
            {
                path: '',
                name: 'detailescript',
                component: () => import('./view/movieSingle/overview/MovieOverview.vue'),
                props: true,
            },
            {
                path: 'detailreview',
                name: 'detailreview',
                component: () => import('./view/movieSingle/review/MovieReview.vue'),
                props: true,
            },
            {
                path: 'detailmedia',
                name: 'detailmedia',
                component: () => import('./view/movieSingle/media/MovieMedia.vue'),
                props: true,
            },
            {
                path: 'detailreview',
                name: 'detailrelated',
                component: () => import('./view/movieSingle/related/MovieRelated.vue'),
                props: true,
            },
        ],
    },
    // { path: '/:id', name: 'home', component: HomeMovie, props: true },
    {
        path: '/admin/:id',
        name: 'dashboard',
        // redirect: { name: 'accountlist' },
        component: () => import('./layout/home/ContentAdmin.vue'),
        props: true,
        // children: [
        //     {
        //         path: '',
        //         name: 'accountlist',
        //         component: AccountList,
        //     },
        // ],
    },
    {
        path: '/account',
        components: {
            routerAdmin: () => import('./view/account/AccountList.vue'),
        },
    },
    {
        path: '/movieList',
        components: {
            routerAdmin: () => import('./view/movieManager/MovieList.vue'),
        },
    },
    {
        path: '/orderUserList',
        components: {
            routerAdmin: () => import('./view/orderUser/OrderUserList.vue'),
        },
    },
    {
        path: '/overviewAdmin',
        components: {
            routerAdmin: () => import('./view/overviewAdmin/OverviewAdmin.vue'),
        },
    },
    {
        path: '/userprof/:id',
        name: 'userprofile',
        component: () => import('./view/userProfile/UserProfile.vue'),
    },
    {
        path: '/userHistory/:id',
        name: 'userhistory',
        component: () => import('./view/userProfile/UserMovieViewed.vue'),
    },
    {
        path: '/moviegrid/:id',
        name: 'moviegrid',
        component: () => import('./view/movieGrid/MovieGrid.vue'),
    },
    {
        path: '/actorgrid',
        name: 'actorgrid',
        component: () => import('./view/actorGrid/ActorGrid.vue'),
    },
    {
        path: '/actorgrid/:id',
        name: 'actorsingle',
        component: () => import('./view/actorGrid/ActorSingle.vue'),
    },
    {
        path: '/actor',
        components: {
            routerAdmin: () => import('./view/actorManager/ActorList.vue'),
        },
    },
];
// const routes = [
//     { path: '/', component: HomeMovie },
//     {
//         path: '/moviesingle/:id',
//         name: 'MovieSingle',
//         redirect: { name: 'MovieDescript' },
//         component: MovieSingle,
//         children: [
//             {
//                 path: '',
//                 name: 'MovieDescript',
//                 component: MovieOverview,
//                 props: true,
//             },
//             {
//                 path: 'moviereview',
//                 name: 'moviereview',
//                 component: MovieReview,
//                 props: true,
//             },
//             {
//                 path: 'moviemedia',
//                 name: 'moviemedia',
//                 component: MovieMedia,
//                 props: true,
//             },
//             {
//                 path: 'movierelated',
//                 name: 'movierelated',
//                 component: MovieRelated,
//                 props: true,
//             },
//         ],
//     },
//     {
//         path: '/moviesingle/:id/watchmovie',
//         name: 'watchmovie',
//         redirect: { name: 'detailescript' },
//         component: WatchMovie,
//         children: [
//             {
//                 path: '',
//                 name: 'detailescript',
//                 component: MovieOverview,
//                 props: true,
//             },
//             {
//                 path: 'detailreview',
//                 name: 'detailreview',
//                 component: MovieReview,
//                 props: true,
//             },
//             {
//                 path: 'detailreview',
//                 name: 'detailmedia',
//                 component: MovieMedia,
//                 props: true,
//             },
//             {
//                 path: 'detailreview',
//                 name: 'detailrelated',
//                 component: MovieRelated,
//                 props: true,
//             },
//         ],
//     },
//     // { path: '/:id', name: 'home', component: HomeMovie, props: true },
//     {
//         path: '/admin/:id',
//         name: 'dashboard',
//         // redirect: { name: 'accountlist' },
//         component: ContentAdmin,
//         props: true,
//         // children: [
//         //     {
//         //         path: '',
//         //         name: 'accountlist',
//         //         component: AccountList,
//         //     },
//         // ],
//     },
//     {
//         path: '/account',
//         components: {
//             routerAdmin: AccountList,
//         },
//     },
//     {
//         path: '/movieList',
//         components: {
//             routerAdmin: MovieList,
//         },
//     },
//     {
//         path: '/orderUserList',
//         components: {
//             routerAdmin: OrderUserList,
//         },
//     },
//     {
//         path: '/overviewAdmin',
//         components: {
//             routerAdmin: OverviewAdmin,
//         },
//     },
//     {
//         path: '/userprof/:id',
//         name: 'userprofile',
//         component: UserProfile,
//     },
//     {
//         path: '/userHistory/:id',
//         name: 'userhistory',
//         component: UserMovieViewed,
//     },
//     {
//         path: '/moviegrid/:id',
//         name: 'moviegrid',
//         component: MovieGrid,
//     },
//     {
//         path: '/actorgrid',
//         name: 'actorgrid',
//         component: ActorGrid,
//     },
//     {
//         path: '/actorgrid/:id',
//         name: 'actorsingle',
//         component: ActorSingle,
//     },
//     {
//         path: '/actor',
//         components: {
//             routerAdmin: ActorList,
//         },
//     },
// ];

const router = createRouter({
    history: createWebHashHistory(process.env.BASE_URL),
    routes,
});

app.use(ToastPlugin);
app.use(TippyPlugin);
app.use(TippyPlugin, {
    tippyDefaults: {},
});
app.use(VueClickAway);
app.use(VCalendar, {});
app.use(VueLazyLoad, {
    preLoad: 1.3,
    attempt: 1,
    listenEvents: ['scroll'],
});
app.directive('lazyload-youtube', lazyload);
app.use(router).mount('#app');
