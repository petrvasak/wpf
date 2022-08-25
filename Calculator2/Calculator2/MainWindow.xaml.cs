using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;
        public MainWindow()
        {
            InitializeComponent();
            
            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
               switch(selectedOperator)
                {
                    case SelectedOperator.Sub:
                        result = SimpleMath.Sub(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Add:
                        result = SimpleMath.Add(lastNumber, newNumber); 
                        break;
                    case SelectedOperator.Div:
                        result = SimpleMath.Div(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multi:
                        result = SimpleMath.Multi(lastNumber, newNumber);
                        break;
                }
                resultLabel.Content = result.ToString();
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber = tempNumber / 100;
                if (lastNumber != 0)
                    tempNumber *= lastNumber;
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            result = 0;
            lastNumber = 0;

        }
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }
            if (sender == multiplyButton)
                selectedOperator = SelectedOperator.Multi;
            if (sender == divisionButton)
                selectedOperator = SelectedOperator.Div;
            if (sender == plusButton)
                selectedOperator = SelectedOperator.Add;
            if (sender == minusButton)
                selectedOperator = SelectedOperator.Sub;
                
        }
        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            if(resultLabel.Content.ToString().Contains("."))
            {
                //do nothing
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}.";
            }
            
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());
            

            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = selectedValue.ToString();
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }


        }
    }
    public enum SelectedOperator
    {
        Add,
        Sub,
        Multi,
        Div

    }
    public class SimpleMath
    {
        public static double Add(double n1, double n2)
            { return n1 + n2; }
        public static double Sub(double n1, double n2)
        {
            return n1 - n2; 
        }
        public static double Multi(double n1, double n2)
        {
            return n1 * n2;
        }
        public static double Div(double n1, double n2)
        {
            if(n2 == 0)
            {
                MessageBox.Show("Div by 0 is not supported", "Wrong Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return n1 / n2;
        }
    }
}
