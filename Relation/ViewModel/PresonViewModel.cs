using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realtion.ViewModel
{
    public class PresonViewModel : INotifyPropertyChanged
    {
        public  PresonViewModel()
        {
            //this._AddCommand=new 
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public string _AddCommand { get; set; }
        public string PersonName { get; set; }
        public string PersonGender { get; set; }
        public string AddBUttonCommand { get; set; }
        public string CanAdd { get; set; }
       
    }
}
