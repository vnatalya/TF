﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TF
{
    public class StringService
    {
        public virtual void Init()
        { }

        public StringService()
        {
            Init();
        }

        private static object lockObject = new object();

        private static StringService instance;
        public static StringService Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                        instance = new LocalizationManager();
                    return instance;
                }
            }
        }

        protected string trainingSaved;
        public string couldntSave;
        protected string incorrectDistance;
        protected string incorrectTime;
        protected string incorrectType;
        protected string swimming;
        protected string backstroke;
        protected string breaststroke;
        protected string butterfly;
        protected string swimmingExercices;
        protected string running;
        protected string run;
        protected string runningExercises;
        protected string cycling;
        protected string bike;
        protected string trainer; 
        protected string triathlon;
        protected string freestyle;
        protected string history;
        protected string all;
        protected string today;
        protected string week;
        protected string month;
        protected string type;
        protected string time;
        protected string distance;
        protected string date;
        protected string deleteAlert;
        protected string start;
        protected string none;
        protected string finish;
        protected string m;
        protected string km;
        protected string choose;
        protected string period;
        protected string numberOfTrainings;
        protected string error;
        protected string ok;
        protected string startDate;
		protected string endDate;
		protected string logout;
		protected string myGroups;
		protected string myResults;
		protected string addFeedback;
		protected string myFeedbacks;
		protected string addGroup;
		protected string addNewTraining;
		protected string group;

        public string TrainingSaved { get { return trainingSaved; } }
        public string CouldntSave { get { return couldntSave; } }
        public string IncorrectDistance { get { return incorrectDistance; } }
        public string IncorrectTime { get { return incorrectTime; } }
        public string Breaststroke { get { return breaststroke; } }
        public string Backstroke { get { return backstroke; } }
        public string Butterfly { get { return butterfly; } }
        public string SwimmingExercices { get { return swimmingExercices; } }
        public string Running { get { return running; } }
        public string RunningExercises { get { return runningExercises; } }
        public string Run { get { return run; } }
        public string Cycling { get { return cycling; } }
        public string Bike { get { return bike; } }
        public string Trainer { get { return trainer; } }
        public string Triathlon { get { return triathlon; } }
        public string Swimming { get { return swimming; } }
        public string Freestyle { get { return freestyle; } }
        public string History { get { return history; } }
        public string All { get { return all; } }
        public string Today { get { return today; } }
        public string Week { get { return week; } }
        public string Month { get { return month; } }
        public string IncorrectType { get { return incorrectType; } }
        public string Type { get { return type; } }
        public string Time { get { return time; } }
        public string Distance { get { return distance; } }
        public string Date { get { return date; } }
        public string DeleteAlert { get { return deleteAlert; } }
        public string Start { get { return start; } }
        public string Finish { get { return finish; } }
        public string None { get { return none; } }
        public string M { get { return m; } }
        public string Km { get { return km; } }
        public string Ok { get { return ok; } }
        public string Choose { get { return choose; } }
        public string Period { get { return period; } }
        public string NumberOfTrainings { get { return numberOfTrainings; } }
        public string Error { get { return error; } }
        public string StartDate { get { return startDate; } }
		public string EndDate { get { return endDate; } }
		public string Logout { get { return logout; } }
		public string MyGroups { get { return myGroups; } }
		public string MyResults { get { return myResults; } }
		public string AddFeedback { get { return addFeedback; } }
		public string MyFeedbacks { get { return myFeedbacks; } }
		public string AddGroup { get { return addGroup; } }
		public string AddNewTraining { get { return addNewTraining; } }
        public string Group { get { return group; } }
    }
}
