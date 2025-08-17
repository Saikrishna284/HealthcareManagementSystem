import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../services/user.service';
import {  NgForm } from '@angular/forms';
import Swal from 'sweetalert2';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-manager-users',
  standalone: false,
  templateUrl: './manager-users.component.html',
  styleUrl: './manager-users.component.css'
})
export class ManagerUsersComponent implements OnInit{
  @ViewChild('userForm')
  userForm!: NgForm;
  page: number = 1;
  Users: any[] = [];
  currentUser: any = this.getEmptyUser();
  isEdit: boolean = false;
  searchText: string = '';
  constructor(private _userService: UserService, private _authService: AuthService) { }
  ngOnInit(): void {
    this.refreshPage();
    this.initializeBootstrapModals();
  }
  
  private initializeBootstrapModals(): void {
    setTimeout(() => {
      try {
        if (typeof window !== 'undefined' && window.document) {
          if (!(window as any).bootstrap) {
            const bootstrapScript = document.createElement('script');
            bootstrapScript.src = 'https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js';
            bootstrapScript.integrity = 'sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4';
            bootstrapScript.crossOrigin = 'anonymous';
            document.body.appendChild(bootstrapScript);
          }
        }
      } catch (error) {
        console.error('Error initializing Bootstrap:', error);
      }
    }, 100);
  }
  deleteUser(id: string) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        Swal.fire({
          title: 'Deleting...',
          allowOutsideClick: false,
          allowEscapeKey: false,
          didOpen: () => {
            Swal.showLoading();
          }
        });
        
        this._userService.deleteUser(id).subscribe({
          next: response => {
            Swal.fire({
              title: "Deleted!",
              text: response.message,
              icon: "success",
              draggable: true
            });
            this.refreshPage();
          },
          error: err => {
            Swal.fire({
              title: "Error!",
              text: err.message || 'Failed to delete user',
              icon: "error",
              draggable: true
            });
          }
        });
      }
    });
  }
  trackByUserId(index: any, user: any): any {
    return user.id;
  }
  resetForm(form?: NgForm): void {
    this.currentUser = this.getEmptyUser();
    this.isEdit = false;
    if (form) {
      form.resetForm();
    }
    this.openModal();
  }
  editUser(user: any): void {
    this.currentUser = { 
      id: user.id,
      username: user.userName,
      email: user.email,
      role: user.roles[0]
    };
    this.isEdit = true;
    this.openModal();
  }
  
  private openModal(): void {
    try {
      const modalElement = document.getElementById('userModal');
      if (modalElement) {
        if ((window as any).bootstrap && (window as any).bootstrap.Modal) {
          const modalInstance = new (window as any).bootstrap.Modal(modalElement);
          modalInstance.show();
        } else {
          console.error('Bootstrap modal functionality is not available');
        }
      } else {
        console.error('Modal element not found in the DOM');
      }
    } catch (error) {
      console.error('Error opening modal:', error);
    }
  }
  saveUser(form?: NgForm): void {
    if (!form || form.valid) {
      if (this.isEdit) {
        Swal.fire({
          title: 'Processing...',
          allowOutsideClick: false,
          allowEscapeKey: false,
          didOpen: () => {
            Swal.showLoading();
          }
        });
        
        const updateUser = {
          id: this.currentUser.id,
          username: this.currentUser.username,
          email: this.currentUser.email,
          role: this.currentUser.role
        };
        this._userService.editUser(this.currentUser.id, updateUser).subscribe({
          next: response => {
            Swal.fire({
              title: "Updated!",
              text: response.message,
              icon: "success",
              draggable: true
            });
            this.refreshPage();
            this.closeModal();
          },
          error: err => {
            Swal.fire({
              title: "Error!",
              text: err.message || 'Failed to update user',
              icon: "error",
              draggable: true
            });
          }
        });
      } else {
        const newUser = {
          username: this.currentUser.username,
          email: this.currentUser.email,
          password: this.currentUser.password,
          role: this.currentUser.role
        };
        
        Swal.fire({
          title: 'Adding...',
          allowOutsideClick: false,
          allowEscapeKey: false,
          didOpen: () => {
            Swal.showLoading();
          }
        });
        
        this._authService.register(newUser).subscribe({
          next: response => {
            Swal.fire({
              title: "Added!",
              text: response.message,
              icon: "success",
              draggable: true
            });
            this.refreshPage();
            this.closeModal();
          },
          error: err => {
            Swal.fire({
              title: "Error!",
              text: err.message || 'Failed to add user',
              icon: "error",
              draggable: true
            });
          }
        });
      }
    }
  }
  private getEmptyUser(): any {
    return {
      id: '',
      username: '',
      email: '',
      password: '',
      role: ''
    };
  }
  private closeModal(): void {
    try {
      const modalElement = document.getElementById('userModal');
      if (modalElement) {
        if ((window as any).bootstrap && (window as any).bootstrap.Modal) {
          const bootstrapModal = (window as any).bootstrap.Modal.getInstance(modalElement);
          if (bootstrapModal) {
            bootstrapModal.hide();
          } else {
            const modalInstance = new (window as any).bootstrap.Modal(modalElement);
            modalInstance.hide();
          }
        } else {
          console.error('Bootstrap modal functionality is not available');
        }
      }
    } catch (error) {
      console.error('Error closing modal:', error);
    }
  }
  refreshPage() {
    this._userService.getAllUsersWithRoles().subscribe(
      (response) => this.Users = response,
      (error) => {
        console.error('Error fetching users:', error);
        Swal.fire({
          title: "Error!",
          text: 'Failed to load users',
          icon: "error"
        });
      }
    );
  }
}
