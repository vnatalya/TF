using System;
using System.Threading.Tasks;
using TF.Services;
using TF.ViewModels;
namespace TF
{
	public class UserViewModel : BaseViewModel
	{
		static object ob = new object ();
		private static UserViewModel instance;
		public static UserViewModel Instance 
		{
			get 
			{
				lock(ob) 
				{
					if (instance == null) 
					{
						instance = new UserViewModel ();
					}
				}

				return instance;
			}
		}

		private User user;
		public User User 
		{
			get { return user; }
			set { user = value; }
		}

		public void Initialize ()
		{
			if (user == null)
				user = new User { Role = User.UserRole.Student };
		}

		public async Task<Result> AddNewUser ()
		{
            var user = new User { Name = "new user", Role = User.UserRole.Student, ID = 111 };
            var res = await OnlineService.CreateUserAsync(user);
			return res;
		}
	}
}
