using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TF
{
	public class User
	{
        [JsonIgnore]
		public List<TriathlonTraining> Trainings { get; set; }
		public int ID { get; set; }
        [JsonIgnore]
		public int GroupID { get; set; }
		public string Name { get; set; }
        [JsonIgnore]
		public List<Group> Groups { get; set;} 
		public UserRole Role { get; set; }
        [JsonIgnore]
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
