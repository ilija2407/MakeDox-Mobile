using Makedox2019.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Makedox2019.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimelineView : StackLayout
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
                grid.BatchBegin();
                GenerateDateLabels();
                GenerateChildren(ItemsSource.Min(x => x.Value.Min(y => y.StartTime)));
                grid.BatchCommit();
            }
        }

        void GenerateChildren(DateTimeOffset? date)
        {
            SelectDateLabel(date.Value.Date);
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            locations.Children.Clear();
            locations.RowDefinitions.Clear();
            locations.ColumnDefinitions.Clear();
            locations.ColumnDefinitions.Add(new ColumnDefinition { Width = 120 });

            DateTimeOffset? maxDate = ItemsSource.Max(x => x.Value.Where(y => y.StartTime.Value.Date == date.Value.Date).Max(y => y.EndTime));
            DateTimeOffset? minDate = ItemsSource.Min(x => x.Value.Where(y => y.StartTime.Value.Date == date.Value.Date).Min(y => y.StartTime));

            while (minDate.Value.Minute % 15 != 0)
                minDate = minDate.Value.AddMinutes(-1);

            while (maxDate.Value.Minute % 15 != 0)
                maxDate = maxDate.Value.AddMinutes(+1);

            PopulateDates(minDate, maxDate);

            grid.RowDefinitions.Add(new RowDefinition { Height = 20 });
            locations.RowDefinitions.Add(new RowDefinition { Height = 20 });

            for (var i = 0; i < ItemsSource.Count; i++)
                grid.RowDefinitions.Add(new RowDefinition());

            var timesLabel = new Label { Text = "Times" };
            locations.Children.Add(timesLabel);

            var movieRow = 1;
            foreach (var item in ItemsSource)
            {
                var titleLayout = GetVenueTitleForCategory(item.Key);
                locations.RowDefinitions.Add(new RowDefinition());
                locations.Children.Add(titleLayout);
                Grid.SetRow(titleLayout, locations.Children.Count - 1);

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
                        BackgroundColor = Color.FromRgba(0, 0, 0, .5),
                        TextColor = Color.White,
                        VerticalTextAlignment = TextAlignment.End
                    };
                    grid.Children.Add(movieLabel);
                    Grid.SetRow(movieLabel, movieRow);
                    Grid.SetColumn(movieLabel, column);
                    Grid.SetColumnSpan(movieLabel, columnSpan);
                }
                movieRow++;
            }
        }

        FFImageLoading.Forms.CachedImage GetVenueTitleForCategory(string category)
        {
            var icon = new FFImageLoading.Forms.CachedImage
            {
                HeightRequest = 80,
                WidthRequest = 100,
                Margin = new Thickness(10, 0)
            };
            switch (category.ToLowerInvariant())
            {
                case "kurshumli an":
                    icon.Source = "timeline_kurshumli.png";
                    break;
                case "kurshumli out":
                    icon.Source = "timeline_kurshumliout.png";
                    break;
                case "кино милениум":
                    icon.Source = "kinomilenium.png";
                    break;
                case "mkc":
                    icon.Source = "timeline_msu.png";
                    break;
            }
            return icon;
        }

        int GetColumnForDate(DateTimeOffset? date)
        {
            var time = TimesWithColumns.FirstOrDefault(x => x.Key == date.Value).Value;
            if (time > 0)
                return time;

            var theDate = TimesWithColumns.FirstOrDefault(x => x.Key.AddMinutes(-14) < date && date < x.Key.AddMinutes(14));
            return theDate.Value;
        }


        int GetColumnSpanForMovie(TimelineItem movie)
        {
            if (movie.StartTime == movie.EndTime || (movie.EndTime - movie.StartTime) < TimeSpan.FromMinutes(15) || (movie.StartTime.HasValue && !movie.EndTime.HasValue))
                return 1;

            var startcolumn = GetColumnForDate(movie.StartTime);
            var endColumn = GetColumnForDate(movie.EndTime);
            return Math.Abs(startcolumn - endColumn);
        }

        void SelectDateLabel(DateTime date)
        {
            foreach (Label label in daysGrid.Children)
                label.TextColor = label.ClassId == date.ToShortDateString() ? Color.Orange : Color.Default;
        }

        void GenerateDateLabels()
        {
            daysGrid.Children.Clear();
            daysGrid.ColumnDefinitions.Clear();

            daysGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 120 });
            var datesLabel = new Label { Text = "Dates", VerticalTextAlignment = TextAlignment.End };
            daysGrid.Children.Add(datesLabel);
            Grid.SetColumn(datesLabel, 0);
            Grid.SetRow(datesLabel, 0);

            var daysList = new List<DateTime>();
            foreach (var item in ItemsSource)
                foreach (var movie in item.Value)
                    if (daysList.Any(x => x.Date == movie.StartTime.Value.Date) == false)
                        daysList.Add(movie.StartTime.Value.Date);

            daysList.OrderBy(x => x.Date).ToList().ForEach(x =>
            {
                daysGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 50 });
                var formattedDate = new FormattedString();
                formattedDate.Spans.Add(new Span { Text = x.ToString("ddd") });
                formattedDate.Spans.Add(new Span { Text = Environment.NewLine });
                formattedDate.Spans.Add(new Span { Text = x.ToString("dd") });
                formattedDate.Spans.Add(new Span { Text = Environment.NewLine });
                formattedDate.Spans.Add(new Span { Text = x.ToString("MMM") });
                var dateLabel = new Label()
                {
                    FormattedText = formattedDate,
                    WidthRequest = 40,
                    BackgroundColor = Color.FromRgba(252, 245, 106, .5),
                    ClassId = x.Date.ToShortDateString()
                };
                dateLabel.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = SelectDateCommand,
                    CommandParameter = new DateTimeOffset(x)
                });
                daysGrid.Children.Add(dateLabel);
                Grid.SetRow(dateLabel, 0);
                Grid.SetColumn(dateLabel, daysGrid.Children.Count - 1);
            });

        }

        public ICommand SelectDateCommand => new DelegateCommand<DateTimeOffset?>(date => GenerateChildren(date));

        public List<KeyValuePair<DateTimeOffset, int>> TimesWithColumns { get; set; } = new List<KeyValuePair<DateTimeOffset, int>>();
        private void PopulateDates(DateTimeOffset? minDate, DateTimeOffset? maxDate)
        {
            var tempDate = minDate;
            var column = 0;
            TimesWithColumns.Clear();
            while (maxDate >= tempDate)
            {
                if (tempDate > minDate && tempDate < maxDate && !HasMovieDisplayedAt(tempDate))
                {
                    tempDate = tempDate.Value.AddMinutes(15);
                    continue;
                }

                var timeLabel = new Label()
                {
                    Text = tempDate.Value.ToString("HH:mm"),
                    WidthRequest = 70,
                    HeightRequest = 50,
                    BackgroundColor = Color.FromRgba(255, 255, 255, .5)
                };
                TimesWithColumns.Add(new KeyValuePair<DateTimeOffset, int>(tempDate.Value, column));
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 50 });
                grid.Children.Add(timeLabel);
                Grid.SetRow(timeLabel, 0);
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

    public class TimelineItem
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
    }
}