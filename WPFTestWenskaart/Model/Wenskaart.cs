using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFTestWenskaart.Model
{
    public class Wenskaart
    {
        public List<string> Kleuren { get; set; }
        public Brush Kleur { get; set; }
        public List<string> Lettertypes { get; set; }
        public FontFamily Lettertype { get; set; }
        public int Lettergrootte { get; set; }
        public string Tekst { get; set; }
        public string Zichtbaar { get; set; }
        public ImageBrush Achtergrond { get; set; }
        public Ellipse Bal { get; set; }
        public ObservableCollection<Bal> Ballen { get; set; }
        public ImageBrush Vuilbak { get; set; }
        public string Path { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Point Point { get; set; }
        public int Aantal { get; set; }
        public string Kerst { get; set; }
        public string Geboorte { get; set; }
        public string OpslaanAfdruk { get; set; }
    }
    public class Bal
    {
        public Bal(double x, double y, Ellipse ellips, Brush kleur)
        {
            X = x;
            Y = y;
            Ellips = ellips;
            Kleur = kleur;
        }
        private double x;
        private double y;
        private Ellipse ellips;
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        public Ellipse Ellips
        {
            get { return ellips; }
            set { ellips = value; }
        }
        public Brush Kleur
        {
            get { return Ellips.Fill; }
            set { Ellips.Fill = value; }
        }
    }
}
