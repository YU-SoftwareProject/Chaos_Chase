using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;

    public GameObject[] uiPanels;

    private Quaternion originalCameraRotation;
    private Quaternion originalCharacterRotation;

    void Reset()
    {
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // 시작 시 카메라 및 캐릭터 초기 회전값 저장
        originalCameraRotation = transform.localRotation;
        originalCharacterRotation = character.localRotation;
    }

    // UI 비활성화 시 마우스 조작에 따라 카메라 시야 변경, UI 활성화 시 회전값 고정되어 카메라 시야 고정
    void Update()
    {
        bool isUIActive = CheckUIActivation();

        if (!isUIActive)
        {
            HandleCameraView();
        }
        else
        {
            LockCameraView();
        }
    }

    private void HandleCameraView()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    // 카메라 및 캐릭터 현재 회전값 고정
    private void LockCameraView()
    {
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    // UI가 활성화되어있는 확인하는 함수로 UI 패널 하나라도 활성화된 경우 true 반환
    private bool CheckUIActivation()
    {
        foreach (GameObject panel in uiPanels)
        {
            if (panel.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
