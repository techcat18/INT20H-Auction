import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { JwtTokenModel, LoginModel, SignupModel } from 'src/app/models/auth';
import { environment } from 'src/environments/environment';
import * as jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isAuthenticated: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(localStorage['token']);

  constructor(private http: HttpClient) { }

  getCurrentUserId(){
    var token = localStorage.getItem('token');

    if (token == null){
      return null;
    }

    const tokenInfo = this.getDecodedAccessToken(token);
    console.log(tokenInfo);
    var id = tokenInfo.sub;
    return id;
  }

  getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode.jwtDecode(token);
    } catch(Error) {
      return null;
    }
  }

  login(loginModel: LoginModel): Observable<JwtTokenModel>{
    return this.http.post<JwtTokenModel>(environment.apiUrl + 'auth/login', loginModel)
      .pipe(tap(r => {
        localStorage.setItem('token', r.token);
        this.isAuthenticated.next(true);
      }))
  }

  signup(signupModel: SignupModel): Observable<any>{
    return this.http.post<any>(environment.apiUrl + 'auth/signup', signupModel);
  }

  logout(){
    localStorage.removeItem('token');
    this.isAuthenticated.next(false);
  }
}
