﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteNavegation.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteNavegation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuarioPage : ContentPage
    {
        
        public UsuarioPage()
        {
            InitializeComponent();
            BindingContext = new UsuarioViewModel(this.Navigation);
        }

    }
}