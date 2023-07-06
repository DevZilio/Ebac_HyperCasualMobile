using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DevZilio.Core.Singleton;
using DG.Tweening;
using UnityEngine;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    [Header("Animation")]
    public float scaleDuration = .2f;
    public float timeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    public List<ItemCollectableCoin> itens;



    private void Start()
    {
        itens = new List<ItemCollectableCoin>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartAnimationsCoins();
        }
    }

    public void RegisterCoin(ItemCollectableCoin i)
    {
        if (!itens.Contains(i))
        {
            itens.Add(i);
            i.transform.localScale = Vector3.zero;
        }

    }

    public void StartAnimationsCoins()
    {
        if (itens != null)
        {
            StartCoroutine(ScalePiecesByTime());
        }
        else
        {
            Debug.LogWarning("CoinsAnimationManager itens list is null. Make sure it's properly initialized.");
        }
    }


    IEnumerator ScalePiecesByTime()
    {
        itens.RemoveAll(item => item == null); // Remove referências a objetos nulos
        foreach (var p in itens)
        {
            p.transform.localScale = Vector3.zero;
        }

        Sort();
        yield return null;

        for (int i = 0; i < itens.Count; i++)
        {
            if (itens[i] != null)
            {
                itens[i].transform.DOScale(1, scaleDuration).SetEase(ease);
                yield return new WaitForSeconds(timeBetweenPieces);
            }
        }
    }



    // Para usar o OrderBy precisa chamar a biblioteca Using.Linq;
    private void Sort()
    {
        itens = itens.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();

    }

    public void ResetCoins()
    {
        if (Instance == null)
        {
            return;
        }

        StopAllCoroutines();

        foreach (var coin in Instance.itens)
        {
            if (coin != null)
            {
                coin.transform.localScale = Vector3.zero;
            }
        }

        Instance.itens.Clear();
    }


}
