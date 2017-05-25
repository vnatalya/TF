using System;
using System.Globalization;
using System.Threading;
//#if __ANDROID__
namespace TF
//#else
//namespace App2.iOS
//#endif
{
    public class LocalizationManager : StringService
    {

        public static CultureInfo Culture { get; set; }

#if __ANDROID__

        public static void ChangeCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;

            Thread.CurrentThread.CurrentUICulture = culture;

            var locale = new Java.Util.Locale(culture.TwoLetterISOLanguageName);

            Java.Util.Locale.Default = locale;

            var resources = global::Android.App.Application.Context.Resources;

            var config = resources.Configuration;

            config.Locale = locale;

            resources.UpdateConfiguration(config, resources.DisplayMetrics);
        }

#endif

        public static string Translate(string key)
        {
#if __ANDROID__

            if (Culture != null && Thread.CurrentThread.CurrentCulture.Name != Culture.Name)
            {
                ChangeCulture(Culture);
            }
            else
            {
                var locale = Java.Util.Locale.Default;

                if (locale != null && locale.Language != null && locale.Language.ToLower() != Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower())
                {
                    ChangeCulture(Culture);
                }
            }
#endif

            string result = "[null]";

            if (!string.IsNullOrEmpty(key) && !string.IsNullOrWhiteSpace(key))
            {
                //#if __ANDROID__
                result = Language.ResourceManager.GetString(key);
                //#elif __IOS__
                //                result =  App2.iOS.Language.ResourceManager.GetString(key);
                //#elif WINDOWS
                //                result = App2.UWP.Language.ResourceManager.GetString(key);
                //#endif
            }
#if DEBUG
            if (result == null)
            {
                //  Console.WriteLine("------------Localization: key='{0}'", key);
            }
#endif

            return result ?? "[null]";
        }

        public override void Init()
        {

            // inc  = Translate("LogIn");  
            trainingSaved = Translate("TrainingSaved");
            couldntSave = Translate("CouldntSave");
            incorrectDistance = Translate("IncorrectDistance");
            incorrectTime = Translate("IncorrectTime");
            swimming = Translate("Swimming");
            backstroke = Translate("Backstroke");
            breaststroke = Translate("Breaststroke");
            butterfly = Translate("Butterfly");
            swimmingExercices = Translate("SwimmingExercices");
			running = Translate("Running");
            run = Translate("Run");
            runningExercises = Translate("RunningExercises");
            cycling = Translate("Cycling");
            bike = Translate("Bike");
            trainer = Translate("Trainer");
            triathlon = Translate("Triathlon");
            freestyle = Translate("Freestyle");
            history = Translate("History");
            all = Translate("All");
            week = Translate("Week");
            month = Translate("Month");
            today = Translate("Today");
            type = Translate("Type");
            time = Translate("Time");
            distance = Translate("Distance");
            date = Translate("Date");
            deleteAlert = Translate("DeleteAlert");
            start = Translate("Start");
            none = Translate("None");
            finish = Translate("Finish");
            m = Translate("m");
            choose = Translate("Choose");
            period = Translate("Period");
            numberOfTrainings = Translate("NumberOfTrainings");
			hours = Translate ("Hours");
			triathlon = Translate ("Triathlon");
			addFeedback = Translate ("AddFeedback");
			addGroup = Translate ("AddGroup");
			addNewTraining = Translate ("AddNewTraining");
			endDate = Translate ("EndDate");
			error = Translate ("Error");
			group = Translate ("Group");
			logout = Translate ("Logout");
			minutes = Translate ("Minutes");
			myFeedbacks = Translate ("MyFeedbacks");
			myGroups = Translate ("MyGroups");
			myResults= Translate ("MyResults");
			seconds = Translate ("Seconds");
			noInternet = Translate ("NoInternet");
			save = Translate ("Save");
			name = Translate ("Name");
			password = Translate ("Password");
			confirmPassword = Translate ("ConfirmPassword");
			email = Translate ("Email");
			register = Translate ("Register");
			createAccount = Translate ("CreateAccount");
			login = Translate ("Login");
			nameIsRequired = Translate ("NameIsRequired");
			alreadyHaveAccount = Translate ("AlreadyHaveAccount");
			emailRequired = Translate ("EmailRequired");
			dontHaveAccount = Translate ("DontHaveAccount");
			student = Translate ("Student");
			privateUser = Translate ("Private");
			teacher = Translate ("Teacher");
			selectMode = Translate ("SelectMode");
			confirmPasswordRequired = Translate ("ConfirmPasswordRequired");
			passwordRequired = Translate ("PasswordRequired");
			loginToAccount = Translate ("LoginToAccount");
			or = Translate ("Or");
			continueWithoutLogging = Translate ("ContinueWithoutLogging");
			dataMayBeLost = Translate ("DataMayBeLost");
			back = Translate ("Back");
			enterFeedback = Translate ("EnterFeedback");
			menu = Translate ("Menu");
        }
    }
}

