using System.ComponentModel.DataAnnotations;

namespace UsefulStuff.Models
{
	public class Person
	{
		public int id;
		[Display(Name = "Name")]
		public string Name { get; set; }
		[Display(Name = "Age")]
		public int Age { get; set; }

		static int currentID = 1000;

		public Person()
		{
			id = currentID++;
		}

		public Person(int age, string name)
		{
			id = currentID++;
			this.Age = age;
			this.Name = name;
		}
	}
}
