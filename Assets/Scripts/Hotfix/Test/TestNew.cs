using UnityEngine;

public class TestNew : ISingleton, ISingletonUpdate
{
    public void Register()
    {
    
    }

    public void UnRegister()
    {
      
    }

    public void Update()
    {
        CommonLog.Error("Update~ TestNew");
    }
}
