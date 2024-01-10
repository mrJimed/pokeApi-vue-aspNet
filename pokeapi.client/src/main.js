import './assets/main.css'

import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import { autoAnimatePlugin } from '@formkit/auto-animate/vue'

import Home from './pages/Home.vue'
import Authentication from './pages/Authentication.vue'
import App from './App.vue'

const routes = [
    { path: '/', name: 'Home', component: Home },
    { path: '/authentication', name: 'Authentication', component: Authentication },
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

const app = createApp(App);
app.use(router);
app.use(autoAnimatePlugin);
app.mount('#app');
