

namespace Realtion.ViewModel
{
    using Realtion.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Relation.Data;
    using Realtion.Model;
    using System.Collections.ObjectModel;
    public class PersonViewModel : INotifyPropertyChanged
    {
        private readonly Person perObject;
        private readonly ICommand _addCommand;
        private readonly ObservableCollection<RelationShipModel> _relationList = new ObservableCollection<RelationShipModel>();
        private RelationShipModel _selectedRelation;
        private readonly ObservableCollection<PersonModel> _personList = new ObservableCollection<PersonModel>();
        private PersonModel _selectedPerson;

        private readonly ObservableCollection<dynamic> _genderList = new ObservableCollection<dynamic>();
        private dynamic _selectedGender;



        public string PersonFirstName
        {

            get { return perObject.Name; }
            set
            {
                perObject.Name = value;
                OnPropertyChanged("PersonFirstName");
            }

        }

        public string PersonMaidenName
        {

            get { return perObject.MaidenName; }
            set
            {
                perObject.MaidenName = value;
                OnPropertyChanged("PersonMaidenName");
            }

        }

        public string PersonLastName
        {

            get { return perObject.LastName; }
            set
            {
                perObject.LastName = value;
                OnPropertyChanged("PersonLastName");
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
        


        public PersonViewModel()
        {
            PopulateGender();
            PopulateRelations();
            PopulatePerson();
            perObject = new Person();
            _addCommand = new RelayCommand(AddRecord, CanAdd);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        #region Person Code
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
        public void PopulatePerson()
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
                }

            }


        }
        #endregion

        #region Gender Code
        public ObservableCollection<dynamic> GenderList
        {
            get
            {
                return this._genderList;
            }

        }
        public dynamic SelectedGender
        {
            get
            {
                return this._selectedGender;
            }
            set
            {
                this._selectedGender = value;

            }
        }
        public void PopulateGender()
        {

            var item = new
            {
                GenderName = "Male",
                GernderId = 1
            };
            _genderList.Add(item);
            var item1 = new
            {
                GenderName = "Female",
                GernderId = 2
            };
            _genderList.Add(item1);

        }
        #endregion

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
            try
            {
                using (RelationEntities dbEntities = new RelationEntities())
                {
                    /*** ADD PERSON **/
                    perObject.Gender = _selectedGender.GenderName;
                    perObject.Name = PersonFirstName;
                    perObject.MaidenName = PersonMaidenName;
                    perObject.LastName = PersonLastName;
                    dbEntities.People.Add(perObject);
                    dbEntities.SaveChanges();
                    /***END***/

                    /***ADD RELATION ***/
                    var relation = new Relationship();
                    relation.RelationTypeID = _selectedRelation.RelationId;
                    relation.FirstPersonID = perObject.PersonID;
                    relation.SecondPersonID = _selectedPerson.PersonId;
                    dbEntities.Relationships.Add(relation);
                    dbEntities.SaveChanges();
                    /****END**/

                    ClearControls();
                    this.Message= "Person Added Successfully.";

                }
            }
            catch (Exception ex)
            {

                this.Message = ex.Message;
            }
            
        }
        private void ClearControls()
        {
            PersonFirstName = string.Empty;
            PersonMaidenName = string.Empty;
            PersonLastName = string.Empty;
        }


    }
}
