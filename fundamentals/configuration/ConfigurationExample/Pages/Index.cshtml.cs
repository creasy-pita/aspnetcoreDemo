using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ConfigurationExample.Models;

namespace ConfigurationExample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;

        public IndexModel(IConfiguration config)
        {
            _config = config;
        }

        public TvShow TvShow { get; private set; }
        public JsonArrayExample JsonArrayExample { get; private set; }

        public void OnGet()
        {
            // Take a subset of the configuration entries because the 
            // AddEnvironmentVariables call provided by CreateDefaultBuilder 
            // doesn't have a prefix filter. All environment variables available 
            // are provided to the app's configuration. Without the filtering
            // applied here, the list of configuration entries shown by the app
            // can number over 50.

            #region snippet_tvshow
            TvShow = _config.GetSection("tvshow").Get<TvShow>();
            #endregion

            JsonArrayExample = _config.GetSection("json_array").Get<JsonArrayExample>();
        }
    }
}
