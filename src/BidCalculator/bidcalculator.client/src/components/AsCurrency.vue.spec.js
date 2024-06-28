import { expect, describe, it } from 'vitest';
import { shallowMount } from '@vue/test-utils';
import AsCurrency from './AsCurrency.vue'

describe('AsCurrency component', () => {

    it('defines an "amount" prop', () => {
        var wrapper = shallowMount(AsCurrency, { props: { amount: 10 } });

        expect(wrapper.props().amount).toBe(10);
    });

    it('outputs the value formatted as CAD', () => {
        var wrapper = shallowMount(AsCurrency, { props: { amount: 10 } });

        expect(wrapper.html().replaceAll('\u00A0', ' ')).toBe('10,00 $');
    });

    it('returns currency with at least 2 decimals', () => {
        var wrapper = shallowMount(AsCurrency, { props: { amount: 125.1 } });

        expect(wrapper.html().replaceAll('\u00A0', ' ')).toBe('125,10 $');
    });

    it('returns currency with at most 2 decimals', () => {
        var wrapper = shallowMount(AsCurrency, { props: { amount: 125.12654 } });

        expect(wrapper.html().replaceAll('\u00A0', ' ')).toBe('125,13 $');
    });

    it('updates when amount prop changes', async () => {
        var wrapper = shallowMount(AsCurrency, { props: { amount: 10 } });

        expect(wrapper.html().replaceAll('\u00A0', ' ')).toBe('10,00 $');

        await wrapper.setProps({ amount: 15 });

        expect(wrapper.html().replaceAll('\u00A0', ' ')).toBe('15,00 $');
    });

});
