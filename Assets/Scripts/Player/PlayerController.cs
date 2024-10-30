using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed; // 움직이는 속도
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public float useStamina;
    public float boostDuration = 5f;
    public float speedBoostMultiplier = 2f;
    private float originalSpeed;


    [Header("Jump")]
    public float jumpForce; // 점프 힘
    private int maxJumpCount = 1; // 기본적으로 한 번만 점프 가능
    private int currentJumpCount = 0; // 현재 점프 횟수
    private bool canDoubleJump = false; // 이중 점프 가능 여부

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        originalSpeed = moveSpeed;
  
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // 점프 처리
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && (IsGrounded() || (canDoubleJump && currentJumpCount < maxJumpCount)))
        {
            if (CharacterManager.Instance.player.condition.UseStamina(useStamina))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // 기존 Y 속도 초기화
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 점프 힘 가하기
                currentJumpCount++; // 점프 횟수 증가
            }
        }
    }

    // Ray를 사용하여 지면에 닿아 있는지 확인하는 메서드
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        foreach (var ray in rays)
        {
            if (Physics.Raycast(ray, 0.1f, groundLayerMask))
            {
                currentJumpCount = 0; // 지면에 닿으면 점프 횟수 초기화
                return true;
            }
        }
        return false;
    }

    // 이중 점프 활성화 메서드 (일정 시간 동안 이중 점프 가능)
    public void EnableDoubleJump()
    {
        
        canDoubleJump = true;
        maxJumpCount = 2; // 이중 점프 가능 설정
        Invoke(nameof(DisableDoubleJump), boostDuration); // duration 후 이중 점프 비활성화
    }

    // 이중 점프 비활성화 메서드
    private void DisableDoubleJump()
    {
        canDoubleJump = false;
        maxJumpCount = 1; // 기본 점프만 가능하도록 복귀
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        moveSpeed *= speedBoostMultiplier;
        Debug.Log("Speed boosted to: " + moveSpeed);
        yield return new WaitForSeconds(boostDuration);
        moveSpeed = originalSpeed;
        Debug.Log("Speed reset to: " + originalSpeed);
    }

    public void SpeedUp()
    {
        StartCoroutine(SpeedBoostCoroutine());
    }
}
