import img1 from './images/img1-slider.jpg'
import img2 from './images/img2-slider.jpg'
import img3 from './images/img3-slider.jpg'
import './Slider.css'
export default function Slider() {
  return (
    <>
      <div id="carouselExampleAutoplaying" className="carousel slide mt-5 mb-5 " data-bs-ride="carousel">
  <div className="carousel-inner ">
    <div className="carousel-item active">
      <img src={img1} className="d-block w-100 img-carousel" alt='imagee'/>
    </div>
    <div className="carousel-item">
      <img src={img2} className="d-block w-100 img-carousel " alt='imagee'/>
    </div>
    <div className="carousel-item">
      <img src={img3} className="d-block w-100 img-carousel h-3" alt='imagee'/>
    </div>
  </div>
  <button className="carousel-control-prev" type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide="prev">
    <span className="carousel-control-prev-icon sahm " aria-hidden="true"></span>
    <span className="visually-hidden">Previouss</span>
  </button>
  <button className="carousel-control-next" type="button" data-bs-target="#carouselExampleAutoplaying" data-bs-slide="next">
    <span className="carousel-control-next-icon sahm"  aria-hidden="true"></span>
    <span className="visually-hidden">Next</span>
  </button>
</div>
    </>
  );
}
