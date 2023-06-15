import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductApiService } from './services/product-api.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [ProductApiService]
})
export class ApiServicesModule { }
