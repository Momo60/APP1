using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APP1.Controllers
{
    /*[Authorize]*/  /*Pour utilisation du token générer, cela permet de bloquer la route entièrement*/
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            if (ValidateToken(Request.Headers["Authorization"]))
            {
                return new string[] { "value1", "value2" };
            } else {
                return null;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            if (ValidateToken(Request.Headers["Authorization"]))
            {
                return "Hello"+id;
            } else {
                return "Not Authorized";
            }
        }

        // POST api/values
        [HttpPost]
        public string Post()
        {
			if (ValidateToken(Request.Headers["Authorization"]))
			{
				return "POST";
			}
			else
			{
				return "Not Authorized";
			}
        }


        /***************************************
         * Fonction l'authentifiction du token *
         * *************************************/
		public bool ValidateToken(string token)
		{
			string myToken = System.IO.File.ReadAllText("token.json");
			dynamic key = JsonConvert.DeserializeObject(myToken);
			foreach (var values in key.tokens)
			{
				if (token == (string)values.token)
				{
					return true;
				}
			}
			return false;
		}


	}


}
