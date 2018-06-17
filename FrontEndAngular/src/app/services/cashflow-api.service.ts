import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import { catchError } from 'rxjs/operators';
import { CashFlowInput } from '../cashflow.model';
import Config from '../config';

@Injectable()
export class CashflowApiService {

  constructor(private http: Http) { }

    getNetPresentValue(entry: CashFlowInput) {
        const promise = new Promise((resolve, reject) => {
        const apiURL = Config.endpoint;
        this.http.post(apiURL, entry)
            .toPromise()      
            .then(  res => { // Success
                    resolve(res.json());
                    })
            .catch(function(error) {
                alert('Error calling getNetPresentValue, please check endpoint assigned: ' + error);
            });
        });
        return promise;
    }
}
