import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BidDto } from 'src/app/models/lot';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BidService {

  constructor(private http: HttpClient) { }

  createBid(bid: BidDto){
    return this.http.post(`${environment.apiUrl}lots/${bid.lotId}/bids`, bid);
  }
}
