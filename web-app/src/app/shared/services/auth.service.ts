import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginForm, RegisterForm} from '../types/dialog';
import {lastValueFrom} from 'rxjs';
import {ProfileService} from './profile.service';
import {Router} from '@angular/router';
import {environment} from '../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly url = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private profileService: ProfileService,
    private router: Router,
  ) { }

  async login(body: LoginForm) {
    const response = await lastValueFrom(this.http.post<{token: string}>(`${this.url}/auth/login`, body));
    await this.saveToken(response.token);
  }

  async logout() {
    localStorage.removeItem('atoken');
    this.profileService._profile.set(undefined);
    await this.router.navigateByUrl('/home');
  }

  async register(body: RegisterForm) {
    const response = await lastValueFrom(this.http.post<{token: string}>(`${this.url}/auth/register`, body));
    await this.saveToken(response.token);
  }

  async saveToken(token: string) {
    localStorage.setItem('atoken', token);
    await this.profileService.getProfileData();
  }

}
