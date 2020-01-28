using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1;
using WpfApp1.Annotations;

namespace MonteCarloVisualization.Model
{
    public class DataContext : INotifyPropertyChanged
    {
        public FixedNumberContext FixedNumberContext { get; set; }
        public IterativeContext IterativeContext { get; set; }
        public SizeContext SizeContext { get; set; }
        public ResultContext ResultContext { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}