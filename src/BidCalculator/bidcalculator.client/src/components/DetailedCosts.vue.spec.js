import { expect, describe, it } from 'vitest';
import { mount } from '@vue/test-utils';
import DetailedCosts from './DetailedCosts.vue'
import AsCurrency from './AsCurrency.vue';


var createWrapper = function(analysis) {
    return mount(DetailedCosts,
        {
            global: {
                components: { AsCurrency }
            },
            props: {
                analysis: analysis
            }
        });
}

describe('DetailedCosts component', () => {

    it('defines an "analysis" prop', () => {
        var analysis = {};
        var wrapper = createWrapper(analysis);

        expect(wrapper.props().analysis).toStrictEqual(analysis);
    });

    it('outputs amount for the base price', () => {
        var analysis = { basePrice: 10 };

        var wrapper = createWrapper(analysis);

        var selector = 'tbody tr:first-child td:last-child';
        var component = wrapper.get(selector).findComponent(AsCurrency);
        expect(component.exists()).toBe(true);
        expect(component.props()).toEqual({ amount: analysis.basePrice });
    });

    it('outputs amount for each fee', () => {
        var analysis = {
            basePrice: 10,
            fees: [
                { name: 'fee1', amount: 1 },
                { name: 'fee2', amount: 2 },
                { name: 'fee3', amount: 3 }
            ]
        };

        var wrapper = createWrapper(analysis);

        var selector = `tbody tr.fee`;
        wrapper.findAll(selector).map((w, i) => {
            var component = w.findComponent(AsCurrency);

            expect(component.exists()).toBe(true);
            expect(component.props()).toEqual({ amount: analysis.fees[i].amount });
        });
    });

    it('outputs amount for the total', () => {
        var analysis = { total: 10 };

        var wrapper = createWrapper(analysis);

        var selector = 'tbody tr:last-child td:last-child';
        var component = wrapper.get(selector).findComponent(AsCurrency);
        expect(component.exists()).toBe(true);
        expect(component.props()).toEqual({ amount: analysis.total });
    });

});
