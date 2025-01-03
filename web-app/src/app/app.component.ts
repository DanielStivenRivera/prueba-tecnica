import {Component, OnInit} from '@angular/core';
import {MainLayoutComponent} from './shared/components/main-layout/main-layout.component';

import {LoadingService} from './shared/services/loading.service';
import {MatProgressSpinner} from '@angular/material/progress-spinner';
import {ProfileService} from './shared/services/profile.service';

@Component({
  selector: 'app-root',
  imports: [MainLayoutComponent, MatProgressSpinner],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {

  constructor(
    public loadingService: LoadingService,
    private profileService: ProfileService,
  ) {
  }

  async ngOnInit() {
    console.log('init app')
    await this.profileService.getProfileData();
  }

}
