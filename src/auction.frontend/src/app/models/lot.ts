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
}

export enum LotStatus {
  NotStarted = 'NotStarted',
  Active = 'Active',
  Finished = 'Finished'
}
