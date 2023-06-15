import { Component, Input, OnInit } from '@angular/core';
import { ProductList } from '../../../shared/api-services/models/product-list.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

  constructor(private router: Router, private activatedRoute: ActivatedRoute) { }

  @Input()
  product: ProductList | undefined;

  ngOnInit(): void {
  }

  openDetails(): void {
    this.router.navigate(['./', this.product?.id, 'details'], { relativeTo: this.activatedRoute });
  }

  addToCart(): void {
    console.log('adicionado ao carrinho!');
  }
}
