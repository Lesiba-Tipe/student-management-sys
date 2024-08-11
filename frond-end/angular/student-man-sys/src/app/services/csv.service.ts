import { Injectable } from '@angular/core';
import * as Papa from 'papaparse';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CsvService {

  private dataSource = new BehaviorSubject<any>(null);
  currentData = this.dataSource.asObservable();

  private headerSource = new BehaviorSubject<any>(null);
  currentHead = this.headerSource.asObservable();

  constructor() {}

  changeData(data: any) {
    this.dataSource.next(data);
  }

  changeHeader(data: any) {
    this.headerSource.next(data);
  }

  parseCsv(file: File): Promise<any[]> {
    return new Promise((resolve, reject) => {
      Papa.parse(file, {
        header: true,
        complete: (result: any) => {
          resolve(result.data);
        },
        error: (error: any) => {
          reject(error);
        }
      });
    });
  }

  
}
