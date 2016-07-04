using System.Collections.Generic;

namespace OwnApt.Api.Domain.Model
{
	public class ContactModel
	{
		public IEnumerable<PhoneModel> Phones { get; set; }
		public string Email { get; set; }
	}
}
