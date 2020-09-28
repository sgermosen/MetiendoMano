using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace FacebookClientSample
{
    public class FaceBookData 
    {
		public  string            FullName { get; set; }
		public  UriImageSource    Cover    { get; set; }
		public  UriImageSource    Picture  { get; set; }
        //public  string MessagePosted       { get; set; }
        //public  string Story               { get; set; }
    }
}
