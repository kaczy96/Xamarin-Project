using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Service.Notification;
using Android.Views;
using Android.Widget;

namespace Project.Model
{
    public class Ranking
    {
        private int Id { get; set; }
        private int Score { get; set; }

        public Ranking(int Id, int Score)
        {
            this.Id = Id;
            this.Score = Score;
        }

        public static implicit operator NotificationListenerService.Ranking(Ranking v)
        {
            throw new NotImplementedException();
        }
    }
}