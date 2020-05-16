using ProcessDemo.Commons.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ProcessDemo.Commons
{
    public class AppleTree: INotifyPropertyChanged
    {
        private double appleYield;
        private double waterConsumption;
        private Fertilizer fertilizingAgent;

        [Key]
        public int Id { get; set; }

        public double AppleYield { get =>appleYield; set => RaisePropertyChange(ref appleYield,value); }
        public double WaterConsumption { get => waterConsumption; set => RaisePropertyChange(ref waterConsumption, value); }
        public Fertilizer FertilizingAgent { get => fertilizingAgent; set => RaisePropertyChange(ref fertilizingAgent,value); }

        public AppleTree()
        {

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
