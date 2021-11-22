using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Views;
using System;
using Android.Content;
using Targil2.Models;


namespace Targil2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, Android.Views.View.IOnClickListener
    {
        Button upBtn, downBtn, onBtn, offBtn;
        TextView tvTemp;
        ImageButton settings;
        RemoteControl rc;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            InitMembers();
            InitViews();


        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            try
            {
                base.OnActivityResult(requestCode, resultCode, data);
                if (requestCode == (int)GeneralSettings.PAGES.SETTINGS_PAGE)
                {
                    if (resultCode == Result.Ok)
                        UpdateTemp(data);
                }
            }
            catch(Exception e)
            {
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }
        }

            private void InitViews()
        {
            upBtn = FindViewById<Button>(Resource.Id.btnPlus);
            downBtn = FindViewById<Button>(Resource.Id.btnMinus);
            onBtn = FindViewById<Button>(Resource.Id.btnOn);
            offBtn = FindViewById<Button>(Resource.Id.btnOff);
            tvTemp = FindViewById<TextView>(Resource.Id.tvTemp);
            settings = FindViewById<ImageButton>(Resource.Id.imgSettings);
            upBtn.SetOnClickListener(this);
            downBtn.SetOnClickListener(this);
            offBtn.SetOnClickListener(this);
            onBtn.SetOnClickListener(this);
            settings.SetOnClickListener(this);
            
            //TurnOff();




        }

        private void InitMembers()
        {
            try
            {
                rc = new RemoteControl(this);
                

            }
            catch (Exception e)
            {
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }

        }
private void UpdateTemp(Intent data)
        {
            RemoteControl.MAX_TEMPERTURE = data.GetIntExtra(nameof(RemoteControl.MAX_TEMPERTURE),RemoteControl.MAX_TEMPERTURE);
            RemoteControl.MIN_TEMPERTURE = data.GetIntExtra(nameof(RemoteControl.MIN_TEMPERTURE), RemoteControl.MIN_TEMPERTURE);
            rc.SaveSettings();
            if (rc.Temperture < RemoteControl.MIN_TEMPERTURE)
            {
                rc.Temperture = RemoteControl.MIN_TEMPERTURE;
              
            }
            rc.SaveCurrentTemp();

            DisplayTemp();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void DisplayTemp()
        {
            tvTemp.Text = rc.Temperture.ToString();
        }

        public void OnClick(View v)
        {
            try
            {
                switch (v.Id)
                {
                    case Resource.Id.btnPlus:
                        if (rc.status == RemoteControl.Status.On)
                        {
                            rc.Temperture++;
                            rc.SaveCurrentTemp();
                            DisplayTemp();
                        }
                        break;
                    case Resource.Id.btnMinus:
                        if (rc.status == RemoteControl.Status.On)
                        {
                            rc.Temperture--;
                            rc.SaveCurrentTemp();
                            DisplayTemp();
                        }
                        break;
                    case Resource.Id.btnOff:
                        TurnOff();
                        rc.SaveState();
                        break;
                    case Resource.Id.btnOn:
                        TurnOn();
                        DisplayTemp();
                        rc.SaveState();
                        break;
                    case Resource.Id.imgSettings:
                        OpenSettingsPage();
                        break;
                    default:
                        break;
                }




            }
            catch (Exception e)
            {
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }



        }

        private void OpenSettingsPage()
        {
           
            Intent intent = new Intent(this, typeof(Activity_settings));
            intent.PutExtra(nameof(RemoteControl.MIN_TEMPERTURE), RemoteControl.MIN_TEMPERTURE);
            intent.PutExtra(nameof(RemoteControl.MAX_TEMPERTURE), RemoteControl.MAX_TEMPERTURE);
            intent.PutExtra(nameof(rc.Temperture), rc.Temperture);
            try
            {
                StartActivityForResult(intent, (int)GeneralSettings.PAGES.SETTINGS_PAGE);
            }
            catch (Exception e)
            {
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }
        }

        private void TurnOff()
        {
            rc.status = RemoteControl.Status.Off; 
            tvTemp.Text = "";
            downBtn.Enabled = false;
            upBtn.Enabled = false;
            onBtn.Enabled = true;
            offBtn.Enabled = false;
        }

        private void TurnOn()
        {
            rc.status = RemoteControl.Status.On;
            downBtn.Enabled = true;
            upBtn.Enabled = true;
            onBtn.Enabled = false;
            offBtn.Enabled = true;
        }
    }
}