﻿using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.Dtos
{
	public class RegisterDto
	{
		[Required]
		public string DisplayName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Phone {  get; set; }

		[Required]
		[RegularExpression("(?=^.{6,10}$)((?=.*\\d))(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;,])(?!.*\\s).*$",
			ErrorMessage = "Password must have 1 Uppercase 1 lowercase 1 number, 1 non alphanumeric and at least 6 characters")]
		public string Password { get; set; }
	}
}









