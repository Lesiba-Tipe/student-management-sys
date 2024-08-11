import { Component } from '@angular/core';
import { StudentService } from '../../../../services/student.service';
import { IStudent } from '../../../../data/student';
import { MatTableModule } from '@angular/material/table';


@Component({
  selector: 'app-students-list',
  standalone: true,
  imports: [MatTableModule],
  templateUrl: './students-list.component.html',
  styleUrl: './students-list.component.css'
})
export class StudentsListComponent {

  
  students: IStudent[] = []

  displayedColumns: string[] = ['name', 'academics', 'contacts', 'symbol'];

  constructor(private studentService: StudentService){
    this.getStudents();
  }

  private getStudents(){

    this.studentService.get().subscribe({
      next: (response) => {
        this.students = response
        console.log(this.students)
      },
      error: (error) => {
        console.error('GET-STUDENTS', error)
      }
    })
  }

}
