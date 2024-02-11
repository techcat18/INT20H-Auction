import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LotDto } from 'src/app/models/lot';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LotService {

  constructor(private http: HttpClient) { }

  getAllLots(): Observable<LotDto[]> {
    return this.http.get<LotDto[]>(`${environment.apiUrl}lots`);
  }

  getLotById(id: string): Observable<LotDto>{
    return this.http.get<LotDto>(`${environment.apiUrl}lots/${id}`)
  }
}
