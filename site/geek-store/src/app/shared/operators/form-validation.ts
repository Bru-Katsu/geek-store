import { FormGroup, ValidatorFn, AbstractControl } from '@angular/forms';
import { Observable, OperatorFunction } from 'rxjs';
import { map } from 'rxjs/operators';

export interface ValidationMap {
  [key: string]: ValidatorFn[];
}

export function useValidations<T extends { [K in keyof T]: AbstractControl<any, any>; }>(validations: ValidationMap): OperatorFunction<FormGroup<T>, FormGroup<T>> {
  return (source: Observable<FormGroup<T>>) =>
    source.pipe(
      map((formGroup) => {
        for (const key in validations) {
          const control = formGroup.get(key);
          if (control) {
            control.setValidators(validations[key]);
            control.updateValueAndValidity();
          }
        }

        return formGroup;
      })
    );
}
