namespace Okonau.Persistence.Mappings {
    using Domain;
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Defines the mapping between <see cref="Task"/> and the persistence table.
    /// </summary>
    public sealed class TaskMapping : ClassMap<Task> {
        public TaskMapping() {
            // Specify the table to use
            Table("Tasks");

            // Sets the key for the table to use
            Id(x => x.Id)
                .GeneratedBy
                .Increment();
            
            // Maps all the columns
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Created, "CreatedDate");
        }
    }
}
