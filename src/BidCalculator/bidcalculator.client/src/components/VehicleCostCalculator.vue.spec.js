import { expect, describe, it, beforeEach, vi } from 'vitest';
import { shallowMount } from '@vue/test-utils';
import VehicleCostCalculator from './VehicleCostCalculator.vue'
import VehicleCostApi from '../services/VehicleCostApi';


var createWrapper = function () {
    return shallowMount(VehicleCostCalculator);
}

vi.mock('../services/VehicleCostApi');

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

        expect(VehicleCostApi.getCostAnalysis).toHaveBeenCalledTimes(0);
    });

    it('doesn\'t call the api when vehicle type is null', () => {
        var wrapper = createWrapper();

        wrapper.vm.basePrice = 10;
        wrapper.vm.vehicleType = null;
        wrapper.vm.fetchData();

        expect(VehicleCostApi.getCostAnalysis).toHaveBeenCalledTimes(0);
    });

    it('calls the VehicleCostApi.getCostAnalysis', async () => {
        var costAnalysis = {};
        VehicleCostApi.getCostAnalysis.mockResolvedValue(costAnalysis);

        var wrapper = createWrapper();

        wrapper.vm.basePrice = 10;
        wrapper.vm.vehicleType = 'Common';
        await wrapper.vm.fetchData();

        expect(VehicleCostApi.getCostAnalysis).toHaveBeenCalledWith(
            expect.objectContaining({
                basePrice: wrapper.vm.basePrice,
                vehicleType: wrapper.vm.vehicleType
            })
        );
    });

    it('sets the costAnalysis on successful api call', async () => {
        var costAnalysis = {};
        VehicleCostApi.getCostAnalysis.mockResolvedValue(costAnalysis);

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
        VehicleCostApi.getCostAnalysis.mockRejectedValue(new Error(message));

        var wrapper = createWrapper();

        wrapper.vm.basePrice = 10;
        wrapper.vm.vehicleType = 'Common';
        wrapper.vm.costAnalysis = {};
        await wrapper.vm.fetchData();

        expect(wrapper.vm.error).contains(message);
    });

});
