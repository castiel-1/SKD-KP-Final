using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MonitorSetup", menuName = "Scriptable Objects/MonitorSetup")]
public class MonitorSetup : ScriptableObject
{
    [System.Serializable]
    public class MonitorEntry
    {
        public int MonitorID;
        public Sprite frescoImage1;
        public Sprite frescoImage2;
        public Sprite frescoImage3;
    }

    public List<MonitorEntry> entries = new List<MonitorEntry>();

    public MonitorEntry GetEntry(int ID)
    {
        MonitorEntry entry = entries.Find(m => m.MonitorID == ID);

        return entry;
    }
}
