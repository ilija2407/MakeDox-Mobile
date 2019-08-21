using PropertyChanged;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makedox2019.Models
{
    [DoNotNotify]
    public class Notification : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int NotificationId { get; set; }
        public int MovieId { get; set; }

        public Notification()
        {

        }

        public Notification(int notificationId, int movieId)
        {
            NotificationId = notificationId;
            MovieId = movieId;
        }
    }
}
