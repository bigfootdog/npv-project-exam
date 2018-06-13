import { TestBed, inject } from '@angular/core/testing';

import { CashflowApiService } from './cashflow-api.service';

describe('CashflowApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CashflowApiService]
    });
  });

  it('should be created', inject([CashflowApiService], (service: CashflowApiService) => {
    expect(service).toBeTruthy();
  }));
});
