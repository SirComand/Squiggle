﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Squiggle.Chat
{
    public static class ExceptionMonster
    {
        public static bool EatTheException(Action action, string actionDescription)
        {
            Exception ex;
            return EatTheException(action, actionDescription, out ex);
        }

        public static bool EatTheException(Action action, string actionDescription, out Exception ex)
        {
            bool success;
            EatTheException(() =>
            {
                action();
                return (object)null;
            }, actionDescription, out success, out ex);
            return success;
        }

        public static T EatTheException<T>(Func<T> action, string actionDescription)
        {
            Exception ex;
            bool success;
            return EatTheException(action, actionDescription, out success, out ex);
        }

        // Oh well I know this is ugly but bridge has got to stay alive right?
        public static T EatTheException<T>(Func<T> action, string actionDescription, out bool success, out Exception ex)
        {
            ex = null;
            try
            {                
                T result = action();
                success = true;
                return result;
            }
            catch (Exception exception)
            {
                ex = exception;
                Trace.WriteLine("Erorr occured while " + actionDescription + ": " + ex.Message);
            }
            success = false;
            return default(T);
        }
    }
}