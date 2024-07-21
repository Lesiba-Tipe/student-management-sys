import { Routes, RouterLink} from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AcademicComponent } from './components/academic/academic.component';
import { AdminComponent } from './components/account/admin/admin.component';
import { ParentComponent } from './components/account/parent/parent.component';
import { StudentComponent } from './components/account/student/student.component';
import { ProfileComponent } from './components/account/profile/profile.component';

export const routes: Routes = 
[
    //{ path: '', redirectTo: '/host', pathMatch: 'full' },
    {'path': '', component: LoginComponent},

    {'path': 'login', component: LoginComponent},
    {'path': 'register', component: RegisterComponent},

    {'path': 'profile', component: ProfileComponent},
    {'path': 'admin', component: AdminComponent},
    {'path': 'parent', component: ParentComponent},
    {'path': 'student', component: StudentComponent},

    {'path': 'academic', component: AcademicComponent},
    
    // { path: '**', redirectTo: '/host' } // Wildcard route for a 404 page
];
