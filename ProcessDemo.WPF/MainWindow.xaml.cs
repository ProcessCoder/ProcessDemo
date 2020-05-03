using MaterialDesignThemes.Wpf;
using ProcessDemo.Commons;
using ProcessDemo.Commons.Database;
using ProcessDemo.Commons.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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

        private async void btnGenerateTrees_Click(object sender, RoutedEventArgs e)
        {
            //We apply an OverrideCursor to singal to the user that there's work in progress
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                //We fetch 10 new trees one by one and add them to our ObservableCollection which is the Itemssource of our DataGrid
                await foreach (var tree in GetAppleTreesAsync())
                {
                    AppleTrees.Add(tree);
                }
            }
            finally
            {
                //After completion, the OverrideCursor can be disposed
                Mouse.OverrideCursor = null;
            }

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


        public async IAsyncEnumerable<AppleTree> GetAppleTreesAsync()
        {
            //We initialize 10 new trees
            var newTrees = AppleTreeDbInitializer.InitialiseTrees();

            //Sum equals 100 % progress
            double sum = newTrees.Count;

            int i = 0;
            foreach (var tree in newTrees)
            {
                //We wait for 200 ms to simulate a long running operation
                await Task.Delay(200);

                //We return the respective tree without exiting the method
                yield return tree;

                //i is augmented with each return
                i++;

                //We output the progress to the Output Window
                Trace.WriteLine($"Progress: {Math.Round(i / sum * 100,1)} %");
            }
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

    }
}
