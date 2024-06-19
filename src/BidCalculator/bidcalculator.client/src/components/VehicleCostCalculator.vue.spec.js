import { expect, describe, it } from 'vitest';
import { shallowMount } from '@vue/test-utils';
import VehicleCostCalculator from './VehicleCostCalculator.vue'

describe('fetchData method', () => {

    it('doesn\'t call the api when base price it not greater than 0', () => {
        var wrapper = shallowMount(VehicleCostCalculator);

        wrapper.vm.basePrice = 0;
        wrapper.vm.vehicleType = 'Common';
        wrapper.vm.fetchData();

        expect(fetch).toHaveBeenCalledTimes(0);
    });

    it('doesn\'t call the api when vehicle type is null', () => {
        var wrapper = shallowMount(VehicleCostCalculator);

        wrapper.vm.basePrice = 10;
        wrapper.vm.vehicleType = null;
        wrapper.vm.fetchData();

        expect(fetch).toHaveBeenCalledTimes(0);
    });

    it('calls the /api/vehicle-cost endpoint', async () => {
        fetch.mockResponse(JSON.stringify({}));

        var wrapper = shallowMount(VehicleCostCalculator);

        wrapper.vm.basePrice = 10;
        wrapper.vm.vehicleType = 'Common';
        await wrapper.vm.fetchData();

        expect(fetch).toHaveBeenCalledWith('api/vehicle-cost?BasePrice=10&Type=Common');
    });

    it('sets the costAnalysis on successful api call', async () => {
        var costAnalysis = {};
        fetch.mockResponse(JSON.stringify(costAnalysis));

        var wrapper = shallowMount(VehicleCostCalculator);

        wrapper.vm.basePrice = 10;
        wrapper.vm.vehicleType = 'Common';
        await wrapper.vm.fetchData();

        expect(wrapper.vm.costAnalysis).toEqual(costAnalysis);
    });

    it('sets the costAnalysis to null when inputs are invalid', async () => {
        var wrapper = shallowMount(VehicleCostCalculator);

        wrapper.vm.basePrice = 0;
        wrapper.vm.vehicleType = null;
        wrapper.vm.costAnalysis = {};
        await wrapper.vm.fetchData();

        expect(wrapper.vm.costAnalysis).toBeNull();
    });

});

describe('formatCurrency method', () => {

    it('returns the value formatted as CAD', () => {
        var wrapper = shallowMount(VehicleCostCalculator);

        var result = wrapper.vm.formatCurrency(10);

        expect(result.replaceAll('\u00A0', ' ')).toBe('10,00 $');
    });

    it('returns currency with at least 2 decimals', () => {
        var wrapper = shallowMount(VehicleCostCalculator);

        var result = wrapper.vm.formatCurrency(125.1);

        expect(result.replaceAll('\u00A0', ' ')).toBe('125,10 $');
    });

    it('returns currency with at most 2 decimals', () => {
        var wrapper = shallowMount(VehicleCostCalculator);

        var result = wrapper.vm.formatCurrency(125.12654);

        expect(result.replaceAll('\u00A0', ' ')).toBe('125,13 $');
    });

});
