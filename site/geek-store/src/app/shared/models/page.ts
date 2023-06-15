export interface Page<T> {
  items: T[];
  pageIndex: number;
  pageSize: number;
  totalItemCount: number;
}
