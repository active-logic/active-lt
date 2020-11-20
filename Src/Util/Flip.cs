namespace Active.Util{
public class Flip{

    bool value = false;

    public static bool operator true (Flip s) => s.value = !s.value;
    public static bool operator false (Flip s) => s.value = !s.value;

}}
