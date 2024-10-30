using UnityEngine;

public enum ItemType
{
    Resource, // 책, 코인, 등등 다
    Equipable, // 활 화살
    Consumable // 주머니
}

public enum ConsumableType
{
    Health,
    Shield
}
public enum EffectType
{
    None,
    HealShield,
    SpeedBoost,
    DoubleJump
}


[System.Serializable]
public class ItemDataConsumable //얼마만큼 업그레이드 될건지
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Effect")]
    public EffectType effectType; // 아이템 효과 타입 추가


}