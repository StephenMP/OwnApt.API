using OwnApt.Api.Domain.Enum;

namespace OwnApt.Api.Repository.Entity
{
    public class PersonEntity
	{
		public string Id { get; set; }
		public PersonType Type { get; set; }
		public NameEntity Name { get; set; }
		public int Age { get; set; }
		public Gender Gender { get; set; }
		public int CreditScore { get; set; }
		public ContactEntity Contact { get; set; }
	}
}
