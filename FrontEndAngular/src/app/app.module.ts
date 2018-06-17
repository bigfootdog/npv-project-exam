import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http'; //using httpclient

import { AppComponent } from './app.component';
import { CashflowApiService } from './services/cashflow-api.service';
import { HighchartsChartComponent } from './highcharts-chart.component';

@NgModule({
  declarations: [
    AppComponent, HighchartsChartComponent
  ],
  imports: [
    BrowserModule, FormsModule, HttpModule
  ],
  providers: [CashflowApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
