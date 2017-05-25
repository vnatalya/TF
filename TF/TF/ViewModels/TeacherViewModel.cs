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



		public void Initialize ()
		{
			
		}


	}
}
