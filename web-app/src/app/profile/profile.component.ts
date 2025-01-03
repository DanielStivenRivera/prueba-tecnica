import {ChangeDetectionStrategy, Component, OnInit, signal} from '@angular/core';
import {ProfileService} from '../shared/services/profile.service';
import {Reservation} from '../shared/types/reservation';
import {ReservationService} from '../shared/services/reservation.service';
import {SpaceService} from '../shared/services/space.service';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-profile',
  standalone: false,

  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProfileComponent implements OnInit {

  reservations   = signal<Reservation[]>([]);

  constructor(
    public profileService: ProfileService,
    private reservationService: ReservationService,
    private spaceService: SpaceService,
    private router: Router,
    private toastrService: ToastrService
  ) {
  }

  ngOnInit() {
    this.getProfileData();
    this.getReservations();
  }

  async getProfileData() {
    if(!this.profileService._profile()) {
      await new Promise(resolve => setTimeout(resolve => {
        if (!this.profileService._profile())
          this.router.navigateByUrl('/home');
      }, 300));
    }
  }

  getReservations() {
    this.reservationService.getReservations({userId: this.profileService._profile().id}).subscribe(async (reservations) => {
      for (let res of reservations) {
        res.space = await this.spaceService.getById(res.spaceId);
        res.endDate = new Date(res.endDate);
        res.startDate = new Date(res.startDate);
      }
      this.reservations.set(reservations);
    });
  }


  async deleteReservation(id: number) {
    try {
      await this.reservationService.cancelReservation(id);
      this.getReservations();
      this.toastrService.success('La reservación ha sido cancelada.')
    }catch (e) {
      console.error(e);
    }
  }
}
