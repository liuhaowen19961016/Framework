using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFsmState
{
    void OnInit(FsmBase fsmBase);

    void OnEnter(params object[] objs);

    void OnUpdate(float deltaTime);

    void OnExit();
}