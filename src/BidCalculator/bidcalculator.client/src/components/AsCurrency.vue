<template>
    {{ formatCurrency }}
</template>

<script lang="js" setup>
    defineProps({
        amount: Number
    })
</script>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        computed: {
            formatCurrency() {
                var formatter = new Intl.NumberFormat('fr-CA', {
                    style: 'currency',
                    currency: 'CAD',
                    minimumFractionDigits: 2
                });

                return formatter.format(this.amount);
            }
        }
    });
</script>