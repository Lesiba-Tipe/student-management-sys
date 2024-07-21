import { Component } from '@angular/core';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { MatFormFieldModule} from '@angular/material/form-field'
import { MatTableModule} from '@angular/material/table'
import { StudentService } from '../../../services/student.service';
import { MatPaginatorModule } from '@angular/material/paginator';
import { Router } from '@angular/router';
@Component({
  selector: 'app-parent',
  standalone: true,
  imports: [DashboardComponent,MatFormFieldModule, MatTableModule,MatPaginatorModule ],
  templateUrl: './parent.component.html',
  styleUrl: './parent.component.css'
})
export class ParentComponent {

  dataSource : any;// = new MatTableDataSource<any>([]);
  addNewUserForm: any;
  user: any;
  clickedRows = new Set<any>();

  constructor(
    private studentService: StudentService,
    private router: Router
  ){}


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

    //Navigate to clicked user
    onRowClick(user: any) {
      // Handle row click event 
      console.log('Row clicked:', user); 
      this.user = user;
  
      const id: any = '/users/' + String(user.id);
      console.log('ROUTE: ',id);
  
      this.router.navigate([id]);
    }
  
    editUser(event: Event, user: any){
      event.stopPropagation();
      if(user){
        this.user = user;
        //Edit user
  
        // this.studentService.edit(user).subscribe(
        //   ()=>{
  
        //     console.log('Edit clicked...',user)
        //   },
        //   ()=>{
            
        //   }
        // )
  
  
      }
    }
  
    deleteUser(event: Event, user: any){
      event.stopPropagation();
      if(user){     
        this.studentService.delete(user.id).subscribe(
          ()=>{
  
          },
          ()=>{
  
          }
        )
      }
  
    }


}
