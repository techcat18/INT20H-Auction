import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LotDto } from '../models/lot';

@Injectable({
  providedIn: 'root'
})
export class LotService {

  baseUrl: string = 'http://localhost:5076/api';

  constructor(private http: HttpClient) { }

  getAllLots(): Observable<LotDto[]> {
    return this.http.get<LotDto[]>(`${this.baseUrl}/lots`);
  }
}
