import { Component } from '@angular/core';
import { DashboardComponent } from '../../dashboard/dashboard.component';

@Component({
  selector: 'app-student',
  standalone: true,
  imports: [DashboardComponent],
  templateUrl: './student.component.html',
  styleUrl: './student.component.css'
})


export class StudentComponent {

  user =
  {
    grade : 10
  }
  subjects = 
  [
    "Mathematics",
    "English",
    "Accounting",
    "Physical Sciences",
    "Life Sciences",
    "Geography"
  ]


}
