import { expect, describe, it, beforeEach } from 'vitest';
import VehicleCostApi from './VehicleCostApi';

describe('fetchData method', () => {

    beforeEach(() => {
        fetch.resetMocks();
    });

    it('calls the /api/vehicle-cost endpoint', async () => {
        fetch.mockResponse(JSON.stringify({}));

        await VehicleCostApi.getCostAnalysis({ basePrice: 10, vehicleType: 'Common' });

        expect(fetch).toHaveBeenCalledWith('api/vehicle-cost?BasePrice=10&Type=Common');
    });

});
