import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'product-rating',
  templateUrl: './product-rating.component.html',
  styleUrls: ['./product-rating.component.scss']
})
export class ProductRatingComponent implements OnInit {
  maxRate: number = 5;
  rates: boolean[] =  [].constructor(this.maxRate);

  ngOnInit(): void {
    this.rates.fill(true, 0, this.rate);
    this.rates.fill(false, this.rate, this.maxRate - this.rate);
    // console.log(this.rates)
  }



  @Input()
  rate: number = 0;

  @Input()
  showBadge: boolean = false;
}
