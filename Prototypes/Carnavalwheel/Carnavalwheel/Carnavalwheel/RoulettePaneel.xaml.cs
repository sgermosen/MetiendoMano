using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Carnavalwheel
{
    public partial class RoulettePaneel : ContentPage
    {
        int speed = 30;
        int curSpeed = 1;
        bool toStop = false;
        public RoulettePaneel(string param)
        {
            InitializeComponent();
            var roulette = this.roulettePan;
            Device.StartTimer(TimeSpan.FromSeconds(0.03), () => {

                if (toStop == true)
                    curSpeed = curSpeed - 1;
                if (toStop == true && curSpeed <= 0)
                    return false;

                if (toStop == false && curSpeed < speed)
                    curSpeed = curSpeed + 1;

                var rouletteRotation = roulette.Rotation;
                rouletteRotation += curSpeed;

                roulette.Rotation = rouletteRotation;

                return true;
            });

            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                toStop = true;
                return false;
            });

            /*			Timer timer = new System.Timers.Timer();
                        timer.Interval = 0.1;
                        timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                        timer.Start();*/
        }
    }
}
