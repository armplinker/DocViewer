#define DEBUG
#define DUMPGLOBALS
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Configuration;
#if DEBUG
using ObjectDumping;
#endif
using log4net;

namespace DocViewer
{
    public static class Globals
    {
        #region DECLARATIONS
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string EXAMPLESTRINGPROPERTY { get; set; }
        public static string EXAMPLESTRINGVARIABLE;
        #endregion

        #region CONSTRUCTOR
        static Globals()
        {
            EXAMPLESTRINGPROPERTY = @"Example Global String Property";
            EXAMPLESTRINGVARIABLE = @"Example Global String Variable";
        }
        #endregion

        #region EVENTS
        #endregion

        #region METHODS

        public static void Dump()
        {
#if DUMPGLOBALS
            Type type =   typeof(Globals) ; // Globals is static class with static properties

            Logger?.Debug($@"{Environment.NewLine}");
            
            Logger?.Debug($@"{new string('=', 80)}");
            Logger?.Debug($"Fields");
            Logger?.Debug($@"{new string('=', 80)}");
            var globalsDump = ObjectDumper.Dump(type.GetFields());//System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic));
            Logger?.Debug(globalsDump);
            Logger?.Debug($@"{new string('=', 80)}");
            Logger?.Debug($@"{Environment.NewLine}");

            
            Logger?.Debug($@"{new string('=', 80)}");
            Logger?.Debug($"Properties");
            Logger?.Debug($@"{new string('=', 80)}");
            globalsDump = ObjectDumper.Dump(type.GetProperties());//System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic));
            Logger?.Debug(globalsDump);
            foreach (var p in type.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic))
            {
                var v = p.GetValue(null); // static classes cannot be instanced, so use null...
                //do something with v
                var msg = $"Prop: {v.ToString()}";
                Console.WriteLine(msg);
                Logger?.Debug(msg);
            }
            Logger?.Debug($@"{new string('=', 80)}");
            Logger?.Debug($@"{Environment.NewLine}");
 
            Logger?.Debug($@"{new string('=', 80)}");
            Logger?.Debug($"Methods");
            Logger?.Debug($@"{new string('=', 80)}");

            globalsDump = ObjectDumper.Dump(typeof(Globals).GetMethods());//System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic));
            Logger?.Debug(globalsDump);
            foreach (var p in type.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic))
            {
                var v = p.GetBaseDefinition(); // static classes cannot be instanced, so use null...
                //do something with v
                var msg = $"Method: {v.ToString()}";
                Console.WriteLine(msg);
                Logger?.Debug(msg);
            }
            Logger?.Debug($@"{new string('=', 80)}");
            Logger?.Debug($@"{Environment.NewLine}");

           

#endif
        }


        #endregion
    }
}