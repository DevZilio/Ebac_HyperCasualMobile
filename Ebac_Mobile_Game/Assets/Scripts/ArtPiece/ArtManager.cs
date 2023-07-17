using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZilio.Core.Singleton;

public class ArtManager : Singleton<ArtManager>
{
    public enum ArtType
    {
        TYPE_01,
        TYPE_02,
        TYPE_03,
    }

    public List<ArtSetup> artSetups;

    public ArtSetup GetSetupByType(ArtType artType)
    {
        ArtSetup setup = artSetups.Find(i => i.artType == artType);

    if (setup == null)
    {
        Debug.LogWarning("ArtSetup not found for art type: " + artType);
    }

    return setup;
    }
}

[System.Serializable]
public class ArtSetup{
    public ArtManager.ArtType artType;
    public GameObject gameObject;
}
