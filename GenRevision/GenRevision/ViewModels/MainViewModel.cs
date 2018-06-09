using System;
using System.Timers;
using System.Windows.Input;
using GenRevision.Commands;

namespace GenRevision.ViewModels
{
    public class MainViewModel : Notifier, IMainViewModel
    {
        private static Timer _timer;
        private static DateTime _currentDate = DateTime.Now.Date;
        private static DateTime _lastCheckedDate = _currentDate;

        public MainViewModel()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += ProcessTimer;
            StartTimer();

            _showCommand = new ShowCommand(this);
        }

        public event EventHandler<EventArgs> ShowWindow;

        private readonly ICommand _showCommand;
        public ICommand ShowCommand => _showCommand;

        private DateTime _selectedDate = DateTime.Now.Date;
        public DateTime InputCalendarDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                NotifyPropertyChanged(nameof(InputCalendarDate));
                NotifyPropertyChanged(nameof(OutputJulianDate));
            }
        }

        public string OutputJulianDate => CalculateJulianDate(InputCalendarDate);

        private string _inputJulianDate;
        public string InputJulianDate
        {
            get => _inputJulianDate;
            set
            {
                _inputJulianDate = value;
                NotifyPropertyChanged(nameof(InputJulianDate));
                NotifyPropertyChanged(nameof(OutputCalendarDate));
            }
        }

        public string OutputCalendarDate
        {
            get
            {
                string result = string.Empty;

                if (!int.TryParse(InputJulianDate, out int julianDate))
                {
                    return result;
                }

                if (InputJulianDate.Length < 4 || InputJulianDate.Length > 5)
                {
                    return result;
                }

                int dayOfYear = int.Parse(InputJulianDate.Substring(InputJulianDate.Length - 3));
                int year = int.Parse(InputJulianDate.Substring(0, InputJulianDate.Length - 3)) + 2000;

                if (dayOfYear >= 1 && dayOfYear <= (DateTime.IsLeapYear(year) ? 366 : 365))
                {
                    var date = new DateTime(year, 1, 1);
                    date = date.AddDays(dayOfYear - 1);
                    result = date.ToShortDateString();
                }

                return result;
            }

        }

        public string ToolTipMessage => $"{DateTime.Now.Date.ToShortDateString()}: {CalculateJulianDate(DateTime.Now.Date)}";

        public void OnShowWindow()
        {
            ShowWindow?.Invoke(this, new EventArgs());
        }

        public void StartTimer()
        {
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        private static string CalculateJulianDate(DateTime date)
        {
            return (date.Year > 2000) ? $"{date.Year - 2000}{date.DayOfYear:D3}" : string.Empty;
        }

        private void ProcessTimer(object sender, ElapsedEventArgs e)
        {
            _currentDate = DateTime.Now.Date;
            if (_currentDate != _lastCheckedDate)
            {
                _lastCheckedDate = _currentDate;
                InputCalendarDate = _currentDate;
                NotifyPropertyChanged(nameof(ToolTipMessage));
            }
        }
    }
}
