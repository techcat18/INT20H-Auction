import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../data-access/auth.service';

@Component({
  selector: 'signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
  signupForm!: FormGroup;
  urlPattern: string = '(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?';
  submitted: boolean = false;
  passwordMismatch: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
    this.createForm();
  }

  createForm(){
    this.signupForm = this.fb.group({
      userName: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.email]],
      homePage: [null, [Validators.pattern(this.urlPattern)]],
      password: [null, [Validators.required]],
      confirmPassword: [null, [Validators.required]]
    })
  }

  get userName(){
    return this.signupForm.get('userName');
  }

  get email(){
    return this.signupForm.get('email');
  }

  get homePage(){
    return this.signupForm.get('homePage');
  }

  get password(){
    return this.signupForm.get('password');
  }

  get confirmPassword(){
    return this.signupForm.get('confirmPassword');
  }

  onSubmit(){
    this.submitted = true;

    if (this.confirmPassword?.value != this.password?.value){
      this.passwordMismatch = true;
      return;
    }

    if (!this.signupForm.valid){
      return;
    }

    this.authService.signup(this.signupForm.value).subscribe(u => {
      this.router.navigateByUrl('/auth/login');
    })
  }
}
