using System;
using System.Threading.Tasks;
using TF.Services;
using TF.ViewModels;
using System.Collections.Generic;
namespace TF
{
	public class UserViewModel : BaseViewModel
	{
		#region properties and fields
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
		public User User 
		{
			get { return user; }
			set { user = value; }
		}

		private List<Feedback> feedbackList;
		public List<Feedback> FeedbackList 
		{
			get { return feedbackList;}
			set { feedbackList = value;}
		}


		private List<Group> groupsList;
		public List<Group> GroupsList 
		{
			get { return groupsList; }
			set { groupsList = value; }
		}

		private List<User> studentsList;
		public List<User> StudentsList 
		{
			get { return studentsList; }
			set { studentsList = value; }
		}

		private List<TriathlonTraining> currentTrainings;
		public List<TriathlonTraining> Trainings 
		{
			get { return currentTrainings; }
			set { currentTrainings = value; }
		}

		private User currentStudent;
		public User CurrentStudent 
		{
			get { return currentStudent; }
			set { currentStudent = value; }
		}

		private Group currentGroup;
		public Group CurrentGroup 
		{
			get { return currentGroup; }
			set { currentGroup = value; }
		}
#endregion
		#region Methods
		public void Initialize ()
		{
			if (user == null)
				user = new User { Role = User.UserRole.Teacher };
			if (feedbackList == null)
				feedbackList = new List<Feedback> ();

			feedbackList.Add (new Feedback { Description = "5. Good result. Continue" , Date = new DateTime (2017, 5, 4)});
			feedbackList.Add (new Feedback { Description = "2. Are you training at all??", Date = new DateTime (2017, 5, 3) });
			feedbackList.Add (new Feedback { Description = "4. You could do better", Date = new DateTime (2017, 5, 1) });
			feedbackList.Add (new Feedback { Description = "4. Good training", Date = new DateTime (2017, 4, 29) });
			feedbackList.Add (new Feedback { Description = "5. Not bad", Date = new DateTime (2017, 4, 28) });
			feedbackList.Add (new Feedback { Description = "4. Pretty well", Date = new DateTime (2017, 4, 27)});



			if (studentsList == null)
				studentsList = new List<User> ();

			studentsList.Add (new User { Name = "Anna"});
			studentsList.Add (new User { Name = "Vova" });
			studentsList.Add (new User { Name = "Vasya"});
			studentsList.Add (new User { Name = "Top"});
			studentsList.Add (new User { Name = "Jerry"});
			studentsList.Add (new User { Name = "Vinny"});

			if (groupsList == null)
				groupsList = new List<Group> ();

			groupsList.Add (new Group { Students = studentsList, Name = "Awesome group" });
			groupsList.Add (new Group { Students = studentsList, Name = "New group" });
			groupsList.Add (new Group { Students = studentsList, Name = "New new group" });
			groupsList.Add (new Group { Students = studentsList, Name = "New new new group" });
			groupsList.Add (new Group { Students = studentsList, Name = "My favoirites" });
			groupsList.Add (new Group { Students = studentsList, Name = "Interesting name for group" });
			groupsList.Add (new Group { Students = studentsList, Name = "My first group" });
		}

		public async Task<Result> AddNewUser ()
		{
			IsBusy = true;
			var user = new User { Name = "new user", Role = User.UserRole.Student, ID = 111 };
			var res = await OnlineService.CreateUserAsync (user);
			IsBusy = false;
			return res;
		}

#region teacher
		public async Task<Result> AddNewGroup (string name)
		{
			if (string.IsNullOrWhiteSpace (name))
				return new Result (false, StringService.Instance.Error, StringService.Instance.NameIsRequired);
			IsBusy = true;
			var group = new Group { Name = name, TeacherID = user.ID };
			var res = await OnlineService.AddGroupAsync (group);
			IsBusy = false;
			return res;
		}

		public async Task<Result> AddFeedback (string feedbackValue)
		{
			if (string.IsNullOrWhiteSpace (feedbackValue))
				return new Result (false, StringService.Instance.Error, StringService.Instance.NameIsRequired);
			IsBusy = true;
			var feedback = new Feedback {
				Date = DateTime.Today,
				Description = feedbackValue,
				StudentID = currentStudent.ID
			};
			var res = await OnlineService.AddFeedbackAsync (feedback);
			IsBusy = false;
			return res;
		}

		public async Task<Result> DeleteFeedback (int id)
		{
			IsBusy = true;
			var res = await OnlineService.DeleteFeedbackAsync (id);
			IsBusy = false;
			return res;
		}

		public async Task<Result> DeleteStudentFromGroup (int id)
		{
			IsBusy = true;
			var res = await OnlineService.DeleteStudentFromGroupAsync  (id);
			IsBusy = false;
			return res;
		}

		public async Task<Result> DeleteGroup (int id)
		{
			IsBusy = true;
			var res = await OnlineService.DeleteGroupAsync(id);
			IsBusy = false;
			return res;
		}
#endregion

		#endregion
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
		public string StringEnterFeedback { get { return StringService.Instance.EnterFeedback; } }
		public string StringAddGroup { get { return StringService.Instance.AddGroup; } }

  #endregion
	}
}
