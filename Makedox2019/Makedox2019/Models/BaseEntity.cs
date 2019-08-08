using PropertyChanged;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makedox2019.Models
{
    [DoNotNotify]
    public class BaseEntity : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
    }
}
