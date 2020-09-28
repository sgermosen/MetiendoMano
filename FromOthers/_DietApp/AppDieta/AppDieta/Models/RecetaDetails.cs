namespace AppDieta.Models
{
    public class RecetaDetails : BaseDataObject
    {
        string text = string.Empty;
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }


        string receta = string.Empty;
        public string Receta
        {
            get { return receta; }
            set { SetProperty(ref receta, value); }
        }

        string description = string.Empty;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }
    }
}
