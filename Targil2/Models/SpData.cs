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
    class SpData
    {
        private readonly ISharedPreferences sp;
        private readonly ISharedPreferencesEditor editor;

        public SpData(Context context, string spFileName)
        {
            sp = context.GetSharedPreferences(spFileName, FileCreationMode.Private);
            editor = sp.Edit();
                       
        }
        
        
        public bool PutIntValue(string key, int value)
        {
            editor.PutInt(key, value);
            return editor.Commit();
        }

        public int GetIntValue(string key, int defValue)
        {
            return sp.GetInt(key, defValue);
        }

        public bool PutBoolValue(string key, bool value)
        {
            editor.PutBoolean(key, value);
            
            return editor.Commit();
        }

        public bool GetBoolValue(string key, bool defValue)
        {
            return sp.GetBoolean(key, defValue);
        }

    }
}