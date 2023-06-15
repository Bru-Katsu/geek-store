import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsListComponent } from './pages/products-list/products-list.component';
import { CatalogRoutingModule } from './catalog-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { ProductCardComponent } from './components/product-card/product-card.component';
import { ProductDetailsComponent } from './pages/product-details/product-details.component';
import { ApiServicesModule } from '../shared/api-services/api-services.module';
import { ProductRatingComponent } from './components/product-rating/product-rating.component';
import { IconsModule } from '../shared/icons/icons.module';

@NgModule({
  declarations: [
    ProductsListComponent,
    ProductDetailsComponent,
    ProductCardComponent,
    ProductRatingComponent
  ],
  imports: [
    CommonModule,
    CatalogRoutingModule,
    HttpClientModule,
    ApiServicesModule,
    IconsModule
  ],
  providers: [

  ]
})
export class CatalogModule { }
