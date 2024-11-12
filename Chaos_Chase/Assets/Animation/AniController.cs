using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class AniController : MonoBehaviour
{
    public Animator ani;
    public GameObject npc;
    public GameObject NPCSelection_Panel;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // NPC1 버튼 클릭 시 Maya 앉기 애니메이션 동작
    public void OnNPC1ButtonClick()
    {
        NPCSelection_Panel.SetActive(false);
        npc.SetActive(true);
        ani.SetTrigger("MayaSitting");   
    }

    // NPC2 버튼 클릭 시 Oliver 앉기 애니메이션 동작
    public void OnNPC2ButtonClick()
    {
        NPCSelection_Panel.SetActive(false);
        npc.SetActive(true);
        ani.SetTrigger("OliverSitting");
    }

    // NPC3 버튼 클릭 시 Lucy 앉기 애니메이션 동작
    public void OnNPC3ButtonClick()
    {
        NPCSelection_Panel.SetActive(false);
        npc.SetActive(true);
        ani.SetTrigger("LucySitting");
    }

    // NPC4 버튼 클릭 시 Leo 앉기 애니메이션 동작
    public void OnNPC4ButtonClick()
    {
        NPCSelection_Panel.SetActive(false);
        npc.SetActive(true);
        ani.SetTrigger("LeoSitting");
    }
}
