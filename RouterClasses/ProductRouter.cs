namespace AdvWorksAPI {
    public class ProductRouter : RouterBase {
        public ProductRouter()
        {
            UrlFragment = "product";
        }

        protected virtual List<Product> GetAll() {
            return new List<Product> {
                new Product {
                    ProductID = 706,
                    Name = "socks",
                    Color = "Red",
                    ListPrice = 1500.0000m
                },
                new Product {
                    ProductID = 707,
                    Name = "basketball",
                    Color = "Red",
                    ListPrice = 34.9900m
                },
                new Product {
                    ProductID = 708,
                    Name = "shoes",
                    Color = "Black",
                    ListPrice = 34.9900m
                },
                new Product {
                    ProductID = 709,
                    Name = "car",
                    Color = "White",
                    ListPrice = 9.5000m
                },
                new Product {
                    ProductID = 710,
                    Name = "phone",
                    Color = "White",
                    ListPrice = 9.5000m
                }
            };
        }

        protected virtual IResult Get() {
            return Results.Ok(GetAll());
        }

        protected virtual IResult Get(int id) {

            // Locate a single row of data
            Product? current = GetAll().Find(p => p.ProductID == id);

            if(current != null) {
                return Results.Ok(current);
            }
            else {
                return Results.NotFound();
            }
        }

        protected virtual IResult Post(Product entity) {

            // Generate a new ID
            entity.ProductID = GetAll().Max(p => p.ProductID) + 1;

            // TODO: Insert into Data Store

            // Return the newly-created object
            return Results.Created($"/{UrlFragment}/{entity.ProductID}", entity);
        }

        protected virtual IResult Put(int id, Product entity) {

            IResult ret;

            // Locate a single row of data
            Product? current = GetAll().Find(p => p.ProductID == id);

            if(current != null) {
                // TODO: Update the entity
                current.Name = entity.Name;
                current.Color = entity.Color;
                current.ListPrice = entity.ListPrice;

                // TODO: Update the Data Store
                ret = Results.Ok(current);
            }

            // Return the updated entity
            else {
                ret = Results.NotFound();
            }

            return ret;
        }

        protected virtual IResult Delete(int id) {
            
            IResult ret;

            // Locate a single row of data
            Product? current = GetAll().Find(p => p.ProductID == id);

            if(current != null) {
                // TODO: Delete data from the Data Store
                GetAll().Remove(current);

                // Return NoContent
                ret = Results.NoContent();
            }
            else {
                ret = Results.NotFound();
            }

            return ret;
        }

        /// <summary>
        /// Add Routes
        /// </summary>
        public override void AddRoutes(WebApplication app)
        {
            app.MapGet($"/{UrlFragment}", () => Get());
            app.MapGet($"/{UrlFragment}/{{id:int}}", (int id) => Get(id));
            app.MapPost($"/{UrlFragment}", (Product entity) => Post(entity));
            app.MapPut($"/{UrlFragment}/{{id:int}}", (int id, Product entity) => Put(id, entity));
        }
    }
}