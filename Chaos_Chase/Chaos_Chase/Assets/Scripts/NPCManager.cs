using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject npcMaya;
    public GameObject npcOlver;
    public GameObject npcLucy;
    public GameObject npcLeo;

    // 모든 NPC 캐릭터 비활성화
    void Start()
    {
        npcMaya.SetActive(false);
        npcOlver.SetActive(false);
        npcLucy.SetActive(false);
        npcLeo.SetActive(false);
    }
}
