using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TF.Helpers;

namespace TF
{
	public class TriathlonTraining : ObservableObject
    {
        public enum TriathlonType
        {
            None = -1,
            Triathlon = 0,
            Swimming = 10,
            Running = 20,
            Cycling = 30,
            SwimmingExercises = 11,
            Butterfly = 12,
            Frrestyle = 13,
            Breaststroke = 14,
            Backstroke = 15,
            RunningExercises = 21,
            Run = 22,
            Bike = 31,
            Trainer = 32
        };

        public string DisplayType
        {
            get
            {
                string displayValue = string.Empty;
                switch (type)
                {
                    case TriathlonType.SwimmingExercises:
                        displayValue = StringService.Instance.SwimmingExercices;
                        break;
                    case TriathlonType.Butterfly:
                        displayValue = StringService.Instance.Butterfly;
                        break;
                    case TriathlonType.Frrestyle:
                        displayValue = StringService.Instance.Freestyle;
                        break;
                    case TriathlonType.Backstroke:
                        displayValue = StringService.Instance.Backstroke;
                        break;
                    case TriathlonType.Breaststroke:
                        displayValue = StringService.Instance.Breaststroke;
                        break;
                    case TriathlonType.Run:
                        displayValue = StringService.Instance.Run;
                        break;
					case TriathlonType.Running:
						displayValue = StringService.Instance.Running;
						break;
                    case TriathlonType.RunningExercises:
                        displayValue = StringService.Instance.RunningExercises;
                        break;
                    case TriathlonType.Bike:
                        displayValue = StringService.Instance.Bike;
                        break;
                    case TriathlonType.Trainer:
                        displayValue = StringService.Instance.Trainer;
                        break;
                    case TriathlonType.None:
                        displayValue = StringService.Instance.None;
                        break; 
					case TriathlonType.Triathlon:
						displayValue = StringService.Instance.Triathlon;
						break;
				}
                return displayValue;
            }
        }

        private TriathlonType type;
        public TriathlonType Type
        {
            get { return type; }
            set
            {
                type = value;
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
            }
        }

		public string DisplayDate
		{
			get { return date.Date.ToString("MM/dd/yy", DateTimeFormatInfo.InvariantInfo); }
		}

        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set
            {
                time = value;
            }
        }

        private double distance;
        public double Distance
        {
            get { return distance; }
            set
            {
                distance = value;
            }
        }

        public int UserId { get; set; }

        private int id;
        public int Id
        {
            get; set;
        }

        public TriathlonTraining()
        {
            id = -1;
            distance = 0;
            time = TimeSpan.Zero;
			date = DateTime.Today;
            type = TriathlonType.Triathlon;
        }

		public TriathlonTraining(TriathlonType type, TimeSpan time, double distance, DateTime date)
		{
			this.type = type;
			this.time = time;
			this.distance = distance;
			this.date = date;
		}

        public string DisplayItem
        {
            get
            {
				return string.Format ("{0}: {1} \n{2}: {3} \n{4}: {5} {6} \n{7}: {8}",
									 StringService.Instance.Type, DisplayType,
					StringService.Instance.Time, time,
				                      StringService.Instance.Distance, distance, StringService.Instance.Km,
				                      StringService.Instance.Date, DisplayDate);
            }
        }
    }
}
