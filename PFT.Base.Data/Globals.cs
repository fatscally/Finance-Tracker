using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFT.Base;
using PFT.Data;
//using PFT.Pages;

namespace PFT
{
    public sealed class Globals
    {
        private Globals()
        {}

        public static Globals Instance { get { return lazy.Value; } }

        private static readonly Lazy<Globals> lazy = new Lazy<Globals>(() => new Globals());

        //private Settings _settings;

        //public Settings Settings
        //{
        //    get {
        //        if (_settings == null)
        //            _settings = new Settings();
                
        //        return _settings;
        //    }
        //    set { _settings = value; }
        //}

        private Connection _sqlCeConnection;

        public Connection SqlCeConnection
        {
            get {
                if (_sqlCeConnection == null)
                    _sqlCeConnection = new Connection();

                return _sqlCeConnection; }
            set { _sqlCeConnection = value; }
        }

                

    }
}
