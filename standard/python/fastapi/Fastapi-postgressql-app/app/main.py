
from fastapi import FastAPI, status,Depends, HTTPException
from sqlalchemy.orm import Session
from models import Product as ProductModel
from schemas import ProductCreate, ProductUpdate, Product, ProductBase
from db import  get_db, create_table
from contextlib import asynccontextmanager

@asynccontextmanager
async def lifespan(app: FastAPI):
    # 🚀 This line automatically creates all missing tables in PostgreSQL on startup
    create_table()
    
    print("Database tables verified/created!")
    yield
    
    print("Shutting down...")


# create an instance of the FastAPI class
app = FastAPI(lifespan=lifespan)


@app.get("/health")
def read_health():
    return {"status": "ok"} 

@app.get("/") 
def read_root():
    return {"message": "Hello World"} 


@app.get("/products/{product_id}", response_model=Product)
async def get_product(product_id: int, db: Session = Depends(get_db)):
   product = db.query(ProductModel).filter(ProductModel.id == product_id).first()
   if not product:
       raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail="Product not found") 
   return product

@app.get("/products/", response_model=list[Product])
async def get_products(skip: int = 0, limit: int = 10, db: Session = Depends(get_db)):
    products = db.query(ProductModel).offset(skip).limit(limit).all()
    return products

@app.post("/products/", response_model=Product, status_code=status.HTTP_201_CREATED)
async def create_product(product: ProductCreate, db: Session = Depends(get_db)):
    new_product = ProductModel(**product.model_dump())
    db.add(new_product)
    db.commit()
    db.refresh(new_product)
    return new_product

@app.put("/product/{product_id}", response_model=Product)
async def update_product(product_id: int, product: ProductUpdate, db: Session = Depends(get_db)):
    existing_product = db.query(ProductModel).filter(ProductModel.id == product_id).first()
    if not existing_product:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail="Product not found")

    updated_data = {k: v for k, v in product.model_dump().items() if v is not None}
    db.query(ProductModel).filter(ProductModel.id == product_id).update(updated_data)
    db.commit()
    return existing_product

@app.delete("/product/{product_id}", status_code=status.HTTP_204_NO_CONTENT)
async def delete_product(product_id: int, db: Session = Depends(get_db)):
    product = db.query(ProductModel).filter(ProductModel.id == product_id).first()
    if not product:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail="Product not found")
    db.delete(product)
    db.commit()
    return {"message": "Product deleted successfully"}

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)