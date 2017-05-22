using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TF;
using System.Collections;
using Xamarin.Forms;
using SQLite;

namespace TF
{
	public abstract class DataBaseService
	{
		static object locker = new object();

		public virtual SQLiteConnection Connection
		{
			get; set;
		}

		public DataBaseService()
		{
			CreateDB();
		}

		public void CreateDB()
		{
			CreateTable<TriathlonTraining>();            
		}

		public void CreateTable<T>()
		{
			lock (locker)
			{
				Connection.CreateTable<T>();
			}
		}

		public IEnumerable<T> Select<T>(String statement)
			where T : new()
		{
			lock (locker)
			{
				var cmd = Connection.CreateCommand(statement);
				var result = cmd.ExecuteQuery<T>();
				return result;
			}
		}

		public void Delete(string statement)
		{
			try
			{
				lock (locker)
				{
					var cmd = Connection.CreateCommand(statement);
					cmd.ExecuteNonQuery();
				}
			}
			catch (SQLiteException ex)
			{
				System.Diagnostics.Debug.WriteLine("Deletion failed: " + ex.Message);
				throw ex;
			}
		}


        public void Insert(string tablename, Dictionary<string, object> fields)
        {
            try
            {
                lock (locker)
                {
                    var statement = "INSERT INTO " + tablename;
                    var fieldsToInsert = " (";
                    var valuesToInsert = " (";

                    foreach (KeyValuePair<string, object> o in fields)
                    {
                        fieldsToInsert += o.Key + ",";
                        valuesToInsert += "@" + o.Key + ",";
                    }
                    fieldsToInsert = fieldsToInsert.Substring(0, fieldsToInsert.Length - 1) + ") ";
                    valuesToInsert = valuesToInsert.Substring(0, valuesToInsert.Length - 1) + ") ";
                    statement += fieldsToInsert + " VALUES " + valuesToInsert;

                    var cmd = Connection.CreateCommand(statement);
                    foreach (KeyValuePair<string, object> o in fields)
                    {
                        cmd.Bind("@" + o.Key, o.Value);
                    }
                    cmd.ExecuteScalar<string>();
                }
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine("Insert failed: " + ex.Message);
                throw ex;
            }
        }


        public void SaveTrinathlonTraining(TriathlonTraining item)
		{
			Dictionary<string, object> fields = new Dictionary<string, object>();

			fields.Add("Date", item.Date);
			fields.Add("Distance", item.Distance);
			fields.Add("Time", item.Time);
			fields.Add("Type", item.Type);
			fields.Add("Id", item.Id);
			fields.Add("UserId", item.UserId);

            Insert("TriathlonTraining", fields);
        }     

		public IEnumerable<TriathlonTraining> GetTriathlonTrainings()
		{
			string sqlRequest = @"SELECT * FROM TriathlonTable";
			var list = Select<TriathlonTraining>(sqlRequest);
			return list;
		}

		public TriathlonTraining GetTriathlonTrainingById(int id)
		{
			string sqlRequest = @"SELECT * FROM TriathlonTable WHERE id == " + id.ToString();
			var list = Select<TriathlonTraining>(sqlRequest);
			return list != null && (list as List<TriathlonTraining>).Count > 0 ? (list as List<TriathlonTraining>)[0]
				: new TriathlonTraining();
		}        
	}
}
