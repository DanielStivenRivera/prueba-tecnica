import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Space} from '../types/space';
import {environment} from '../../../environments/environment';
import {lastValueFrom} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpaceService {

  readonly url = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getSpaces() {
    return this.http.get<Space[]>(`${this.url}/spaces`);
  }

  async getById(spaceId: number) {
    return await lastValueFrom(this.http.get<Space>(`${this.url}/spaces/${spaceId}`));
  }
}
