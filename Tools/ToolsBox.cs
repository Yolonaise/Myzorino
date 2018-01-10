using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class ToolsBox
    {
		public static bool IsEmailValid(string address)
		{
			try
			{
				MailAddress m = new MailAddress(address);
				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}

		public static int GetNewId()
		{
			Random r = new Random();
			return r.Next(int.MaxValue);
		}
    }
}
