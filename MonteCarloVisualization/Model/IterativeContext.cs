using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1.Annotations;

namespace MonteCarloVisualization.Model
{
    public class IterativeContext : INotifyPropertyChanged
    {
        private int _startCount;
        private int _endCount;
        private int _iterationsCount;

        public int StartCount
        {
            get => _startCount;
            set
            {
                _startCount = value;
                OnPropertyChanged(nameof(StartCount));
            }
        }

        public int EndCount
        {
            get => _endCount;
            set
            {
                _endCount = value;
                OnPropertyChanged(nameof(EndCount));
            }
        }

        public int IterationsCount
        {
            get => _iterationsCount;
            set
            {
                _iterationsCount = value;
                OnPropertyChanged(nameof(IterationsCount));
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