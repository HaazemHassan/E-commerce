import About from "./Components/About";
import Nav from "./Components/NavBar";
import ProductsDetails from "./Components/ProductDetails";
import Products from "./Components/Products";
import Slider from "./Components/Slider";
import { Routes, Route } from "react-router-dom";

function App() {
  return (
    <>
      <Nav />
      <Routes>
        <Route
          path="/"
          element={
            <>
              <Slider />
              <Products />
            </>
          }
        />
        <Route path="about" element={<About />} />
        <Route path="product/:productId" element={<ProductsDetails/>}/>
      </Routes>
    </>
  );
}

export default App;
