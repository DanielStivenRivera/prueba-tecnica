import {Injectable, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {User} from '../types/user';
import * as jwt_decode from 'jwt-decode';
import {lastValueFrom} from 'rxjs';
import {TokenPayload} from '../types/token';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  readonly apiUrl = environment.apiUrl;

  _profile = signal<User | undefined>(undefined);
  constructor(
    private http: HttpClient,
  ) { }

  async getProfileData() {
    try {
      const token = localStorage.getItem('atoken');
      console.log('init profile data');
      if (!token) return;
      console.log('init profile data');
      const payload = jwt_decode.jwtDecode<TokenPayload>(token);
      const user = await lastValueFrom(this.http.get<User>(`${this.apiUrl}/users/${payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']}`));
      this._profile.set(user);
    } catch (e) {
      localStorage.removeItem('atoken');
    }
  }

}
