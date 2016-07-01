using System.Collections.Generic;

namespace Api.Domain.Model
{
	public class ContactModel
	{
		public IEnumerable<PhoneModel> Phones { get; set; }
		public string Email { get; set; }
	}
}
