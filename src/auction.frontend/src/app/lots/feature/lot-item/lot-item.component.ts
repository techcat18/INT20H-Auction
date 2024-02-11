import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LotDto } from 'src/app/models/lot';
import { LotService } from '../../data-access/lot.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CreateBidDialogComponent } from '../../utils/create-bid-dialog/create-bid-dialog.component';

@Component({
  selector: 'app-lot-item',
  templateUrl: './lot-item.component.html',
  styleUrls: ['./lot-item.component.scss']
})
export class LotItemComponent implements OnInit {

  lot: LotDto | undefined;

  constructor(
    private route: ActivatedRoute,
    private lotService: LotService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.route.params.subscribe(p => {
      this.lotService.getLotById(p['id']).subscribe(
        (x) => {
          console.log(x);
          this.lot = x;
        }
      )
    })
  }

  createBid(){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '400px';
    dialogConfig.data = {lotId: this.lot?.id};

    const dialogRef = this.dialog.open(CreateBidDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(x => {
      if (x){
        this.lotService.getLotById(this.lot!.id).subscribe(x => {
          this.lot = x
        })
      }
    })
  }
}
