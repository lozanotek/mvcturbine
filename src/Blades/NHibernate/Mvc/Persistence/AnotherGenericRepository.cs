namespace Mvc.Persistence {
	using Mappings;
	using MvcTurbine.NHibernate;

	// Since this repository goes against another 'session factory', 
	// we need to specify the new provider to use.
	public class AnotherGenericRepository<T> : GenericRepository<T> {
		public AnotherGenericRepository(AnotherSessionProvider provider) : base(provider) {
		}
	}
}
