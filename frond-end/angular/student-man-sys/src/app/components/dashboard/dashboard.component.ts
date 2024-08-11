import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { IProfile } from '../../data/profile';
import { StudentComponent } from '../account/student/student.component';
import { NgIf } from '@angular/common';
import { NavBarComponent } from "../shared/nav-bar/nav-bar.component";
import { ParentComponent } from "../account/parent/parent.component";
import { ProfileService } from '../../services/profile.service';
import { AdminComponent } from "../account/admin/admin.component";
import { SideBarComponent } from '../shared/side-bar/side-bar.component';


@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink, StudentComponent, NgIf, RouterOutlet, NavBarComponent, 
    ParentComponent, AdminComponent, SideBarComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

  profile : IProfile

  constructor(
    private profileService: ProfileService
  ){
    this.profile = this.profileService.getProfile();
  }

}
