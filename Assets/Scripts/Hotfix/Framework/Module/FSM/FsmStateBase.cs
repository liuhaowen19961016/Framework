using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FsmStateBase
{
    public virtual void OnInit(FsmBase fsmBase)
    {
    }

    public virtual void OnEnter(params object[] objs)
    {
    }

    public virtual void OnPause()
    {
    }

    public virtual void OnResume()
    {
    }

    public virtual void OnUpdate(float deltaTime)
    {
    }

    public virtual void OnExit()
    {
    }

    public virtual void Dispose()
    {
    }
}