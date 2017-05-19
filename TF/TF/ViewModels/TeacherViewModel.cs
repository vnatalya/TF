using System;
using TF.ViewModels;
using System.Collections.Generic;

namespace TF
{
	public class TeacherViewModel : BaseViewModel
	{
		static object ob = new object ();
		private static TeacherViewModel instance;
		public static TeacherViewModel Instance 
		{
			get 
			{
				lock (ob) 
				{
					if (instance == null) 
					{
						instance = new TeacherViewModel ();
					}
				}

				return instance;
			}
		}

		private List<Group> groups;
		public List<Group> Groups 
		{
			get { return groups; }
			set { groups = value; }
		}

		private List<User> currentStudents;
		public List<User> CurrentStudents 
		{
			get { return currentStudents; }
			set { currentStudents = value; }
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

		public void Initialize ()
		{
			
		}

		public Result AddNewGroup (Group newGroup)
		{
			return new Result (true);
		}

		public Result AddFeedback (string feedbackValue)
		{
			var feedback = new Feedback 
			{
				Date = DateTime.Today,
				Description = feedbackValue,
				StudentID = currentStudent.ID
			};
			return new Result (true);
		}
	}
}
