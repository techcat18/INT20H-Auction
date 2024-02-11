import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LotListComponent } from './lots/feature/lot-list/lot-list.component';
import { LoginComponent } from './auth/feature/login/login.component';
import { SignupComponent } from './auth/feature/signup/signup.component';
import { LotItemComponent } from './lots/feature/lot-item/lot-item.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: 'lots'},
  {path: 'auth/login', component: LoginComponent},
  {path: 'auth/signup', component: SignupComponent},
  {path: 'lots', component: LotListComponent},
  {path: 'lots/:id', component: LotItemComponent},
  {path: '**', redirectTo: 'lots'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
