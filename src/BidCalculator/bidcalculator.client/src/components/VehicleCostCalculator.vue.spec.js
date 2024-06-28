import { expect, describe, it, beforeEach } from 'vitest';
import { mount } from '@vue/test-utils';
import VehicleCostCalculator from './VehicleCostCalculator.vue'
import DetailedCosts from './DetailedCosts.vue'
import AsCurrency from './AsCurrency.vue';

var createWrapper = function () {
    return mount(VehicleCostCalculator, {
        global: {
            components: { DetailedCosts, AsCurrency }
        }
    });
}

describe('fetchData method', () => {

    beforeEach(() => {
        fetch.resetMocks();
    });

    it('outputs input for base price', () => {
        var wrapper = createWrapper();

        expect(wrapper.get('input[type=number]').exists()).toBe(true);
    });

    it('outputs select for vehicle type', () => {
        var wrapper = createWrapper();

        expect(wrapper.get('select').exists()).toBe(true);
    });

    it('outputs select option for Common vehicle type', () => {
        var wrapper = createWrapper();

        expect(wrapper.get('select option:nth-child(n+2)').text()).toBe('Common');
    });

    it('outputs select option for Luxury vehicle type', () => {
        var wrapper = createWrapper();

        expect(wrapper.get('select option:nth-child(n+3)').text()).toBe('Luxury');
    });

    it('doesn\'t call the api when base price it not greater than 0', () => {
        var wrapper = createWrapper();

        wrapper.vm.basePrice = 0;
        wrapper.vm.vehicleType = 'Common';
        wrapper.vm.fetchData();

        expect(fetch).toHaveBeenCalledTimes(0);
    });

    it('doesn\'t call the api when vehicle type is null', () => {
        var wrapper = createWrapper();

        wrapper.vm.basePrice = 10;
        wrapper.vm.vehicleType = null;
        wrapper.vm.fetchData();

        expect(fetch).toHaveBeenCalledTimes(0);
    });

    it('calls the /api/vehicle-cost endpoint', async () => {
        fetch.mockResponse(JSON.stringify({}));

        var wrapper = createWrapper();

        wrapper.vm.basePrice = 10;
        wrapper.vm.vehicleType = 'Common';
        await wrapper.vm.fetchData();

        expect(fetch).toHaveBeenCalledWith('api/vehicle-cost?BasePrice=10&Type=Common');
    });

    it('sets the costAnalysis on successful api call', async () => {
        var costAnalysis = {};
        fetch.mockResponse(JSON.stringify(costAnalysis));

        var wrapper = createWrapper();

        wrapper.vm.basePrice = 10;
        wrapper.vm.vehicleType = 'Common';
        await wrapper.vm.fetchData();

        expect(wrapper.vm.costAnalysis).toEqual(costAnalysis);
    });

    it('sets the costAnalysis to null when inputs are invalid', async () => {
        var wrapper = createWrapper();

        wrapper.vm.basePrice = 0;
        wrapper.vm.vehicleType = null;
        wrapper.vm.costAnalysis = {};
        await wrapper.vm.fetchData();

        expect(wrapper.vm.costAnalysis).toBeNull();
    });

    it('sets error message when exception occurs', async () => {
        var message = 'an error occured!';
        fetch.mockReject(new Error(message));

        var wrapper = createWrapper();

        wrapper.vm.basePrice = 10;
        wrapper.vm.vehicleType = 'Common';
        wrapper.vm.costAnalysis = {};
        await wrapper.vm.fetchData();

        expect(wrapper.vm.error).contains(message);
    });

});
