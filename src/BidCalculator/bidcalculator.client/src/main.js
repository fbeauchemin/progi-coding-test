import AsCurrency from './components/AsCurrency.vue';

import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'

createApp(App)
    .component("AsCurrency", AsCurrency)
    .mount('#app')
