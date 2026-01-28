using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;

        /*
        if (go.layer == LayerMask.NameToLayer("InteractionTrigger"))
        {
            int id = go.GetComponent
            MonitorManager.ChangeFrescoTo(ID);
        }*/
    }

    
}
