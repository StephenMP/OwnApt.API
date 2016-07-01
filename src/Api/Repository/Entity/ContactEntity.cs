﻿using System.Collections.Generic;

namespace Api.Repository.Entity
{
	public class ContactEntity
	{
		public IEnumerable<PhoneEntity> Phones { get; set; }
		public string Email { get; set; }
	}
}
