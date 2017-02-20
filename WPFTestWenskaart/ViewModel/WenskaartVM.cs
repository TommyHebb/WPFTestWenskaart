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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFTestWenskaart.Model;
//Nog te implementeren:
//    Printvoorbeeld/Printen
//    Vuilbak
//    Verplaatsen bal op Canvas
namespace WPFTestWenskaart.ViewModel
{
    public class WenskaartVM : ViewModelBase
    {
        private Wenskaart kaart;
        public WenskaartVM(Wenskaart nKaart)
        {
            kaart = nKaart;
            ImageBrush brush = new ImageBrush();
            Uri bron = new Uri("pack://application:,,,/Images/vuilnisbak.png", UriKind.Absolute);
            brush.ImageSource = new BitmapImage(bron);
            Vuilbak = brush;
            VulKleuren();
            VulLettertypes();
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
        private ObservableCollection<Bal> ballenValue = new ObservableCollection<Bal>();
        public ObservableCollection<Bal> Ballen
        {
            get
            {
                return ballenValue;
            }
            set
            {
                ballenValue = value;
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
        public string Kerst
        {
            get
            {
                return kaart.Kerst;
            }
            set
            {
                kaart.Kerst = value;
                RaisePropertyChanged("Kerst");
            }
        }
        public string Geboorte
        {
            get
            {
                return kaart.Geboorte;
            }
            set
            {
                kaart.Geboorte = value;
                RaisePropertyChanged("Geboorte");
            }
        }
        public string OpslaanAfdruk
        {
            get
            {
                return kaart.OpslaanAfdruk;
            }
            set
            {
                kaart.OpslaanAfdruk = value;
                RaisePropertyChanged("OpslaanAfdruk");
            }
        }
        public RelayCommand NieuwCommand
        {
            get { return new RelayCommand(NieuweKaart); }
        }
        private void NieuweKaart()
        {
            Zichtbaar = "Hidden";
            Kerst = "False";
            Geboorte = "False";
            OpslaanAfdruk = "False";
            Tekst = "Je tekst hier";
            Lettergrootte = 18;
            Path = "nieuw";
            Ballen.Clear();
            Kleur = Brushes.AliceBlue;
            Lettertype = new FontFamily("Agency FB");
            Aantal = 0;
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
                        bestand.WriteLine(Achtergrond.ImageSource.ToString());  // Regel 1 - URI
                        bestand.WriteLine(Aantal.ToString());                   // Regel 2 - Aantal ballen
                        foreach (Bal child in Ballen)                           // regel 3...Ballen (kleur;X;Y)
                        {
                            bestand.WriteLine(child.Kleur.ToString() + ';' + child.X.ToString() + ';' + child.Y.ToString());
                        }
                        bestand.WriteLine(Tekst);                               // 3e laatste regel - Tekstje
                        bestand.WriteLine(Lettertype.ToString());               // Voorlaatste regel - Lettertype
                        bestand.WriteLine(Lettergrootte.ToString());            // Laatste regel - Lettergrootte
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
                        ImageBrush brush = new ImageBrush();
                        string uriString = bestand.ReadLine();              // Regel 1 - URI
                        Uri bron = new Uri(uriString, UriKind.Absolute);
                        brush.ImageSource = new BitmapImage(bron);
                        Achtergrond = brush;
                        int positieA = uriString.LastIndexOf("/");
                        string soortKaart = uriString.Substring(positieA + 1);
                        int positieB = soortKaart.LastIndexOf(".");
                        soortKaart = soortKaart.Remove(positieB, 4);
                        if (soortKaart == "kerstkaart")
                        {
                            Kerst = "True";
                            Geboorte = "False";
                        }
                        else
                        {
                            Kerst = "False";
                            Geboorte = "True";
                        }
                        Ballen.Clear();
                        Aantal = int.Parse(bestand.ReadLine());             // Regel 2 - Aantal ballen
                        for (int i = 1; i <= Aantal; i++)
                        {
                            string lijn = bestand.ReadLine();               // Regel 3...Ballen (kleur;X;Y)
                            var delen = lijn.Split(new[] { ';' });
                            Kleur = (Brush)new BrushConverter().ConvertFromString(delen[0]);
                            X = double.Parse(delen[1]);
                            Y = double.Parse(delen[2]);
                            Ellipse balletje = new Ellipse();
                            Bal bal = new Model.Bal(X, Y, balletje, Kleur);
                            Ballen.Add(bal);
                        }
                        Tekst = bestand.ReadLine();                         // 3e laatste regel - Tekstje
                        Lettertype = new FontFamily(bestand.ReadLine());    // Voorlaatste regel - Lettertype
                        Lettergrootte = int.Parse(bestand.ReadLine());      // Laatste regel - Lettergrootte
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
            if (Geboorte == "True")
            {
                Geboorte = "False";
                Tekst = "Je tekst hier";
                Lettergrootte = 18;
                Path = "nieuw";
                Ballen.Clear();
                Aantal = 0;
            }
            Kerst = "True";
            OpslaanAfdruk = "True";
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
            if (Kerst == "True")
            {
                Kerst = "False";
                Tekst = "Je tekst hier";
                Lettergrootte = 18;
                Path = "nieuw";
                Ballen.Clear();
                Aantal = 0;
            }
            Geboorte = "True";
            OpslaanAfdruk = "True";
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
        public RelayCommand<MouseEventArgs> Bal_MouseMove
        {
            get { return new RelayCommand<MouseEventArgs>(OnMouseMove); }
        }
        private Ellipse sleepbal = new Ellipse();
        private void OnMouseMove(MouseEventArgs e)
        {
            sleepbal = (Ellipse)e.OriginalSource;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(sleepbal, Kleur, DragDropEffects.Move);
            }
        }
        public RelayCommand<DragEventArgs> Bal_MouseDrop
        {
            get { return new RelayCommand<DragEventArgs>(OnMouseDrop); }
        }
        private void OnMouseDrop(DragEventArgs e)
        {
            ItemsControl doekje = (ItemsControl)e.Source;
            Point point = e.GetPosition(doekje);
            X = point.X - 20;
            Y = point.Y - 20;
            Ellipse balletje = new Ellipse();
            Bal bal = new Bal(X, Y, balletje, Kleur);
            Ballen.Add(bal);
            Aantal = Ballen.Count();
        }
        public RelayCommand<MouseEventArgs> BalCanvas_MouseMove
        {
            get { return new RelayCommand<MouseEventArgs>(OnCanvasMouseMove); }
        }
        private Ellipse canvasSleepbal = new Ellipse();
        private void OnCanvasMouseMove(MouseEventArgs e)
        {
            canvasSleepbal = (Ellipse)e.OriginalSource;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(canvasSleepbal, Kleur, DragDropEffects.Move);
            }
        }
        public RelayCommand<DragEventArgs> BalDelete_MouseDrop
        {
            get { return new RelayCommand<DragEventArgs>(OnDeleteMouseDrop); }
        }
        private void OnDeleteMouseDrop(DragEventArgs e)
        {

        }
    }
}
