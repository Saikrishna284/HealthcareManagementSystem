import { Component, OnInit, ViewChild } from '@angular/core';
import { PatientService } from '../../services/patient.service';
import Swal from 'sweetalert2';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { jsPDF } from 'jspdf';

@Component({
  selector: 'app-manage-patients',
  standalone: false,
  templateUrl: './manage-patients.component.html',
  styleUrl: './manage-patients.component.css'
})
export class ManagePatientsComponent implements OnInit{
  @ViewChild('patientForm')
  patientForm!: NgForm;
  page: number = 1;
  Patients: any[] = [];
  currentPatient: any = this.getEmptyPatient();
  isEdit: boolean = false;
  searchText: string = '';
  userRole: string | null = '';
  
  

  constructor(private _patientService: PatientService, private router: Router, private _authService: AuthService) { }

  ngOnInit(): void {
    this.refreshPage();
    this.initializeBootstrapModals();
    this.userRole = this._authService.getUserRoleFromToken();
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

  deletePatient(id: number) {
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
        
        this._patientService.deletePatient(id).subscribe({
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
              text: err.message || 'Failed to delete patient',
              icon: "error",
              draggable: true
            });
          }
        });
      }
    });
  }

  trackByUserId(index: any, patient: any): any {
    return patient.patientId;
  }

  resetForm(form?: NgForm): void {
    this.currentPatient = this.getEmptyPatient();
    this.isEdit = false;
    if (form) {
      form.resetForm();
    }
    this.openModal();
  }

  editPatient(patient: any): void {
    this.currentPatient = { 
      id: patient.patientId,
      name: patient.name,
      email: patient.email,
      phone: patient.phone
    };
    this.isEdit = true;
    this.openModal();
  }
  
  private openModal(): void {
    try {
      const modalElement = document.getElementById('patientModal');
      if (modalElement) {
        if ((window as any).bootstrap && (window as any).bootstrap.Modal) {
          const modalInstance = new (window as any).bootstrap.Modal(modalElement);
          
          // Set up event handler to remove aria-hidden attribute
          modalElement.addEventListener('shown.bs.modal', () => {
            modalElement.removeAttribute('aria-hidden');
          }, { once: true });
          
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

  savePatient(form?: NgForm): void {
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
        
        const updatePatient = {
          patientId: this.currentPatient.id,
          name: this.currentPatient.name,
          email: this.currentPatient.email,
          phone: this.currentPatient.phone
        };
        
        this._patientService.updatePatient(this.currentPatient.id, updatePatient).subscribe({
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
              text: err.message || 'Failed to update patient',
              icon: "error",
              draggable: true
            });
          }
        });
      } else {
        const newPatient = {
          name: this.currentPatient.name,
          email: this.currentPatient.email,
          phone: this.currentPatient.phone
        };
        
        Swal.fire({
          title: 'Adding...',
          allowOutsideClick: false,
          allowEscapeKey: false,
          didOpen: () => {
            Swal.showLoading();
          }
        });
        
        this._patientService.addPatient(newPatient).subscribe({
          next: response => {6
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
              text: err.message || 'Failed to add patient',
              icon: "error",
              draggable: true
            });
          }
        });
      }
    }
  }
  patientPdf(patient: any)
  {
     const pdf = new jsPDF();
     pdf.text(`      ${patient.name} 
      ${patient.email}
      ${patient.phone}`, 10, 30);
     pdf.save(`${patient.name}.pdf`);
  }
  private getEmptyPatient(): any {
    return {
      id: '',
      name: '',
      email: '',
      phone: '',
    };8
  }


  private closeModal(): void {
    try {
      const modalElement = document.getElementById('patientModal');
      if (modalElement) {
        if ((window as any).bootstrap && (window as any).bootstrap.Modal) {
          const bootstrapModal = (window as any).bootstrap.Modal.getInstance(modalElement);
          if (bootstrapModal) {
            bootstrapModal.hide();
          } else {
            // Create a new instance if we couldn't get an existing one
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
    this._patientService.getAllPatients().subscribe(
      (response) => this.Patients = response,
      (error) => {
        console.error('Error fetching patients:', error);
        Swal.fire({
          title: "Error!",
          text: 'Failed to load patients',
          icon: "error"
        });
      }
    );
  }

  patientDetails(patientId:string): void
  {
     this.router.navigate(['patient-interactions', patientId])
  }

}
