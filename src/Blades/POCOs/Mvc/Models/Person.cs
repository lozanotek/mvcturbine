namespace PocoSample.Mvc.Models {
	public class Person {
		public Person() {
			FullName = new Name();
		}

		public int Id { get; set; }
		public Name FullName { get; set; }
	}
}