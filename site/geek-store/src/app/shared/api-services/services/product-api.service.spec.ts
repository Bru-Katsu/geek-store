import { TestBed, getTestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ProductApiService } from './product-api.service';
import { environment } from 'src/environments/environment';
import { ProductList } from '../models/product-list.model';
import { Page } from 'src/app/shared/models/page';
import { ProductListQuery } from '../queries/product-list-query';

describe('ProductApiService', () => {
  let injector: TestBed;
  let service: ProductApiService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ProductApiService],
    });

    injector = getTestBed();
    service = injector.get(ProductApiService);
    httpMock = injector.get(HttpTestingController);
  });

  const dummyPageResponse: Page<ProductList> = {
    totalItemCount: 3,
    pageIndex: 0,
    pageSize: 3,
    items: [
      { id: 'ca24fd19-db2c-4de0-b255-bcd740114865', name: 'Nike SB', price: 300 },
      { id: 'e77e55bc-f122-4abe-959a-05c0856cabd4', name: 'Nike Shocks', price: 320 },
      { id: '5f8cab62-0e56-4305-a290-bf709d3dee1b', name: 'Nike Air Force', price: 1320 },
    ]
  };

  it('getAllProducts() deve retornar dados', () => {
    service.getAllProducts(new ProductListQuery('', 1, 10)).subscribe((res) => {
      expect(res).toEqual(dummyPageResponse);
    });

    const req = httpMock.expectOne(`${environment.apiAddress}/product/all`);
    expect(req.request.method).toBe('GET');

    req.flush(dummyPageResponse);
  });

  afterEach(() => {
    httpMock.verify();
  });
});
