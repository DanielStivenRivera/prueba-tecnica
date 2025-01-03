import {User} from './user';
import {Space} from './space';

export interface Reservation {
  id: number;
  userId: string;
  spaceId: number;
  startDate: Date;
  endDate: Date;
  user: User;
  space: Space;
}

export interface ReservationParams {
  userId?: number;
  spaceId?: number;
  startDate?: Date;
  endDate?: Date;
  fetchPlaces?: boolean;
}


export interface CreateReservation {
  userId: number;
  spaceId: number;
  startDate: Date;
  endDate: Date;
}
