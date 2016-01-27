using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace freelqnce
{
   public class manip : INotifyPropertyChanged
    {
       private int vvertic;
       private int hhoriz;

        public int Vertic {
            get { return vvertic; }
            set
            {
                vvertic = value; OnPropertyChanged("Vartic");
            }

        }

        public int Horiz
        {
            get { return hhoriz; }
            set
            {
                hhoriz = value; OnPropertyChanged("Horiz");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propretyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propretyName));
            }
        }



    }
}
