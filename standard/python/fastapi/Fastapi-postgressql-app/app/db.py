from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, declarative_base
from app.config import DATABASE_URL, IS_PRODUCTION

# echo=True for development debugging
engine = create_engine(DATABASE_URL, echo=not IS_PRODUCTION) 

SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)

Base = declarative_base()

def get_db():
    """FastAPI Dependency providing a scoped database session per request."""
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

def create_table():
    """Initializes schema tables if they do not already exist."""
    Base.metadata.create_all(bind=engine) 
