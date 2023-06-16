import { FormGroup, ValidatorFn, AbstractControl } from '@angular/forms';
import { Observable, OperatorFunction } from 'rxjs';
import { map } from 'rxjs/operators';

export interface ValidationMap {
  [key: string]: ValidationProperty[];
}

export interface ValidationProperty {
  0: ValidatorFn;
  1: string;
}

export function useValidations<T extends { [K in keyof T]: AbstractControl<any, any>; }>(validations: ValidationMap): OperatorFunction<FormGroup<T>, FormGroup<T>> {
  return (source: Observable<FormGroup<T>>) =>
    source.pipe(
      map((formGroup) => {
        for (const key in validations) {
          const control = formGroup.get(key);
          if (control) {
            const props = validations[key];

            control.setValidators(props.map(p => p[0]));
            control.updateValueAndValidity();
          }
        }

        return formGroup;
      })
    );
}

