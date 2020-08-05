# Hydra Catalog Data
Layer data implements the IRepository and persist the object to Database

### Creating entity migration
Package Manager Console:

``` Add-Migration Initial -Context CatalogContext```

``` update-database --Context CatalogContext```