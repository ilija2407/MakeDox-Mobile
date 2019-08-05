using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Makedox2019.Models
{
    public class Movie : RealmObject, INotifyPropertyChanged
    {
        [PrimaryKey]
        public int ID { get; set; }
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public string Location { get; set; }
        public string OneLiner { get; set; }
        public string Description { get; set; }
        public string Trailer { get; set; }
        public string CoverImage { get; set; }
        public string LogoImage { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Rating { get; set; }
        public bool IsFavorite { get; set; }
        [Ignored]
        public bool IsNotFavorite => !IsFavorite;
        public Team Team { get; set; }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(IsFavorite))
                RaisePropertyChanged(nameof(IsNotFavorite));
        }
    }

    public class Team : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Director { get; set; }
        public string Script { get; set; }
        public string Photography { get; set; }
        public string Editing { get; set; }
        public string Sound { get; set; }
        public string Production { get; set; }
        public string Coproduction { get; set; }
    }
}
