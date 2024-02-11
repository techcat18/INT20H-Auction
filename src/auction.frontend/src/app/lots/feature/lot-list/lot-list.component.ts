import { Component, OnInit } from '@angular/core';
import { LotDto } from 'src/app/models/lot';
import { LotService } from '../../data-access/lot.service';

@Component({
  selector: 'app-lot-list',
  templateUrl: './lot-list.component.html',
  styleUrls: ['./lot-list.component.scss']
})
export class LotListComponent implements OnInit {

  lots: LotDto[] | undefined;

  constructor(private lotService: LotService) { }

  ngOnInit(): void {
    this.lotService.getAllLots().subscribe(lots => {
      this.lots = lots;
    });
  }
}