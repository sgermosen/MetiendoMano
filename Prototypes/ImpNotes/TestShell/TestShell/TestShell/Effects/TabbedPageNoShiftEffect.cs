using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestShell.Effects
{
    public class TabbedPageNoShiftEffect : RoutingEffect
    {
        public TabbedPageNoShiftEffect() : base($"TestShell.{nameof(TabbedPageNoShiftEffect)}")
        {
        }
    }
}
