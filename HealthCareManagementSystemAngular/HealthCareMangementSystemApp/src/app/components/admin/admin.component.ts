import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { PatientService } from '../../services/patient.service';
import { response } from 'express';

@Component({
  selector: 'app-admin',
  standalone: false,
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {
  constructor(private _userService: UserService, private _patientService: PatientService) { }
  usersCount: number = 0;
  patientsCount: number = 0;
  patients: any[] = [];
  ngOnInit(): void {
   
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this._userService.getAllUsers().subscribe(
      response => this.usersCount = response.length
    )
    this._patientService.getAllPatients().subscribe(
      response => { 
        this.patientsCount = response.length;
        this.patients = response;
      }
    )
    
  }

  


 
}
