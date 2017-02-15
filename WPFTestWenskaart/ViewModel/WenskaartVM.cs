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
            VulKleuren();
            VulLettertypes();
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
        public Ellipse Bal
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
        public double X
        {
            get
            {
                return kaart.X;
            }
            set
            {
                kaart.X = value;
                RaisePropertyChanged("X");
            }
        }
        public double Y
        {
            get
            {
                return kaart.Y;
            }
            set
            {
                kaart.Y = value;
                RaisePropertyChanged("Y");
            }
        }
        private double x;
        private double y;
        public Point Point
        {
            get
            {
                return new Point(x, y);
            }
            set
            {
                x = value.X;
                y = value.Y;
                RaisePropertyChanged("X", "Y", "Point");
            }
        }
        public Canvas Doek
        {
            get
            {
                return kaart.Doek;
            }
            set
            {
                kaart.Doek = value;
                RaisePropertyChanged("Doek");
            }
        }
        public int Aantal
        {
            get
            {
                return kaart.Aantal;
            }
            set
            {
                kaart.Aantal = value;
                RaisePropertyChanged("Aantal");
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
                        //Aantal = Doek.Children.Count;
                        //foreach (Ellipse ellips in Doek.Children)
                        //    // Kan Canvas niet binden aan Doek, 
                        //    // maar kan ObservableCollection ook niet aanvullen vanuit de view (waar drop nu zit)
                        //    // dus zit hier vast
                        //{
                        //    double x = Canvas.GetLeft(ellips);
                        //    double y = Canvas.GetTop(ellips);
                        //    Brush kleur = ellips.Fill;
                        //    bestand.WriteLine(x.ToString());
                        //    bestand.WriteLine(y.ToString());
                        //    bestand.WriteLine(kleur.ToString());
                        //    Ballen.Add(ellips);
                        //}
                        Path = dlg.FileName;
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
                        // Aantal en Coördinaten ballen moeten er nog bij, eens opslaan ervan lukt
                        Path = dlg.FileName;
                        Zichtbaar = "Visible";
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
        public RelayCommand KerstCommand
        {
            get { return new RelayCommand(KerstKaart); }
        }
        private void KerstKaart()
        {
            Zichtbaar = "Visible";
            ImageBrush brush = new ImageBrush();
            Uri bron = new Uri("pack://application:,,,/Images/kerstkaart.jpg", UriKind.Absolute);
            brush.ImageSource = new BitmapImage(bron);
            Achtergrond = brush;
        }
        public RelayCommand GeboorteCommand
        {
            get { return new RelayCommand(GeboorteKaart); }
        }
        private void GeboorteKaart()
        {
            Zichtbaar = "Visible";
            ImageBrush brush = new ImageBrush();
            Uri bron = new Uri("pack://application:,,,/Images/geboortekaart.jpg", UriKind.Absolute);
            brush.ImageSource = new BitmapImage(bron);
            Achtergrond = brush;
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
        //public RelayCommand<MouseEventArgs> Bal_MouseMove
        //{
        //    get { return new RelayCommand<MouseEventArgs>(OnMouseMove); }
        //}
        //private Ellipse sleepbal = new Ellipse();
        //private void OnMouseMove(MouseEventArgs e)
        //{
        //    //sleepbal = (Ellipse)sender;
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        //DataObject sleepkleur = new DataObject("deKleur", sleepbal.Fill);
        //        DragDrop.DoDragDrop(Bal, Kleur, DragDropEffects.Move);
        //    }
        //}
    }
}
