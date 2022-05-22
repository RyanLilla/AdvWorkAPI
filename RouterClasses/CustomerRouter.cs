namespace AdvWorksAPI {
    public class CustomerRouter : RouterBase {
        public CustomerRouter()
        {
            UrlFragment = "customer";
        }

        protected virtual List<Customer> GetAll() {
            return new List<Customer> {
                new Customer {
                    CustomerID = 706,
                    FirstName = "socks",
                    LastName = "Red",
                    CompanyName = "",
                    EmailAddress = ""
                },
                new Customer {
                    CustomerID = 707,
                    FirstName = "basketball",
                    LastName = "Red",
                    CompanyName = "",
                    EmailAddress = ""
                },
                new Customer {
                    CustomerID = 708,
                    FirstName = "shoes",
                    LastName = "Black",
                    CompanyName = "",
                    EmailAddress = ""
                },
                new Customer {
                    CustomerID = 709,
                    FirstName = "car",
                    LastName = "White",
                    CompanyName = "",
                    EmailAddress = ""
                },
                new Customer {
                    CustomerID = 710,
                    FirstName = "phone",
                    LastName = "White",
                    CompanyName = "",
                    EmailAddress = ""
                }
            };
        }

        protected virtual IResult Get() {
            return Results.Ok(GetAll());
        }

        protected virtual IResult Get(int id) {

            // Locate a single row of data
            Customer? current = GetAll().Find(p => p.CustomerID == id);

            if(current != null) {
                return Results.Ok(current);
            }
            else {
                return Results.NotFound();
            }
        }

        protected virtual IResult Post(Customer entity) {

            // Generate a new ID
            entity.CustomerID = GetAll().Max(p => p.CustomerID) + 1;

            // TODO: Insert into Data Store

            // Return the newly-created object
            return Results.Created($"/{UrlFragment}/{entity.CustomerID}", entity);
        }

        protected virtual IResult Put(int id, Customer entity) {

            IResult ret;

            // Locate a single row of data
            Customer? current = GetAll().Find(p => p.CustomerID == id);

            if(current != null) {
                // TODO: Update the entity
                current.FirstName = entity.FirstName;
                current.LastName = entity.LastName;
                current.CompanyName = entity.CompanyName;
                current.EmailAddress = entity.EmailAddress;

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
            Customer? current = GetAll().Find(p => p.CustomerID == id);

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
            app.MapPost($"/{UrlFragment}", (Customer entity) => Post(entity));
            app.MapPut($"/{UrlFragment}/{{id:int}}", (int id, Customer entity) => Put(id, entity));
        }
    }
}