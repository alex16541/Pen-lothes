using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace SrezShend.Pages
{
    /// <summary>
    /// Логика взаимодействия для WindowSelectImage.xaml
    /// </summary>
    public partial class WindowSelectImage : Window
    {
        private string _uriImgFolder;
        public string imgUri = null;
        public WindowSelectImage(string uriImgFolder)
        {
            _uriImgFolder = uriImgFolder;

            InitializeComponent();

            CreateSpImages();
        }

        private void CreateSpImages()
        {
            string[] files = CreateUriImages();

            const int maxImagesInRow = 5;
            int countRows = files.Length / maxImagesInRow;

            StackPanel spImages = new StackPanel();
            spImages.Orientation = Orientation.Vertical;
            spImages.HorizontalAlignment = HorizontalAlignment.Center;
            spImages.VerticalAlignment = VerticalAlignment.Center;

            for (int i = 0; i < countRows; i++)
            {
                StackPanel spRowImages = new StackPanel();
                spRowImages.Orientation = Orientation.Horizontal;
                spRowImages.Margin = new Thickness(0, 10, 0, 10);

                for (int j = i * maxImagesInRow; j < i * maxImagesInRow + maxImagesInRow; j++)
                {
                    Image img = new Image();
                    img.Width = 64;
                    img.Height = 64;
                    img.Margin = new Thickness(10, 0, 10, 0);
                    img.Cursor = Cursors.Hand;
                    img.Tag = files[j];
                    img.Source = new BitmapImage(new Uri(files[j], UriKind.Relative));
                    img.MouseLeftButtonDown += Img_MouseLeftButtonDown;
                    spRowImages.Children.Add(img);
                }

                spImages.Children.Add(spRowImages);
            }

            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Content = spImages;
            gridMain.Children.Add(scrollViewer);
        }

        private string[] CreateUriImages()
        {
            string[] files = Directory.GetFiles(_uriImgFolder);

            List<string> correctFiles = new List<string>();
            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                correctFiles.Add("/img/materials/" + fileInfo.Name);
            }

            return correctFiles.ToArray();
        }
        private void Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            imgUri = img.Tag.ToString();
            DialogResult = true;
            this.Close();
        }
    }
}
