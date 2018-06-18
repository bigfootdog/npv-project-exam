export class CashFlowInput {
    InitialInvestment: number = 0;
    DiscountRate: number = 0;
    LowerBoundDiscountRate: number = 0;
    UpperBoundDiscountRate: number = 0;
    DiscountRateIncrement: number = 0;
    CashFlow = [];

    constructor() {
        this.CashFlow = [0, 0, 0];        
    }
}

export class CashFlowOutput {
    NetPresentValue: number = 0;
    Discount: number = 0;
}

export class CashFlow {
    Index: number = 0; 
    Amount: number = 0; 
    Discount: number = 0;
}