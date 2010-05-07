using System;

namespace WebTest.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public DateTime Created
		{
			get { return DateTime.Now; }
		}

		public bool InStock
		{
			get { return true; }
		}
	}
}