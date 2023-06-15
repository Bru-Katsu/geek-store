import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subject, catchError, combineLatest, map, switchMap, tap } from 'rxjs';
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
  product$!: Observable<ProductDetail>;
  form$!: Observable<FormGroup>;

  saveProduct$ = new Subject<ProductDetail>().pipe(switchMap(product => this.productApi.updateProduct(product)));

  ngOnInit(): void {
    this.product$ = this.productApi.getProduct(this.route.snapshot.params['id']).pipe(
      indicate(this.loading$)
    );

    this.form$ = this.product$.pipe(map(product => this.fb.group(product)),
                                    useValidations({
                                      name: [Validators.required, Validators.maxLength(150)],
                                      price: [Validators.required],
                                      category: [Validators.maxLength(50)],
                                      description: [Validators.maxLength(500)]
                                    }));


  }

  save(form: ProductDetail){
    return this.productApi.updateProduct(form);
  }
}
