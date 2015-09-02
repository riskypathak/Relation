﻿using Realtion.Model;
using Relation.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realtion.ViewModel
{
    public class RelationNetworkViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<PersonModel> _personList = new ObservableCollection<PersonModel>();
        public RelationNetworkViewModel()
        {
            GetPersonList();
        }
        public ObservableCollection<PersonModel> PersonList
        {
            get
            {
                return this._personList;
            }

        }
        private PersonModel _selectedPerson;
        public PersonModel SelectedPerson
        {
            get
            {

                return this._selectedPerson;
            }
            set
            {
                this._selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }
        public void GetPersonList()
        {
            using (RelationEntities dbEntities = new RelationEntities())
            {

                var raltionList = dbEntities.People.ToList();
                foreach (var relation in raltionList)
                {
                    PersonModel item = new PersonModel
                    {
                        PersonName = relation.Name,
                        PersonId = Convert.ToInt32(relation.PersonID)
                    };
                    _personList.Add(item);
                    _personList.Add(item);
                }

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PopulateNetworkView()
        {
            using (RelationEntities dbEntities = new RelationEntities())
            {
                var relationList = dbEntities.Relationships.Where(x=>x.FirstPersonID== _selectedPerson.PersonId || x.SecondPersonID==_selectedPerson.PersonId).ToList();
                foreach (var relation in relationList)
                {
                    //RelationShipModel item = new RelationShipModel
                    //{
                    //    PersonName = relation.Name,
                    //    PersonId = Convert.ToInt32(relation.PersonID)
                    //};
                    //_personList.Add(item);
                }

            }
           
        }
    }
}
