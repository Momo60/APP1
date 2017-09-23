using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            return new SimpleSondageDAO().GetAvailablePolls();
        }

        [HttpPost]
        public PollQuestion Post(int pollId,int currentQuestionId)
		{
            return new SimpleSondageDAO().GetNextQuestion(pollId, currentQuestionId);
		}

    }
}
     