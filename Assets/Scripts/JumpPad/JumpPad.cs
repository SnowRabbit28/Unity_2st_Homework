using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 800f; // 위로 가해질 힘의 크기 설정

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어가 점프대에 닿았는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 Rigidbody를 가져와 순간적인 힘을 가함
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                // 기존 속도 초기화 후 위쪽으로 힘을 가함
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
