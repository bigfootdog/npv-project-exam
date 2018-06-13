import { Component, OnInit } from '@angular/core';
import { CashflowApiService } from './services/cashflow-api.service';
import { CashFlowInput } from './cashflowinput.model';
import Config from './config';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    title = `Net Present Value (NPV) Calculator`;
    cashFlowInput = new CashFlowInput();
    netPresentValue = 0;
    cashFlows = this.cashFlowInput.CashFlow.map(i => ({'value' : i}));
  
    constructor(private cashflowApi: CashflowApiService) {}

    ngOnInit() {      
    }
   
    addCashFlow() {
        this.cashFlows.push({value: 0});
    }

    removeCashFlow(term: any){
        this.cashFlows.splice(term, 1);        
    }

    calculateCashFlow() {
        this.cashFlowInput.CashFlow = this.cashFlows.map(i => Number(i.value.toString().replace(",","")));
        this.cashflowApi.getNetPresentValue(this.cashFlowInput).then((x) => {
            this.netPresentValue = Number(x);
        });
    }

    reset() {
        this.cashFlowInput = new CashFlowInput();
        this.cashFlows = this.cashFlowInput.CashFlow.map(i => ({'value' : i}));    
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