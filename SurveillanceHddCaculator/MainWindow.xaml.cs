using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SurveillanceHddCaculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        float stream =0;
        int framTimes = 1;
        float capacity=0;
        int channel;
        int days = 30;
        int hours = 24;

        const int SecondPerDay = 3600;
        const int BitToByte = 8;
        const int ThousandInBit = 1024;
        public MainWindow()
        {
            InitializeComponent();
            initialControls();

            Update();
        }

        private void initialControls()
        {
            for (int i = 1; i <= 16; i++)
                channelCombo.Items.Add(i);
            channelCombo.SelectedIndex = 7;
            ratioCombo.SelectedIndex = 3;
            FramCombo.SelectedIndex = 1;
            hourTextBox.Text = "24";
            dayTextBox.Text = "30";
        }

        private void channelCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            channel = int.Parse(channelCombo.SelectedItem.ToString());
            Update();
        }

        private void ratioCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ratio = ratioCombo.SelectedIndex;
            switch (ratio)
            {
                case 0:
                    stream = 128;
                    break;
                case 1:
                    stream = 256;
                    break;
                case 2:
                    stream = 512;
                    break;
                case 3:
                    stream = 1024;
                    break;
                case 4:
                    stream = 2048;
                    break;
                case 5:
                    stream = 4096;
                    break;
                case 6:
                    stream = 6656;
                    break;
            }
            Update();
        }

        private void FramCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FramCombo.SelectedIndex == 0) framTimes = 2;
            else framTimes = 1;
            Update();
        }

        private float CalculateStream(float stream, int framTimes)
        {
            return stream *= framTimes;
        }

        private void Update()
        {
            StreamTextBlock.Text = CalculateStream(stream, framTimes).ToString();
            resultTextBlock.Text = CalculateCapcity().ToString();
        }

        private float CalculateCapcity()
        {
            capacity = stream / BitToByte * SecondPerDay * hours * days / ThousandInBit / ThousandInBit / ThousandInBit * channel * framTimes;
            return capacity;
        }

        private void hourTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int.Parse(hourTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Please input integer between 1 to 24");
                hourTextBox.Text = "24";
            }

            if (int.Parse(hourTextBox.Text) <= 0 || int.Parse(hourTextBox.Text) > 24)
            {
                MessageBox.Show("Please input integer between 1 to 24");
                hourTextBox.Text = "24";
            }
            hours = int.Parse(hourTextBox.Text);
            Update();
        }

        private void dayTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int.Parse(dayTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Please input integer between 1 to 60");
                dayTextBox.Text = "30";
            }

            if (int.Parse(dayTextBox.Text) <= 0 || int.Parse(dayTextBox.Text) > 60)
            {
                MessageBox.Show("Please input integer between 1 to 60");
                dayTextBox.Text = "30";
            }
            days = int.Parse(dayTextBox.Text);
            Update();
        }
    }
}
