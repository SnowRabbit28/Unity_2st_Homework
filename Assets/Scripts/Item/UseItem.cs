using UnityEngine;
using System.Collections;

public class UseItem : MonoBehaviour
{
    public ItemData itemData;
  

    private PlayerController playerController;


    void Start()
    {
        playerController = CharacterManager.Instance.player.controller;
        
    }

    // 아이템이 Consumable 타입인지 확인하는 메서드
    public bool CanUseItem()
    {
        return itemData != null && itemData.type == ItemType.Consumable;
    }

    public void ApplyItemEffect()
    {
        if (CanUseItem() && playerController != null)
        {
            Debug.Log("Applying item effect. itemData: " + itemData + ", effectType: " + itemData.effectType); // itemData와 effectType 값 확인

            switch (itemData.effectType)
            {
                case EffectType.SpeedBoost:
                    Debug.Log("Applying SpeedBoost effect.");
                    playerController.SpeedUp();
                    break;

                case EffectType.DoubleJump:
                    Debug.Log("Applying DoubleJump effect.");
                    playerController.EnableDoubleJump();
                    break;

                case EffectType.None:
                    Debug.Log("No effect to apply.");
                    break;

                case EffectType.HealShield:
                    Debug.Log("Applying HealOrShield effect.");
                    HealOrShield(); // 체력 및 쉴드 회복 적용
                    break;
            }
        }
        else
        {
            Debug.Log("Cannot apply effect. Either item is not consumable or playerController is null.");
        }
    }
   

    private void HealOrShield()
    {
        var character = CharacterManager.Instance.player.condition;

        // `consumables` 배열을 순회하며 각 항목에 맞는 회복 적용
        foreach (var consumable in itemData.consumables)
        {
            if (consumable.type == ConsumableType.Health) // 체력 회복
            {
                character.Heal(consumable.value);
                Debug.Log("Health recovered by " + consumable.value);
            }
            else if (consumable.type == ConsumableType.Shield) // 쉴드 회복
            {
                character.Shield(consumable.value);
                Debug.Log("Shield recovered by " + consumable.value);
            }
        }
    }
}
