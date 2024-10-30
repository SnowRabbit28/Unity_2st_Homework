using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //헤더를 통해 변수 위에 구분점 생성
    [Header("Movement")]
    public float moveSpeed; // 움직이는 속도
    private Vector2 curMovementInput; // 지금 위치
    public float jumptForce; // 점프 힘
    public LayerMask groundLayerMask;

    public float useStamina;

    [Header("Look")]
    public Transform cameraContainer; // 마우스 위치
    public float minXLook; // 최소회전범위
    public float maxXLook; // 최대회전범위
    private float camCurXRot; // 마우스 델타 값 받아서 넣기
    public float lookSensitivity; //민감도
    private Vector2 mouseDelta; // 마우스 델타 값

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    { // 게임 시작할때 마우스 안보이게 하기
        Cursor.lockState = CursorLockMode.Locked;
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
        dir.y = rb.velocity.y; //y값 초기화, velocity 동일한속도로

        rb.velocity = dir;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    { // context는 받아오는 키값을 의미
        if (context.phase == InputActionPhase.Performed)
        {   // 지금위치를 읽어와서 넣어준다.
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
        
    }

    void CameraLook() // 카메라가 어디 쳐다보는지 적는 함수
    { // x는 y에게 넣어줘야하고 y는 x에게 넣어주어야한다.
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        //여기서 음수인 이유는 화면과 로테이션이 반대기 때문에 위를 보고 뛰기위해 음수를 넣는다.
        //카메라는 음수면 위를 보고 양수면 아래를 본다.

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            if (CharacterManager.Instance.player.condition.UseStamina(useStamina))
            {
            
                rb.AddForce(Vector2.up * jumptForce, ForceMode.Impulse);
            }
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4] // 다리4개 생긴느낌
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            { //raycast로 검출 ( t / f 로 출력 )
                return true;
            }
        }

        return false;
    }

}
