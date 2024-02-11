import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BidService } from 'src/app/bids/data-access/bid.service';
import { BidDto } from 'src/app/models/lot';

@Component({
  selector: 'app-create-bid-dialog',
  templateUrl: './create-bid-dialog.component.html',
  styleUrls: ['./create-bid-dialog.component.scss']
})
export class CreateBidDialogComponent implements OnInit {

  amount: number = 0;

  constructor(
    public dialogRef: MatDialogRef<CreateBidDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private bidService: BidService,) { }

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close(false);
  }

  onClick(){
    console.log(this.amount)
    if (this.amount && this.amount > 0){
      const bid: BidDto = {amount: this.amount, lotId: this.data.lotId, id: '', userId: ''};
      this.bidService.createBid(bid).subscribe(x => {
        this.dialogRef.close(true);
      });
    }

    this.dialogRef.close(false);
  }
}
