using ProcessDemo.Commons;
using ProcessDemo.Commons.Helper;
using ProcessDemo.Commons.Models;
using ProcessDemo.WPF.DataAccess;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProcessDemo.WPF.ViewModels
{
    public class FarmViewModel:INotifyPropertyChanged
    {
        //private fields
        private FarmHelper _farmHelper;
        private AppleTreeHelper _appleTreeHelper;
        private Farm selectedfarm;
        private IList<AppleTree> trees;
        private string searchString;

        //Searchstring for farms
        public string SearchString
        {
            get => searchString;
            set
            {
                RaisePropertyChange(ref searchString, value);
                //The farm list is updated in the searchstring setter to get an immediate response
                Farms = searchString == "" ? _farmHelper.GetFarmAll() : _farmHelper.FindFarms(c => c.Name.Contains(searchString));
            }
        }

        //The list of trees to be displayed in the tree groupbox
        public IList<AppleTree> Trees 
        { 
            get=> trees; 
            set => RaisePropertyChange(ref trees,value); 
        }

        //The list of farms to be displayed in the farms groupbox
        private IList<Farm> farms;
        public IList<Farm> Farms 
        { 
            get=> farms; 
            set=>RaisePropertyChange(ref farms, value); 
        }
       
       //The manually selected farm
        public Farm SelectedFarm { 
            get=> selectedfarm;
            set
            {
                RaisePropertyChange(ref selectedfarm, value);
                //The tree list is updated whenever the selected farm changes
                Trees = selectedfarm == null ? null : _appleTreeHelper.FindTrees(c => c.FarmId == selectedfarm.FarmId);
            }
        }
        
        //constructor
        public FarmViewModel()
        {
            //Initialising helpers for database access
            _farmHelper = Instances.GetFarmHelper.Value;
            _appleTreeHelper = Instances.GetAppleTreeHelper.Value;

            //Initialising the searchstring to an empty string so all farms are displayed
            SearchString = "";
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChange<T>(ref T field, T newValue, [CallerMemberName] string propertyname = null)
        {
            field = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        #endregion
    }


}
