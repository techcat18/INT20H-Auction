import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LotListComponent } from './lots/list/lot-list/lot-list.component';

const routes: Routes = [
  {path: 'lots', component: LotListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
