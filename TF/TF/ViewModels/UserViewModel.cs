using System;
using System.Threading.Tasks;
using TF.Services;
using TF.ViewModels;
using System.Collections.Generic;
namespace TF
{
	public class UserViewModel : BaseViewModel
	{
		static object ob = new object ();
		private static UserViewModel instance;
		public static UserViewModel Instance {
			get {
				lock (ob) {
					if (instance == null) {
						instance = new UserViewModel ();
					}
				}

				return instance;
			}
		}

		private User user;
		public User User {
			get { return user; }
			set { user = value; }
		}

		private List<Feedback> feedbackList;
		public List<Feedback> FeedbackList {
			get { return feedbackList;}
			set { feedbackList = value;}
		}

		public void Initialize ()
		{
			if (user == null)
				user = new User { Role = User.UserRole.Teacher };
			if (feedbackList == null)
				feedbackList = new List<Feedback> ();

			feedbackList.Add (new Feedback { Description = "5. Good result. Continue" , Date = new DateTime (2017, 5, 4)});
			feedbackList.Add (new Feedback { Description = "2. Are you training at all??", Date = new DateTime (2017, 5, 3) });
			feedbackList.Add (new Feedback { Description = "4. You could do better", Date = new DateTime (2017, 5, 1)});
		}

		public async Task<Result> AddNewUser ()
		{
			var user = new User { Name = "new user", Role = User.UserRole.Student, ID = 111 };
			var res = await OnlineService.CreateUserAsync (user);
			return res;
		}

		#region strings
		public string StringEmail { get { return StringService.Instance.Email; } }
		public string StringName { get { return StringService.Instance.Name; } }
		public string StringPassword { get { return StringService.Instance.Password; } }
		public string StringConfirmPassword { get { return StringService.Instance.ConfirmPassword; } }
		public string StringRegister { get { return StringService.Instance.Register; } }
		public string StringLogin { get { return StringService.Instance.Login; } }
		public string StringNameIsRequired { get { return StringService.Instance.NameIsRequired; } }
		public string StringAlreadyHaveAccount { get { return StringService.Instance.AlreadyHaveAccount; } }
		public string StringEmailRequired { get { return StringService.Instance.EmailRequired; } }
		public string StringDontHaveAccount { get { return StringService.Instance.DontHaveAccount; } }
		public string StringStudent { get { return StringService.Instance.Student; } }
		public string StringTeacher { get { return StringService.Instance.Teacher; } }
		public string StringPrivate { get { return StringService.Instance.Private; } }
		public string StringSelectMode { get { return StringService.Instance.SelectMode; } }
		public string StringConfirmPasswordRequired { get { return StringService.Instance.ConfirmPasswordRequired; } }
		public string StringPasswordRequired { get { return StringService.Instance.PasswordRequired; } }
		public string StringLoginToAccount { get { return StringService.Instance.LoginToAccount; } }
		public string StringOr { get { return StringService.Instance.Or; } }
		public string StringContinueWithoutLogging { get { return StringService.Instance.ContinueWithoutLogging; } }
		public string DataMayBeLost { get { return StringService.Instance.DataMayBeLost; } }
		public string StringCreateAccount { get { return StringService.Instance.CreateAccount; } }

  #endregion
	}
}
