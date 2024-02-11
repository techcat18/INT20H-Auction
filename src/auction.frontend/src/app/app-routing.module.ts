import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LotListComponent } from './lots/feature/lot-list/lot-list.component';
import { LoginComponent } from './auth/feature/login/login.component';
import { SignupComponent } from './auth/feature/signup/signup.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: 'lots'},
  {path: 'auth/login', component: LoginComponent},
  {path: 'auth/signup', component: SignupComponent},
  {path: 'lots', component: LotListComponent},
  {path: '**', redirectTo: 'lots'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
