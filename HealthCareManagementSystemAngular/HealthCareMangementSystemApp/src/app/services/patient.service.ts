import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  constructor(private _httpClient: HttpClient) { }

  baseUrl: string = 'http://localhost:5008/api/Patient';

  getAllPatients(): Observable<any>
  {
    return this._httpClient.get<any>(`${this.baseUrl}/GetAllPatients`);
  }
  
  getPatientById(id: number): Observable<any>
  {
    return  this._httpClient.get<any>(`${this.baseUrl}/GetPatientById/${id}`);
  }

  addPatient(newPatient: any): Observable<any>
  {
    return this._httpClient.post(`${this.baseUrl}/AddPatient`, newPatient);
  }

  updatePatient(id: number, data: any): Observable<any>
  {
    return this._httpClient.put(`${this.baseUrl}/UpdatePatient/${id}`, data);
  }

  deletePatient(id: number): Observable<any>
  {
    return this._httpClient.delete(`${this.baseUrl}/DeletePatient/${id}`)
  }
}