using MaterialDesignThemes.Wpf;
using ProcessDemo.Commons.Database;
using System.Windows;
using System.Windows.Media;

namespace ProcessDemo.WPF
{

    public partial class MainWindow : Window 
    {
        
        public MainWindow()
        {
            InitializeComponent();
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
