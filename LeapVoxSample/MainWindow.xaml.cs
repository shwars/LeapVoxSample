using Leap;
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
using System.Windows.Threading;

namespace LeapVoxSample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Controller Leap = new Controller();

        DispatcherTimer dt = new DispatcherTimer() 
            { 
                Interval = TimeSpan.FromSeconds(1 / 30)
            };

        public MainWindow()
        {
            InitializeComponent();
            dt.Tick += dt_Tick;
            dt.Start();
        }

        void dt_Tick(object sender, EventArgs e)
        {
            var fr = Leap.Frame();
            if (fr!=null && fr.Fingers.Count>0)
            {
                var f = fr.Fingers[0];
                // Устанавливаем координаты кружка
                Canvas.SetLeft(ptr, 512 + f.TipPosition.x);
                Canvas.SetTop(ptr, 768-f.TipPosition.y);
            }
        }
    }
}
