import { Routes } from '@angular/router';
import {AuthGuard} from './shared/guards/auth.guard';

export const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'profile',
    loadChildren: () => import('./profile/profile.module').then(m=> m.ProfileModule),
    canActivate: [AuthGuard],
  },
  {
    path: '**',
    redirectTo: 'home',
    pathMatch: 'full',
  },
];
