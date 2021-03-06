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

		public IActionResult Filter(int age)
		{
			var filteredPeople = people.Where(person => person.Age == age);
			return View("People", filteredPeople);

			/*
			 * The where method is an extension method https://www.youtube.com/watch?v=VkrKNXscoto&list=PL6n9fhu94yhWi8K02Eqxp3Xyh_OmQ0Rp6&index=3
			 * from LINQ which allows you to pass a lambda (anonymous delegate) to filter by.
			 * In other words, where just does a foreach on the people list, and passes each
			 * person to the method you pass it. The whole "person => person.Age == age" is just a method or function
			 * being passed to the Where method. It takes in an object, then returns true or false.
			 * See aFilter and bFilter to see examples which accomplish the same thing.
			 */
		}

		public IActionResult aFilter(int age)
		{
			var filteredPeople = new List<Person>();
			// for, foreach, if, and other keywords that are usually followed by a {} code block
			// Do not require {} for one line statments. Everything up to the semicolon is
			// assumed to be part of the block. This foreach -> if -> statement could be on one line.
			foreach (var person in people)
				if (person.Age == age)
					filteredPeople.Add(person);
			return View("People", filteredPeople);
		}

		public IActionResult bFilter(int age)
		{
			// Here we declare a delegate of type Func. It takes in a parameter of type Person
			// which we call person (but could call anything) then compare that person's age
			// with the age passed into this OtherUnusedFilter.
			// Note, when the method is only one statement (basically one line) it doesn't
			// need {} or a return keyword. They are only used here for clarity.
			Func<Person, bool> filterDelegate = person => { return person.Age == age; };
			return View("People", people.Where(filterDelegate));

		}

		[HttpPost]
		//The person is passed to this method by the form (via HttpPost)
		public IActionResult NewPerson(Person person)
		{
			if (person != null && !string.IsNullOrWhiteSpace(person.Name)) people.Add(person);

			//This can't magically return the matching view because there is no NewPerson view.
			//Instead this returns the People view.
			//The second argument, people, is the object to associate with the view.
			return View("People", people);
		}

		//This person info is passed from the form, then a new person is constructed and added.
		public IActionResult NewPerson(int age, string name)
		{
			if (!string.IsNullOrWhiteSpace(name)) people.Add(new Person(age, name));
			return View("People", people);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
