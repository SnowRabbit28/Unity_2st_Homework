using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition shield;
    public Condition stamina;

    private void Start()
    { //이 ui는 플레이어의 컨디션에 따라 표시만 해주기 때문에 값을 가져오는 식
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}