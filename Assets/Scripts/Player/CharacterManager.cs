//using 중에 색이 약간 회색인 얘들 있는데 안쓰는 애들이니 지워주자!
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;
    public static CharacterManager Instance
    {
        get //get을 이용해서 외부에서 대문자로 들어오면 소문자로 반환해주도록 만들것
        {
            if(instance == null)
            {
                instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return instance;
        }

    }

    public Player player;
    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    private void Awake()
    {
        if (instance == null) // awake가 실행되었다는건 옵젝이 있다는 뜻이니
        {
            instance = this; // 이거 나야.. 지금 있는 데이터가 나야!
            DontDestroyOnLoad(gameObject);
        }
        else // 혹시라도 인스턴스가 있는데
        {   // 새로운 친구가 또 들어온다면 지금꺼 삭제하고 새로운 친구 받아내자
            if (instance == this)
            {
                Destroy(gameObject);
            }
        } 
    }


}
