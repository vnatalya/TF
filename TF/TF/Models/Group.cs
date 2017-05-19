using System;
using System.Collections.Generic;

namespace TF
{
	public class Group
	{
		public List<User> Students { get; set; }
		public int ID { get; set; }
		public int TeacherID { get; set; }
		public string Name { get; set; }
		public Group ()
		{
		}
	}
}
