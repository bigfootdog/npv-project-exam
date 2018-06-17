import { Component, OnInit } from '@angular/core';
import { CashflowApiService } from './services/cashflow-api.service';
import { CashFlowInput, CashFlowOutput } from './cashflowinput.model';
import Config from './config';

import * as Highcharts from 'highcharts/highstock';
import * as HC_exporting from 'highcharts/modules/exporting';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    title = `Net Present Value (NPV) Calculator`;
    cashFlowInput = new CashFlowInput();
    netPresentValue = 0;
    cashFlows = this.cashFlowInput.CashFlow.map(i => ({'value' : i, 'discount' : 0}));
  
    Highcharts = Highcharts;

    // Demo #1
    // optFromInputString = `
    // {
    //   "subtitle": { "text": "Highcharts chart" },
    //   "series": [{
    //     "type": "line",
    //     "data": [11,2,3]
    //   }, {
    //     "data": [5,6,7]
    //   }]
    // }
    // `;

//     optFromInput1 =  {
//       "subtitle": { "text": "Highcharts chart" },
//       "series": [{
//         "type": "line",
//         "data": [11,2,3]
//       }, {
//         "data": [5,6,7]
//       }]
//     }
// ;

  optFromInput = {};//JSON.parse(this.optFromInputString);
  updateFromInput = false;

  updateInputChart = ()=> {
  //    var t = this.cashflowinput.
    this.optFromInput = {
        "subtitle": { "text": "Highcharts chart" },
        "series": [{
          "type": "line",
          "data": this.cashFlows.map(i => Number(i.value))
        }]
      }
    console.log(this.cashFlows.map(i => Number(i.value)));
    //= JSON.parse(this.optFromInputString);
  };

    constructor(private cashflowApi: CashflowApiService) {}

    ngOnInit() {
        this.updateInputChart();
    }
   
    addCashFlow() {
        this.cashFlows.push({value: 0, discount : 0});
    }

    removeCashFlow(term: any){
        this.cashFlows.splice(term, 1);        
    }

    calculateCashFlow() {
        this.updateInputChart();
        return;
        this.cashFlowInput.CashFlow = this.cashFlows.map(i => Number(i.value.toString().replace(",","")));
        this.cashflowApi.getNetPresentValue(this.cashFlowInput).then((x) => {
         var t = <CashFlowOutput> x; 
          
           this.netPresentValue = t.NetPresentValue;// Number(x);

            this.cashFlows = t.CashFlows.map(i => ({'value' : i.Amount, 'discount' : i.Discount }));
            console.log(x);
        });
    }

    reset() {
        this.cashFlowInput = new CashFlowInput();
        this.cashFlows = this.cashFlowInput.CashFlow.map(i => ({'value' : i, 'discount' : 0}));    
    }

    formatNumber(model: any) {
        const _input =  eval('this.' + model);
        let _result = Number(_input.toString().replace(/,/g , '')).toFixed(2);
        eval(`this.${model} = Number(${_result}).toLocaleString()`);
    }

    handleNumberKey(evt: any) {
        const charCode = (evt.which) ? evt.which : evt.keyCode;
       
        if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
            return false;
        } else {
            if (charCode == 46 && evt.target.value.indexOf('.') > 0)  {        
                return false;
            } else {
                return true;
            }                         
        }
     }
}