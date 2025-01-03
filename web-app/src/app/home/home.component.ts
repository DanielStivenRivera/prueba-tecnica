import {ChangeDetectionStrategy, Component, OnInit, signal} from '@angular/core';
import {Space} from '../shared/types/space';
import {Reservation, ReservationParams} from '../shared/types/reservation';
import {ReservationService} from '../shared/services/reservation.service';
import {SpaceService} from '../shared/services/space.service';
import {DialogService} from '../shared/services/dialog.service';
import {lastValueFrom} from 'rxjs';
import {ProfileService} from '../shared/services/profile.service';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent implements OnInit {

  spaces = signal<Space[]>([]);


  constructor(
    private reservationService: ReservationService,
    private spaceService: SpaceService,
    private dialogService: DialogService,
    public profileService: ProfileService,
  ) {}

  ngOnInit() {
    this.getSpaces();
  }

  getSpaces() {
    this.spaceService.getSpaces().subscribe((spaces: Space[]) => {
      this.spaces.set(spaces);
    });
  }

  getReservations(params: ReservationParams) {
    return this.reservationService.getReservations(params);
  }

  async createReservation() {
    try {
      const resp = await lastValueFrom(this.dialogService.openReservationModal());
      if (resp === 'success') {
        this.getSpaces();
      }
    } catch (e) {
      console.error(e);
    }
  }

}
