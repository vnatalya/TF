using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.Helpers;
using static TF.TriathlonTraining;

namespace TF
{
    public class TriathlonViewModel : ObservableObject
    {
        private static TriathlonViewModel instance;
        public static TriathlonViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TriathlonViewModel();
                }
                
                return instance;
            }
        }

        //private static StringService.Instance StringService.Instance;
        //public static StringService.Instance StringService.Instance
        //{
        //    get
        //    {
        //        if (StringService.Instance == null)
        //            throw new NullReferenceException(string.Format("ViewModelBase.StringService.Instance has not been set. Derive a class from Shared.Localization.StringService.InstanceBase, override Init() metod and init mentioned reference in {0}", devicePlatform.EntryPointName));

        //        return StringService.Instance;
        //    }

        //    set
        //    {
        //        StringService.Instance = value;
        //    }
        //}

        private TriathlonTraining currentItem;
        public TriathlonTraining CurrentItem
        {
            get { return currentItem; }
        }

        public List<string> AllHeadersList;
        public Dictionary<string, List<string>> AllChildrenList;

        public List<string> RegisterHeadersList;
        public Dictionary<string, List<string>> RegisterChildrenList;

        private List<TriathlonTraining> trainings;
        public List<TriathlonTraining> Trainings
        {
			get { return trainings;}
        }

        private TriathlonType currentType;
        public TriathlonType CurrentType
        {
            get { return currentType; }
            set { currentType = value; }
        }

        private PeriodType currentPeriod;
        public PeriodType CurrentPeriod
        {
            get { return currentPeriod; }
            set { currentPeriod = value; }
        }

		public List<NamedItem> SwimmingList { get; set; }
		public List<NamedItem> CyclingList { get; set; }
		public List<NamedItem> RunningList { get; set; }

		public void Initialize()
        {
            if (devicePlatform == null)
            {
#if __ANDROID__
                devicePlatform = DevicePlatform.Android;
#else                
                devicePlatform = DevicePlatform.iOS;
#endif
            }

            if (trainings == null)
                trainings = new List<TriathlonTraining>();
			trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 1)));
			trainings.Add(new TriathlonTraining(TriathlonType.Run, new TimeSpan(0, 40, 0), 5, new DateTime(2017, 5, 3)));
			trainings.Add(new TriathlonTraining(TriathlonType.Run, new TimeSpan(1, 0, 0), 8, new DateTime(2017, 5, 4)));
			trainings.Add(new TriathlonTraining(TriathlonType.SwimmingExercises, new TimeSpan(0, 20, 0), 2, new DateTime(2017, 5, 1)));
			trainings.Add(new TriathlonTraining(TriathlonType.RunningExercises, new TimeSpan(1, 0, 0), 5, new DateTime(2017, 5, 6)));
			trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 7)));
			trainings.Add(new TriathlonTraining(TriathlonType.Breaststroke, new TimeSpan(0, 15, 0), 1, new DateTime(2017, 5, 2)));
			trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(0, 50, 0), 15, new DateTime(2017, 4, 29)));
			trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 1)));
			//trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 1)));
			//trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 1)));
			//trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 1)));
			//trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 1)));
            SetTriathlonTypesLists();
            currentPeriod = PeriodType.All;
            currentType = TriathlonType.Triathlon;
        }

		void SetTriathlonTypesLists()
		{
			RegisterHeadersList = new List<string>();
			RegisterChildrenList = new Dictionary<string, List<string>>();

			RegisterHeadersList.Add(StringService.Instance.Triathlon);
			RegisterHeadersList.Add(StringService.Instance.Swimming);
			RegisterHeadersList.Add(StringService.Instance.Running);
			RegisterHeadersList.Add(StringService.Instance.Cycling);

			SwimmingList = new List<NamedItem>();
			var ni = new NamedItem();
			ni.Name = "sdkfksdfhsdjhf";
			SwimmingList.Add(ni);
			SwimmingList.Add(new NamedItem(StringService.Instance.Butterfly));
			SwimmingList.Add(new NamedItem(StringService.Instance.Freestyle));
			SwimmingList.Add(new NamedItem(StringService.Instance.Breaststroke));
			SwimmingList.Add(new NamedItem(StringService.Instance.Backstroke));
			SwimmingList.Add(new NamedItem(StringService.Instance.SwimmingExercices));

			//    RegisterChildrenList.Add(StringService.Instance.Swimming, swimmingListView);

			RunningList = new List<NamedItem>();
			RunningList.Add(new NamedItem(StringService.Instance.Run));
			RunningList.Add(new NamedItem(StringService.Instance.RunningExercises));

			//RegisterChildrenList.Add(StringService.Instance.Running, runningListView);

			CyclingList = new List<NamedItem>();
			CyclingList.Add(new NamedItem { Name = StringService.Instance.Bike });
			CyclingList.Add(new NamedItem { Name = StringService.Instance.Trainer });

			//RegisterChildrenList.Add(StringService.Instance.Cycling, cyclingListView);

			AllHeadersList = RegisterHeadersList;
			AllHeadersList.Add(StringService.Instance.History);

			AllChildrenList = RegisterChildrenList;

			var historyListView = new List<string>();
			historyListView.Add(StringService.Instance.All);
			historyListView.Add(StringService.Instance.Today);
			historyListView.Add(StringService.Instance.Week);
			historyListView.Add(StringService.Instance.Month);
			AllChildrenList.Add(StringService.Instance.History, historyListView);
		}

        public void SetCurrentItem(int id = -1) {
            if (id == -1)
            {
                currentItem = new TriathlonTraining();
            }
            else
            {
                currentItem = dbService.GetTriathlonTrainingById(id);
            }
        }

        public Result SaveCurrentItem()
        {
            Result result = new Result(true, StringService.Instance.TrainingSaved, string.Empty);

            if (currentItem.Type == TriathlonType.Triathlon || currentItem.Type == TriathlonType.Swimming || currentItem.Type == TriathlonType.Running || currentItem.Type == TriathlonType.Cycling)
            {
                result.Status = false;
                result.Title = StringService.Instance.CouldntSave;
                result.ErrorMessage = StringService.Instance.IncorrectType;
                return result;
            }

            if (currentItem.Distance == 0 && currentItem.Type != TriathlonType.Trainer)
            {
                result.Status = false;
                result.Title = StringService.Instance.CouldntSave;
                result.ErrorMessage = StringService.Instance.IncorrectDistance;
                return result;
            }

            if (currentItem.Time == TimeSpan.Zero)
            {
                result.Status = false;
                result.Title = StringService.Instance.CouldntSave;
                result.ErrorMessage = StringService.Instance.IncorrectTime;
                return result;
            }
            dbService.SaveTrinathlonTraining(currentItem);
            return result;
        }

        static DevicePlatform devicePlatform;
        
        private static DataBaseService dbService;
        public static DataBaseService DBService
        {
            get
            {
                if (dbService == null)
                    throw new NullReferenceException(string.Format("ViewModelBase.DBService has not been set. Derive a class from Shared.Services.DBServiceBase, override Init() method and init mentioned reference in {0}", devicePlatform.EntryPointName));

                return dbService;
            }
            set
            {
                dbService = value;
            }
        }

#region strings
        public string StringType { get { return StringService.Instance.Type; } }
		public string StringTriathlon { get { return StringService.Instance.Triathlon; } }
		public string StringSwimming { get { return StringService.Instance.Swimming; } }
		public string StringRunning { get { return StringService.Instance.Running; } }
		public string StringCycling { get { return StringService.Instance.Cycling; } }
        public string StringDistance { get { return StringService.Instance.Distance; } }
        public string StringTime { get { return StringService.Instance.Time; } }
        public string StringDate { get { return StringService.Instance.Date; } }
        public string StringStart { get { return StringService.Instance.Start; } }
        public string StringFinish { get { return StringService.Instance.Finish; } }
        public string DisplayPeriod
        {
            get
            {
                string period = string.Empty;
                switch(currentPeriod)
                {
                    case PeriodType.All:
                        period = StringService.Instance.All;
                        break;
                    case PeriodType.Today:
                        period = StringService.Instance.Today;
                        break;
                    case PeriodType.Week:
                        period = StringService.Instance.Week;
                        break;
                    case PeriodType.Month:
                        period = StringService.Instance.Month;
                        break;
                    case PeriodType.Choose:
                        period = StringService.Instance.Choose;
                        break;
                }
                return string.Format("{0}: {1}", StringService.Instance.Period, period);
            }
        }

        public string DisplaySummary
        {
            get
            {
                //return string.Format("{0}: {1} \n {2}: {3} \n {4}: {5} \n {6}: {7}",
				            //         StringService.Instance.Type, ,
                //    StringService.Instance.Time, "",
                //    StringService.Instance.Distance, distance,
                //    StringService.Instance.Date, date);
                return "summary";
            }
        }

		public string DisplayType
		{
			get
			{
				string displayValue = string.Empty;
				switch (currentType)
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

		#endregion

		#region period enum
		public enum PeriodType
        {
            All = -1,
            Today = 0,
            Week = 1,
            Month = 2,
            Choose = 3
        }
        #endregion

    }
}
