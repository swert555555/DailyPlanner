using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace WpfAppPract2
{
    public partial class DailyPlanner : Window
    {
        private int index_del;
        private List<Tasks> tasks = new List<Tasks>();
        private List<Tasks> tasks_all = new List<Tasks>();
        private readonly string PATH = "C:\\Users\\Kotam\\OneDrive\\Рабочий стол\\DP.json";
        public DailyPlanner()
        {
            InitializeComponent();
            
            spisok.BorderBrush = Brushes.Black;
            spisok.BorderThickness = new Thickness(5, 10, 15, 20);

            Date.Text = DateTime.Now.ToString();
            Date.BorderBrush = Brushes.Black;
            
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text != "")
            {
                Tasks item = new Tasks(Name.Text, Description.Text, Date.Text);
                tasks.Add(item); 
                spisok.Items.Refresh(); //обновляем отображение в списке //эт нам нада!
                Name.Text = "";
                Description.Text = "";
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e) //ОНО УДАЛЯЕЕЕЕЕЕТ, ЕЕЕЕЕЕЕЕЕЕЕЕ
        {
            if (spisok.SelectedIndex != null)
            {
                Name.Text = "";
                Description.Text = "";
                (spisok.SelectedItem as Tasks).Name = "";
                (spisok.SelectedItem as Tasks).Description = "";
                tasks.Remove(spisok.SelectedItem as Tasks);
                spisok.Items.Refresh();
            }
            
        }

        private void spisok_SelectionChanged(object sender, SelectionChangedEventArgs e) //ОНО РАБОТАЕЕЕЕЕЕЕЕЕЕЕТТТТТТТТТТТТТТТТТТТТ
        {
            if ((spisok.SelectedItem as Tasks) != null)
            {
                Name.Text = (spisok.SelectedItem as Tasks).Name;
                Description.Text = (spisok.SelectedItem as Tasks).Description;
            }
        }

        private void spisok_Loaded(object sender, RoutedEventArgs e)
        {
            Zagruzka();
        }

        private void Zagruzka()
        {
            FileIOService _fileIOService = new FileIOService(PATH);
            tasks_all = _fileIOService.Deserialize<List<Tasks>>();
            tasks.Clear();
            foreach (Tasks task in tasks_all)
            {
                if ((string)task.Date == Date.Text) tasks.Add(task);
            }
            spisok.ItemsSource = tasks;
        }

        private void Sefe_Click(object sender, RoutedEventArgs e) //нажимать после криэйт и в идеале до делете!
        {
            if (Name.Text != "")
            {
                (spisok.SelectedItem as Tasks).Name = Name.Text;
                (spisok.SelectedItem as Tasks).Description = Description.Text;
            }
            spisok.Items.Refresh();
            FileIOService _fileIOService = new FileIOService(PATH);
            _fileIOService.Serialize<List<Tasks>>(tasks);
            
        }

        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e) //НЭ РАБОТАЕТ(((
        {
            //MessageBox.Show("hui");
            /*tasks.Clear();
            foreach (var task in tasks_all)
            {
                if ((string)task.Date == Date.Text) tasks.Add(task);
            }
            spisok.Items.Refresh();*/
            Zagruzka();
        }
    }
}
