using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpensesPredictor.Mobile.Behaviors.Abstract;
using ExpensesPredictor.Mobile.Controls;
using Xamarin.Forms;

namespace ExpensesPredictor.Mobile.Behaviors
{
    public class RequiredOptionBehavior:Behavior<BindablePicker>
    {
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(RequiredOptionBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        private BindablePicker _picker;
        protected override void OnAttachedTo(BindablePicker bindable)
        {
            bindable.SelectedIndexChanged += Bindable_SelectedIndexChanged;
            _picker = bindable;
        }

        private void Bindable_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsValid = _picker.SelectedIndex >= 0;
        }

        protected override void OnDetachingFrom(BindablePicker bindable)
        {
            bindable.SelectedIndexChanged -= Bindable_SelectedIndexChanged;
            _picker = null;
        }
    }
}
