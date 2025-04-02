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
    private readonly string _youIfText = "If your power is:";
    private readonly string _themIfText = "If their power is:";
    private readonly string _youLowerThenText = "Then they need to cook a team of at least this power to attack you:";
    private readonly string _themLowerThenText = "Then you need to cook a team of at least this power to attack them:";
    private readonly string _youHigherThenText = "Then you can attack teams up to this power:";
    private readonly string _themHigherThenText = "Then you need more than this power to start overpowering them:";
    
    public MainWindow()
    {
        InitializeComponent();

        LabelSelectedPowerL.Text = _youIfText;
        LabelSelectedPowerR.Text = _themIfText;
        LabelLowerPowerL.Text = _youLowerThenText;
        LabelLowerPowerR.Text = _themLowerThenText;
        LabelHigherPowerL.Text = _youHigherThenText;
        LabelHigherPowerR.Text = _themHigherThenText;
        
        SliderPower.ValueChanged += SliderPower_ValueChanged;
        SliderAttackerPower.ValueChanged += SliderAttackerPower_ValueChanged;
        SliderDefenderPower.ValueChanged += SliderDefenderPower_ValueChanged;
    }

    private void SliderPower_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        TextBoxSelectedPower.Text = e.NewValue.ToString("n0");
        TextBoxLowerPower.Text = GetLowerPowerValue(e.NewValue).ToString("n0");
        TextBoxHigherPower.Text = GetHigherPowerValue(e.NewValue).ToString("n0");
    }
    
    private void SliderAttackerPower_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        LabelAttackerPower.Text = "Attacker: " + e.NewValue.ToString("n0");
        UpdateAttackerVsDefender();
    }
    
    private void SliderDefenderPower_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        LabelDefenderPower.Text = "Defender: " + e.NewValue.ToString("n0");
        UpdateAttackerVsDefender();
    }

    private void UpdateAttackerVsDefender()
    {
        var attackerPower = SliderAttackerPower.Value;
        var defenderPower = SliderDefenderPower.Value;
        var penaltyPercentage = GetPowerPenalty(attackerPower, defenderPower);
        
        if (penaltyPercentage == 0)
        {
            LabelAttackerVsDefender.Foreground = Brushes.Green;
            LabelAttackerVsDefender.Text = "No stat penalty.";
        }
        else
        {
            LabelAttackerVsDefender.Foreground = Brushes.Red;
            LabelAttackerVsDefender.Text = $"Attacker has {penaltyPercentage}% stat penalty.";
        }
    }

    /// <summary>
    /// Rounded to nearest thousand.
    /// </summary>
    private double GetLowerPowerValue(double selectedValue)
    {
        return Math.Round((selectedValue * 0.846)/ 1000) * 1000;
    }
    
    /// <summary>
    /// Rounded to nearest thousand.
    /// </summary>
    private double GetHigherPowerValue(double selectedValue)
    {
        return Math.Round((selectedValue / 0.846) / 1000)  * 1000;
    }
    
    private double GetPowerPenalty(double attackerPower, double defenderPower)
    {
        if (attackerPower > defenderPower) { return 0; }
        
        var ratio = (attackerPower / defenderPower);
        if (ratio > 0.846) { return 0; }
        
        var penaltyPercentage = 20 + ((ratio - 0.846) * -1 * 100 * 2.7);
        return Math.Round(penaltyPercentage);
        
        return 0;
    }
}