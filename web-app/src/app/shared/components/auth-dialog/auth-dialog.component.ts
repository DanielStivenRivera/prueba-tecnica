import {ChangeDetectionStrategy, Component, effect, Inject, OnInit, signal} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {AuthDialogData, LoginForm, RegisterForm} from '../../types/dialog';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {AuthService} from '../../services/auth.service';
import {LoadingService} from '../../services/loading.service';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-auth-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
  ],
  templateUrl: './auth-dialog.component.html',
  styleUrl: './auth-dialog.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AuthDialogComponent implements OnInit {

  authForm: FormGroup;

  type = signal<'login' | 'register'>('login')

  constructor(
    private matDialogRef: MatDialogRef<AuthDialogComponent>,
     @Inject(MAT_DIALOG_DATA) public data:AuthDialogData,
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private loadingService: LoadingService,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.type.set(this.data.type);
    this.matDialogRef.disableClose = true;
    this.initializeForm();
  }

  initializeForm() {
    if (this.type() === 'login') {
      this.authForm = this.formBuilder.group({
        email: this.formBuilder.control('', {
          validators: [
            Validators.required,
            Validators.email,
            Validators.pattern(/^[^\s@]+@[^\s@]+\.[^\s@]+$/)
          ],
          nonNullable: true
        }),
        password: this.formBuilder.control('', {
          validators: [
            Validators.required,
            Validators.minLength(6)
          ],
          nonNullable: true
        }),
      });
    } else {
      this.authForm = this.formBuilder.group({
        email: this.formBuilder.control('', {
          validators: [
            Validators.required,
            Validators.email,
            Validators.pattern(/^[^\s@]+@[^\s@]+\.[^\s@]+$/)
          ],
          nonNullable: true
        }),
        password: this.formBuilder.control('', {
          validators: [
            Validators.required,
            Validators.minLength(6)
          ],
          nonNullable: true
        }),
        username: this.formBuilder.control('', {
          validators: [
            Validators.required,
            Validators.minLength(3),
            Validators.pattern(/^[a-zA-Z0-9_-]*$/)
          ],
          nonNullable: true
        }),
      });
    }
    this.authForm.markAsPristine();
  }

  changeForm(type: 'login'| 'register') {
    if (this.type() === type)
      return;
    this.type.set(type);
    this.initializeForm();
  }

  cancel() {
    this.matDialogRef.close('cancel');
  }

  async save() {
    this.authForm.markAllAsTouched();
    if (this.authForm.invalid) {
      this.toastrService.info('Completar los campos obligatorias');
      return;
    }
    try {
      this.loadingService.setLoading(true);
      if (this.type() === 'login') {
        const body: LoginForm = this.authForm.value;
        await this.authService.login(body);
      } else {
        const body: RegisterForm = this.authForm.value;
        await this.authService.register(body);
      }
      this.toastrService.success(`${this.type() === 'login' ? 'Inicio de sesión' : 'Registro'} exitoso.`)
      this.matDialogRef.close('success');
    } catch (e: any) {
      console.log(e)
      let message = 'ha ocurrido un error inesperado.'
      if (e.error && e.error.message === 'email/password incorrect') {
        message = 'Usuario o contraseña incorrectos'
      } else if (e.error && e.error.message === 'Email already registered') {
        message = 'El correo electrónico ya se encuentra registrado.'
      }
      this.toastrService.error(message);

    }
    this.loadingService.setLoading(false);
  }

}
