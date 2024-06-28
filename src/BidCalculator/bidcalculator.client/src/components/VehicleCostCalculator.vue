<template>
    <div>
        <h1>Vehicle Cost Calculator</h1>
        <p>Calculate the total price of a vehicle at a car auction.</p>

        <div class="form">
            <div class="form-input">
                <span>Base Price</span>
                <input type="number" v-model.lazy="basePrice" />
            </div>
            <div class="form-input">
                <span>Type</span>
                <select v-model="vehicleType">
                    <option disabled value="">Please select one</option>
                    <option>Common</option>
                    <option>Luxury</option>
                </select>
            </div>
        </div>

        <div v-if="costAnalysis" class="content">
            <DetailedCosts :analysis="costAnalysis" />
        </div>

        <div v-if="error">
            {{ error }}
        </div>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';
    import DetailedCosts from './DetailedCosts.vue';
    import vehicleCostApi from '../services/VehicleCostApi';

    export default defineComponent({
        data() {
            return {
                costAnalysis: null,
                basePrice: null,
                vehicleType: null,
                error: null
            };
        },
        watch: {
            basePrice: 'fetchData',
            vehicleType: 'fetchData'
        },
        methods: {
            async fetchData() {
                if (this.basePrice <= 0 || !this.vehicleType) {
                    this.costAnalysis = null;
                    return;
                }

                try {
                    this.error = null;
                    this.costAnalysis = await vehicleCostApi.getCostAnalysis({ basePrice: this.basePrice, vehicleType: this.vehicleType });
                }
                catch (error) {
                    this.error = `Something went wrong: ${error}`;
                }
            }
        },
        components: {
            DetailedCosts
        }
    });
</script>

<style scoped>
h1, h1 + p {
    text-align: center;
}

.form {
    margin-top: 3em;
}
.form-input {
    display: flex;
    justify-content: space-between;
}

.content {
    margin-top: 2em;
}
</style>