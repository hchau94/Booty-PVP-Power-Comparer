using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PowerCompare;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        SliderPower.ValueChanged += SliderValue_ValueChanged;
    }

    private void SliderValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        // TODO: actually implement logic here rather than the sample given below
        
        
        LabelLowerPower.Text = $"Test 1: { (int)SliderPower.Value }";

        int test2Value = (int)(SliderPower.Value * 0.5);
        LabelHigherPower.Text = $"Test 2: { test2Value }";
    }
}