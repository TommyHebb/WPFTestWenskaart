using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Shape Bal { get; set; }
        public ObservableCollection<Shape> Ballen { get; set; }
        public ImageBrush Vuilbak { get; set; }
        public string Path { get; set; }
        public string NietLeeg { get; set; }
    }
}
