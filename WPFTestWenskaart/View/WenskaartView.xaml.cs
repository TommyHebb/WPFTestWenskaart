using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
namespace WPFTestWenskaart.View
{
    /// <summary>
    /// Interaction logic for WenskaartView.xaml
    /// </summary>
    public partial class WenskaartView : Window
    {
        public WenskaartView()
        {
            InitializeComponent();
        }
        private Ellipse sleepellips = new Ellipse();
        private void Ellips_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sleepellips = (Ellipse)sender;
            if ((e.LeftButton == MouseButtonState.Pressed) && (sleepellips.Fill != Brushes.White))
            {
                DataObject sleepKleur = new DataObject("deKleur", sleepellips.Fill);
                DragDrop.DoDragDrop(sleepellips, sleepKleur, DragDropEffects.Move);
            }
        }
        private void CanVas_Drop(object sender, DragEventArgs e)
        {
            Ellipse bal = new Ellipse();
            bal.Fill = sleepellips.Fill;
            Canvas doek = (Canvas)sender;
            Point point = e.GetPosition(doek);
            double x = point.X - 20;
            double y = point.Y - 20;
            Canvas.SetLeft(bal, x);
            Canvas.SetTop(bal, y);
            doek.Children.Add(bal);
            //ViewModel.WenskaartVM.Ballen.Add(bal); // Lukt niet
        }
    }
}
