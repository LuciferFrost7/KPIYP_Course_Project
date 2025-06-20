using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace The_Fallen_Snow_WPF.classes
{
    public class MyButton
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public string Source { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }

        public Image Create()
        {
            var buttonImage = new Image
            {
                Width = this.Width,
                Height = this.Height,
                Source = new BitmapImage(new Uri(this.Source, UriKind.Relative))
            };
            Canvas.SetLeft(buttonImage, this.Left);
            Canvas.SetTop(buttonImage, this.Top);
            return buttonImage;
        }
    }
    internal class MyImage
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Source { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }

        public Image Create()
        {
            try
            {
                var img = new Image
                {
                    Width = Width,
                    Height = Height,
                    Stretch = Stretch.None,
                    Source = new BitmapImage(new Uri(Path.Combine(Directory.GetCurrentDirectory(), Source)))
                };

                RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.NearestNeighbor);
                Canvas.SetLeft(img, Left);
                Canvas.SetTop(img, Top);

                return img;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MyImage.Create] Ошибка: {ex.Message}");
                return null;
            }
        }
    }
}
