using DataProvider.ClientResponse;
using DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DataProvider
{
	public class AccountHelper
	{
		public static BasicDataResponse ExistsWithUsername(string username, string password)
		{
			try
			{
				using (Models.popopopoEntities bdd = new popopopoEntities())
				{
					//TODO : Check if there is no other email (there we consider the add function worked well)
					var receipt = bdd.Accounts.Where(a => a.username == username).ToList();

					if(receipt.Count == 0)
						return new BasicDataResponse { Message = "No user found whit this login", Status = HttpStatusCode.BadRequest };

					var account = receipt[0];
					if(account == null)
						return new BasicDataResponse { Message = "No user found whit this login", Status = HttpStatusCode.BadRequest };

					if(account.password != password)
						return new BasicDataResponse { Message = "Password is not correct", Status = HttpStatusCode.BadRequest };
				}
				return new BasicDataResponse { Message = "OK", Status = HttpStatusCode.OK };
			}
			catch (InvalidOperationException)
			{
				return new BasicDataResponse { Message = "SQLError", Status = HttpStatusCode.InternalServerError };
			}
		}

		public static BasicDataResponse ExistsWithUsername(string username)
		{
			try
			{
				using (Models.popopopoEntities bdd = new popopopoEntities())
				{
					//TODO : Check if there is no other email (there we consider the add function worked well)
					var receipt = bdd.Accounts.Where(a => a.username == username).ToList();

					if (receipt.Count == 0)
						return new BasicDataResponse { Message = "No user found whit this login", Status = HttpStatusCode.BadRequest };

					var account = receipt[0];
					if (account == null)
						return new BasicDataResponse { Message = "No user found whit this login", Status = HttpStatusCode.BadRequest };
				}
				return new BasicDataResponse { Message = "OK", Status = HttpStatusCode.OK };
			}
			catch (InvalidOperationException)
			{
				return new BasicDataResponse { Message = "SQLError", Status = HttpStatusCode.InternalServerError };
			}
		}

		public static BasicDataResponse ExistsWithEmail(string email, string password)
		{
			try
			{
				using (Models.popopopoEntities bdd = new popopopoEntities())
				{
					//TODO : Check if there is no other username (there we consider the add function worked well)
					var receipt = bdd.Accounts.Where(a => a.email == email).ToList();

					if (receipt.Count == 0)
						return new BasicDataResponse { Message = "No user found whit this login", Status = HttpStatusCode.BadRequest };

					var account = receipt[0];
					if (account == null)
						return new BasicDataResponse { Message = "No user found whit this login", Status = HttpStatusCode.BadRequest };

					if (account.password != password)
						return new BasicDataResponse { Message = "Password is not correct", Status = HttpStatusCode.BadRequest };
				}
				return new BasicDataResponse { Message = "OK", Status = HttpStatusCode.OK };
			}
			catch (InvalidOperationException)
			{
				return new BasicDataResponse { Message = "SQLError", Status = HttpStatusCode.InternalServerError };
			}
		}

		public static AccountResponse GetUserInfoById(int id)
		{
			try
			{
				using (Models.popopopoEntities bdd = new popopopoEntities())
				{
					var receipt = bdd.Accounts.Where(a => a.ID == id).ToList();

					if (receipt.Count == 0)
						return new AccountResponse { Message = "No user found whit this id", Status = HttpStatusCode.BadRequest, Account = null};

					var account = receipt[0];
					if (account == null)
						return new AccountResponse { Message = "No user found whit this id", Status = HttpStatusCode.BadRequest, Account = null };

					return new AccountResponse { Message = "OK", Status = HttpStatusCode.OK, Account = account };
				}
			}
			catch (Exception)
			{
				return new AccountResponse { Status = HttpStatusCode.InternalServerError, Account = null, Message = "Internal server Error" };
			}
		}

		public static AccountResponse GetUserInfoByUsername(string username)
		{
			try
			{
				using (Models.popopopoEntities bdd = new popopopoEntities())
				{
					var receipt = bdd.Accounts.Where(a => a.username == username).ToList();

					if (receipt.Count == 0)
						return new AccountResponse { Message = "No user found whit this username", Status = HttpStatusCode.BadRequest, Account = null};

					var account = receipt[0];
					if (account == null)
						return new AccountResponse { Message = "No user found whit this username", Status = HttpStatusCode.BadRequest, Account = null };

					return new AccountResponse { Message = "OK", Status = HttpStatusCode.OK, Account = account };
				}
			}
			catch (Exception)
			{
				return new AccountResponse { Status = HttpStatusCode.InternalServerError, Account = null, Message = "Internal server Error" };
			}
		}

		public static AccountResponse GetUserInfoByUsername(string username, string password)
		{
			try
			{
				using (Models.popopopoEntities bdd = new popopopoEntities())
				{
					var receipt = bdd.Accounts.Where(a => a.username == username).ToList();

					if (receipt.Count == 0)
						return new AccountResponse { Message = "No user found whit this username", Status = HttpStatusCode.BadRequest, Account = null };

					var account = receipt[0];
					if (account == null)
						return new AccountResponse { Message = "No user found whit this username", Status = HttpStatusCode.BadRequest, Account = null };

					if (account.password != password)
						return new AccountResponse { Message = "Password is not correct", Status = HttpStatusCode.BadRequest, Account = null };

					return new AccountResponse { Message = "OK", Status = HttpStatusCode.OK, Account = account };
				}
			}
			catch (Exception)
			{
				return new AccountResponse { Status = HttpStatusCode.InternalServerError, Account = null, Message = "Internal server Error" };
			}
		}

		public static AccountResponse GetUserInfoByEmail(string email)
		{
			try
			{
				using (Models.popopopoEntities bdd = new popopopoEntities())
				{
					var receipt = bdd.Accounts.Where(a => a.email == email).ToList();

					if (receipt.Count == 0)
						return new AccountResponse { Message = "No user found whit this email", Status = HttpStatusCode.BadRequest, Account = null };

					var account = receipt[0];
					if (account == null)
						return new AccountResponse { Message = "No user found whit this email", Status = HttpStatusCode.BadRequest, Account = null };

					return new AccountResponse { Message = "OK", Status = HttpStatusCode.OK, Account = account };
				}
			}
			catch (Exception)
			{
				return new AccountResponse { Status = HttpStatusCode.InternalServerError, Account = null, Message = "Internal server Error" };
			}
		}

		public static AccountResponse GetUserInfoByEmail(string email, string password)
		{
			try
			{
				using (Models.popopopoEntities bdd = new popopopoEntities())
				{
					var receipt = bdd.Accounts.Where(a => a.email == email).ToList();

					if (receipt.Count == 0)
						return new AccountResponse { Message = "No user found whit this email", Status = HttpStatusCode.BadRequest, Account = null };

					var account = receipt[0];
					if (account == null)
						return new AccountResponse { Message = "No user found whit this email", Status = HttpStatusCode.BadRequest, Account = null };

					if (account.password != password)
						return new AccountResponse { Message = "Password is not correct", Status = HttpStatusCode.BadRequest, Account = null };

					return new AccountResponse { Message = "OK", Status = HttpStatusCode.OK, Account = account };
				}
			}
			catch (Exception)
			{
				return new AccountResponse { Status = HttpStatusCode.InternalServerError, Account = null, Message = "Internal server Error" };
			}
		}

		public static CreateAccountResponse AddAccount(Account account)
		{
			try
			{
				using (Models.popopopoEntities bdd = new popopopoEntities())
				{
					bool alreadyHasEmail = bdd.Accounts.Where(a => a.email == account.email).Count() != 0;
					bool alreadyHasUsername = bdd.Accounts.Where(a => a.username == account.username).Count() != 0;
					bool alreadyHasId = true;

					do
					{
						account.ID = ToolsBox.GetNewId();
						alreadyHasId = bdd.Accounts.Where(a => a.ID == account.ID).Count() != 0;
					} while (alreadyHasId);

					if (alreadyHasEmail)
						return new CreateAccountResponse { Message = "EmailTaken", Status = HttpStatusCode.BadRequest };

					if (alreadyHasUsername)
						return new CreateAccountResponse { Message = "UsernameTaken", Status = HttpStatusCode.BadRequest };

					bdd.Accounts.Add(account);
					bdd.SaveChanges();
				}

				return new CreateAccountResponse { Message = "OK", Status = HttpStatusCode.OK, Id = account.ID};
			}
			catch (InvalidOperationException)
			{
				return new CreateAccountResponse { Message = "SQLError", Status = HttpStatusCode.InternalServerError };
			}
		}
	}
}
