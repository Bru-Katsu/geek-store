import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LOCALE_ID } from '@angular/core';

import { ProductCardComponent } from './product-card.component';
import { CurrencyPipe } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import {registerLocaleData} from '@angular/common';

registerLocaleData(localePt);

describe('ProductCardComponent', () => {
  let component: ProductCardComponent;
  let fixture: ComponentFixture<ProductCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductCardComponent ],
      providers: [{provide: LOCALE_ID, useValue: 'pt-BR'}]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('Nome deve corresponder com a propriedade do produto', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    component.product = {
      id: 'iuhosaihd-dawdojawi-s',
      name: 'Nike SB',
      price: 300
    };

    fixture.detectChanges();
    const cp = new CurrencyPipe('pt-BR');

    expect(compiled.querySelector('.card-title')?.textContent).toBe(component.product?.name);
    expect(compiled.querySelector('#price')?.textContent).toBe(cp.transform(component.product?.price, 'BRL'));
  });
});
