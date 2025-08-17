import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientInteractionsService {

  constructor(private _httpClient: HttpClient) { }

  baseUrl: string = 'http://localhost:5008/api/Interaction';

  addInteraction(data: any): Observable<any>
  {
    return this._httpClient.post(`${this.baseUrl}/AddInteraction`, data);
  }

  getInteractionsByPatientId(id: number): Observable<any>
  {
    return this._httpClient.get<any>(`${this.baseUrl}/GetInteractionsByPatientsId/${id}`);
  }

  updatePatientInteraction(id: number, data: any):  Observable<any>
  {
    return  this._httpClient.put(`${this.baseUrl}/UpdatePatientInteraction/${id}`, data);
  }
}
