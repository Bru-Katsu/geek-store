export class ProductListQuery {
  /**
   *
   */
  constructor(name: string, pageIndex: number, pageSize: number) {
    this.name = name;
    this.pageIndex = pageIndex;
    this.pageSize = pageSize;
  }

  name!: string;
  pageIndex!: number;
  pageSize!: number;
}
