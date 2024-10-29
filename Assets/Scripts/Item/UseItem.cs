using UnityEngine;
using System.Collections;

public class UseItem : MonoBehaviour
{
    public ItemData itemData;
    public float speedBoostMultiplier = 2f;
    public float boostDuration = 5f;

    private PlayerController playerController;
    private float originalSpeed;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            originalSpeed = playerController.moveSpeed;
        }
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
            Debug.Log("Starting Coroutine"); // 코루틴 시작 전 확인 로그
            StartCoroutine(SpeedBoostCoroutine());
        }
        else
        {
            Debug.Log("Cannot start coroutine. Either item is not consumable or playerController is null.");
        }
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        playerController.moveSpeed *= speedBoostMultiplier;
        Debug.Log("Speed Boost Applied: " + playerController.moveSpeed); // 디버그 로그로 확인

        yield return new WaitForSeconds(boostDuration);

        playerController.moveSpeed = originalSpeed;
        Debug.Log("Speed Reset: " + playerController.moveSpeed); // 디버그 로그로 확인
    }
}
