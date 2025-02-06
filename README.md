# SdfTools

SdfTools is a WPF application for converting geospatial data formats (SHP, SDF, SQLite) using **OSGeo FDO**. The application follows the **MVVM** architectural pattern and is built with **.NET 8**.

## Features
- **Import Geospatial Data**  
  - Supports **SDF** and **SQLite**.  
  - Uses **FDO API** for reading data.  
  - Extracts schema, attributes, and geometry (points, lines, polygons).  

- **Attribute Mapping**  
  - Auto-maps attributes by name and type.  
  - Manual editing of attribute mapping with data type conversion.  
  - Save and load mapping templates.  

- **Data Transformation & Validation**  
  - Automatic type conversion (e.g., `Text → Integer`).  
  - Validation of data formats (`DateTime`, numeric values).  
  - Error reporting before conversion.  

- **Export Converted Data**  
  - Supports **SDF** and **SQLite** (compatible with MapGuide).  
  - Ensures schema compliance and correct geometry.  
  - Generates export logs with errors and warnings.  

- **User Interface**  
  - Select source files and define target formats
