export class UserModel{
  id!: string;
  userName!: string;
  email!: string;
}

export class LoginModel{
  email!: string;
  password!: string;
}

export class SignupModel{
  userName!: string;
  email!: string;
  password!: string;
  confirmPassword!: string;
}

export class JwtTokenModel{
  token!: string;
}
