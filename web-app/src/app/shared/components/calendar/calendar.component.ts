import {
  ChangeDetectionStrategy,
  Component, effect,
  input, OnInit,
  signal
} from '@angular/core';
import {CommonModule} from '@angular/common';
import {Space} from '../../types/space';
import {Reservation} from '../../types/reservation';
import {ReservationService} from '../../services/reservation.service';

@Component({
  selector: 'app-calendar',
  imports: [CommonModule],
  templateUrl: './calendar.component.html',
  styleUrl: './calendar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class CalendarComponent implements OnInit {

  actualWeek =  signal<number>(0);

  hours = signal(Array.from({length: 14}, (_, i) => i + 8));

  daysOfWeek = signal(['Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado', 'Domingo']);

  space = input<Space | undefined>(undefined);

  reservations = signal<Reservation[]>([]);


  constructor(
    private reservationsService: ReservationService
  ) {
    effect(() => {
      this.forceRenderer();
    });
  }

  public ngOnInit(): void {
    this.fetchData();
  }

  private forceRenderer() {
    this.hours.set(Array());
    this.daysOfWeek.set(Array());
    setTimeout(() => {
      this.hours.set(Array.from({length: 14}, (_, i) => i + 8));
      this.daysOfWeek.set(['Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado', 'Domingo']);
    }, 100);
  }

  fetchData() {
    const startDate = this.actualWeek() === 0 ? new Date() : this.getDate('Lunes');
    const finalDate = this.getDate('Domingo');
    this.reservationsService.getReservations({spaceId: this.space().id, startDate: startDate, endDate: finalDate}).subscribe(reservations => {
      this.reservations.set(reservations);
    });
  }

  getDate(weekDay: string): Date {
    const today = new Date();
    const firstDay = new Date(today);
    firstDay.setDate(today.getDate() - today.getDay() + 1 + this.actualWeek() * 7);
    const date = new Date(firstDay);
    date.setDate(firstDay.getDate() + this.daysOfWeek().indexOf(weekDay));
    return date;
  }

  formatDate(date: Date): string {
    return date.toLocaleDateString('es-ES', {day: 'numeric', month: 'short'});
  }

  public nextWeek() {
    this.actualWeek.update((week) => week + 1);
    this.fetchData();
  }

  previousWeek() {
    if (this.actualWeek() > 0) {
      this.actualWeek.update((week) => week - 1);
    }
    this.fetchData();
  }

  existReservation(weekDay: string, hour: number): boolean {
    if (this.reservations().length === 0) {
      return false;
    }
    const date = this.getDate(weekDay);
    date.setHours(hour,0,0,0);
    return this.reservations().some(reservation => {
      const start = new Date(reservation.startDate);
      const end = new Date(reservation.endDate);
      return date.getTime() >= start.getTime() && date.getTime()<= end.getTime();
    });
  }

  dateIsInPast(weekDay: string): boolean {
    return this.getDate(weekDay) < new Date();
  }

  hourIsInPast(day: string, hour: number): boolean {
    const hourAndDate = this.getDate(day);
    hourAndDate.setHours(hour, 0, 0, 0);
    return hourAndDate < new Date();
  }

}
