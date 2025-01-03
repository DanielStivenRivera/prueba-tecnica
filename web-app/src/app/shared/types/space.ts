import {Reservation} from './reservation';

export interface Space {
  id: number;
  name: string;
  capacity: number;
  reservations: Reservation[];
}
