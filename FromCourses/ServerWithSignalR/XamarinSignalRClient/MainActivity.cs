using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Microsoft.AspNetCore.SignalR.Client;
using static Android.Views.View;

namespace XamarinSignalRClient
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnTouchListener
    {
        Button btn_start;
        View view_more;
        HubConnection hubConnection;

        public bool OnTouch(View v, MotionEvent e)
        {
            view_more.SetX(e.RawX);
            view_more.SetY(e.RawY);
            if(hubConnection.State== HubConnectionState.Connected)
            {
                hubConnection.SendAsync("MoveViewFromServer", e.RawX, e.RawY);
            }
            return true;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            btn_start = FindViewById<Button>(Resource.Id.btn_start);
            view_more = FindViewById(Resource.Id.view_move);

            hubConnection = new HubConnectionBuilder()
                .WithUrl("htpp://localhost:500/movehub").Build();

            hubConnection.On<float, float>("ReceiveNewPosition", (newX, newY) =>
            {
                view_more.SetX(newX);
                view_more.SetY(newY);
            });

            btn_start.Click += async delegate
            {
                if (btn_start.Text.ToLower().Equals("start"))
                {
                    if (hubConnection.State == HubConnectionState.Disconnected)
                    {
                        try
                        {
                            await hubConnection.StartAsync();
                            btn_start.Text = "stop";
                        }
                        catch (System.Exception)
                        {

                            throw;
                        }
                    }

                }
                else if (btn_start.Text.ToLower().Equals("stop"))
                {
                    if (hubConnection.State == HubConnectionState.Connected)
                    {
                        try
                        {
                            await hubConnection.StopAsync();
                            btn_start.Text = "start";
                        }
                        catch (System.Exception)
                        {

                            throw;
                        }
                    }

                }
            };

            view_more.SetOnTouchListener(this);
        }
    }
}