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

namespace HW_Calculator_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string text
        {
            get
            {
                return textBox.Text;
            }
            private set { textBox.Text = value; }
        }
        private char[] operations = new char[] { '*', '/', '+', '-' };
        private List<char> operationsCurrent;

        public MainWindow()
        {
            InitializeComponent();
            textBox.FontSize = 20;
            textBox.Padding = new Thickness(7, 3, 7, 3);
            operationsCurrent = new List<char>(0);
        }

        private void btn_numeric_click(object sender, RoutedEventArgs e)
        {
            text += (sender as Button).Content.ToString();
        }

        private void btn_operation_click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(text) && !is_operation_symbol(text.Last()))
            {
                text += (sender as Button).Content.ToString();
                operationsCurrent.Add((sender as Button).Content.ToString().First());
            }
        }

        private void btn_equal_click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(text)) return;

            if (is_operation_symbol(text.Last()))
            {
                text = text.Remove(text.Length - 1);
                operationsCurrent.RemoveAt(operationsCurrent.Count - 1);
            }
            string[] nums = text.Split(operations, StringSplitOptions.RemoveEmptyEntries);

            float result;
            float.TryParse(nums[0], out result);

            for (int i = 1; i < nums.Length; i++)
            {
                float num;
                float.TryParse(nums[i], out num);

                switch (operationsCurrent[i - 1])
                {
                    case '+': result += num; break;
                    case '-': result -= num; break;
                    case '*': result *= num; break;
                    case '/': result /= num; break;
                    default: break;
                }
            }

            text = result.ToString();
            operationsCurrent.Clear();
        }

        private void btn_clear_click(object sender, RoutedEventArgs e)
        {
            text = "";
        }

        private bool is_operation_symbol(char symbol)
        {
            return Array.Exists(operations, sym => sym == symbol);
        }
    }
}
