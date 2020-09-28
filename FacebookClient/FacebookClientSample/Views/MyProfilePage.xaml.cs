﻿using System;
using System.Collections.Generic;
using FacebookClientSample.Models;
using Xamarin.Forms;

namespace FacebookClientSample
{
    public partial class MyProfilePage : ContentPage
    {
        public MyProfilePage(FaceBookData facebookprofile , List<PostData> post)
        {
            BindingContext = new
            {
                Profile = facebookprofile,
                Posts   = post
            };

            InitializeComponent();

        }
 
    }
}
