using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int IgnitionCount;
    public int IgnitionWrong = 0;

    public void Door1()
    {
        IgnitionCount++;
        Debug.Log("Ignition");
    }

    public void Door1W()
    {
        IgnitionWrong++;
    }

    public void Door1R()
    {
        IgnitionWrong--;
    }

    public void OpenDoor()
    {
        IgnitionCount = 0;
    }
}
