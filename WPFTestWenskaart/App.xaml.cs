using System.Windows;

namespace WPFTestWenskaart
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Model.Wenskaart mijnKaart = new Model.Wenskaart();
            ViewModel.WenskaartVM vm = new ViewModel.WenskaartVM(mijnKaart);
            View.WenskaartView mijnWenskaartView = new View.WenskaartView();
            mijnWenskaartView.DataContext = vm;
            mijnWenskaartView.Show();
        }
    }
}
