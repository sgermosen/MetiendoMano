namespace LineDietXF.Types
{
    public class ResultWithErrorText
    {
        public bool Success { get; private set; }
        public string ErrorText { get; private set; }

        public ResultWithErrorText(bool success, string errorText)
        {
            Success = success;
            ErrorText = errorText;
        }
    }
}