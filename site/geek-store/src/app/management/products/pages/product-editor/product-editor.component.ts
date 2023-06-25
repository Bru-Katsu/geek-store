import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject, catchError, combineLatest, filter, from, iif, map, merge, of, startWith, switchMap, take, tap } from 'rxjs';
import { ProductDetail, newProductDetail } from 'src/app/shared/api-services/models/product-detail';
import { ProductApiService } from 'src/app/shared/api-services/services/product-api.service';
import { useValidations } from 'src/app/shared/operators/form-validation';
import { indicate } from 'src/app/shared/operators/indicator';

@Component({
  selector: 'app-product-editor',
  templateUrl: './product-editor.component.html',
  styleUrls: ['./product-editor.component.scss']
})
export class ProductEditorComponent implements OnInit {
  constructor(private route: ActivatedRoute,
              private productApi: ProductApiService,
              private fb: FormBuilder,
              private router: Router) { }

  loading$ = new Subject<boolean>();
  saving$ = new Subject<boolean>();
  product$!: Observable<ProductDetail>;
  form$!: Observable<FormGroup>;

  addProductAction$ = new Subject<ProductDetail>();
  updateProductAction$ = new Subject<ProductDetail>();

  private productUpdated$ = this.updateProductAction$.pipe(
    indicate(this.saving$),
    switchMap(product => this.productApi.updateProduct(product))
  );

  private productAdded$ = this.addProductAction$.pipe(
    indicate(this.saving$),
    switchMap(product => this.productApi.addProduct(product)),
    //navegar para a edição atual do item (preciso ajustar commands da API para retornar o produto inserido)
    tap(() => this.router.navigate([''], {relativeTo: this.route}))
  );

  private productEvent$ = merge(this.productUpdated$, this.productAdded$);

  ngOnInit(): void {
    this.product$ = this.productEvent$.pipe(
      startWith(null),
      switchMap(() => this.route.paramMap),
      switchMap(params => {
        const productId = params.get('id');
        if(productId)
          return this.productApi.getProduct(productId).pipe(
            indicate(this.loading$)
          );
        else
          return of(newProductDetail())
      }),
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
    if(product.id)
      this.updateProductAction$.next(product);
    else
      this.addProductAction$.next(product);
  }
}
