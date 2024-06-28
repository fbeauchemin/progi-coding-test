import AsCurrency from './components/AsCurrency.vue';
import DetailedCosts from './components/DetailedCosts.vue';

import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'

createApp(App)
    .component("AsCurrency", AsCurrency)
    .component("DetailedCosts", DetailedCosts)
    .mount('#app')
