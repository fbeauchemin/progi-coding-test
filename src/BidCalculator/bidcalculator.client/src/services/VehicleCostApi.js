export default {
    async getCostAnalysis(bid) {
        var response = await fetch(`api/vehicle-cost?BasePrice=${bid.basePrice}&Type=${bid.vehicleType}`);
        return await response.json();
    }
}