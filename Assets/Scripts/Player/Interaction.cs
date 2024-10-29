
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance; //얼마나 멀리있는거에 채크할지
    public LayerMask layerMask; // 어떤 레이어에 담긴걸 추출할래

    public GameObject curInteractGameObject; // 상호작용되었다면 정보 가지고 있기
    private IInteractable curInteractable; // 인터페이스 캐싱

    public TextMeshProUGUI promptText; // 띄워야지!
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
        if (camera == null)
        {
            camera = FindObjectOfType<Camera>();
            Debug.Log(camera == null ? "Camera is still null" : "Camera found using FindObjectOfType");
        }
    }
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}