using System;
using System.Collections.Generic;
namespace TF
{
	public class Student
	{
		//private List<TriathlonTraining> trainings;
		public List<TriathlonTraining> Trainings { get; set; }
		public int ID { get; set; }
		public int GroupID { get; set; }
		public string Name { get; set; }
		public Student ()
		{
		}
	}
}
