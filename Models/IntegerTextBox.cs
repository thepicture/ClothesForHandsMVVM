using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ClothesForHandsMVVM.Models
{
    public class IntegerTextBox : DecimalTextBox
    {
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (new Regex("[.,]{1}").IsMatch(e.Text))
            {
                e.Handled = true;
            }
            base.OnPreviewTextInput(e);
        }
    }
}
