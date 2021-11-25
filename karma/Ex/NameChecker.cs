namespace karma.Ex
{
    public class NameChecker
    {
        public static bool isStringCorrect(string str)
        {
            str.ToLower();
            if (str.Length > 100)
            {
                throw new TitleTooLongException();
            }
            if (str.Contains("ipsum") || str.Contains("nigga") || str.Contains("nigger") || str.Contains("shit") || str.Contains("fuck"))
            {
                throw new CurseWordException();
            }
            return true;
        }
    }
}
