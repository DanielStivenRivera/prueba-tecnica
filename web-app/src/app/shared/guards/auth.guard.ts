import {
  ActivatedRouteSnapshot,
  CanActivate,Router,
  RouterStateSnapshot
} from '@angular/router';
import {ProfileService} from '../services/profile.service';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private profileService: ProfileService,
    private router: Router
  ) {}

  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean> {
    if (localStorage.getItem('atoken')) {
      return true;
    }

    this.profileService._profile.set(undefined);
    await this.router.navigateByUrl('/home');
    return false;
  }
}

