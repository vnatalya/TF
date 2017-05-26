using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.Helpers;
using Xamarin.Forms;
using static TF.TriathlonTraining;
using TF.ViewModels;
using TF.Services;
using System.Threading.Tasks;
using System.Globalization;

namespace TF
{
	public class TriathlonViewModel : BaseViewModel
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

        private TriathlonTraining currentItem;
        public TriathlonTraining CurrentItem
        {
            get { return currentItem; }
        }

        private List<TriathlonTraining> trainings;
        public List<TriathlonTraining> Trainings
        {
			get
            {
                int aboveTypeValue = (int)currentType % 10 == 0 && currentType != TriathlonType.Triathlon ? (int)currentType  + 9 : (int) currentType;
                var newList = trainings.Where(x => x.Distance > 0); //x => (int) x.Type <= aboveTypeValue && (int) x.Type >= (int) currentType && x.Date.Ticks > startDate.Ticks && x.Date.Ticks < endDate.Ticks);
                var list = newList.ToList();
                return list;
            }
        }

        private TriathlonType currentType;
        public TriathlonType CurrentType
        {
            get { return currentType; }
            set
            {
                currentType = value;
                if (currentItem != null)
                    currentItem.Type = value;
            }
        }

		private bool isEditMode = true;
		public bool IsEditMode 
		{
			get { return isEditMode; }
			set { isEditMode = value; }
		}

        private DateTime startDate = DateTime.MinValue;
        private DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime endDate = DateTime.Today;
        private DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        private PeriodType currentPeriod;
        public PeriodType CurrentPeriod
        {
            get { return currentPeriod; }
            set
            {
                currentPeriod = value;
                switch(value)
                {
                    case PeriodType.Week:
                        endDate = DateTime.Today;
                        startDate = DateTime.Today.AddDays(-7);
                        break;
                    case PeriodType.Month:
                        endDate = DateTime.Today;
                        startDate = DateTime.Today.AddDays(-30);
                        break;
                    case PeriodType.Today:
                        endDate = DateTime.Today;
                        startDate = DateTime.Today;
                        break;
                    case PeriodType.All:
                        endDate = DateTime.Today;
                        startDate = DateTime.MinValue;
                        break;
                }
            }
        }

		public List<NamedItem> SwimmingList { get; set; }
		public List<NamedItem> CyclingList { get; set; }
		public List<NamedItem> RunningList { get; set; }
		public List<NamedItem> PeriodList { get; set; }

		public User.UserRole Mode 
		{
			get { return UserViewModel.Instance.User != null ? UserViewModel.Instance.User.Role : User.UserRole.Teacher; }
		}

        public async void Initialize()
        {
           
            if (trainings == null)
                trainings = new List<TriathlonTraining>();
			//trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 1)));
			trainings.Add(new TriathlonTraining(TriathlonType.Run, new TimeSpan(0, 50, 0), 5, new DateTime(2017, 5, 3)));
			trainings.Add(new TriathlonTraining(TriathlonType.Run, new TimeSpan(1, 0, 0), 8, new DateTime(2017, 5, 4)));
			//trainings.Add(new TriathlonTraining(TriathlonType.SwimmingExercises, new TimeSpan(0, 20, 0), 2, new DateTime(2017, 5, 1)));
			trainings.Add (new TriathlonTraining (TriathlonType.RunningExercises, new TimeSpan (0, 55, 0), 7, new DateTime (2017, 5, 6)));
			trainings.Add (new TriathlonTraining (TriathlonType.RunningExercises, new TimeSpan (1, 10, 0), 10, new DateTime (2017, 5, 10)));
			trainings.Add(new TriathlonTraining(TriathlonType.RunningExercises, new TimeSpan(1, 0, 0), 9, new DateTime(2017, 5, 11)));
			//trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 7)));
			//trainings.Add(new TriathlonTraining(TriathlonType.Breaststroke, new TimeSpan(0, 15, 0), 1, new DateTime(2017, 5, 2)));
			//trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(0, 50, 0), 15, new DateTime(2017, 4, 29)));
			//trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 1)));
			//trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 1)));
			//trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 1)));
			//trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 1)));
			//trainings.Add(new TriathlonTraining(TriathlonType.Bike, new TimeSpan(1, 0, 0), 20, new DateTime(2017, 5, 1)));
            SetTriathlonTypesLists();
            currentPeriod = PeriodType.All;
			currentType = TriathlonType.Running;


        }

		void SetTriathlonTypesLists()
		{

			SwimmingList = new List<NamedItem>();
			SwimmingList.Add(new NamedItem(StringService.Instance.Butterfly));
			SwimmingList.Add(new NamedItem(StringService.Instance.Freestyle));
			SwimmingList.Add(new NamedItem(StringService.Instance.Breaststroke));
			SwimmingList.Add(new NamedItem(StringService.Instance.Backstroke));
			SwimmingList.Add(new NamedItem(StringService.Instance.SwimmingExercices));

			RunningList = new List<NamedItem>();
			RunningList.Add(new NamedItem(StringService.Instance.Run));
			RunningList.Add(new NamedItem(StringService.Instance.RunningExercises));

			CyclingList = new List<NamedItem>();
			CyclingList.Add(new NamedItem { Name = StringService.Instance.Bike });
			CyclingList.Add(new NamedItem { Name = StringService.Instance.Trainer });

			PeriodList = new List<NamedItem>();
            PeriodList.Add(new NamedItem(StringService.Instance.All));
            PeriodList.Add(new NamedItem(StringService.Instance.Today));
            PeriodList.Add(new NamedItem(StringService.Instance.Week));
            PeriodList.Add(new NamedItem(StringService.Instance.Month));
            PeriodList.Add(new NamedItem(StringService.Instance.Choose));
		}

        public void SetCurrentItem(int id = -1) {
            if (id == -1)
            {
                currentItem = new TriathlonTraining();
            }
            else
            {
				currentItem = DBService.GetTriathlonTrainingById(id);
            }
        }

        public async Task<Result> SaveCurrentItem()
        {
            Result result = new Result(true, StringService.Instance.TrainingSaved, string.Empty);

            if ((int)currentItem.Type % 10 == 0)
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
           // result = await  OnlineService.SaveTrainingAsync(currentItem);
			DBService.SaveTrinathlonTraining(currentItem);
            return result;
        }

        public async Task<Result> GetTrainings(int userId = -1)
        {
			IsBusy = true;
			if (userId == -1)
				userId = UserViewModel.Instance.User.ID;
			var res = await OnlineService.GetTrainingsAsync(userId);
			IsBusy = false;
            return res;
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
        public string StringKm { get { return StringService.Instance.Km; } }
        public string StringHours { get { return StringService.Instance.Hours; } }
        public string StringMinutes { get { return StringService.Instance.Minutes; } }
        public string StringSeconds { get { return StringService.Instance.Seconds; } }
		public string StringSave { get { return StringService.Instance.Save; } }
        public string StringAddTraining { get { return StringService.Instance.AddNewTraining; } }

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
					period = string.Format ("{0} - {1}", startDate.ToString ("d", DateTimeFormatInfo.InvariantInfo), endDate.ToString ("d", DateTimeFormatInfo.InvariantInfo));
                        break;
                }
                return string.Format("{0}: {1}", StringService.Instance.Period, period);
            }
        }

        public string DisplaySummary
        {
            get
            {
                TimeSpan totalTime = TimeSpan.Zero;
                double totalDistance = 0;
                for (int i = 0; i < trainings.Count; ++ i)
                {
                    totalDistance += trainings[i].Distance;
                    totalTime += trainings[i].Time;
                }
				return string.Format("{0}: {1} \n{2}: {0:0}{3} {4} {5} {6} {7} {8}\n{9}: {10}",
				                     StringService.Instance.NumberOfTrainings, trainings.Count,
				                     StringService.Instance.Time, totalTime.TotalHours, StringService.Instance.Hours, totalTime.Minutes, StringService.Instance.Minutes, totalTime.Seconds, StringService.Instance.Seconds,
                    StringService.Instance.Distance, totalDistance);
            }
        }

        public string DisplayStartDate
        {
            get { return string.Format("{0}: {1}", StringService.Instance.StartDate, startDate); }
        }

        public string DisplayEndDate
        {
            get { return string.Format("{0}: {1}", StringService.Instance.EndDate, endDate); }
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
				return string.Format ("{0}: {1}", StringService.Instance.Type, displayValue);
			}
		}

		#endregion

		#region period enum
		public enum PeriodType
        {
            All = 0,
            Today = 1,
            Week = 2,
            Month = 3,
            Choose = 4
        }
		#endregion

    }
}
