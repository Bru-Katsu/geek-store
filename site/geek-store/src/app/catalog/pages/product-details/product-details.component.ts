import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subject, map, switchMap, tap } from 'rxjs';
import { ProductDetail } from 'src/app/shared/api-services/models/product-detail';
import { ProductApiService } from 'src/app/shared/api-services/services/product-api.service';
import { indicate } from 'src/app/shared/operators/indicator';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  constructor(private route: ActivatedRoute, private productApi: ProductApiService) { }
  loading$ = new Subject<boolean>();
  product$!: Observable<ProductDetail>;

  ngOnInit(): void {
    this.product$ = this.productApi.getProduct(this.route.snapshot.params['id']).pipe(
      indicate(this.loading$)
    );
  }

}
