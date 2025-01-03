import {ChangeDetectionStrategy, Component, OnInit, signal} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import { MatFormFieldModule, MatLabel} from '@angular/material/form-field';
import { MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {SpaceService} from '../../services/space.service';
import {Space} from '../../types/space';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {provideNativeDateAdapter} from '@angular/material/core';
import {MatTimepickerModule} from '@angular/material/timepicker';
import {MatDialogRef} from '@angular/material/dialog';
import {ToastrService} from 'ngx-toastr';
import {LoadingService} from '../../services/loading.service';
import {ReservationService} from '../../services/reservation.service';
import {CreateReservation} from '../../types/reservation';
import {ProfileService} from '../../services/profile.service';

@Component({
  selector: 'app-reservation-modal',
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatDatepickerModule,
    MatTimepickerModule,
  ],
  templateUrl: './reservation-modal.component.html',
  styleUrl: './reservation-modal.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [provideNativeDateAdapter()],
})
export class ReservationModalComponent implements OnInit {

  minDate = new Date();

  reservationForm: FormGroup;

  spaces = signal<Space[]>([])

  constructor(
    private spaceService: SpaceService,
    private dialogRef: MatDialogRef<ReservationModalComponent>,
    private toastrService: ToastrService,
    private loadingService: LoadingService,
    private reservationService: ReservationService,
    private profileService: ProfileService,
  ) {
  }

  ngOnInit() {
    this.dialogRef.disableClose = true;
    this.getSpaces();
    this.buildForm();

  }

  getSpaces() {
    try {
      this.spaceService.getSpaces().subscribe(spaces => {
        this.spaces.set(spaces);
      });
    } catch (e) {
      console.error(e);
    }
  }

  buildForm() {
    this.reservationForm = new FormGroup({
      place: new FormControl('', [Validators.required]),
      date: new FormControl('', [Validators.required]),
      startHour: new FormControl('', [Validators.required]),
      endHour: new FormControl('', [Validators.required]),
    });
  }


  async save() {
    console.log(this.reservationForm.value)
    if (this.reservationForm.invalid) {
      this.toastrService.info('Complete los campos requeridos.');
      return;
    }
    if (!this.validateInterval()) return;

    if (!this.validateMinAndMax()) return;

    this.loadingService.setLoading(true);
    try {
      const resp = await this.reservationService.createReservation(this.getBody());
      this.toastrService.success('La reserva fue creada correctamente.');
      this.dialogRef.close('success');
    } catch (e) {
      let msg = 'Ha ocurrido un error inesperado.';
      if (e.error && e.error.message === 'Reservation duration must be between 2 and 5 hours.') {
        msg = 'La reservación debe ser entre 2 y 5 horas.'
      } else if (e.error && e.error.message === 'User cant have more than one reservation at the same time') {
        msg = 'El usuario ya tiene una reservación para esta fecha y hora';
      } else if (e.error && e.error.message === 'The space is already reserved during this time.') {
        msg = 'ya existe una reservación para este espacio.'
      } else if (e.error && e.error.message === 'The user already has a reservation during this time.') {
        msg = 'El usuario ya tiene una reservación para esta fecha y hora';
      }
      this.toastrService.error(msg);
      console.error(e);
    }
    this.loadingService.setLoading(false);
  }

  validateMinAndMax(): boolean {
    if ((this.reservationForm.get('startHour').value as Date).getHours() < 8) {
      this.toastrService.error('La hora de inicio debe ser mayor o igual a 8:00 AM');
      return false;
    }
    else if ((this.reservationForm.get('endHour').value as Date).getHours() > 21) {
      this.toastrService.error('La hora final no puede ser mayor a 9:00 PM');
      return false;
    }
    return true;
  }

  getBody(): CreateReservation {
    const startDate: Date = new Date(this.reservationForm.get('date').value);
    startDate.setHours((this.reservationForm.get('startHour').value as Date).getHours());
    const endDate: Date = new Date(this.reservationForm.get('date').value);
    endDate.setHours((this.reservationForm.get('endHour').value as Date).getHours());
    return {
      endDate: endDate,
      startDate: startDate,
      spaceId: this.reservationForm.get('place').value,
      userId: this.profileService._profile()!.id,

    }
  }

  validateInterval(): boolean {
    const startDate: Date = this.reservationForm.get('startHour').value;
    const endDate : Date = this.reservationForm.get('endHour').value;

    const minTime = new Date(startDate);
    minTime.setHours(minTime.getHours() + 2);

    const maxTime = new Date(startDate);
    maxTime.setHours(maxTime.getHours() + 5);

    if (startDate.getTime() > endDate.getTime()) {
      this.toastrService.error('La hora de fin debe ser mayor que la hora de inicio.')
      return false;
    } else if (minTime.getTime() > endDate.getTime()) {
      this.toastrService.error('El tiempo mínimo de reserva es de 2 horas.');
      return false;
    } else if (maxTime.getTime() < endDate.getTime()) {
      this.toastrService.error('El tiempo máximo de reserva es de 5 horas.');
      return false;
    }


    return true;
  }

  async cancel() {
    this.dialogRef.close('cancel');
  }

}
