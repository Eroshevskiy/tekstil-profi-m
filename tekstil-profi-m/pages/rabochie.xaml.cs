using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using tekstil_profi_m.models;

namespace tekstil_profi_m.pages
{
    /// <summary>
    /// Логика взаимодействия для rabochie.xaml
    /// </summary>
    public partial class rabochie : Window
    {
        private ObservableCollection<Otvetstvenie> otvCollection;
        public rabochie()
        {
            InitializeComponent();
            otvCollection = new ObservableCollection<Otvetstvenie>(dipEntitie.GetContext().Otvetstvenie.ToList());
            BDotvet.ItemsSource = otvCollection;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            addRab add = new addRab(null);
            Visibility = Visibility.Hidden;
            add.Show();
        }

        private void nazClick(object sender, RoutedEventArgs e)
        {
            admin adm = new admin();
            Visibility = Visibility.Hidden;
            adm.Show();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            editRab edit = new editRab((sender as Button).DataContext as Otvetstvenie);
            Visibility = Visibility.Hidden;
            edit.Show();

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var otvetDell = BDotvet.SelectedItems.Cast<Otvetstvenie>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {otvetDell.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    IEnumerable<Otvetstvenie> enumerable = dipEntitie.GetContext().Otvetstvenie.RemoveRange((IEnumerable<Otvetstvenie>)otvetDell);
                    dipEntitie.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    BDotvet.ItemsSource = dipEntitie.GetContext().Otvetstvenie.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }


    }
}
