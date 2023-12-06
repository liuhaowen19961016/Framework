
public class Root : Singleton<Root>
{
    public ComponentRoot ComponentRoot;

    public Root()
    {
        ComponentRoot = new ComponentRoot();
    }
}
