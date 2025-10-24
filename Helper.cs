namespace cs_gl;

public class Helper
{
    public static string PrintListFloat(List<float> l, int segment)
    {
        var result = "[";
        for(var i = 0; i < l.Count; i++)
        {
            if (i % segment == 0)
            {
                result += "\n";
            }
            result += l[i] + ",";
            
        }
        result += "]";
        return result;
    }
}