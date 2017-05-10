﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TF
{
    public class TriathlonTraining
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
            date = DateTime.MinValue;
            type = TriathlonType.Triathlon;
        }

        public string DisplayItem
        {
            get
            {
                return string.Format("{0}: {1} \n {2}: {3} \n {4}: {5}",
                    StringService.Instance.NumberOfTrainings, DisplayType,
                    StringService.Instance.Time, time,
                    StringService.Instance.Distance, distance);
            }
        }
    }
}
