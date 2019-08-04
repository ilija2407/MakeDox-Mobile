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
            var maxColCount = 0;
            DateTimeOffset? maxDate = DateTimeOffset.Now;
            DateTimeOffset? minDate = DateTimeOffset.Now;
            foreach (var item in ItemsSource)
            {
                maxColCount = maxColCount < item.Value.Count ? item.Value.Count : maxColCount;

                var maxCat = item.Value.Max(x => x.EndTime);
                maxDate = maxCat.HasValue ? maxCat : maxDate;


                var minCat = item.Value.Min(x => x.StartTime);
                minDate = minCat.HasValue ? minCat : minDate;

                if (!maxCat.HasValue)
                    maxDate = item.Value.Max(x => x.StartTime).Value.AddMinutes(15);
            }

            PopulateDates(minDate, maxDate);

            //for (var i = 0; i < maxColCount; i++)
            //    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });

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
                        HeightRequest = 40
                    };
                    grid.Children.Add(movieLabel);
                    Grid.SetRow(movieLabel, movieRow);
                    Grid.SetColumn(movieLabel, column);
                    Grid.SetColumnSpan(movieLabel, columnSpan);
                    Console.WriteLine("====================================================");
                    Console.WriteLine($"Column: {column}, ColSpan: {columnSpan}");
                    Console.WriteLine("====================================================");
                    if (maxColPlusSpan < column + columnSpan)
                        maxColPlusSpan = column + columnSpan;
                }
                movieRow++;
            }
            Console.WriteLine("====================================================");
            Console.WriteLine($"Column: {grid.ColumnDefinitions.Count}, Rows: {grid.RowDefinitions.Count}");
            Console.WriteLine($"MaxCol&Span: {maxColPlusSpan}, Columns: {grid.ColumnDefinitions.Count}");
            Console.WriteLine("====================================================");
        }

        int GetColumnForDate(DateTimeOffset? date)
            => TimesWithColumns.FirstOrDefault(x => x.Key == date.Value).Value;

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