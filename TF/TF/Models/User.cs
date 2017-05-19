using System;
using System.Collections.Generic;

namespace TF
{
	public class User
	{
		public List<TriathlonTraining> Trainings { get; set; }
		public int ID { get; set; }
		public int GroupID { get; set; }
		public string Name { get; set; }
		public List<Group> Groups { get; set;} 
		public UserRole Role { get; set; }
		public List<Feedback> Feedbacks;

		public User ()
		{
		}

		public enum UserRole
		{
			Private = 0,
			Student = 1,
			Teacher = 2
		}
	}
}
