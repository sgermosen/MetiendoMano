namespace AppDieta.Models
{
    public class ConsejoDetalle : BaseDataObject
    {
        string text = string.Empty;
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        string concejo = string.Empty;
        public string Concejo
        {
            get { return concejo; }
            set { SetProperty(ref concejo, value); }
        }

        string description = string.Empty;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }
    }
}
