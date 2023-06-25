export interface ProductDetail {
  id: string;
  name: string;
  price: number;
  description: string;
  category: string;
  imageURL: string;
}

export function newProductDetail(): ProductDetail {
  return {
    id: '',
    name: '',
    price: 0,
    description: '',
    category: '',
    imageURL: ''
  }
}
