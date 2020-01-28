using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1.Annotations;

namespace MonteCarloVisualization.Model
{
    public class SizeContext : INotifyPropertyChanged
    {
        private int _radius;

        public int Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                OnPropertyChanged(nameof(Diameter));
                OnPropertyChanged(nameof(Radius));
            }
        }

        public int Diameter
        {
            get => 2 * _radius;
            set
            {
                _radius = (int) Math.Floor(value / 2.0);
                OnPropertyChanged(nameof(Diameter));
                OnPropertyChanged(nameof(Radius));
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