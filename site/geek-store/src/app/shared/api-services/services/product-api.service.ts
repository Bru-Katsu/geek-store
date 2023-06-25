import { ApiServiceBase } from "src/app/shared/api/api-service-base";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { ProductList } from "../models/product-list.model";
import { Page } from "src/app/shared/models/page";
import { ProductDetail } from "../models/product-detail";
import { ProductListQuery } from "../queries/product-list-query";

@Injectable()
export class ProductApiService extends ApiServiceBase {
  constructor(private http: HttpClient) {
    super();
  }

  getProduct(id: string): Observable<ProductDetail> {
    return this.http.get<ProductDetail>(`${this.apiAddress}/product/${id}`);
  }

  getAllProducts(query: ProductListQuery): Observable<Page<ProductList>> {
    return this.http.get<Page<ProductList>>(`${this.apiAddress}/product/all`, { params: query as any });
  }

  addProduct(product: ProductDetail) {
    return this.http.post<ProductDetail>(`${this.apiAddress}/product`, product);
  }

  updateProduct(product: ProductDetail) {
    return this.http.put<ProductDetail>(`${this.apiAddress}/product`, product);
  }

  removeProduct(productId: string) {
    return this.http.delete<void>(`${this.apiAddress}/product/${productId}`);
  }
}
