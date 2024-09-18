using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Student : Person
    {
        #region Field and property

        private Class _classStudent;

        public Class ClassStudent
        {
            set
            {
                _classStudent = value;
            }

            get
            {
                return _classStudent;
            }
        }

        #endregion

        #region Constructors

        //Constructeur par défaut :

        public Student() : base()
        {
            ClassStudent = new Class("2E",25, true);
        }

        //Constructeur d'initialisation :

        public Student(string fN, string lN,string imgPath,Class classS) : base(fN, lN,imgPath)
        {
            ClassStudent = classS;
        }

        #endregion

        #region ToStringMethod

        public override string ToString()
        {
            return base.ToString() + ClassStudent;
        }

        #endregion
    }
}
