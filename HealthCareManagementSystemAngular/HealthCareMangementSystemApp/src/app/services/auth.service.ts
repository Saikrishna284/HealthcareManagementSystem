import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl: string  = 'http://localhost:5008/api/Auth';
  private isBrowser: boolean;
  jwtHelper = new JwtHelperService();

  constructor(@Inject(PLATFORM_ID) platformId: Object, private router : Router, private _httpClient: HttpClient ) 
  { 
    this.isBrowser = isPlatformBrowser(platformId);
  }


  login(userCredentials: any): Observable<any>
  {
    return this._httpClient.post<any>(`${this.baseUrl}/login`,userCredentials);
  }

  register(userData: any): Observable<any>
  {
    return this._httpClient.post<any>(`${this.baseUrl}/register`,userData);
  }
 
  isLoggedIn(): boolean {
      const token = this.getToken();
      return token != null && !this.jwtHelper.isTokenExpired(token);
    }

    logout(): void {
      if (this.isBrowser) {
        localStorage.removeItem('token');
        this.router.navigate(['/login']);
      };
    }

    getToken(): string | null {
      if(!this.isBrowser)
      {
        return null;
      }
    return localStorage.getItem('token');
    
    }

    getDecodedToken(): any | null {
      const token = this.getToken();
      if (token) {
        // return jwtDecode(token);
      return this.jwtHelper.decodeToken(token)
      }
      return null;
    }
  
    getUserIdFromToken(): string | null {
      const decoded = this.getDecodedToken();
      const userId = decoded?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
      return userId ? userId : null;
    }
  
  
    getUserRoleFromToken(): string | null {
      const decoded = this.getDecodedToken();
      return decoded?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null;
    }

 
 


}
