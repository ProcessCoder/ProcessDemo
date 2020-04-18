using ProcessDemo.Commons;
using ProcessDemo.Commons.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
            AppleTrees = AppleTreeHelper.InitialiseTrees();

            //The MainWindow's DataContext is the current MainWindow instance itself
            DataContext = this;
        }

        private void btnGenerateTrees_Click(object sender, RoutedEventArgs e)
        {
            //Generate 10 new trees
            AppleTrees = AppleTreeHelper.InitialiseTrees();
        }

        private void btnSortByYieldAscending_Click(object sender, RoutedEventArgs e)
        {
            //Order Apple trees by apple yield
            AppleTrees = AppleTrees.OrderBy(x=>x.AppleYield).ToList();
        }

        
    }
}
