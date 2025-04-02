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
    private readonly string _youIfText = $"---------- If your power is ---------->";
    private readonly string _themIfText = "<---------- If their power is ----------";
    
    public MainWindow()
    {
        InitializeComponent();

        LabelSelectedPowerL.Text = _youIfText;
        LabelSelectedPowerR.Text = _themIfText;
        
        SliderPower.ValueChanged += SliderPower_ValueChanged;
    }

    private void SliderPower_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        TextBoxSelectedPower.Text = e.NewValue.ToString("n0");
    }
}