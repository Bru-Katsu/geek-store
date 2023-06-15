import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, Subject, combineLatest, debounceTime, switchMap } from 'rxjs';
import { ProductList } from 'src/app/shared/api-services/models/product-list.model';
import { ProductListQuery } from 'src/app/shared/api-services/queries/product-list-query';
import { ProductApiService } from 'src/app/shared/api-services/services/product-api.service';
import { Page } from 'src/app/shared/models/page';
import { indicate } from 'src/app/shared/operators/indicator';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.scss']
})
export class ProductsListComponent implements OnInit {
  constructor(private productApi: ProductApiService) { }

  page$ = new BehaviorSubject<number>(1);
  productName$ = new BehaviorSubject<string>('');
  loading$ = new Subject<boolean>();

  allProducts$!: Observable<Page<ProductList>>;

  ngOnInit(): void {
    this.allProducts$ = combineLatest([this.page$, this.productName$.pipe(debounceTime(500))]).pipe(
      indicate(this.loading$),
      switchMap(([page, produto]) => this.productApi.getAllProducts(new ProductListQuery(produto, page, 10)))
    );
  }

  pageChange(index: number): void {
    this.page$.next(index);
  }

  onSearch(e: any): void {
    this.productName$.next(e.target.value);
  }
}
