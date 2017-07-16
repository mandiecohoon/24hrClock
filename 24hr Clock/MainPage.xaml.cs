using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Animation;
using System.Diagnostics;
using Windows.Storage;
using System.ComponentModel;
using Windows.UI;

namespace _24hr_Clock
{
    public sealed partial class MainPage : Page
    {
        String offSet;
        Int32 offSetIndex;
        String hourFormat;
        ApplicationDataContainer AppSettings = ApplicationData.Current.LocalSettings;

        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(330, 160);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(330, 160));
            
            if (AppSettings.Values["timeZone"] == null) {
                timeZoneComboBox.SelectedIndex = 27;
            } else {
                Debug.WriteLine(offSetIndex);
                timeZoneComboBox.SelectedIndex = Convert.ToInt32(AppSettings.Values["timeZone"]);
                hourFormat = Convert.ToString(AppSettings.Values["hourFormat"]);
            }

            if (AppSettings.Values["hourFormat"] == null) {
                hourFormat = "HH:mm:ss";
            } else if (AppSettings.Values["hourFormat"].Equals("T")) {
                tweleveRB.IsChecked = true;
            } else {
                militaryRB.IsChecked = true;
                AppSettings.Values["hourFormat"] = "HH:mm:ss";
            }

            if (AppSettings.Values["gridTheme"] != null)
            {
                if (AppSettings.Values["gridTheme"].Equals("Dark")) {
                    darkRB.IsChecked = true;
                } else {
                    lightRB.IsChecked = true;
                    AppSettings.Values["gridTheme"] = "Light";
                }
            }

            HourRadioButton_Checked(null, null);

            StartClock();
        }
        
        public DateTime SetTimeZone(DateTime dateTime)
        {
            String selectedTimeZone = (timeZoneComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(GetTimeZone(selectedTimeZone)));
        }
        
        private void StartClock()
        {
            DispatcherTimer ticker = new DispatcherTimer();
            ticker.Interval = TimeSpan.FromSeconds(1);
            ticker.Tick += Ticker_Tick;
            ticker.Start();
        }

        private void Ticker_Tick(object sender, object e)
        {
            DateTime utcTimeNow = DateTime.UtcNow;
            clockTime.Text = SetTimeZone(utcTimeNow).ToString(hourFormat);
        }

        private void ThemeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null)
            {
                string themeChoice = radioButton.Tag.ToString();
                switch (themeChoice)
                {
                    case "Light":
                        clockGrid.Background = new SolidColorBrush(Colors.White);
                        clockTime.Foreground = new SolidColorBrush(Colors.Black);
                        AppSettings.Values["gridTheme"] = "Light";
                        break;
                    case "Dark":
                        clockGrid.Background = new SolidColorBrush(Colors.DarkSlateGray);
                        clockTime.Foreground = new SolidColorBrush(Colors.White);
                        AppSettings.Values["gridTheme"] = "Dark";
                        break;
                }
            }
        }

        private void HourRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null)
            {
                string hourChoice = radioButton.Tag.ToString();
                switch (hourChoice)
                {
                    case "24hr":
                        hourFormat = "HH:mm:ss";
                        AppSettings.Values["hourFormat"] = "HH:mm:ss";
                        break;
                    case "12hr":
                        hourFormat = "T";
                        AppSettings.Values["hourFormat"] = "T";
                        break;
                }
            }
        }

        public String GetTimeZone(String selectedTimeZone)
        {
            if (selectedTimeZone.Equals("Afghanistan Standard Time(GMT + 04:30)"))
            {
                offSet = "Afghanistan Standard Time";
                offSetIndex = 0;
            }
            else if (selectedTimeZone.Equals("Alaskan Standard Time (GMT-09:00)"))
            {
                offSet = "Alaskan Standard Time";
                offSetIndex = 1;
            }
            else if (selectedTimeZone.Equals("Arab Standard Time (GMT+03:00)"))
            {
                offSet = "Arab Standard Time";
                offSetIndex = 2;
            }
            else if (selectedTimeZone.Equals("Arabian Standard Time (GMT+04:00)"))
            {
                offSet = "Arabian Standard Time";
                offSetIndex = 3;
            }
            else if (selectedTimeZone.Equals("Arabic Standard Time (GMT+03:00)"))
            {
                offSet = "Arabic Standard Time";
                offSetIndex = 4;
            }
            else if (selectedTimeZone.Equals("Atlantic Standard Time (GMT-04:00)"))
            {
                offSet = "Atlantic Standard Time";
                offSetIndex = 5;
            }
            else if (selectedTimeZone.Equals("AUS Central Standard Time (GMT+09:30)"))
            {
                offSet = "AUS Central Standard Time";
                offSetIndex = 6;
            }
            else if (selectedTimeZone.Equals("AUS Eastern Standard Time (GMT+10:00)"))
            {
                offSet = "AUS Eastern Standard Time";
                offSetIndex = 7;
            }
            else if (selectedTimeZone.Equals("Azerbaijan Standard Time (GMT +04:00)"))
            {
                offSet = "Azerbaijan Standard Time";
                offSetIndex = 8;
            }
            else if (selectedTimeZone.Equals("Azores Standard Time (GMT-01:00)"))
            {
                offSet = "Azores Standard Time";
                offSetIndex = 9;
            }
            else if (selectedTimeZone.Equals("Canada Central Standard Time (GMT-06:00)"))
            {
                offSet = "Canada Central Standard Time";
                offSetIndex = 10;
            }
            else if (selectedTimeZone.Equals("Cape Verde Standard Time (GMT-01:00)"))
            {
                offSet = "Cape Verde Standard Time";
                offSetIndex = 11;
            }
            else if (selectedTimeZone.Equals("Caucasus Standard Time (GMT+04:00)"))
            {
                offSet = "Caucasus Standard Time";
                offSetIndex = 12;
            }
            else if (selectedTimeZone.Equals("Cen. Australia Standard Time (GMT+09:30)"))
            {
                offSet = "Cen. Australia Standard Time";
                offSetIndex = 13;
            }
            else if (selectedTimeZone.Equals("Central America Standard Time (GMT-06:00)"))
            {
                offSet = "Central America Standard Time";
                offSetIndex = 14;
            }
            else if (selectedTimeZone.Equals("Central Asia Standard Time (GMT+06:00)"))
            {
                offSet = "Central Asia Standard Time";
                offSetIndex = 15;
            }
            else if (selectedTimeZone.Equals("Central Brazilian Standard Time (GMT -04:00)"))
            {
                offSet = "Central Brazilian Standard Time";
                offSetIndex = 16;
            }
            else if (selectedTimeZone.Equals("Central Europe Standard Time (GMT+01:00)"))
            {
                offSet = "Central Europe Standard Time";
                offSetIndex = 17;
            }
            else if (selectedTimeZone.Equals("Central European Standard Time (GMT+01:00)"))
            {
                offSet = "Central European Standard Time";
                offSetIndex = 18;
            }
            else if (selectedTimeZone.Equals("Central Pacific Standard Time (GMT+11:00)"))
            {
                offSet = "Central Pacific Standard Time";
                offSetIndex = 19;
            }
            else if (selectedTimeZone.Equals("Central Standard Time (GMT-06:00)"))
            {
                offSet = "Central Standard Time";
                offSetIndex = 20;
            }
            else if (selectedTimeZone.Equals("China Standard Time (GMT+08:00)"))
            {
                offSet = "China Standard Time";
                offSetIndex = 21;
            }
            else if (selectedTimeZone.Equals("Dateline Standard Time (GMT-12:00)"))
            {
                offSet = "Dateline Standard Time";
                offSetIndex = 22;
            }
            else if (selectedTimeZone.Equals("E. Africa Standard Time (GMT+03:00)"))
            {
                offSet = "E. Africa Standard Time";
                offSetIndex = 23;
            }
            else if (selectedTimeZone.Equals("E. Australia Standard Time (GMT+10:00)"))
            {
                offSet = "E. Australia Standard Time";
                offSetIndex = 24;
            }
            else if (selectedTimeZone.Equals("E. Europe Standard Time (GMT+02:00)"))
            {
                offSet = "E. Europe Standard Time";
                offSetIndex = 25;
            }
            else if (selectedTimeZone.Equals("E. South America Standard Time (GMT-03:00)"))
            {
                offSet = "E. South America Standard Time";
                offSetIndex = 26;
            }
            else if (selectedTimeZone.Equals("Eastern Standard Time (GMT-05:00)"))
            {
                offSet = "Eastern Standard Time";
                offSetIndex = 27;
            }
            else if (selectedTimeZone.Equals("Egypt Standard Time (GMT+02:00)"))
            {
                offSet = "Egypt Standard Time";
                offSetIndex = 28;
            }
            else if (selectedTimeZone.Equals("Ekaterinburg Standard Time (GMT+05:00)"))
            {
                offSet = "Ekaterinburg Standard Time";
                offSetIndex = 29;
            }
            else if (selectedTimeZone.Equals("Fiji Standard Time (GMT+12:00)"))
            {
                offSet = "Fiji Standard Time";
                offSetIndex = 30;
            }
            else if (selectedTimeZone.Equals("FLE Standard Time (GMT+02:00)"))
            {
                offSet = "FLE Standard Time";
                offSetIndex = 31;
            }
            else if (selectedTimeZone.Equals("Georgian Standard Time (GMT +04:00)"))
            {
                offSet = "Georgian Standard Time";
                offSetIndex = 32;
            }
            else if (selectedTimeZone.Equals("Greenland Standard Time (GMT-03:00)"))
            {
                offSet = "Greenland Standard Time";
                offSetIndex = 33;
            }
            else if (selectedTimeZone.Equals("Greenwich Standard Time (GMT)"))
            {
                offSet = "Greenwich Standard Time";
                offSetIndex = 34;
            }
            else if (selectedTimeZone.Equals("Hawaiian Standard Time (GMT-10:00)"))
            {
                offSet = "Hawaiian Standard Time";
                offSetIndex = 35;
            }
            else if (selectedTimeZone.Equals("India Standard Time (GMT+05:30)"))
            {
                offSet = "India Standard Time";
                offSetIndex = 36;
            }
            else if (selectedTimeZone.Equals("Iran Standard Time (GMT+03:30)"))
            {
                offSet = "Iran Standard Time";
                offSetIndex = 37;
            }
            else if (selectedTimeZone.Equals("Israel Standard Time (GMT+02:00)"))
            {
                offSet = "Israel Standard Time";
                offSetIndex = 38;
            }
            else if (selectedTimeZone.Equals("Korea Standard Time (GMT+09:00)"))
            {
                offSet = "Korea Standard Time";
                offSetIndex = 39;
            }
            else if (selectedTimeZone.Equals("Mid-Atlantic Standard Time (GMT-02:00)"))
            {
                offSet = "Mid-Atlantic Standard Time";
                offSetIndex = 40;
            }
            else if (selectedTimeZone.Equals("Mountain Standard Time (GMT-07:00)"))
            {
                offSet = "Mountain Standard Time";
                offSetIndex = 41;
            }
            else if (selectedTimeZone.Equals("Myanmar Standard Time (GMT+06:30)"))
            {
                offSet = "Myanmar Standard Time";
                offSetIndex = 42;
            }
            else if (selectedTimeZone.Equals("N. Central Asia Standard Time (GMT+06:00) "))
            {
                offSet = "N. Central Asia Standard Time ";
                offSetIndex = 43;
            }
            else if (selectedTimeZone.Equals("Namibia Standard Time (GMT +02:00)"))
            {
                offSet = "Namibia Standard Time";
                offSetIndex = 44;
            }
            else if (selectedTimeZone.Equals("Nepal Standard Time (GMT+05:45)"))
            {
                offSet = "Nepal Standard Time";
                offSetIndex = 45;
            }
            else if (selectedTimeZone.Equals("New Zealand Standard Time (GMT+12:00)"))
            {
                offSet = "New Zealand Standard Time";
                offSetIndex = 46;
            }
            else if (selectedTimeZone.Equals("Newfoundland Standard Time (GMT-03:30)"))
            {
                offSet = "Newfoundland Standard Time";
                offSetIndex = 47;
            }
            else if (selectedTimeZone.Equals("North Asia East Standard Time (GMT+08:00)"))
            {
                offSet = "North Asia East Standard Time";
                offSetIndex = 48;
            }
            else if (selectedTimeZone.Equals("North Asia Standard Time (GMT+07:00)"))
            {
                offSet = "North Asia Standard Time";
                offSetIndex = 49;
            }
            else if (selectedTimeZone.Equals("Pacific SA Standard Time (GMT-04:00)"))
            {
                offSet = "Pacific SA Standard Time";
                offSetIndex = 50;
            }
            else if (selectedTimeZone.Equals("Pacific Standard Time (GMT-08:00)"))
            {
                offSet = "Pacific Standard Time";
                offSetIndex = 51;
            }
            else if (selectedTimeZone.Equals("Romance Standard Time (GMT+01:00)"))
            {
                offSet = "Romance Standard Time";
                offSetIndex = 52;
            }
            else if (selectedTimeZone.Equals("Russian Standard Time (GMT+03:00)"))
            {
                offSet = "Russian Standard Time";
                offSetIndex = 53;
            }
            else if (selectedTimeZone.Equals("SA Eastern Standard Time (GMT-03:00)"))
            {
                offSet = "SA Eastern Standard Time";
                offSetIndex = 54;
            }
            else if (selectedTimeZone.Equals("SA Pacific Standard Time (GMT-05:00)"))
            {
                offSet = "SA Pacific Standard Time";
                offSetIndex = 55;
            }
            else if (selectedTimeZone.Equals("SA Western Standard Time (GMT-04:00)"))
            {
                offSet = "SA Western Standard Time";
                offSetIndex = 56;
            }
            else if (selectedTimeZone.Equals("Samoa Standard Time (GMT-11:00)"))
            {
                offSet = "Samoa Standard Time";
                offSetIndex = 57;
            }
            else if (selectedTimeZone.Equals("SE Asia Standard Time (GMT+07:00)"))
            {
                offSet = "SE Asia Standard Time";
                offSetIndex = 58;
            }
            else if (selectedTimeZone.Equals("Singapore Standard Time (GMT+08:00)"))
            {
                offSet = "Singapore Standard Time";
                offSetIndex = 59;
            }
            else if (selectedTimeZone.Equals("South Africa Standard Time (GMT+02:00)"))
            {
                offSet = "South Africa Standard Time";
                offSetIndex = 60;
            }
            else if (selectedTimeZone.Equals("Sri Lanka Standard Time (GMT+06:00)"))
            {
                offSet = "Sri Lanka Standard Time";
                offSetIndex = 61;
            }
            else if (selectedTimeZone.Equals("Taipei Standard Time (GMT+08:00)"))
            {
                offSet = "Taipei Standard Time";
                offSetIndex = 62;
            }
            else if (selectedTimeZone.Equals("Tasmania Standard Time (GMT+10:00)"))
            {
                offSet = "Tasmania Standard Time";
                offSetIndex = 63;
            }
            else if (selectedTimeZone.Equals("Tokyo Standard Time (GMT+09:00)"))
            {
                offSet = "Tokyo Standard Time";
                offSetIndex = 64;
            }
            else if (selectedTimeZone.Equals("Tonga Standard Time (GMT+13:00)"))
            {
                offSet = "Tonga Standard Time";
                offSetIndex = 65;
            }
            else if (selectedTimeZone.Equals("US Eastern Standard Time (GMT-05:00)"))
            {
                offSet = "US Eastern Standard Time";
                offSetIndex = 66;
            }
            else if (selectedTimeZone.Equals("US Mountain Standard Time (GMT-07:00)"))
            {
                offSet = "US Mountain Standard Time";
                offSetIndex = 67;
            }
            else if (selectedTimeZone.Equals("Vladivostok Standard Time (GMT+10:00)"))
            {
                offSet = "Vladivostok Standard Time";
                offSetIndex = 68;
            }
            else if (selectedTimeZone.Equals("W. Australia Standard Time (GMT+08:00)"))
            {
                offSet = "W. Australia Standard Time";
                offSetIndex = 69;
            }
            else if (selectedTimeZone.Equals("W. Central Africa Standard Time (GMT+01:00)"))
            {
                offSet = "W. Central Africa Standard Time";
                offSetIndex = 70;
            }
            else if (selectedTimeZone.Equals("W. Europe Standard Time (GMT+01:00)"))
            {
                offSet = "W. Europe Standard Time";
                offSetIndex = 71;
            }
            else if (selectedTimeZone.Equals("West Asia Standard Time (GMT+05:00)"))
            {
                offSet = "West Asia Standard Time";
                offSetIndex = 72;
            }
            else if (selectedTimeZone.Equals("West Pacific Standard Time (GMT+10:00)"))
            {
                offSet = "West Pacific Standard Time";
                offSetIndex = 73;
            }
            else if (selectedTimeZone.Equals("Yakutsk Standard Time (GMT+09:00)"))
            {
                offSet = "Yakutsk Standard Time";
                offSetIndex = 74;
            }
            //Debug.WriteLine(offSet);
            AppSettings.Values["timeZone"] = offSetIndex;
            return offSet;
        }
    }
}