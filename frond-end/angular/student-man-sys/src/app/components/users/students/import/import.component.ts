import { Component } from '@angular/core';
import { CsvService } from '../../../../services/csv.service';
import { NgFor, NgIf } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-import',
  standalone: true,
  imports: [NgIf,MatTableModule],
  templateUrl: './import.component.html',
  styleUrl: './import.component.css'
})
export class ImportComponent {

  students: any[] = [];
  displayedColumns: string[] = ['name', 'academics', 'contacts', 'symbol'];
  clickedRows = new Set<any>();
  constructor(private dataService: CsvService, private router: Router) {}

  ngOnInit() {
    this.dataService.currentData.subscribe(data => this.students = data);
    // this.dataService.currentHead.subscribe(data => this.headers = data);
    console.log(this.students)
  }

  //Navigate to clicked user
  onRowClick(student: any) {
    // Handle row click event 
    console.log('Row clicked:', student); 
    //this.user = student;

    const url: any = '/students/' + String(student.Id);
    //console.log('ROUTE: ',id);

    this.router.navigate([url]);
  }
}
