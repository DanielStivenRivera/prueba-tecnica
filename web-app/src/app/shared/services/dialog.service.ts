import { Injectable } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {AuthDialogComponent} from '../components/auth-dialog/auth-dialog.component';
import {AuthDialogData} from '../types/dialog';
import {Observable} from 'rxjs';
import {ReservationModalComponent} from '../components/reservation-modal/reservation-modal.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(
    private matDialog: MatDialog,
  ) { }

  openAuthDialog(type: 'login' | 'register'): Observable<'cancel' | 'success'> {
    const dialog = this.matDialog.open(AuthDialogComponent, {
      data: <AuthDialogData>{
        type: type,
      },
      width: '50%',
      height: 'auto',
      maxWidth: '500px',
      panelClass: 'custom-dialog-panel'
    });
    return dialog.afterClosed();
  }

  openReservationModal() {
    const dialog = this.matDialog.open(ReservationModalComponent, {
      width: '50%',
      height: 'auto',
      maxWidth: '500px',
      panelClass: 'custom-dialog-panel'
    });
    return dialog.afterClosed();
  }

}
