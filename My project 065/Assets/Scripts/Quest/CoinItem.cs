using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CoinItem : InteractableObject
{
    [Header("동전설정")]
    public int coinValue = 10;
    public string questTag = "coin";
    
    protected override void Start()
    {
        base.Start();
        objectName = "동전";
        interactionText = "[E] 동전획득";
        interactionType = InteractionType.Item;
    }


    protected override void CollectItem()
    {
        if(QuestManager.Instance != null)
        {
            QuestManager.Instance.AddCollectProgress(questTag);
        }
        Destroy(gameObject);
    }
}
