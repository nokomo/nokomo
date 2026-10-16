import os
from pathlib import Path
from dotenv import load_dotenv

# Path helper: calculates absolute paths robustly
BASE_DIR = Path(__file__).resolve().parent
PROJECT_ROOT = BASE_DIR.parent

# 1. Determine environment context
ENV_TYPE = os.getenv("APP_ENV", "development").lower()
ENV_FILENAME = ".env.production" if ENV_TYPE == "production" else ".env.development"
ENV_FILE_PATH = PROJECT_ROOT / ENV_FILENAME

# 2. Load the target environment file
if ENV_FILE_PATH.exists():
    load_dotenv(dotenv_path=ENV_FILE_PATH, override=True)
else:
    raise FileNotFoundError(f"Configuration file missing: {ENV_FILE_PATH}")

# 3. Expose typed config settings to the app
IS_PRODUCTION = ENV_TYPE == "production"

DB_USER = os.getenv("DB_USER")
DB_PASSWORD = os.getenv("DB_PASSWORD")
DB_HOST = os.getenv("DB_HOST", "localhost")
DB_PORT = os.getenv("DB_PORT", "5432")
DB_NAME = os.getenv("DB_NAME")

# Quick safety assertion
if not all([DB_USER, DB_PASSWORD, DB_NAME]):
    raise ValueError(f"Crucial database variables missing inside {ENV_FILENAME}!")

DATABASE_URL = f"postgresql+psycopg2://{DB_USER}:{DB_PASSWORD}@{DB_HOST}:{DB_PORT}/{DB_NAME}"
