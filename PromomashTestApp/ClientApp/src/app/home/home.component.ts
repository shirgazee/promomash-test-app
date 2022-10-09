import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import Validation from '../helpers/validation';
import { ApiService } from '../services/api.service';
import Country from '../services/models/country';
import Province from '../services/models/province';

export enum RegistrationSteps {
  Step1 = 1,
  Step2,
  Completed,
  Error,
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  currentStep: RegistrationSteps = RegistrationSteps.Step1;
  form!: FormGroup;
  submitted = false;
  countriesLoading = false;
  countries: Country[] = [];
  provinces: Province[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group(
      {
        login: ['', [Validators.required, Validators.email]],
        password: [
          '',
          [
            Validators.required,
            Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$'),
          ],
        ],
        confirmPassword: ['', Validators.required],
        agreedToWorkForFood: [false, Validators.requiredTrue],
        countryId: ['', [Validators.required]],
        provinceId: ['', [Validators.required]],
      },
      {
        validators: [Validation.match('password', 'confirmPassword')],
      }
    );

    this.apiService
      .getCountries()
      .subscribe((result) => (this.countries = result));
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    const formValue = this.form.value;
    this.apiService
      .registerUser({
        login: formValue.login,
        password: formValue.password,
        agreedToWorkForFood: formValue.agreedToWorkForFood,
        countryId: formValue.countryId,
        provinceId: formValue.provinceId,
      })
      .subscribe({
        next: (_) => (this.currentStep = RegistrationSteps.Completed),
        error: (e: HttpErrorResponse) => {
          if (e.status === 409) {
            this.currentStep = RegistrationSteps.Error;
          }
          console.error(e);
        },
      });
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }

  onCountryChange(countryId: string): void {
    this.countriesLoading = true;
    this.apiService.getProvinces(countryId).subscribe((result) => {
      this.provinces = result;
      this.countriesLoading = false;
    });
  }

  public validateStep1() {
    this.f.login.updateValueAndValidity();
    this.f.password.updateValueAndValidity();
    this.f.confirmPassword.updateValueAndValidity();
    this.f.agreedToWorkForFood.updateValueAndValidity();
    this.submitted = true;

    if (
      this.f.login.errors ||
      this.f.password.errors ||
      this.f.confirmPassword.errors ||
      this.f.agreedToWorkForFood.errors
    ) {
      return;
    }

    this.submitted = false;
    this.currentStep = RegistrationSteps.Step2;
  }
}
