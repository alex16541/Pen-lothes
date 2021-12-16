using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SrezShend.Classes
{
    public class Switcher
    {
        private Grid _gridSwitcher = new Grid();
        private int _fontSize = 16;

        private List<Material> _materials = new List<Material>();
        private ListBox _lbMaterial;
        private const int _maxMaterialsInPage = 15;
        private Brush _mainColor = (Brush)new BrushConverter().ConvertFrom("#FFA5E887");
        private StackPanel _spSwitcherPages = new StackPanel();
        private List<TextBlock> _tblSwitcherPages = new List<TextBlock>();
        private int _pageNumber = 0;

        public Grid GridSwitcher
        {
            get
            {
                return _gridSwitcher;
            }
        }

        public Switcher(List<Material> materials, ListBox lbMaterial)
        {
            _materials = materials;
            _lbMaterial = lbMaterial;

            CreateSwitcherPages();
            CreateSpSwitcher();

            ShowPageNumbers();
            ShowElementsOnPage();
        }

        /// <summary>
        /// Создаёт переключатель страниц
        /// </summary>
        private void CreateSpSwitcher()
        {
            _gridSwitcher.ColumnDefinitions.Add(new ColumnDefinition());

            ColumnDefinition column = new ColumnDefinition();
            column.MinWidth = 130;
            _gridSwitcher.ColumnDefinitions.Add(column);

            _gridSwitcher.ColumnDefinitions.Add(new ColumnDefinition());

            TextBlock tblBack = new TextBlock();
            tblBack.Text = "<";
            tblBack.MouseLeftButtonDown += TblBack_MouseLeftButtonDown;
            tblBack.FontSize = _fontSize;
            tblBack.FontWeight = FontWeights.Bold;
            Grid.SetColumn(tblBack, 0);
            _gridSwitcher.Children.Add(tblBack);

            _spSwitcherPages.Orientation = Orientation.Horizontal;
            _spSwitcherPages.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            Grid.SetColumn(_spSwitcherPages, 1);
            _gridSwitcher.Children.Add(_spSwitcherPages);

            TextBlock tblNext = new TextBlock();
            tblNext.Text = ">";
            tblNext.MouseLeftButtonDown += TblNext_MouseLeftButtonDown;
            tblNext.FontWeight = FontWeights.Bold;
            tblNext.FontSize = _fontSize;
            Grid.SetColumn(tblNext, 2);
            _gridSwitcher.Children.Add(tblNext);
        }

        /// <summary>
        /// Создаёт все возможные страницы
        /// </summary>
        private void CreateSwitcherPages()
        {
            if (_materials.Count == 0)
            {
                TextBlock tblPage = new TextBlock();
                tblPage.Text = "1";
                tblPage.TextAlignment = System.Windows.TextAlignment.Center;
                tblPage.MinWidth = 25;
                tblPage.FontSize = _fontSize;
                _tblSwitcherPages.Add(tblPage);
                return;
            }

            for (int i = 0; i < _materials.Count; i += _maxMaterialsInPage)
            {
                TextBlock tblPage = new TextBlock();
                tblPage.Text = (i / _maxMaterialsInPage + 1).ToString();
                tblPage.TextAlignment = System.Windows.TextAlignment.Center;
                tblPage.MinWidth = 25;
                tblPage.FontSize= _fontSize;
                tblPage.Cursor = Cursors.Hand;
                tblPage.FontWeight = FontWeights.Bold;
                tblPage.MouseLeftButtonDown += TblPage_MouseLeftButtonDown;
                _tblSwitcherPages.Add(tblPage);
            }
        }

        /// <summary>
        /// Отображает необходимые элементы в ListBox
        /// </summary>
        private void ShowElementsOnPage()
        {
            List<Material> selectedMaterials = new List<Material>();

            for (int i = _maxMaterialsInPage * _pageNumber;
                i < _maxMaterialsInPage * _pageNumber + _maxMaterialsInPage; i++)
            {
                if (i >= _materials.Count) break;
                selectedMaterials.Add(_materials[i]);
            }

            _lbMaterial.ItemsSource = selectedMaterials;
        }

        /// <summary>
        /// Отображает номера страниц
        /// </summary>
        private void ShowPageNumbers()
        {
            const int countPageNumbers = 5;
            int countPages = _tblSwitcherPages.Count;
            _spSwitcherPages.Children.Clear();

            if (_pageNumber >= 0 && _pageNumber < countPageNumbers - 1)
            {
                for (int i = 0; i < countPageNumbers; i++)
                {
                    if (i >= _tblSwitcherPages.Count) break;
                    _spSwitcherPages.Children.Add(_tblSwitcherPages[i]);
                }
            }
            else if (_pageNumber >= countPageNumbers - 1 && _pageNumber < countPages - countPageNumbers)
            {
                for (int i = _pageNumber; i < _pageNumber + countPageNumbers; i++)
                {
                    if (i >= _tblSwitcherPages.Count) break;
                    _spSwitcherPages.Children.Add(_tblSwitcherPages[i]);
                }
            }
            else
            {
                for (int i = countPages - countPageNumbers; i < countPages; i++)
                {
                    if (i >= _tblSwitcherPages.Count) break;
                    _spSwitcherPages.Children.Add(_tblSwitcherPages[i]);
                }
            }

            _tblSwitcherPages[_pageNumber].Foreground = _mainColor;
        }

        /// <summary>
        /// Событие переключения страницы назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TblBack_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_pageNumber > 0)
            {
                _tblSwitcherPages[_pageNumber].Foreground = Brushes.Black;
                _pageNumber--;
            }

            ShowPageNumbers();
            ShowElementsOnPage();
        }

        /// <summary>
        /// Событие переключения страницы вперёд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TblNext_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int countSwitcherPages = _tblSwitcherPages.Count - 1;
            if (_pageNumber < countSwitcherPages)
            {
                _tblSwitcherPages[_pageNumber].Foreground = Brushes.Black;
                _pageNumber++;
            }

            ShowPageNumbers();
            ShowElementsOnPage();
        }

        /// <summary>
        /// Событие перелючения на конкретную страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void TblPage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock tblPage = (TextBlock)sender;
            
            for (int i = 0; i < _tblSwitcherPages.Count; i++)
            {
                if (_tblSwitcherPages[i] == tblPage)
                {
                    _tblSwitcherPages[_pageNumber].Foreground = Brushes.Black;
                    _pageNumber = i;

                    ShowPageNumbers();
                    ShowElementsOnPage();
                    break;
                }
            }
        }

    }
}
