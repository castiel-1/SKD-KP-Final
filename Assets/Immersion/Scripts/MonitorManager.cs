using System;
using UnityEngine;

public class MonitorManager : MonoBehaviour
{
    public static event Action<int> OnFrescoChanged;

    // if this is given an inavlid id the screen goes black
    public void ChangeFrescoTo(int ID)
    {
        OnFrescoChanged?.Invoke(ID);
    }
}
