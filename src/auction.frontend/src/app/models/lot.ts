export interface LotDto {
  id: string;
  name: string;
  description: string;
  category: string;
  startPrice: number;
  currentPrice?: number;
  status: LotStatus;
  startDate?: Date;
  endDate?: Date;
  image: string;
  bids: BidDto[];
}

export interface BidDto{
  id: string;
  lotId: string;
  userId: string;
  amount: number;
}

export enum LotStatus {
  NotStarted = 'NotStarted',
  Active = 'Active',
  Finished = 'Finished'
}
