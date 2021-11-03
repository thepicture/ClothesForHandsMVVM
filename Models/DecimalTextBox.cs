using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClothesForHandsMVVM.Models
{
    public class DecimalTextBox : TextBox
    {
        private readonly Regex _regexDecimalPattern = new Regex("[0-9,.]{1}");
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!_regexDecimalPattern.IsMatch(e.Text))
            {
                e.Handled = true;
            }
            base.OnPreviewTextInput(e);
        }
    }
}
