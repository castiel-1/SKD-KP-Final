using UnityEngine;

public class InteractionDetector : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        GameObject go = collider.gameObject;

        switch (go.name)
        {
            case "fresco0":
                MonitorManager.ChangeFrescoTo(0);
                break;
            case "fresco1":
                MonitorManager.ChangeFrescoTo(1);
                break;
            case "fresco2":
                MonitorManager.ChangeFrescoTo(2);
                break;
            default:
                MonitorManager.ChangeFrescoTo(-1);
                break;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        MonitorManager.ChangeFrescoTo(-1);
    }

    
}
