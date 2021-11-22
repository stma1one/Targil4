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
using Targil2.Models;

namespace Targil2
{
    [Activity(Label = "Activity_settings")]
    public class Activity_settings : Activity, View.IOnClickListener
    {
        EditText maxTemp;
        EditText minTemp;
        TextView currTemp;
        Button btn_update,btn_Cancel;
       
        RemoteControl rc;
        public void OnClick(View v)
        {
            try
            {
                if (v is Button)
                {
                    if (((Button)v) == btn_update)
                    {
                        UpdateRemote();
                    }
                    else if (((Button)v) == btn_Cancel)
                        Finish();

                }
            }
            catch (Exception e)
            {
               
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }
        }

        private void UpdateRemote()
        {


            if (int.Parse(maxTemp.Text) < int.Parse(minTemp.Text))
                throw new ArgumentOutOfRangeException($"max temperture cannot be smaller than minimum temperture");

            RemoteControl.MAX_TEMPERTURE = int.Parse(maxTemp.Text);
            RemoteControl.MIN_TEMPERTURE = int.Parse(minTemp.Text);
            
            Intent intent = new Intent();
            intent.PutExtra(nameof(RemoteControl.MIN_TEMPERTURE),int.Parse(minTemp.Text));
            intent.PutExtra(nameof(RemoteControl.MAX_TEMPERTURE), int.Parse(maxTemp.Text));
            SetResult(Result.Ok, intent);
            Finish();


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_settings);

            InitViews();
            InitObjects();
            
        }

        private void InitObjects()
        {

            rc = new RemoteControl();

            RemoteControl.MIN_TEMPERTURE = Intent.GetIntExtra(nameof(RemoteControl.MIN_TEMPERTURE), 0);
            RemoteControl.MAX_TEMPERTURE = Intent.GetIntExtra(nameof(RemoteControl.MAX_TEMPERTURE), 0);
            currTemp.Text = Intent.GetIntExtra(nameof(rc.Temperture), 0).ToString() ;
            rc.status = RemoteControl.Status.Off;

            maxTemp.Text = RemoteControl.MAX_TEMPERTURE.ToString();
            minTemp.Text = RemoteControl.MIN_TEMPERTURE.ToString();
            
        }

        private void InitViews()
        {
            maxTemp = FindViewById<EditText>(Resource.Id.ev_MaxTemp);
            minTemp = FindViewById<EditText>(Resource.Id.ev_MinTemp);
            currTemp = FindViewById<TextView>(Resource.Id.tvCurrtempVal);
            btn_update = FindViewById<Button>(Resource.Id.btnUpd);
            btn_Cancel = FindViewById<Button>(Resource.Id.btnCncl);
            btn_update.SetOnClickListener(this);
            btn_Cancel.SetOnClickListener(this);
        }
    }
}