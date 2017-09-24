using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using USherbrooke.ServiceModel.Sondage;

namespace APP1.Controllers
{
	[Route("api/[controller]")]
	public class SondageController : Controller
	{
		// GET api/sondage
		[HttpGet]
        public IEnumerable<Poll> GetAll()
        {
            if (!ValidateToken(Request.Headers["Authorization"]))
            {
                return notAuth();
            }

            return new SimpleSondageDAO().GetAvailablePolls();
        }

        // POST api/sondage
        [HttpPost]
        public PollQuestion Post(int pollId,int currentQuestionId, string answer)
		{
			if (!ValidateToken(Request.Headers["Authorization"]))
			{
                notAuth();
			}

            return new SimpleSondageDAO().GetNextQuestion(pollId, currentQuestionId);
		}

        public UnauthorizedResult notAuth(){
            return Unauthorized();
        }

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
     