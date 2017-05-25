using System;
using System.Globalization;

namespace TF
{
	public class Feedback
	{
		public int StudentID { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public string DisplayDate { get { return Date.Date.ToString ("d", DateTimeFormatInfo.InvariantInfo); } }
		public int Id { get; set; }

		public Feedback ()
		{
		}
	}
}
