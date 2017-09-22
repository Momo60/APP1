using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APP1.Models;



namespace APP1.Controllers
{
	/*
	[Route("api/[controller]")]
	public class SondageController : Controller
	{
		private readonly SondageContext _context;

		public SondageController(SondageContext context)
		{
			_context = context;

			if (_context.SondageItems.Count() == 0)
			{
				_context.SondageItems.Add(new SimpleSondageDAO());
				_context.SaveChanges();
			}
		}
	

	[HttpGet]
	public IEnumerable<SondageItem> GetAll()
	{
		return _context.SondageItems.ToList();
	}

	[HttpGet("{id}", Name = "GetSondage")]
	public IActionResult GetById(long id)
	 { 
		var item = _context.SondageItems.FirstOrDefault(t => t.Id == id);
		if (item == null)
		{
			return NotFound();
		}
		return new ObjectResult(item);
	 }
    }
*/

	
     [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "Hello";
        }

        // POST api/values
        [HttpPost]
        public string Post()
        {
            return "YES";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }


}
