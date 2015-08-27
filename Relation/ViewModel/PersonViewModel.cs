using Realtion.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Relation.Data;

namespace Realtion.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private readonly Person perObject;
        private readonly ICommand _addCommand;
        public string PersonName
        {

            get { return perObject.Name; }
            set
            {
                perObject.Name = value;
                OnPropertyChanged("PersonName");
            }

        }
        public string PersonGender
        {
            get { return perObject.Gender; }
            set
            {
                perObject.Gender = value;
                OnPropertyChanged("PersonGender");
            }
        }
        public PersonViewModel()
        {
            perObject = new Person();
            _addCommand = new RelayCommand(AddRecord, CanAdd);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public ICommand AddButtonCommand
        {
            get { return this._addCommand; }
        }


        public bool CanAdd(object obj)
        {
            return true;
        }
        public void AddRecord(object obj)
        {

            using (RelationEntities dbEntities = new RelationEntities())
            {
                
                dbEntities.People.Add(perObject);
                dbEntities.SaveChanges();
            }

        }
    }
}
