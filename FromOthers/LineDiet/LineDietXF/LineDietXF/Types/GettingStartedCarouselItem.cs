namespace LineDietXF.Types
{
    public class GettingStartedCarouselItem
    {
        public string TitleText { get; set; }
        public string ImageSource { get; set; }
        public string BodyText { get; set; }

        public GettingStartedCarouselItem(string titleText, string imageSource, string bodyText)
        {
            TitleText = titleText;
            ImageSource = imageSource;
            BodyText = bodyText;
        }
    }
}