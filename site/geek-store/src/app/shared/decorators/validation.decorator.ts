import { FormControl, ValidatorFn, Validators } from '@angular/forms';

// Decorator para adicionar validações aos campos do modelo de produto
export function UseReactiveValidation(validator: ValidatorFn): PropertyDecorator {
  return function (target: any, propertyKey: string | symbol): void {
    const control = new FormControl(target[propertyKey], validator);
    Object.defineProperty(target, propertyKey, {
      get: () => control.value,
      set: (value) => {
        control.setValue(value);
      },
    });
  };
}
