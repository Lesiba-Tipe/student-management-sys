import { Routes,} from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AcademicComponent } from './components/academic/academic.component';
import { ProfileComponent } from './components/account/profile/profile.component';
import { authGuard } from './auth/auth.guard';
import { ForbiddenComponent } from './components/error/forbidden/forbidden.component';
import { EditProfileComponent } from './components/account/profile/edit-profile/edit-profile.component';
import { StudentsComponent } from './components/users/students/students.component';
import { StudentsListComponent } from './components/users/students/students-list/students-list.component';
import { ImportComponent } from './components/users/students/import/import.component';
import { StudentEditComponent } from './components/users/students/student-edit/student-edit.component';

export const routes: Routes = 
[
    //{ path: '', redirectTo: '/host', pathMatch: 'full' },
    {'path': '', component: DashboardComponent,canActivate: [authGuard]},

    {'path': 'login', component: LoginComponent},
    {'path': 'register', component: RegisterComponent},

    {'path': 'dashboard', component: DashboardComponent,canActivate: [authGuard],children: []},

    {'path': 'profile', component: ProfileComponent,
        canActivate: [authGuard],
        children: [
            
        ]
    },
    
    {'path': 'profile/edit', component: EditProfileComponent},

    {'path': 'forbidden', component: ForbiddenComponent},

    {'path': 'students', component: StudentsComponent,canActivateChild: [authGuard], 
        children: [
            {'path': 'students-list', component: StudentsListComponent},
            {'path': 'import', component: ImportComponent},
            {'path': ':id', component: StudentEditComponent},
        ]
    },

    {'path': 'academic', component: AcademicComponent},
    
    //{ path: '**', redirectTo: '/host' } // Wildcard route for a 404 page
];
