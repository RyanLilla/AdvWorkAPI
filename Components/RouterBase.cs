#nullable disable

namespace AdvWorksAPI {
    public class RouterBase
    {
        public string UrlFragment { get; set; }
        protected ILogger Logger;
        

        public virtual void AddRoutes(WebApplication app) {
            
        }
    }
}