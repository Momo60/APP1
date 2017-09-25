using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using USherbrooke.ServiceModel.Sondage;

namespace APP1.Controllers
{
    /*[Authorize]*/  /*Pour utilisation du token générer, cela permet de bloquer la route entièrement*/
    [Route("api/[controller]")]
	public class SondageController : Controller
	{
        // GET api/sondage
        [HttpGet]
        public IEnumerable<Poll> GetAll()
        {
            if (ValidateToken(Request.Headers["Authorization"]))
            {
                return new SimpleSondageDAO().GetAvailablePolls();
            } else 
            {
                return null;
            }
        }

        // POST api/sondage
        [HttpPost]
        public PollQuestion Post(int pollId,string currentQuestionId, string answer, string username, string test)
		{
			if (ValidateToken(Request.Headers["Authorization"]))
			{
                string a = currentQuestionId;

				if (a == "11" || a == "12"|| a =="13"|| a =="21" ||a =="22"||a =="23") 
                {
                    PollQuestion responseQuestion = new PollQuestion();
					responseQuestion.PollId = pollId;
					responseQuestion.Text = answer;

					int userId = int.Parse(username);

                    new SimpleSondageDAO().SaveAnswer(userId, responseQuestion);

					int b = int.Parse(currentQuestionId);

					return new SimpleSondageDAO().GetNextQuestion(pollId, b);
			    }

				int c = int.Parse(currentQuestionId);

				if (currentQuestionId == "-1" && test =="first")
                {
                    return new SimpleSondageDAO().GetNextQuestion(pollId, c);
                }

                if (currentQuestionId == "-1" && test =="second")
                {
					int userId = int.Parse(username);
					PollQuestion responseQuestion = new PollQuestion();
					responseQuestion.PollId = pollId;
					responseQuestion.Text = answer;

                    if (pollId == 1) 
                    {
						new SimpleSondageDAO().SaveAnswer(userId, responseQuestion);
						return new SimpleSondageDAO().GetNextQuestion(1, 11);
                    }

					if (pollId == 2)
					{
						new SimpleSondageDAO().SaveAnswer(userId, responseQuestion);
						return new SimpleSondageDAO().GetNextQuestion(2, 21);
					}
                }

            } else 
            {
                return null;
            }
            return null;
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
     