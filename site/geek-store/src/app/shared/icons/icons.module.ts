import { NgModule } from '@angular/core';

import { FeatherModule } from 'angular-feather';
import { Edit, Plus, ShoppingCart, Trash, Trash2 } from 'angular-feather/icons';

// Select some icons (use an object, not an array)
const icons = {
  ShoppingCart,
  Trash2,
  Edit,
  Plus
};

@NgModule({
  imports: [
    FeatherModule.pick(icons)
  ],
  exports: [
    FeatherModule
  ]
})
export class IconsModule { }
