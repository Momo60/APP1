using System;
using System.Collections.Generic;
using System.Linq;
using APP1.Models;
using Microsoft.AspNetCore.Mvc;
using USherbrooke.ServiceModel.Sondage;

namespace APP1.Controllers
{
	[Route("api/[controller]")]
	public class SondageController : Controller
	{
/*		private readonly SondageContext _context;

		public SondageController(SondageContext context)
		{
			_context = context;

            if (_context.SimpleSondageDAO.Count() == 0)
            {
                _context.SimpleSondageDAO.Add(new USherbrooke.ServiceModel.Sondage.SimpleSondageDAO{});
                _context.SaveChanges();
            }
		}
*/
        [HttpGet]
        public IEnumerable<Poll> GetAll()
        {
            return new SimpleSondageDAO().GetAvailablePolls(); 
        }

    }
}
     