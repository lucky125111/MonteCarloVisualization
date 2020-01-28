using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MonteCarloVisualization.Model;

namespace MonteCarloVisualization
{
    public partial class MainWindow : Window
    {
        private readonly IInputValidation _inputValidation;
        private DataContext _dataContext;
        private readonly MonteCarloSimulator _monteCarloSimulator;
        private readonly BitmapCanvas _bitmapCanvas;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDataContext();
            _inputValidation = new InputValidation(this);
            _monteCarloSimulator = new MonteCarloSimulator(_dataContext.SizeContext.Diameter);
            _bitmapCanvas = new BitmapCanvas(_dataContext.SizeContext.Diameter + 4, _dataContext.SizeContext.Diameter + 4);
        }

        public void Init(object sender, RoutedEventArgs eventArgs)
        {
            ClearCanvas();
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            Grid_RightPanel.IsEnabled = false;

            _dataContext.ResultContext.PiValue = "0";
            _dataContext.ResultContext.PointCount = "0";
            ClearCanvas();

            Grid_RightPanel.IsEnabled = true;
        }

        private async void Button_StartSimulation_Click(object sender, RoutedEventArgs e)
        {
            Grid_RightPanel.IsEnabled = false;
            _dataContext.ResultContext.PiValue = "Calculating...";
            _dataContext.ResultContext.PointCount = "Calculating...";
            Grid_Progress.Visibility = Visibility.Visible;
            ProgressBar.Value = 0;

            var isFixed = CheckBox_Fixed.IsChecked.HasValue && CheckBox_Fixed.IsChecked.Value;
            var res = await Task.Run(() => isFixed ? _monteCarloSimulator.RunMonteCarlo(_dataContext.FixedNumberContext) : _monteCarloSimulator.RunMonteCarlo(_dataContext.IterativeContext));

            var list = res.ToList();
            for (var index = 0; index < list.Count; index++)
            {
                var pointsAndPiValue = list[index];
                ProgressBar.Value = (index + 1) * 100.0 / list.Count;
                ClearCanvas();
                _bitmapCanvas.DrawPoints(pointsAndPiValue.List);
                Canvas_Graph.Source = _bitmapCanvas.Bitmap;
                _dataContext.ResultContext.PiValue = pointsAndPiValue.Pi.ToString("0.00000000");
                _dataContext.ResultContext.PointCount = pointsAndPiValue.PointCount.ToString();
                await Task.Delay(1000);
            }

            Grid_RightPanel.IsEnabled = true;
            Grid_Progress.Visibility = Visibility.Hidden;
            ProgressBar.Value = 0;
        }

        private void ClearCanvas()
        {
            _bitmapCanvas.Clear();
            Canvas_Graph.Source = _bitmapCanvas.Bitmap;
            DrawCircleInTheMiddle();
            DrawRectangleInTheMiddle();
        }
        
        private void DrawRectangleInTheMiddle()
        {
            Rectangle rectangle = new Rectangle()
            {
                Width = _dataContext.SizeContext.Diameter,
                Height = _dataContext.SizeContext.Diameter,
                Stroke = Brushes.Blue,
                StrokeThickness = 3
            };

            Application.Current.Dispatcher.Invoke(() =>
            {
                Canvas_1.Children.Add(rectangle);
                rectangle.SetValue(Canvas.LeftProperty, (double)0);
                rectangle.SetValue(Canvas.TopProperty, (double)0);
            });
        }

        private void DrawCircleInTheMiddle()
        {
            Ellipse circle = new Ellipse()
            {
                Width = _dataContext.SizeContext.Diameter,
                Height = _dataContext.SizeContext.Diameter,
                Stroke = Brushes.Red,
                StrokeThickness = 3
            };

            Application.Current.Dispatcher.Invoke(() =>
            {
                Canvas_1.Children.Add(circle);
                circle.SetValue(Canvas.LeftProperty, (double)0);
                circle.SetValue(Canvas.TopProperty, (double)0);
            });
        }

        private void TextBox_PointsCount_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _inputValidation.TextBox_PointsCount_OnTextChanged(sender, e);
        }

        private void TextBox_Start_TextChanged(object sender, TextChangedEventArgs e)
        {
            _inputValidation.TextBox_Start_TextChanged(sender, e);
        }

        private void TextBox_End_TextChanged(object sender, TextChangedEventArgs e)
        {
            _inputValidation.TextBox_End_TextChanged(sender, e);
        }

        private void TextBox_Iterations_TextChanged(object sender, TextChangedEventArgs e)
        {
            _inputValidation.TextBox_Iterations_TextChanged(sender, e);
        }

        private void CheckBox_Fixed_Checked(object sender, RoutedEventArgs e)
        {
            _inputValidation.CheckBox_Fixed_Checked(sender, e);
        }

        private void CheckBox_Iterations_Checked(object sender, RoutedEventArgs e)
        {
            _inputValidation.CheckBox_Iterations_Checked(sender, e);
        }

        private void InitializeDataContext()
        {
            _dataContext = new DataContext()
            {
                IterativeContext = new IterativeContext()
                {
                    StartCount = 100,
                    EndCount = 10000,
                    IterationsCount = 9
                },
                SizeContext = new SizeContext()
                {
                    Radius = 200,
                },
                FixedNumberContext = new FixedNumberContext()
                {
                    PointCount = 100
                },
                ResultContext = new ResultContext()
                {
                    PointCount = "1",
                    PiValue = "1"
                }
            };
            DataContext = _dataContext;
        }
    }
}
