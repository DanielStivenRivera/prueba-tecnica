import { ChangeDetectionStrategy, Component } from '@angular/core';
import {CalendarComponent} from '../calendar/calendar.component';
import {Router, RouterOutlet} from '@angular/router';
import {AuthService} from '../../services/auth.service';
import {DialogService} from '../../services/dialog.service';
import {ProfileService} from '../../services/profile.service';
import {MatButtonModule, MatIconButton} from '@angular/material/button';
import {MatIcon, MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {lastValueFrom} from 'rxjs';

@Component({
  selector: 'app-main-layout',
  imports: [
    RouterOutlet,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
  ],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainLayoutComponent {

  constructor(
    private dialogService: DialogService,
    public profileService: ProfileService,
    private authService: AuthService,
    private router: Router
    ) { }

  async auth(type: 'login' | 'register') {
    await lastValueFrom(this.dialogService.openAuthDialog(type));
  }

  async logout() {
    await this.authService.logout();
  }

  async goToProfile() {
    await this.router.navigateByUrl('/profile');
  }

}
