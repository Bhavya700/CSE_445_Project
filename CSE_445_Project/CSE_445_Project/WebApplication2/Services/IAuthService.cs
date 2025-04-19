using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Services
{
	public interface IAuthService
	{
		bool Register(string email, string password);
		bool Authenticate(string email, string password);
	}
}