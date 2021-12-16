using SrezShend.Classes;
using SrezShend.Moduel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SrezShend.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMaterials.xaml
    /// </summary>
    public partial class PageMaterials : Page
    {
        Switcher switcher;

        public PageMaterials()
        {
            InitializeComponent();
            DBimgPathRename();

            lbMat.ItemsSource = DB.db.Material.ToList();
            tbCountAll.Text = lbMat.Items.Count.ToString();

            cbFilter.Items.Add("Фильтрация");
            foreach (var matT in DB.db.MaterialType)
            {
                cbFilter.Items.Add(matT.Title);
            }
            cbFilter.SelectedIndex = 0;

            cbSort.Items.Add("Сортировка");
            cbSort.Items.Add("По наименованию");
            cbSort.Items.Add("По остаткам");
            cbSort.Items.Add("По стоимости");
            cbSort.SelectedIndex = 0;

            EditMat.Visibility = Visibility.Hidden;
        }

        public void FindMat()
        {
            var mats = DB.db.Material.Where(x => x.Title.Contains(tbFind.Text)).ToList();

            switch (cbSort.SelectedIndex)
            {
                case 1:
                    if (rbAsc.IsChecked == true)
                    {
                        mats = mats.OrderBy(m => m.Title).ToList();
                    }
                    else
                        mats = mats.OrderByDescending(m => m.Title).ToList();
                    break;
                case 2:
                    if (rbAsc.IsChecked == true)
                    {
                        mats = mats.OrderBy(m => m.CountInStock).ToList();
                    }
                    else
                        mats = mats.OrderByDescending(m => m.CountInStock).ToList();
                    break;
                case 3:
                    if (rbAsc.IsChecked == true)
                    {
                        mats = mats.OrderBy(m => m.Cost).ToList();
                    }
                    else
                        mats = mats.OrderByDescending(m => m.Cost).ToList();
                    break;
            }

            if (cbFilter.SelectedIndex > 0)
            {
                string matType = cbFilter.SelectedItem.ToString();
                mats = mats.Where(x => x.MaterialType.Title == matType).ToList();
            }
            tbCount.Text = mats.Count.ToString();
            switcher = new Switcher(mats, lbMat);

            gridSwitcher.Children.Clear();
            gridSwitcher.Children.Add(switcher.GridSwitcher);
        }


        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindMat();
            if (cbSort.SelectedIndex == 0)
            {
                rbAsc.IsEnabled = false;
                rbDesc.IsEnabled = false;
            }
            else
            {
                rbAsc.IsEnabled = true;
                rbDesc.IsEnabled = true;
            }
        }

        private void tbFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindMat();
        }


        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindMat();
        }

        private void AddMat_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.frameMain.Navigate(new PageAddMat(new Material()));
        }

        private void EditMat_Click(object sender, RoutedEventArgs e)
        {
            var matSelect = lbMat.SelectedItem;
            FrameObj.frameMain.Navigate(new PageAddMat((Material)matSelect));
        }

        private void radioButton_Click(object sender, RoutedEventArgs e)
        {
            FindMat();
        }

        private void lbMat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMat.SelectedItem != null)
            {
                EditMat.Visibility = Visibility.Visible;
            }
        }
        private void DBimgPathRename()
        {
            List<Material> materials = DB.db.Material.ToList();

            for (int i = 0; i < materials.Count; i++)
            {
                if(materials[i].Image != null)
                {
                    if (materials[i].Image.StartsWith("/materials/"))
                    {
                        materials[i].Image = "/img" + materials[i].Image;
                    }
                }
                
            }
            DB.db.SaveChanges();
        }
    }
}
