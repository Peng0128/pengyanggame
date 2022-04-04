using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;

    // Start is called before the first frame update
    public void OpenDoor(GameObject obj)
    {
        if (!isOpen)
        {
            PlayerManager manager = obj.GetComponent<PlayerManager>();
            if (manager)
            {
                if (manager.IgnitionCount == 2 && manager.IgnitionWrong == 0)
                {
                    isOpen = true;
                    manager.OpenDoor();
                    Destroy(this.gameObject);
                    
                }
            }
        }
        
    }
}
