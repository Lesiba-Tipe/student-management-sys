import { Component } from '@angular/core';
import {MatListModule} from '@angular/material/list'; 
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-side-bar',
  standalone: true,
  imports: [MatListModule,MatIconModule,MatExpansionModule, NgIf],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.css'
})
export class SideBarComponent {

  
  usersExpanded = false;
  accountExpanded = false;
  isCollapsed = false;

  toggleUsers() {
    this.usersExpanded = !this.usersExpanded;
  }
  toggleSidebar() {
    this.isCollapsed = !this.isCollapsed;
  }
}
