using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Targil2.Models

{
   public  class RemoteControl
    {
        
       public  enum Status
        {
            On = 0,
            Off = 1
        };
        #region attributes
        private int _temperture;
        private SpData sp;
        #endregion

        #region properties

        public static int MAX_TEMPERTURE { get; set; }
        public static int MIN_TEMPERTURE { get; set; }
        public int Temperture
        {
            get { return _temperture; }
            set
            {
                if (value > MAX_TEMPERTURE || value < MIN_TEMPERTURE)
                    throw new ArgumentOutOfRangeException($"טמפרטורה חייבת להיות בין {MAX_TEMPERTURE} - {MIN_TEMPERTURE}");
                else
                    _temperture = value;
            }

        }

        public Status status { get; set; }
        #endregion

        #region C'tor
        public RemoteControl()
        {
            MIN_TEMPERTURE = 16;
            MAX_TEMPERTURE = 32;
            Temperture = MIN_TEMPERTURE;
            status = Status.Off;
        }
        public RemoteControl(Context context)
        {
            
           sp = new SpData(context, nameof(RemoteControl)+".sp");
            MIN_TEMPERTURE = sp.GetIntValue(nameof(MIN_TEMPERTURE), 16);
            MAX_TEMPERTURE= sp.GetIntValue(nameof(MAX_TEMPERTURE), 32);
            status = (Status)sp.GetIntValue(nameof(status), (int)Status.Off);
            Temperture= sp.GetIntValue(nameof(Temperture), MIN_TEMPERTURE);
        }
        #endregion

        #region Save Functions
        public bool SaveSettings()
        {
            return ( sp.PutIntValue(nameof(MIN_TEMPERTURE), MIN_TEMPERTURE)&&
                     sp.PutIntValue(nameof(MAX_TEMPERTURE), MAX_TEMPERTURE));
            
        }

        public bool SaveState()
        {
           return  sp.PutIntValue(nameof(status), (int)this.status);
            
        }

        public bool SaveCurrentTemp()
        {
          return  sp.PutIntValue(nameof(Temperture), this.Temperture);
            
        }
        #endregion
    }
}