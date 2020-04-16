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
                
                // let's save apiErrorEvent.TraceAsString() result into appErrors.log file or somewhere else.
                
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

                // let's save commErrorEvent.TraceAsString() result into commErrors.log file or somewhere else.
                
                return;
            }
            InternalErrorEventArgs internalErrorEvent = e as InternalErrorEventArgs;
            if (internalErrorEvent != null)
            {
                //TODO: handle/report basic internal error while using the Rollbar Notifier... 

                // let's save internalErrorEvent.TraceAsString() result into internalErrors.log file or somewhere else.
                
                return;
            }
        }

        public static void Main()
        {
            RollbarQueueController.Instance.InternalEvent += OnRollbarInternalEvent;

            while (true)
            {
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
