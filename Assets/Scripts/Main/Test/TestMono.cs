using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System.Linq;

public class TestMono : MonoBehaviour
{
    Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();

    public void Start()
    {

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
    }
}

public class TestC
{
    public int index;
    public int v;
}
