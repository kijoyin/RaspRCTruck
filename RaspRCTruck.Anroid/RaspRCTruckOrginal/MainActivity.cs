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
    public class MainActivity : AppCompatActivity, ISensorEventListener
    {
        static readonly object _syncLock = new object();
        SensorManager _sensorManager;
        TextView _sensorTextView;
        TextView textView;
        StringBuilder builder = new StringBuilder();
        bool IsFirst = true;

        float[] history = new float[2];
        float[] original = new float[2];
        String[] direction = { "NONE", "NONE" };

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            // We don't want to do anything here.
        }

        public void OnSensorChanged(SensorEvent e)
        {
            lock (_syncLock)
            {
                if(IsFirst)
                {
                    original[0] = e.Values[0];
                    original[1] = e.Values[1];
                    IsFirst = false;
                }
                float xChange = history[0] - e.Values[0];
                float yChange = history[1] - e.Values[1];
                float xChangeFromOriginal = original[0] - e.Values[0];
                float yChangeFromOriginal = original[1] - e.Values[1];
                history[0] = e.Values[0];
                history[1] = e.Values[1];
                if(xChange == 0 && yChange == 0)
                {
                    return;
                }
                if (xChangeFromOriginal <= 2 && xChangeFromOriginal >= -2)
                {
                    direction[0] = "Drive straight";
                }
                else if (xChange > 2)
                {
                    direction[0] = "Turn left";
                }
                else if (xChange < -2)
                {
                    direction[0] = "Turn right";
                }


                builder.Clear();

                builder.Append(direction[0]);

                _sensorTextView.Text = builder.ToString();
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            //Remove notification bar
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

        }

        protected override void OnResume()
        {
            base.OnResume();
            //_sensorManager.RegisterListener(this,
            //                                _sensorManager.GetDefaultSensor(SensorType.Orientation),
            //                                SensorDelay.Ui);
        }

        protected override void OnPause()
        {
            base.OnPause();
            //_sensorManager.UnregisterListener(this);
        }
    }
}

