using System;
using Xamarin.Forms;

namespace hBuddyApp.Features.Rumours.Components
{
    public partial class OutgoingButtonViewCell : ViewCell
    {
        public OutgoingButtonViewCell()
        {
            InitializeComponent();
        }

        private void OnActionSendCommand(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("OutgoingButtonViewCell - OnActionSendCommand");

                var rumoursViewModel = (this.Parent.Parent.BindingContext as RumoursViewModel);

                Button btnActionSendCommand = (Button)sender;
                if (btnActionSendCommand != null)
                {
                    rumoursViewModel.RapidProMessageId = btnActionSendCommand.ClassId.Trim().ToString();
                    rumoursViewModel.ActionInputText = btnActionSendCommand.Text.Trim().ToString();
                }
                rumoursViewModel.OnActionSendCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OutgoingButtonViewCell - OnActionSendCommand: Exception - {ex.Message.ToString()}");
            }
        }
    }
}
