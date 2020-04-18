using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace ProcessDemo.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            #region define Culture
            try
            {

                Thread.CurrentThread.CurrentCulture = new CultureInfo("de-AT");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-AT");
                FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred. Exception: {ex.GetType().FullName}, Error source: {ex.Source}, Message: {ex.Message}");
            }
            #endregion
        }

    }
}
