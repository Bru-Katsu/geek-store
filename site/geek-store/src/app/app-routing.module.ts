import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path:'', redirectTo:'products', pathMatch:'full' },
  { path: 'products', loadChildren: () => import('./catalog/catalog.module').then(m => m.CatalogModule) },
  { path: 'management', loadChildren: () => import('./management/management.module').then(m => m.ManagementModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
