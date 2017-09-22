/***************************
* Controller pour le token *
****************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APP1;

namespace APP1.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        [HttpPost]
		public object Post()
		{
            /*Génération et Renvoie du token*/
			return new ObjectResult(AuthUser.GenerateToken());
		}

    }

}
