using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    Rigidbody playerRigidbody;
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    public GameObject[] uiPanels;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // UI 비활성화 시 컨트롤 키에 따라 플레이어 이동, UI 활성화 시 이동 처리 중지
    void FixedUpdate()
    {
        bool isUIActive = CheckUIActivation();

        if (!isUIActive)
        {
            HandleControlKeys();
        }
    }

    private void HandleControlKeys() 
    {
        float targetMovingSpeed = speed;

        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        playerRigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, playerRigidbody.velocity.y, targetVelocity.y);
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