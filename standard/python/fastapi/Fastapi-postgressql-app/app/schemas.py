from typing import Optional

from pydantic import BaseModel, Field

# Shared attributes used for both reading and writing
class ProductBase(BaseModel):    
    name: str = Field(..., min_length=1, max_length=100)
    price: float = Field(..., gt=0)
    quantity: int = Field(..., gt=0)
    description: Optional[str] = None

# Schema used when CREATING a product (POST request payload)
# It inherits everthing from ProductBase
class ProductCreate(ProductBase):
    pass

class ProductUpdate(ProductBase):    
    name: Optional[str] = Field(None, min_length=1, max_length=100)
    price: Optional[float] = Field(None, gt=0)
    quantity: Optional[int] = Field(None, gt=0)
    description: Optional[str] = None

# Schema used when RETRIEVING a product (GET request response)
# It includes the database-generated ID
class Product(ProductBase):
    id: int  

    class config:
        # Crucial for SQLAlchemy: allows Pydantic to read data
        # from SQLAlchemy objects (e.g., product.name) instead of just dicts
        from_attribute = True
