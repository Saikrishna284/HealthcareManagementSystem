import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { ManagerUsersComponent } from './components/manager-users/manager-users.component';
import { ManagePatientsComponent } from './components/manage-patients/manage-patients.component';
import { PatientInteractionsComponent } from './components/patient-interactions/patient-interactions.component';
import { authGuard } from './guards/auth.guard';
import { roleGuard } from './guards/role.guard';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full'},
  { path: 'login', component: LoginComponent },
  { path: 'admin', component: AdminComponent, canActivate: [authGuard, roleGuard], data: { requiredRole: 'ADMIN' }},
  { path: 'manage-users', component: ManagerUsersComponent, canActivate: [authGuard, roleGuard], data: { requiredRole: 'ADMIN' }},
  { path: 'manage-patients', component: ManagePatientsComponent, canActivate: [authGuard]},
  { path: 'patient-interactions/:patientId', component: PatientInteractionsComponent, canActivate: [authGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
