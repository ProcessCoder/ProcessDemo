using ProcessDemo.Commons;
using ProcessDemo.Commons.Database;
using ProcessDemo.Commons.Helper;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ProcessDemo.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window , INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChange<T>(ref T field, T newValue, [CallerMemberName] string propertyname = null)
        {
            field = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        #endregion

        private List<AppleTree> appleTrees;
        public List<AppleTree> AppleTrees 
        { 
            get => appleTrees; 
            set => RaisePropertyChange(ref appleTrees,value); 
        }

        public MainWindow()
        {
            
            InitializeComponent();

            //We query all Apple Trees from the database on StartUp
            AppleTrees = AppleTreeHelper.GetAppleTreeAll().ToList();

            //The MainWindow's DataContext is the current MainWindow instance itself
            DataContext = this;
        }

        private void btnGenerateTrees_Click(object sender, RoutedEventArgs e)
        {
            //Append 10 new trees to our List
            AppleTreeDbInitializer.InitialiseTrees();
            AppleTrees = AppleTreeHelper.GetAppleTreeAll().ToList();
        }

        private void btnSortByYieldAscending_Click(object sender, RoutedEventArgs e)
        {
            //Order Apple trees by apple yield
            AppleTrees = AppleTrees.OrderBy(x=>x.AppleYield).ToList();
        }

        private void btnDeleteAllTrees_Click(object sender, RoutedEventArgs e)
        {
            AppleTreeHelper.DeleteAppleTreeAll();
            AppleTrees = AppleTreeHelper.GetAppleTreeAll().ToList();
        }
    }
}
