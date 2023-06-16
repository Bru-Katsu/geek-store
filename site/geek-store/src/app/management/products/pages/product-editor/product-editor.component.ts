import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable, Subject, catchError, combineLatest, filter, map, startWith, switchMap, take, tap } from 'rxjs';
import { ProductDetail } from 'src/app/shared/api-services/models/product-detail';
import { ProductApiService } from 'src/app/shared/api-services/services/product-api.service';
import { useValidations } from 'src/app/shared/operators/form-validation';
import { indicate } from 'src/app/shared/operators/indicator';

@Component({
  selector: 'app-product-editor',
  templateUrl: './product-editor.component.html',
  styleUrls: ['./product-editor.component.scss']
})
export class ProductEditorComponent implements OnInit {
  constructor(private route: ActivatedRoute, private productApi: ProductApiService, private fb: FormBuilder) { }

  loading$ = new Subject<boolean>();
  saving$ = new Subject<boolean>();
  product$!: Observable<ProductDetail>;
  form$!: Observable<FormGroup>;

  onEdited$ = new Subject<void>();

  ngOnInit(): void {
    this.product$ = this.onEdited$.pipe(
      startWith(undefined),
      indicate(this.saving$),
      switchMap(() => this.route.paramMap),
      switchMap(params => {
        const productId = params.get('id') ?? '';
        return this.productApi.getProduct(productId);
      })
    );

    this.form$ = this.product$.pipe(
      map(product => this.fb.group(product)),
      useValidations({
        name: [
          [ Validators.required, "Informe o nome" ],
          [ Validators.maxLength(150), "O nome deve conter no máximo 150 caracteres" ]
        ],
        price: [[ Validators.required, "Informe o preço do produto" ]],
        category: [[ Validators.maxLength(50), "A categoria deve conter no máximo 50 caracteres" ]],
        description: [[ Validators.maxLength(500), "A descrição deve conter no máximo 500 caracteres"]]
      })
    );
  }

  save(product: ProductDetail){
    this.productApi.updateProduct(product).pipe(
      indicate(this.loading$),
      tap(() => this.onEdited$.next()),
      take(1)
    ).subscribe();
  }
}
