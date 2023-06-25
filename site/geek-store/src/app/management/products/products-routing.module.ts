import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsListComponent } from './pages/products-list/products-list.component';
import { ProductEditorComponent } from './pages/product-editor/product-editor.component';

const routes: Routes = [
  { path:'', component: ProductsListComponent },
  { path: ':id/edit', component: ProductEditorComponent },
  { path: 'new', component: ProductEditorComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule { }
