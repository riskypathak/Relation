using Realtion.Common;
using Realtion.Model;
using Relation.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Realtion.ViewModel
{
    public class RelationViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<PersonModel> _personList = new ObservableCollection<PersonModel>();
        private PersonModel _selectedPerson;

        private readonly ObservableCollection<PersonModel> _secondPersonList = new ObservableCollection<PersonModel>();
        private PersonModel _secondSelectedPerson;

        private readonly ObservableCollection<RelationShipModel> _relationList = new ObservableCollection<RelationShipModel>();
        private RelationShipModel _selectedRelation;

        private readonly ICommand _addRelationCommand;
        public RelationViewModel()
        {

            PopulatePerson();
            PopulateRelations();
            _addRelationCommand = new RelayCommand(AddRelation, CanAdd);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public string _message;
        public string Message
        {
            get { return this._message; }
            set
            {
                this._message = value;
                OnPropertyChanged("Message");
            }
        }


        #region Relationship Code
        public ObservableCollection<RelationShipModel> RelationList
        {
            get
            {
                return this._relationList;
            }

        }
        public RelationShipModel SelectedRelation
        {
            get
            {
                return this._selectedRelation;
            }
            set
            {
                this._selectedRelation = value;

            }
        }
        public void PopulateRelations()
        {
            using (RelationEntities dbEntities = new RelationEntities())
            {

                var raltionList = dbEntities.RelationShipTypes.ToList();
                foreach (var relation in raltionList)
                {
                    RelationShipModel item = new RelationShipModel
                    {
                        RelationName = relation.Name,
                        RelationId = Convert.ToInt32(relation.ID)
                    };
                    _relationList.Add(item);
                }

            }


        }
        #endregion

        #region First and Second Person Code
        public ObservableCollection<PersonModel> PersonList
        {
            get
            {
                return this._personList;
            }

        }
        public PersonModel SelectedPerson
        {
            get
            {
                return this._selectedPerson;
            }
            set
            {
                this._selectedPerson = value;

            }
        }

        public ObservableCollection<PersonModel> SecondPersonList
        {
            get
            {
                return this._secondPersonList;
            }

        }
        public PersonModel SecondSelectedPerson
        {
            get
            {
                return this._secondSelectedPerson;
            }
            set
            {
                this._secondSelectedPerson = value;

            }
        }
        public void PopulatePerson()
        {
            using (RelationEntities dbEntities = new RelationEntities())
            {

                var raltionList = dbEntities.People.ToList();
                foreach (var relation in raltionList)
                {
                    PersonModel item = new PersonModel
                    {
                        PersonName = relation.PersonID + "_" + relation.Name,
                        PersonId = Convert.ToInt32(relation.PersonID)
                    };
                    _personList.Add(item);
                    _secondPersonList.Add(item);
                }

            }


        }
        #endregion

        public ICommand AddRelationButtonCommand
        {
            get { return this._addRelationCommand; }
        }

        private void AddRelation(object obj)
        {
            using (RelationEntities dbEntities = new RelationEntities())
            {
                try
                {
                    /***ADD RELATION ***/
                    var relation = new Relationship();
                    relation.RelationTypeID = _selectedRelation.RelationId;
                    relation.FirstPersonID = _selectedPerson.PersonId;
                    relation.SecondPersonID = _secondSelectedPerson.PersonId;
                    dbEntities.Relationships.Add(relation);
                    dbEntities.SaveChanges();
                    /****END**/
                    this.Message = "Relation Added Successfully.";
                }
                catch (Exception ex)
                {

                    this.Message = ex.Message;
                }

            }

        }

        public bool CanAdd(object obj)
        {
            return true;
        }
    }
}
