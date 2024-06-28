<template>
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
                <td class="amount"><AsCurrency :amount="analysis.basePrice" /></td>
            </tr>
            <tr>
                <td>Fees</td>
                <td></td>
            </tr>
            <tr class="fee" v-for="fee in analysis.fees" :key="fee.name">
                <td class="fee-name">{{ fee.name }}</td>
                <td class="amount"><AsCurrency :amount="fee.amount" /></td>
            </tr>
            <tr>
                <td>Total</td>
                <td class="amount"><AsCurrency :amount="analysis.total" /></td>
            </tr>
        </tbody>
    </table>
</template>

<script lang="js" setup>
    defineProps({
        analysis: Object
    })
</script>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
    });
</script>

<style scoped>
    table {
        margin-left: auto;
        margin-right: auto;
        width: 100%;
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

    .fee-name {
        text-align: right;
    }

</style>