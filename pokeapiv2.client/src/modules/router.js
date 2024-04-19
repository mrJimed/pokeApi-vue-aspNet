import { createRouter, createWebHistory } from 'vue-router';
import Home from '../pages/Home.vue'
import Login from '../pages/Login.vue'
import Registration from '../pages/Registration.vue'
import PokemonInfo from '../pages/PokemonInfo.vue'

const routes = [
    { path: '/', name: 'Home', component: Home },
    { path: '/login', name: 'Login', component: Login },
    { path: '/registration', name: 'Registration', component: Registration },
    { path: '/pokemon/:id', name: 'PokemonInfo', component: PokemonInfo }
];

export default createRouter({
    history: createWebHistory(),
    routes
});