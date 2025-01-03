
export interface AuthDialogData {
  type: 'login' | 'register'
}

export interface LoginForm {
  email: string;
  password: string;
}

export interface RegisterForm {
  email: string;
  password: string;
  username: string;
}

