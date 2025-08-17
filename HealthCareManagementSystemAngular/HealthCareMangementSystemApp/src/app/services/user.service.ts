import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private  _httpClient: HttpClient) { }

  baseUrl: string = `http://localhost:5008/api/Users`

  getAllUsers(): Observable<any>
  {
    return this._httpClient.get<any>(`${this.baseUrl}/GetAllUsers`);
  }

  getAllUsersWithRoles(): Observable<any>
  {
    return this._httpClient.get<any>(`${this.baseUrl}/GetAllUsersWithRoles`);
  }

  deleteUser(id: string): Observable<any>
  {
    return this._httpClient.delete(`${this.baseUrl}/DeleteUser/${id}`);
  }

  editUser(id: string, userData: any): Observable<any>
  {
    return this._httpClient.put(`${this.baseUrl}/UpdateUser/${id}`,userData);
  }
}
