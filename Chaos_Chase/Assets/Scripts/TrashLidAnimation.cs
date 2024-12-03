using UnityEngine;

public class TrashCanLidAnimation : MonoBehaviour
{
    public Animator animator; // Animator 컴포넌트

    private bool isOpen = false; // 뚜껑 상태

    void OnMouseDown()
    {
        // 상태 토글
        isOpen = !isOpen;

        // Animator 트리거 설정
        if (isOpen)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            animator.SetTrigger("Close");
        }
    }
}

