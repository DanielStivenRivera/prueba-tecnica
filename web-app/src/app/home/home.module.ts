import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import {HomeRoutingModule} from './home-routing.module';
import {CalendarComponent} from '../shared/components/calendar/calendar.component';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    CalendarComponent,
  ]
})
export class HomeModule { }
