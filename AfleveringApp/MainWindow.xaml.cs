using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace AfleveringApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<User> users;

        public MainWindow()
        {
            
            InitializeComponent();

            users = new List<User>()
            {
                new User(123, "Testerson", 50, 1),
                new User(321, "Testman", 100, 0)
            };
            ListBox1.ItemsSource = users;
            infoGrid.DataContext = users;
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {

                try
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog.FileName);

                    String line;

                    int counter = 0;

                    List<User> newUsers = new List<User>();

                    while ((line = file.ReadLine()) != null)
                    {
                        newUsers.Add(new User(line));
                        counter++;
                    }

                    users = newUsers;

                    ListBox1.ItemsSource = users;
                    infoGrid.DataContext = users;

                    statusText.Text = "File has been opened: " + DateTime.Now.ToString() + ", " + counter + " users have been loaded";

                    file.Close();
                } catch
                {
                    statusText.Text = "Could not open the chosen file";
                }

                

            }
        }
    }



    public class User : INotifyPropertyChanged
    {
        public User(int id, String name, int age, int score)
        {
            this.ID = id;
            this.Name = name;
            this.Age = age;
            this.Score = score;
        }

        public User(String s)
        {
            String[] info = s.Split(';');

            this.ID = int.Parse(info[0]);
            this.Name = info[1];
            this.Age = int.Parse(info[2]);
            this.Score = int.Parse(info[3]);

        }


        private int _id;
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged(nameof(ID));
            }
        }

        private String _name;
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                NotifyPropertyChanged(nameof(Age));
            }
        }

        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                NotifyPropertyChanged(nameof(Score));
            }
        }


        public string ListBoxToString
        {
            get { return Name + " : " + ID; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
