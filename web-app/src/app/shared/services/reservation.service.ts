import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {CreateReservation, Reservation, ReservationParams} from '../types/reservation';
import {lastValueFrom} from 'rxjs';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  private readonly url = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getReservations(queryParams?: ReservationParams) {
    let params = new HttpParams();
    if (queryParams?.spaceId) {
      params = params.set('spaceId', queryParams.spaceId);
    }
    if (queryParams?.userId) {
      params = params.set('userId', queryParams.userId);
    }
    if (queryParams?.startDate) {
      params = params.set('startDate', queryParams.startDate.toISOString());
    }
    if (queryParams?.endDate) {
      params = params.set('endDate', queryParams.endDate.toISOString());
    }
    if (queryParams?.fetchPlaces === true) {
      params = params.set('fetchPlaces', queryParams.fetchPlaces);
    }
    return this.http.get<Reservation[]>(`${this.url}/reservations`, {params});
  }

  async cancelReservation(id: number) {
    return lastValueFrom(this.http.delete(`${this.url}/reservations/${id}`));
  }

  async createReservation(body: CreateReservation) {
    return await lastValueFrom(this.http.post(`${this.url}/reservations`, body));
  }

}
