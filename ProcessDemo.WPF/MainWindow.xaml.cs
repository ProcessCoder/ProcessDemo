using MaterialDesignThemes.Wpf;
using ProcessDemo.Commons;
using ProcessDemo.Commons.Helper;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        //Changed List to ObservableCollection because it implements the ICollectionChanged Interface
        private ObservableCollection<AppleTree> appleTrees;
        public ObservableCollection<AppleTree> AppleTrees 
        { 
            get => appleTrees; 
            set => RaisePropertyChange(ref appleTrees,value); 
        }


        public MainWindow()
        {
            
            InitializeComponent();

            //We query all Apple Trees from the database on StartUp
            AppleTrees = new ObservableCollection<AppleTree>(AppleTreeHelper.GetAppleTreeAll());

            //The MainWindow's DataContext is the current MainWindow instance itself
            DataContext = this;
        }

     

        private void btnSortByYieldAscending_Click(object sender, RoutedEventArgs e)
        {
            //Order Apple trees by apple yield
            AppleTrees = new ObservableCollection<AppleTree>(AppleTrees.OrderBy(x=>x.AppleYield));
        }

        private void btnDeleteAllTrees_Click(object sender, RoutedEventArgs e)
        {
            AppleTreeHelper.DeleteAppleTreeAll();
            AppleTrees = new ObservableCollection<AppleTree>(AppleTreeHelper.GetAppleTreeAll());
        }

        #region Change theme
        private void btnChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            //If the background is black, we switch to the Light theme,if not we choose the dark one
            if (theme.Background == (Color)ColorConverter.ConvertFromString("#FF000000"))
            {
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }
        #endregion

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var appletree = (AppleTree)((Button)sender).DataContext;
                AppleTreeHelper.DeleteAppleTreeById(appletree.Id);
                AppleTrees = new ObservableCollection<AppleTree>(AppleTreeHelper.GetAppleTreeAll());
                MessageBox.Show($"Tree with Id {appletree.Id} deleted");
            }
            catch(System.InvalidCastException)
            {
                MessageBox.Show("This row is empty and cannot be deleted.");
            }
            
        }

        private void dgData_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            var appletree = (AppleTree)((DataGrid)sender).SelectedValue;
            if(AppleTreeHelper.GetAppleTreeAll().Where(c=>c.Id==appletree.Id).Any())
            {
                AppleTreeHelper.UpdateTree(appletree);
                MessageBox.Show($"Updated tree with Id {appletree.Id}");
            }
            else
            {
                AppleTreeHelper.CreateAppleTree(appletree);
                MessageBox.Show($"New tree created");
            }
            AppleTrees = new ObservableCollection<AppleTree>(AppleTreeHelper.GetAppleTreeAll());

        }
    }
}
