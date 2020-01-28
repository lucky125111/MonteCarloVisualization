using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1.Annotations;

namespace MonteCarloVisualization.Model
{
    public class FixedNumberContext : INotifyPropertyChanged
    {
        private int _pointCount;

        public int PointCount
        {
            get => _pointCount;
            set
            {
                _pointCount = value;
                OnPropertyChanged(nameof(PointCount));
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