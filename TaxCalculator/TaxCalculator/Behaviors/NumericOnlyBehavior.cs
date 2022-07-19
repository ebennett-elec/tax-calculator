using System;
using Xamarin.Forms;

namespace TaxCalculator.Behaviors
{
    public class NumericOnlyBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.NewTextValue))
            {
                //Also could have used a REGEX, but this is much simpler
                if (!Double.TryParse(args.NewTextValue, out double number))
                {
                    ((Entry)sender).Text = args.OldTextValue ?? "";
                }
            }
        }
    }
}

