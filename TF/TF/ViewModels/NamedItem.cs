using System;
namespace TF
{
	public class NamedItem
	{
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public NamedItem(string name)
		{
			this.Name = name;
		}
	}
}
