using Makedox2019.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Makedox2019.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimelineView : ScrollView
    {
        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(Dictionary<string, List<TimelineItem>>), typeof(TimelineView));
        public Dictionary<string, List<TimelineItem>> ItemsSource
        {
            get => (Dictionary<string, List<TimelineItem>>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public TimelineView()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    grid.BatchBegin();
                    GenerateChildren();
                    grid.BatchCommit();
                });
            }
        }

        void GenerateChildren()
        {
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();


            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 80 });
            DateTimeOffset? maxDate = ItemsSource.Max(x => x.Value.Max(y => y.EndTime));
            DateTimeOffset? minDate = ItemsSource.Min(x => x.Value.Min(y => y.StartTime));

            while (minDate.Value.Minute % 15 != 0)
                minDate = minDate.Value.AddMinutes(-1);

            while (maxDate.Value.Minute % 15 != 0)
                maxDate = maxDate.Value.AddMinutes(+1);

            PopulateDates(minDate, maxDate);

            grid.RowDefinitions.Add(new RowDefinition { Height = 60 });
            grid.RowDefinitions.Add(new RowDefinition { Height = 20 });

            for (var i = 0; i < ItemsSource.Count; i++)
                grid.RowDefinitions.Add(new RowDefinition());

            var datesLabel = new Label { Text = "Dates" };
            var timesLabel = new Label { Text = "Times" };
            grid.Children.Add(datesLabel);
            grid.Children.Add(timesLabel);
            Grid.SetColumn(datesLabel, 0);
            Grid.SetRow(datesLabel, 0);
            Grid.SetColumn(timesLabel, 0);
            Grid.SetRow(timesLabel, 1);

            var movieRow = 2;
            var maxColPlusSpan = 0;
            foreach (var item in ItemsSource)
            {
                var locationLabel = new Label()
                {
                    Text = item.Key,
                    WidthRequest = 70,
                    HeightRequest = 50
                };
                grid.Children.Add(locationLabel);
                Grid.SetRow(locationLabel, movieRow);
                Grid.SetColumn(locationLabel, 0);

                foreach (var movie in item.Value)
                {
                    if (!movie.StartTime.HasValue)
                        continue;

                    var column = GetColumnForDate(movie.StartTime);
                    var columnSpan = GetColumnSpanForMovie(movie);
                    if (column < 1 || columnSpan < 1 || (column + columnSpan > grid.ColumnDefinitions.Count))
                        continue;
                    var movieLabel = new Label
                    {
                        Text = movie.Title,
                        ClassId = movie.Id.ToString(),
                        HeightRequest = 40,
                        BackgroundColor = Color.Black,
                        TextColor = Color.White
                    };
                    grid.Children.Add(movieLabel);
                    Grid.SetRow(movieLabel, movieRow);
                    Grid.SetColumn(movieLabel, column);
                    Grid.SetColumnSpan(movieLabel, columnSpan);
                    if (maxColPlusSpan < column + columnSpan)
                        maxColPlusSpan = column + columnSpan;
                }
                movieRow++;
            }
        }

        int GetColumnForDate(DateTimeOffset? date)
        {
            var time = TimesWithColumns.FirstOrDefault(x => x.Key == date.Value).Value;
            if (time > 0)
                return time;

            var theDate = TimesWithColumns.FirstOrDefault(x => x.Key.AddMinutes(-14) < date && date < x.Key.AddMinutes(14));
            return theDate.Value+1;
        }


        int GetColumnSpanForMovie(TimelineItem movie)
        {
            if (movie.StartTime == movie.EndTime || (movie.EndTime - movie.StartTime) < TimeSpan.FromMinutes(15) || (movie.StartTime.HasValue && !movie.EndTime.HasValue))
                return 1;

            var startcolumn = GetColumnForDate(movie.StartTime);
            var endColumn = GetColumnForDate(movie.EndTime);
            return Math.Abs(startcolumn - endColumn);
        }

        public List<KeyValuePair<DateTimeOffset, int>> TimesWithColumns { get; set; } = new List<KeyValuePair<DateTimeOffset, int>>();
        private void PopulateDates(DateTimeOffset? minDate, DateTimeOffset? maxDate)
        {
            var tempDate = minDate;
            var column = 1;
            TimesWithColumns.Clear();
            while (maxDate >= tempDate)
            {
                if (tempDate > minDate && tempDate < maxDate && !HasMovieDisplayedAt(tempDate))
                {
                    tempDate = tempDate.Value.AddMinutes(15);
                    continue;
                }
                var formattedDate = new FormattedString();
                formattedDate.Spans.Add(new Span { Text = tempDate.Value.ToString("ddd") });
                formattedDate.Spans.Add(new Span { Text = Environment.NewLine });
                formattedDate.Spans.Add(new Span { Text = tempDate.Value.ToString("dd") });
                formattedDate.Spans.Add(new Span { Text = Environment.NewLine });
                formattedDate.Spans.Add(new Span { Text = tempDate.Value.ToString("MMM") });
                var dateLabel = new Label()
                {
                    FormattedText = formattedDate,
                    WidthRequest = 40
                };
                var timeLabel = new Label()
                {
                    Text = tempDate.Value.ToString("HH:mm"),
                    WidthRequest = 70,
                    HeightRequest = 50
                };
                TimesWithColumns.Add(new KeyValuePair<DateTimeOffset, int>(tempDate.Value, column));
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 50 });
                grid.Children.Add(dateLabel);
                grid.Children.Add(timeLabel);
                Grid.SetRow(dateLabel, 0);
                Grid.SetColumn(dateLabel, column);
                Grid.SetRow(timeLabel, 1);
                Grid.SetColumn(timeLabel, column);
                column++;
                tempDate = tempDate.Value.AddMinutes(15);
            }
        }

        bool HasMovieDisplayedAt(DateTimeOffset? date)
        {
            foreach (var item in ItemsSource)
                foreach (var movie in item.Value)
                {
                    var isAfterStart = movie.StartTime <= date;
                    var isBeforeEnd = movie.EndTime >= date;
                    if (isAfterStart && isBeforeEnd)
                        return true;
                }

            return false;
        }
    }

    public class TimelineItem : INotifyPropertyChanged
    {
        public TimelineItem()
        {
        }

        public TimelineItem(int id, string title, DateTimeOffset? startTime, DateTimeOffset? endTime)
        {
            Id = id;
            Title = title;
            StartTime = startTime;
            EndTime = endTime;
        }

        public string Title { get; set; }
        public int Id { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}