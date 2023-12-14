//所有的Component都属于它的子Component
public class Root : Singleton<Root>
{
    public ComponentRoot ComponentRoot;

    public override void Register()
    {
        base.Register();
        ComponentRoot = new ComponentRoot();
    }
}
