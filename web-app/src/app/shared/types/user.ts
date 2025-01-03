import {Reservation} from './reservation';

export interface User {
  id: number;
  username: string;
  email: string;
  createdAt: Date;
  reservations: Reservation[];
}
