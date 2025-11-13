export interface Page<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}

export interface PageRequest {
  pageNumber: number;
  pageSize: number;
}
