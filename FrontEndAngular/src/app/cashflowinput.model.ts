export class CashFlowInput {
    InitialInvestment: number = 0;
    DiscountRate: number = 0;
    LowerBoundDiscountRate: number = 0;
    UpperBoundDiscountRate: number = 0;
    DiscountRateIncrement: number = 0; 
    CashFlow : number[];

    constructor() {
        this.CashFlow = [0, 0, 0];        
    }
}