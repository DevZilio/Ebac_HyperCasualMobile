using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

//Significa que esse script precisa estar atachado em um objeto com Mesh Render necessariamente
[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{

    public float duration = 1f;
    public MeshRenderer meshRenderer;
    public Color startColor = Color.white;

    //Salva a cor que quer usar
    private Color _finalColor;

    private void OnValidate()
    {

        meshRenderer = GetComponent<MeshRenderer>();


    }

    private void Start()
    {
        _finalColor = meshRenderer.materials[0].GetColor("_Color");
        LerpColor();
    }


    private void LerpColor()
    {
        meshRenderer.materials[0].SetColor("_Color", startColor);
        meshRenderer.materials[0].DOColor(_finalColor, duration).SetDelay(.5f);

    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.C))
        {
            LerpColor();
        }
    }
}
