import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { SidebarHeaderComponent } from './components/sidebar-header/sidebar-header.component';

import { tokenInterceptor } from './services/token.interceptor';
import { FilterPipe } from './pipes/filter.pipe';
import { NgxPaginationModule } from 'ngx-pagination';
import { ManagerUsersComponent } from './components/manager-users/manager-users.component';
import { ManagePatientsComponent } from './components/manage-patients/manage-patients.component';
import { PatientInteractionsComponent } from './components/patient-interactions/patient-interactions.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdminComponent,
    SidebarHeaderComponent,
    FilterPipe,
    ManagerUsersComponent,
    ManagePatientsComponent,
    PatientInteractionsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    NgxPaginationModule
  ],
  providers: [
    //provideClientHydration(withEventReplay())
    provideHttpClient(withInterceptors([tokenInterceptor]))
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
