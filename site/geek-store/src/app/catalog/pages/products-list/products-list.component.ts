import { Component, OnInit } from '@angular/core';
import { Observable, Subject, map, tap } from 'rxjs';
import { ProductList } from '../../../shared/api-services/models/product-list.model';
import { indicate } from 'src/app/shared/operators/indicator';
import { ProductApiService } from 'src/app/shared/api-services/services/product-api.service';
import { ProductListQuery } from 'src/app/shared/api-services/queries/product-list-query';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.scss']
})
export class ProductsListComponent implements OnInit {

  constructor(private productApi: ProductApiService) { }

  allProducts$!: Observable<ProductList[]>;

  pageIndex: number = 0;
  pageSize: number = 10;
  totalCount: number = 0;

  loading$ = new Subject<boolean>();

  ngOnInit(): void {
    this.allProducts$ = this.productApi.getAllProducts(new ProductListQuery('', 1, 10)).pipe(
      indicate(this.loading$),
      tap(p => {
        this.pageIndex = p.pageIndex;
        this.pageSize = p.pageSize;
        this.totalCount = p.totalItemCount;
      }),
      map(x => x.items)
    );
  }

}
