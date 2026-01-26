using UnityEngine;

public class Monitor : MonoBehaviour
{
    [SerializeField]
    int monitorID;
    //TEMP
    /*
     For this to work, add a quad as a child to the monitor which needs a meshrenderer and meshfilter. 
     Position it so it is the display area.
     Add it in the editor so it can be found by this script.
     */
    [SerializeField]
    MeshRenderer displayArea;

    private MonitorSetup monitorSetup;
    private Sprite[] images;

    private void OnEnable()
    {
        MonitorManager.OnFrescoChanged += DisplayImage;
    }

    private void OnDisable()
    {
        MonitorManager.OnFrescoChanged -= DisplayImage;
    }

    private void Start()
    {
        monitorSetup = Resources.Load<MonitorSetup>("MonitorSetup");

        var entry = monitorSetup.GetEntry(monitorID);

        if (entry != null)
        {
            images = new Sprite[] { entry.frescoImage1, entry.frescoImage2, entry.frescoImage3 };
        }
     
    }

    private void DisplayImage(int ID)
    {
        if(ID < images.Length && ID >= 0)
        {
            displayArea.gameObject.SetActive(true);
            Texture2D texture = images[ID].texture;
            displayArea.material.mainTexture = texture; 
        }
        else
        {
            displayArea.gameObject.SetActive(false);
        }
    }
}
