﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Xamarin.Forms;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WhereTo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class ProviderLoginPage : ContentPage
    {
        //we will refer providename from renderer page  
        public string ProviderName
        {
            get;
            set;
        }
        public ProviderLoginPage(string _providername)
        {
            ProviderName = _providername;
        }
    }
}
