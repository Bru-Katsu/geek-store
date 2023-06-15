import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductEditorComponent } from './pages/product-editor/product-editor.component';
import { ProductsListComponent } from './pages/products-list/products-list.component';
import { ProductsRoutingModule } from './products-routing.module';
import { ApiServicesModule } from 'src/app/shared/api-services/api-services.module';
import { IconsModule } from 'src/app/shared/icons/icons.module';
import { ComponentsModule } from 'src/app/shared/components/components.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ProductEditorComponent,
    ProductsListComponent
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    ApiServicesModule,
    IconsModule,
    ComponentsModule,
    ReactiveFormsModule
  ]
})
export class ProductsModule { }
