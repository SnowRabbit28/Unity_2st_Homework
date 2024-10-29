using System;

using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition shield { get { return uiCondition.shield; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay; // 줄어들게 하는 변수를 입력받아요
    public event Action onTakeDamage;

    private void Update()
    {
        shield.Subtract(shield.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (shield.curValue <= 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue <= 0.0f)
        {
            Die();
        }
    }
    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Shield(float amount)
    {
        shield.Add(amount);
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }
}
