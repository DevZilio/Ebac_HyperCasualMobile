using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DevZilio.Core.Singleton;

public class ColorManager : Singleton<ColorManager>
{
    public List<Material> materials;
    public List<ColorSetup> colorSetups;

 public enum ArtType
    {
        TYPE_01,
        TYPE_02,
        TYPE_03,
    }

    public void ChangeColorByType(ArtManager.ArtType artType)
    {
        var setup = colorSetups.Find(i => i.artType == artType);

        if (setup != null)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetColor("_Color", setup.colors[i]);
            }
        }
        else
        {
            Debug.LogWarning("Color setup not found for art type: " + artType);
        }


    }
}


[System.Serializable]
public class ColorSetup
{
    public ArtManager.ArtType artType;
    public List<Color> colors;
}
