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
            <h2>Detailed Cost</h2>
            <table>
                <thead>
                    <tr>
                        <th>Costs</th>
                        <th class="amount">Amount (CAD)</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Base Price</td>
                        <td class="amount">{{ formatCurrency(basePrice) }}</td>
                    </tr>
                    <tr>
                        <td>Fees</td>
                        <td></td>
                    </tr>
                    <tr v-for="fee in costAnalysis.fees" :key="fee.name">
                        <td class="fee-name">{{ fee.name }}</td>
                        <td class="amount">{{ formatCurrency(fee.amount) }}</td>
                    </tr>
                    <tr>
                        <td>Total</td>
                        <td class="amount">{{ formatCurrency(costAnalysis.total) }}</td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div v-if="error">
            {{ error }}
        </div>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

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
                    var response = await fetch(`api/vehicle-cost?BasePrice=${this.basePrice}&Type=${this.vehicleType}`);
                    this.costAnalysis = await response.json();
                }
                catch (error) {
                    this.error = `Something went wrong: ${error}`;
                }
            },

            formatCurrency(amount) {
                var formatter = new Intl.NumberFormat('fr-CA', {
                    style: 'currency',
                    currency: 'CAD',
                    minimumFractionDigits: 2
                });

                return formatter.format(amount);
            }
        },
    });
</script>

<style scoped>
h1, h1 + p {
    text-align: center;
}

th {
    font-weight: bold;
}
.amount {
    text-align: right;
}

tr:nth-child(even) {
    background: #020202;
}

tr:nth-child(odd) {
    background: #000;
}

tr:last-child td {
    font-weight: bold;
}

th, td {
    padding-left: .5rem;
    padding-right: .5rem;
}


table {
    margin-left: auto;
    margin-right: auto;
    width: 100%;
}

.form {
    margin-top: 3em;
}
.form-input {
    display: flex;
    justify-content: space-between;
}
.fee-name {
    text-align: right;
}

.content {
    margin-top: 2em;
}
</style>