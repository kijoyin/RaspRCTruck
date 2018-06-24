using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Hardware;
using System;
using System.Text;
using Android.Content.PM;
using Android.Views;

namespace RaspRCTruckOrginal
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : AppCompatActivity
    {
        Button btnStop;
        Button btnStart;
        public MainActivity()
        {
            //RequestWindowFeature(WindowFeatures.NoTitle);
            //Remove notification bar
            //Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            btnStart = FindViewById<Button>(Resource.Id.btnStart);
            btnStart.Click += btnStart_OnClick;
            btnStop = FindViewById<Button>(Resource.Id.btnStop);
            btnStop.Visibility = ViewStates.Invisible;
            btnStop.Click += btnStop_OnClick;

        }


        public void btnStart_OnClick(object sender, EventArgs args)
        {
            btnStop.Visibility = ViewStates.Visible;
            btnStart.Visibility = ViewStates.Invisible;
            Toast.MakeText(this, "Hello from Start", ToastLength.Long).Show();
        }
        public void btnStop_OnClick(object sender, EventArgs args)
        {
            btnStop.Visibility = ViewStates.Invisible;
            btnStart.Visibility = ViewStates.Visible;
            Toast.MakeText(this, "Hello from Stop", ToastLength.Long).Show();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }
    }
}

