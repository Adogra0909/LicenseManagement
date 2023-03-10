/// <summary>
/// Summary description for RemoveSpecialChar
/// </summary>
public class RemoveSpecialChar
{
    public static string RemoveSpecialChars(string str)
    {
        // Create  a string array and add the special characters you want to remove
        string[] chars = new string[] { " ", "+", "-", ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]" };
        //Iterate the number of times based on the String array length.
        for (int i = 0; i < chars.Length; i++)
        {
            if (str.Contains(chars[i]))
            {
                str = str.Replace(chars[i], "");
            }
        }
        return str;
    }
}