using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Person
    {
        #region Fields and properties

        protected int _id;

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        protected string _lastname;

        public string LastName
        {
            set
            {
                _lastname = value;
            }

            get
            {
                return _lastname;
            }
        }

        protected string _firstname;

        public string FirstName
        {
            set
            {
                _firstname = value;
            }

            get
            {
                return _firstname;
            }
        }

        protected string _picture;

        public string Picture
        {
            set
            {
                _picture = value;
            }

            get
            {
                return _picture;
            }
        }

        #endregion

        #region Constructors

        //Constructeur d'initialisation :

        public Person(string lastName, string firstName,string imagePath)
        {
            LastName = lastName;
            FirstName = firstName;
            Picture = imagePath; 
        }

        //Constructeur par défaut :

        public Person() : this("unknown", "unknown", "C:\\Users\\Utilisateur\\source\\repos\\Projet_final_CSharp_CARON\\Images\\defaultPersonImage.jpg")
        {

        }

        #endregion

        #region ToStringMethod

        public override string ToString()
        {
            return  Id + LastName + FirstName + Picture;
        }

        #endregion
    }
}
