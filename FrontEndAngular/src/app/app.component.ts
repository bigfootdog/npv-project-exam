import { Component, OnInit } from '@angular/core';
import { CashflowApiService } from './services/cashflow-api.service';
import { CashFlowInput, CashFlowOutput } from './cashflow.model';
import * as Highcharts from 'highcharts/highstock';
import * as HC_exporting from 'highcharts/modules/exporting';
import Config from './config';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    title = `Net Present Value (NPV) Calculator`;
    cashFlowInput = new CashFlowInput();
    cashFlowOutput = new CashFlowOutput();
    cashFlows = this.cashFlowInput.CashFlow.map(i => ({'value' : i, 'discount' : 0}));
    highcharts = Highcharts;
    optFromInput = {};

    constructor(private cashflowApi: CashflowApiService) {}

    ngOnInit() {
        this.updateInputChart();
    }

    addCashFlow() {
        this.cashFlows.push({value: 0, discount : 0});
    }

    removeCashFlow(term: any) {
        this.cashFlows.splice(term, 1);
    }

    calculateCashFlow() {
        if (!this._isValid()) { return; }
        this.cashFlowInput.CashFlow = this.cashFlows.map(i => Number(i.value.toString().replace(',','')));
        this.cashflowApi.getNetPresentValue(this.cashFlowInput).then((x) => {
            this.cashFlowOutput = <CashFlowOutput> x;
            this.cashFlows = this.cashFlowOutput.CashFlows.map(i => ({'value' : i.Amount, 'discount' : i.Discount }));
            this.updateInputChart();
        });
    }

    reset() {
        this.cashFlowInput = new CashFlowInput();
        this.cashFlows = this.cashFlowInput.CashFlow.map(i => ({'value' : i, 'discount' : 0}));
        this.updateInputChart();
    }

    formatNumber(model: any) {
        const _input =  eval('this.' + model);
        const _result = Number(_input.toString().replace(/,/g , '')).toFixed(2);
        eval(`this.${model} = Number(${_result}).toLocaleString()`);
    }

    handleNumberKey(evt: any) {
        const charCode = (evt.which) ? evt.which : evt.keyCode;
       
        if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode !== 46) {
            return false;
        } else {
            if (charCode === 46 && evt.target.value.indexOf('.') > 0)  {        
                return false;
            } else {
                return true;
            }                         
        }
     }

    updateInputChart() {
        this.optFromInput = {
            title: {
                'text': 'Cashflow Pattern'
            },
            series: [{
            type: 'line',
            name: 'Discount %',
            data: this.cashFlowOutput.CashFlows.map(i => (i.Discount))
            },
            {
            type: 'line',
            name: 'Cashflow amount',
            data: this.cashFlowOutput.CashFlows.map(i => (i.Amount))
            }]
            }
    };

    _isValid() {
        if (this.cashFlowInput.LowerBoundDiscountRate > 100 || 
                this.cashFlowInput.UpperBoundDiscountRate > 100 ||
                this.cashFlowInput.DiscountRateIncrement > 100) {
            return false;
        } else {
            return true;
        }
    }

}