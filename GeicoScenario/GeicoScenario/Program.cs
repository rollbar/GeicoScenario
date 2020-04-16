using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Rollbar;

namespace GeicoScenario
{
    public static class Program
    {

        private static void OnRollbarInternalEvent(object sender, RollbarEventArgs e)
        {
            Console.WriteLine(e.TraceAsString());

            RollbarApiErrorEventArgs apiErrorEvent = e as RollbarApiErrorEventArgs;
            if (apiErrorEvent != null)
            {
                //TODO: handle/report Rollbar API communication error event...
                return;
            }
            CommunicationEventArgs commEvent = e as CommunicationEventArgs;
            if (commEvent != null)
            {
                //TODO: handle/report Rollbar API communication event...
                return;
            }
            CommunicationErrorEventArgs commErrorEvent = e as CommunicationErrorEventArgs;
            if (commErrorEvent != null)
            {
                //TODO: handle/report basic communication error while attempting to reach Rollbar API service... 
                return;
            }
            InternalErrorEventArgs internalErrorEvent = e as InternalErrorEventArgs;
            if (internalErrorEvent != null)
            {
                //TODO: handle/report basic internal error while using the Rollbar Notifier... 
                return;
            }
        }

        public static void Main()
        {
            while (true)
            {
                RollbarQueueController.Instance.InternalEvent += OnRollbarInternalEvent;
                try
                {
                    int value = 1 / int.Parse("0");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Error: You can't divide by zero.");
                    try
                    {
                        RollbarLocator.RollbarInstance.Error(ex);
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }

                Thread.Sleep(500);
            }
        }
    }
}
