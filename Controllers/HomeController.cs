using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UsefulStuff.Models;

namespace UsefulStuff.Controllers
{
	public class HomeController : Controller
	{
		public List<Person> people = new List<Person>
			{
				new Person(15, "Johnny"),
				new Person(37, "Sally")
			};

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult People()
		{
			//Magically returns a view with a file name matching the method name, eg: Index
			//If the view has an associated model and it is returned this way, the Model will be null.
			return View();
		}

		public IActionResult AddPerson()
		{
			return View();
		}

		public IActionResult AddPersonNoModel()
		{
			return View();
		}

		public IActionResult GetPeople()
		{
			//This can't magically return the matching view because there is no GeneratePeople view.
			//Instead this returns the People view.
			//The second argument, people, is the object to associate with the view.
			return View("People", people);
		}


		[HttpPost]
		//The person is passed to this method by the form (via HttpPost)
		public IActionResult NewPerson(Person person)
		{
			if (person != null) people.Add(person);

			//This can't magically return the matching view because there is no NewPerson view.
			//Instead this returns the People view.
			//The second argument, people, is the object to associate with the view.
			return View("People", people);
		}

		public IActionResult NewPerson(int age, string name)
		{
			if (name != null) people.Add(new Person(age, name));
			return View("People", people);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
