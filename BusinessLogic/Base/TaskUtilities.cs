﻿using System;
using System.Threading.Tasks;

namespace BusinessLogic.Base
{
    public static class TaskUtilities
    {
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}
