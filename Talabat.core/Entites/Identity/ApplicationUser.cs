using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public string DisplayName { get; set; }
		public Address? Adress { get; set; }
	}
}
