using System;
namespace CBM_API.Entities
{
	public class LoginResponse
	{
		public Account Account { get; set; }
		public string AccessToken { get; set; }
		public string Message { get; set; }
		public string Status { get; set; }
	}
}

