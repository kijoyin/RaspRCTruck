using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Hardware;
using System;
using System.Text;

namespace RaspRCTruckOrginal
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ISensorEventListener
    {
        static readonly object _syncLock = new object();
        SensorManager _sensorManager;
        TextView _sensorTextView;
        TextView textView;
        StringBuilder builder = new StringBuilder();

        float[] history = new float[2];
        String[] direction = { "NONE", "NONE" };

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            // We don't want to do anything here.
        }

        public void OnSensorChanged(SensorEvent e)
        {
            lock (_syncLock)
            {
                float xChange = history[0] - e.Values[0];
                float yChange = history[1] - e.Values[1];
                history[0] = e.Values[0];
                history[1] = e.Values[1];
                if(xChange == 0 && yChange == 0)
                {
                    return;
                }
                //direction[0] = "Drive straight";
                if (xChange > 2)
                {
                    if (direction[0] == "Turn right")
                    {
                        direction[0] = "Drive straight";
                    }
                    else
                    {
                        direction[0] = "Turn left";
                    }
                }
                else if (xChange < -2)
                {

                    if (direction[0] == "Turn left")
                    {
                        direction[0] = "Drive straight";
                    }
                    else
                    {
                        direction[0] = "Turn right";
                    }
                }


                builder.Clear();

                builder.Append(direction[0]);

                _sensorTextView.Text = builder.ToString();
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            _sensorManager = (SensorManager)GetSystemService(SensorService);
            _sensorTextView = FindViewById<TextView>(Resource.Id.accelerometer_text);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _sensorManager.RegisterListener(this,
                                            _sensorManager.GetDefaultSensor(SensorType.Orientation),
                                            SensorDelay.Ui);
        }

        protected override void OnPause()
        {
            base.OnPause();
            _sensorManager.UnregisterListener(this);
        }
    }
}

