using System;
using Xamarin.Forms;

namespace hBuddyApp.Features.Polls.Components
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

                var pollsViewModel = (this.Parent.Parent.BindingContext as PollsViewModel);

                Button btnActionSendCommand = (Button)sender;
                if (btnActionSendCommand != null)
                {
                    pollsViewModel.RapidProMessageId = btnActionSendCommand.ClassId.Trim().ToString();
                    pollsViewModel.ActionInputText = btnActionSendCommand.Text.Trim().ToString();
                }
                pollsViewModel.OnActionSendCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OutgoingButtonViewCell - OnActionSendCommand: Exception - {ex.Message.ToString()}");
            }
        }
    }
}
