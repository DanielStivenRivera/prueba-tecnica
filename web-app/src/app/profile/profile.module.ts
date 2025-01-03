import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ProfileComponent} from './profile.component';
import {ProfileRoutingModule} from './profile-routing.module';
import {MatIconButton} from '@angular/material/button';
import {MatIcon} from '@angular/material/icon';



@NgModule({
  declarations: [
    ProfileComponent
  ],
  imports: [
    CommonModule,
    ProfileRoutingModule,
    MatIconButton,
    MatIcon,
  ]
})
export class ProfileModule { }
