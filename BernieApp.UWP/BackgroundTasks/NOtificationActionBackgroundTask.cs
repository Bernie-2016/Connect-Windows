using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace BernieApp.UWP.BackgroundTasks
{
    public sealed class NotificationActionBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var details = taskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;

            if (details != null)
            {
                string arguments = details.Argument;
                
                //Navigate to ActionsPage here
            }
        }
    }
}
