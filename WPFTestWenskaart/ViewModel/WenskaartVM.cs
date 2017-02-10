using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFTestWenskaart.Model;
using WPFTestWenskaart.View;

namespace WPFTestWenskaart.ViewModel
{
    public class WenskaartVM : ViewModelBase
    {
        private Wenskaart kaart;
        //Zie NieuweKaart(), routes werken niet!
        //public static RoutedCommand mijnRouteCtrlN = new RoutedCommand();
        //public static RoutedCommand mijnRouteCtrlO = new RoutedCommand();
        //public static RoutedCommand mijnRouteCtrlS = new RoutedCommand();
        //public static RoutedCommand mijnRouteCtrlF2 = new RoutedCommand();
        public WenskaartVM(Wenskaart nKaart)
        {
            kaart = nKaart;
            ImageBrush brush = new ImageBrush();
            Uri bron = new Uri("pack://application:,,,/Images/vuilnisbak.png", UriKind.Absolute);
            brush.ImageSource = new BitmapImage(bron);
            Vuilbak = brush;
            //CommandBinding mijnCtrlN = new CommandBinding(mijnRouteCtrlN, ctrlNExecuted); // ctrlNExecutes nog definiëren
            //this.CommandBindings.Add(mijnCtrlN); // Werkt niet!
            NieuweKaart();
        }
        public List<string> Kleuren
        {
            get
            {
                return kaart.Kleuren;
            }
            set
            {
                kaart.Kleuren = value;
                RaisePropertyChanged("Kleuren");
            }
        }
        public Brush Kleur
        {
            get
            {
                return kaart.Kleur;
            }
            set
            {
                kaart.Kleur = value;
                RaisePropertyChanged("Kleur");
            }
        }
        public List<string> Lettertypes
        {
            get
            {
                return kaart.Lettertypes;
            }
            set
            {
                kaart.Lettertypes = value;
                RaisePropertyChanged("Lettertypes");
            }
        }
        public FontFamily Lettertype
        {
            get
            {
                return kaart.Lettertype;
            }
            set
            {
                kaart.Lettertype = value;
                RaisePropertyChanged("Lettertype");
            }
        }
        public int Lettergrootte
        {
            get
            {
                return kaart.Lettergrootte;
            }
            set
            {
                kaart.Lettergrootte = value;
                RaisePropertyChanged("Lettergrootte");
            }
        }
        public string Tekst
        {
            get
            {
                return kaart.Tekst;
            }
            set
            {
                kaart.Tekst = value;
                NietLeeg = "True";
                RaisePropertyChanged("Tekst");
            }
        }
        public string Zichtbaar
        {
            get
            {
                return kaart.Zichtbaar;
            }
            set
            {
                kaart.Zichtbaar = value;
                RaisePropertyChanged("Zichtbaar");
            }
        }
        public ImageBrush Achtergrond
        {
            get
            {
                return kaart.Achtergrond;
            }
            set
            {
                kaart.Achtergrond = value;
                RaisePropertyChanged("Achtergrond");
            }
        }
        public Shape Bal
        {
            get
            {
                return kaart.Bal;
            }
            set
            {
                kaart.Bal = value;
                RaisePropertyChanged("Bal");
            }
        }
        public ObservableCollection<Shape> Ballen
        {
            get
            {
                return kaart.Ballen;
            }
            set
            {
                kaart.Ballen = value;
                RaisePropertyChanged("Ballen");
            }
        }
        public ImageBrush Vuilbak
        {
            get
            {
                return kaart.Vuilbak;
            }
            set
            {
                kaart.Vuilbak = value;
                RaisePropertyChanged("Vuilbak");
            }
        }
        public string Path
        {
            get
            {
                return kaart.Path;
            }
            set
            {
                kaart.Path = value;
                RaisePropertyChanged("Path");
            }
        }
        public string NietLeeg
        {
            get
            {
                return kaart.NietLeeg;
            }
            set
            {
                kaart.NietLeeg = value;
                RaisePropertyChanged("NietLeeg");
            }
        }
        public RelayCommand NieuwCommand
        {
            get { return new RelayCommand(NieuweKaart); }
        }
        private void NieuweKaart()
        {
            Zichtbaar = "Hidden";
            Tekst = "Je tekst hier";
            Lettergrootte = 18;
            Path = "nieuw";
            NietLeeg = "False";
        }
        public RelayCommand OpslaanCommand
        {
            get { return new RelayCommand(OpslaanBestand); }
        }
        private void OpslaanBestand()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "wenskaart";
                dlg.DefaultExt = ".wkt";
                dlg.Filter = "Wenskaart documents |*.wkt";
                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(Tekst);
                        bestand.WriteLine(Lettertype.ToString());
                        bestand.WriteLine(Lettergrootte.ToString());
                        bestand.WriteLine(Achtergrond.ImageSource.ToString()); 
                        // Aantal en Coördinaten ballen moeten er nog bij
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("opslaan mislukt : " + ex.Message);
            }
        }
        public RelayCommand OpenenCommand
        {
            get { return new RelayCommand(OpenenBestand); }
        }
        private void OpenenBestand()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "";
                dlg.DefaultExt = ".wkt";
                dlg.Filter = "Wenskaart documents |*.wkt";
                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        Tekst = bestand.ReadLine();
                        Lettertype = new FontFamily(bestand.ReadLine());
                        Lettergrootte = int.Parse(bestand.ReadLine());
                        ImageBrush brush = new ImageBrush();
                        Uri bron = new Uri(bestand.ReadLine(), UriKind.Absolute);
                        brush.ImageSource = new BitmapImage(bron);
                        Achtergrond = brush;
                        // Aantal en Coördinaten ballen moeten er nog bij
                        Path = dlg.FileName;
                        Setup();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("openen mislukt : " + ex.Message);
            }
        }
        public RelayCommand AfsluitenCommand
        {
            get { return new RelayCommand(AfsluitenApp); }
        }
        private void AfsluitenApp()
        {
            Application.Current.MainWindow.Close();
        }
        public RelayCommand<CancelEventArgs> ClosingCommand
        {
            get { return new RelayCommand<CancelEventArgs>(OnWindowClosing); }
        }
        public void OnWindowClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("Wilt u het programma sluiten ?", "Afsluiten", 
            MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) ==
            MessageBoxResult.No)
                e.Cancel = true;
        }
        private void VulKleuren()
        {
            Kleuren = new List<String>();
            foreach (PropertyInfo info in typeof(Colors).GetProperties())
            {
                Kleuren.Add(info.Name);
            }
        }
        private void VulLettertypes()
        {
            Lettertypes = new List<String>();
            foreach (FontFamily info in Fonts.SystemFontFamilies)
            {
                Lettertypes.Add(info.ToString());
            }
            Lettertypes.Sort();
        }
        private void Setup()
        {
            Zichtbaar = "Visible";
            Kleuren = null;
            Lettertypes = null;
            VulKleuren();
            VulLettertypes();
        }
        public RelayCommand KerstCommand
        {
            get { return new RelayCommand(KerstKaart); }
        }
        private void KerstKaart()
        {
            //Zichtbaar = "Visible";
            ImageBrush brush = new ImageBrush();
            Uri bron = new Uri("pack://application:,,,/Images/kerstkaart.jpg", UriKind.Absolute);
            brush.ImageSource = new BitmapImage(bron);
            Achtergrond = brush;
            Setup();
        }
        public RelayCommand GeboorteCommand
        {
            get { return new RelayCommand(GeboorteKaart); }
        }
        private void GeboorteKaart()
        {
            //Zichtbaar = "Visible";
            ImageBrush brush = new ImageBrush();
            Uri bron = new Uri("pack://application:,,,/Images/geboortekaart.jpg", UriKind.Absolute);
            brush.ImageSource = new BitmapImage(bron);
            Achtergrond = brush;
            Setup();
        }
        public RelayCommand MeerCommand
        { get { return new RelayCommand(MeerBetalen); } }
        private void MeerBetalen()
        {
            if (Lettergrootte < 40)
                Lettergrootte++;
        }
        public RelayCommand MinderCommand
        { get { return new RelayCommand(MinderBetalen); } }
        private void MinderBetalen()
        {
            if (Lettergrootte > 10)
                Lettergrootte--;
        }

    }
}
