import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(
    private http: HttpClient
  ) { }

  public get(url: string): Observable<any> {
    return this.http.get(environment.api + url);
  }

  public post(url: string, params): Observable<any> {
    return this.http.post(environment.api + url, params);
  }

  public put(url: string, params): Observable<any> {
    return this.http.put(environment.api + url, params);
  }
}
