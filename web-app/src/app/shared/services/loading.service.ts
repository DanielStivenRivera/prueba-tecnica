import {Injectable, signal} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  constructor() { }

  private _loading = signal<boolean>(false);

  get loading(){
    return this._loading();
  }

  setLoading(state: boolean) {
    this._loading.set(state);
  }

}
