import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';


@Component({
  selector: 'app-sidebar-header',
  standalone: false,
  templateUrl: './sidebar-header.component.html',
  styleUrl: './sidebar-header.component.css'
})
export class SidebarHeaderComponent implements OnInit{
  userRole: string | null = '';
  access: boolean = false;
  constructor(private _authService: AuthService) {}
  
  ngOnInit(): void {
    this.userRole = this._authService.getUserRoleFromToken();
    this.setAccess(); // Call setAccess method to determine access level
  }
  private setAccess(): void {
    switch (this.userRole) {
      case 'ADMIN':
        this.access = true;
        break;
      case 'DOCTOR':
      case 'NURSE':
        this.access = false;
        break;
      default:
        this.access = false; // Default case for any unknown roles
        break;
    }
  }
  logout(): void {
    this._authService.logout();
  }
}
