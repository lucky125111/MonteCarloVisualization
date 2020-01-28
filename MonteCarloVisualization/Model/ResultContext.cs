using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1.Annotations;

namespace MonteCarloVisualization.Model
{
    public class ResultContext : INotifyPropertyChanged
    {
        private string _pointCount { get; set; }
        private string _piValue { get; set; }

        public string PointCount
        {
            get => _pointCount;
            set
            {
                _pointCount = value;
                OnPropertyChanged(nameof(PointCount));
            }
        }

        public string PiValue
        {
            get => _piValue;
            set
            {
                _piValue = value;
                OnPropertyChanged(nameof(PiValue));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}