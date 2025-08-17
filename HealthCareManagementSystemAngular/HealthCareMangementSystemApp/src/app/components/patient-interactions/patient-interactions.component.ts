import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientService } from '../../services/patient.service';
import { PatientInteractionsService } from '../../services/patient-interactions.service';
import { AuthService } from '../../services/auth.service';


@Component({
  selector: 'app-patient-interactions',
  standalone: false,
  templateUrl: './patient-interactions.component.html',
  styleUrl: './patient-interactions.component.css'
})
export class PatientInteractionsComponent implements OnInit{
  patient: any | null = null;
  interactions: any[] = [];
  currentDateTime: string = '';
  userRole: string | null = '';
  userId: string | null = '';
  patientId: number = 0;
  showAddInteraction: boolean = false;
  newInteraction: string = '';
  isEdit: boolean = false;
  editingInteractionId: number | null = null;


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private patientService: PatientService,
    private interactionsService: PatientInteractionsService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const patientId = this.route.snapshot.paramMap.get('patientId');
    this.currentDateTime = new Date().toISOString();
    this.userRole = this.authService.getUserRoleFromToken();
    this.userId = this.authService.getUserIdFromToken();

    if (patientId !== null) {
      this.patientId = parseInt(patientId, 10);
      this.fetchData(this.patientId);
    } else {
      console.error('Patient ID is null');
    }
  }

  fetchData(patientId: number): void {
    Promise.all([
      this.patientService.getPatientById(patientId).toPromise(),
      this.interactionsService.getInteractionsByPatientId(patientId).toPromise()
    ])
    .then(([patient, interactions]) => {
      this.patient = patient;
      this.interactions = interactions;
    })
    .catch(error => console.error('Error fetching data:', error));
  }

  saveForm(): void {
    if (this.isEdit) {
      this.updateInteraction(this.editingInteractionId!);
    } else {
      this.addInteraction();
    }
    this.resetForm();
  }

  addInteraction(): void {
    if (this.newInteraction.trim()) {
      const newInteraction = {
        patientId: this.patientId,
        userId: this.userId,
        description: this.newInteraction,
        date: this.currentDateTime
      };
      this.interactionsService.addInteraction(newInteraction).subscribe(
        () => this.getInteractions(),
        error => console.error('Error adding interaction:', error)
      );
    }
  }

  updateInteraction(interactionId: number): void {

    const interactionToUpdate = this.interactions.find(interaction => interaction.interactionId === interactionId);
    if (interactionToUpdate) {
      interactionToUpdate.description = this.newInteraction;
      this.interactionsService.updatePatientInteraction(interactionToUpdate.interactionId, interactionToUpdate).subscribe(
        () => {
         
          this.getInteractions();
          this.resetForm();
        },
        error => console.error('Error updating interaction:', error)
      );
    }
  }

  resetForm(): void {
    this.newInteraction = '';
    this.showAddInteraction = false;
    this.isEdit = false;
    this.editingInteractionId = null;
  }

  initiateEdit(interaction: any): void {
    this.isEdit = true;
    this.editingInteractionId = interaction.interactionId;
    this.newInteraction = interaction.description;
    this.toggleAddInteraction('edit');
  }

  getInteractions(): void {
    this.interactionsService.getInteractionsByPatientId(this.patientId).subscribe(
      interactions => this.interactions = interactions,
      error => console.error('Error fetching interactions:', error)
    );
  }

  toggleAddInteraction(isAdd: string): void {
    if(isAdd === 'add')
    {
      this.newInteraction = '';
      this.isEdit = false;
    }
    this.showAddInteraction = !this.showAddInteraction;
  }

  goBack(): void {
    this.router.navigate(['/manage-patients']);
  }

}

