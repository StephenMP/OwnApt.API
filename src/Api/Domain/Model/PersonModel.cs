using Api.Domain.Enum;

namespace Api.Domain.Model
{
    public class PersonModel
	{
		public string Id { get; set; }
		public PersonType Type { get; set; }
		public NameModel Name { get; set; }
		public int Age { get; set; }
		public Gender Gender { get; set; }
		public int CreditScore { get; set; }
		public ContactModel Contact { get; set; }
	}
}
