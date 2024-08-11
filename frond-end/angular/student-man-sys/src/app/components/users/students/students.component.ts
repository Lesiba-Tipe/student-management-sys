import { Component, ElementRef, ViewChild } from '@angular/core';
import { NavBarComponent } from "../../shared/nav-bar/nav-bar.component";
import { StudentsListComponent } from './students-list/students-list.component';
import { CsvService } from '../../../services/csv.service';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { SideBarComponent } from '../../shared/side-bar/side-bar.component';

//image: 'https://mdbootstrap.com/img/new/avatars/7.jpg',


@Component({
  selector: 'app-students',
  standalone: true,
  imports: [NavBarComponent, StudentsListComponent, RouterOutlet, RouterLink, SideBarComponent ],
  templateUrl: './students.component.html',
  styleUrl: './students.component.css'
})
export class StudentsComponent {

  //csvData: any[] = [];
  //headers: string[] = [];
  csvFile: File | null = null;

  @ViewChild('btnCloseModal') closebutton: any;
  
  constructor(
    private csvParser: CsvService,
    private router: Router
  ){}

  onFileSelected(event: any){
    const file: File = event.target.files[0];
    if (file) {
      this.csvFile = file;

      this.csvParser.parseCsv(this.csvFile).then((data) => {
        var headers = data.length ? Object.keys(data[0]) : [];

        this.csvParser.changeData(data);
        this.csvParser.changeHeader(headers);

      }).catch(error => console.error('Error parsing CSV file:', error));

    }
  }

  onSubmit(){
    if(!this.csvFile){
      return
    }
    
    //Transfer data to table

    this.router.navigate(['students/import'])
    if(this.closebutton){
      this.closebutton.nativeElement.click();
    }

  }
}

