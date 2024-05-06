import { useParams } from "react-router-dom";
import Product from "./Product";
import { useEffect, useState } from "react";

export default function ProductsDetails() {
  const [product, setProduct] = useState({});
  const params = useParams();

  useEffect(() => {
    fetch(`https://fakestoreapi.com/products/${params.productId}`)
      .then((res) => res.json())
      .then((data) => setProduct(data));
  }, []);

  return (
    <>
      <div className="product-container-in-details">
        <Product product={product} />
      </div>
    </>
  );
}
