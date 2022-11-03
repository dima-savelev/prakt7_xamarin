using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using System.Numerics;

namespace app7
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            LocalNotificationCenter.Current.NotificationReceived += OnNotificationReceived;
            InitializeComponent();
            dataPicker.MinimumDate = DateTime.Now.Date;
        }
        DateTime _dateTime;
        private void OnNotificationReceived(NotificationEventArgs e)
        {
            switchToggle.Toggled -= Switch_Toggled;
            switchToggle.IsToggled = false;
            textUser.IsEnabled = true;
            dataPicker.IsEnabled = true;
            timePicker.IsEnabled = true;
            switchToggle.Toggled += Switch_Toggled;
        }
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (textUser.Text == null) return;
            if (switchToggle.IsToggled == true)
            {
                textUser.IsEnabled = false;
                dataPicker.IsEnabled = false;
                timePicker.IsEnabled = false;
                _dateTime = dataPicker.Date;
                _dateTime = _dateTime.Add(timePicker.Time);
                var notification = new NotificationRequest
                {
                    NotificationId = 100,
                    Title = "Напоминалка",
                    Description = $"{textUser.Text}",
                    CategoryType = NotificationCategoryType.Alarm,
                    Schedule =
                    {
                    NotifyTime = _dateTime,
                    RepeatType = NotificationRepeat.No,
                    }
                };
                LocalNotificationCenter.Current.Show(notification);
            }
            else
            {
                LocalNotificationCenter.Current.Cancel(100);
                dataPicker.IsEnabled = true;
                timePicker.IsEnabled = true;
                textUser.IsEnabled = true;
            }
        }
    }
}
