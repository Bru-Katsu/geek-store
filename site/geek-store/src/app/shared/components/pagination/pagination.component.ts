import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit, OnChanges {
  @Input() currentPage = 1;
  @Input() pageSize = 10;
  @Input() totalItems = 0;
  @Output() pageChanged = new EventEmitter<number>();

  pages: number[] = [];

  ngOnInit(): void {
    this.calculatePagination();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['totalItems'] || changes['pageSize'])
      this.calculatePagination();
  }

  private calculatePagination(): void {
    const totalPages = Math.ceil(this.totalItems / this.pageSize);
    this.currentPage = Math.max(1, Math.min(totalPages, this.currentPage));

    let startPage = 1;
    let endPage = totalPages;

    if (totalPages > 10) {
      startPage = Math.max(1, this.currentPage - 5);
      endPage = Math.min(totalPages, startPage + 9);

      const remainingPages = 9 - (endPage - startPage);

      if (remainingPages > 0)
        startPage = Math.max(1, startPage - remainingPages);
    }

    this.pages = Array.from({ length: endPage - startPage + 1 }, (_, i) => startPage + i);
  }

  changePage(index: number): void {
    this.pageChanged.emit(index);
  }
}
