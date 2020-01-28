using System.Windows;
using System.Windows.Controls;
using WpfApp1;

namespace MonteCarloVisualization
{
    public interface IInputValidation
    {
        void TextBox_PointsCount_OnTextChanged(object sender, TextChangedEventArgs e);
        void TextBox_Start_TextChanged(object sender, TextChangedEventArgs e);
        void TextBox_End_TextChanged(object sender, TextChangedEventArgs e);
        void TextBox_Iterations_TextChanged(object sender, TextChangedEventArgs e);
        void CheckBox_Fixed_Checked(object sender, RoutedEventArgs e);
        void CheckBox_Iterations_Checked(object sender, RoutedEventArgs e);
    }

    public class InputValidation : IInputValidation
    {
        private readonly MainWindow _mainWindow;

        public InputValidation(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void TextBox_PointsCount_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = _mainWindow.TextBox_PointsCount;
            if (textBox.Text == "")
                return;

            if (int.TryParse(textBox.Text, out _))
                return;

            MessageBox.Show("Please enter only integer numbers.");
            textBox.Text = textBox.Text.Length == 1 ? "1" : textBox.Text.Remove(textBox.Text.Length - 1);
        }

        public void TextBox_Start_TextChanged(object sender, TextChangedEventArgs e)
        {
            Validate(_mainWindow.TextBox_Start);
        }

        public void TextBox_End_TextChanged(object sender, TextChangedEventArgs e)
        {
            Validate(_mainWindow.TextBox_End);
        }

        public void TextBox_Iterations_TextChanged(object sender, TextChangedEventArgs e)
        {
            Validate(_mainWindow.TextBox_Iterations);
        }

        private void Validate(TextBox mainWindowTextBoxStart)
        {
            if (int.TryParse(_mainWindow.TextBox_Start.Text, out var start) &&
                int.TryParse(_mainWindow.TextBox_End.Text, out var end) &&
                int.TryParse(_mainWindow.TextBox_Iterations.Text, out var iterations))
            {
                if (iterations == 0)
                {
                    iterations = 1;
                    _mainWindow.TextBox_Iterations.Text = $"{iterations}";
                }
                if (1 > start)
                {
                    MessageBox.Show("Start value has to be greater or equal to 1");
                    _mainWindow.TextBox_Start.Text = "1";
                }
                else if (start >= end)
                {
                    MessageBox.Show("Start can't be greater than end");
                    if (mainWindowTextBoxStart.Name == "TextBox_Start")
                    {
                        _mainWindow.TextBox_End.Text = $"{start + iterations}";

                    }
                    if (mainWindowTextBoxStart.Name == "TextBox_End")
                    {
                        if (end - iterations > 1)
                        {
                            _mainWindow.TextBox_Start.Text = $"{end - iterations}";
                        }
                        else
                        {
                            start = 1;
                            _mainWindow.TextBox_Start.Text = $"{start}";
                            _mainWindow.TextBox_Iterations.Text = $"{end - start}";
                        }
                    }
                }
                else if (end > 100000)
                {
                    MessageBox.Show("Start can't be greater than end");
                    _mainWindow.TextBox_Start.Text = "100000";
                }
                else if (end - start < iterations)
                {
                    _mainWindow.TextBox_Iterations.Text = $"{end - start}";
                }
            }
            else
            {
                if (int.TryParse(mainWindowTextBoxStart.Text, out _))
                    return;
                mainWindowTextBoxStart.Text = "";
            }
        }

        public void CheckBox_Fixed_Checked(object sender, RoutedEventArgs e)
        {
            _mainWindow.CheckBox_Iterations.IsChecked = !_mainWindow.CheckBox_Fixed.IsChecked;
            _mainWindow.Grid_Fixed_Inner.IsEnabled = !_mainWindow.Grid_Fixed_Inner.IsEnabled;
            _mainWindow.Grid_Iterations.IsEnabled = !_mainWindow.Grid_Fixed_Inner.IsEnabled;
        }

        public void CheckBox_Iterations_Checked(object sender, RoutedEventArgs e)
        {
            _mainWindow.CheckBox_Fixed.IsChecked = !_mainWindow.CheckBox_Iterations.IsChecked;
            _mainWindow.Grid_Iterations_Inner.IsEnabled = !_mainWindow.Grid_Iterations_Inner.IsEnabled;
            _mainWindow.Grid_Fixed_Inner.IsEnabled = !_mainWindow.Grid_Iterations_Inner.IsEnabled;
        }
    }
}