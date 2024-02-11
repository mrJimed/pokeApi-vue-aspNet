import './assets/main.css'

import { createApp } from 'vue';
import router from './modules/router';
import store from './modules/store.js';
import App from './App.vue';

const app = createApp(App);
app.use(router);
app.use(store);
app.mount('#app');
