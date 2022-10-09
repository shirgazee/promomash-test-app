import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Country from './models/country';
import { Observable } from 'rxjs';
import Province from './models/province';
import RegisterUser from './models/register-user';

@Injectable({
  providedIn: 'root',
 })
export class ApiService {
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  getCountries() : Observable<Country[]>  {
    return this.http.get<Country[]>(this.baseUrl + 'country');
  }

  getProvinces(countryId: string) : Observable<Province[]>  {
    return this.http.get<Province[]>(this.baseUrl + `country/${countryId}/provinces`);
  }

  registerUser(model : RegisterUser) : Observable<any> {
    return this.http.post(this.baseUrl + 'user/register', model);
  }
}
