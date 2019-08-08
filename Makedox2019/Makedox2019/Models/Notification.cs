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
        public int Id { get; set; }
        public int NotificationId { get; set; }

        public Notification()
        {

        }

        public Notification(int id, int notificationId)
        {
            NotificationId = notificationId;
            Id = id;
        }
    }
}
