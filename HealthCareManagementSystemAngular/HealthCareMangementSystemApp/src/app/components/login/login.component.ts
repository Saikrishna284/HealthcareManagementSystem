import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  errorMessage: string = '';

  loginForm  = new FormGroup({
  username : new FormControl('', [Validators.required]),
  password : new FormControl('', [Validators.required])

});

constructor(private  _authService: AuthService, private router: Router) { }

Login()
{
   this._authService.login(this.loginForm.value).subscribe({
    next: (response) => {
      //console.log(response);
      localStorage.setItem('token', response.token);
      const userRole = this._authService.getUserRoleFromToken();
      if(userRole === 'ADMIN')
      {
        this.router.navigate(['admin']);
      }
      else
      {
        this.router.navigate(['manage-patients']);
      }
    },
    error: (err) => {
      if (err.status === 401) {
        this.errorMessage = 'Invalid login attempt!';
      } else {
        this.errorMessage = 'An error occurred. Please try again later.';
      }
    }}
  )
}

}
