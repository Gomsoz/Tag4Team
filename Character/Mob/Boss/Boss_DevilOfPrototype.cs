using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Boss_DevilOfPrototype : MobBehavior
{
    public void SurpriseAttack()
    {
        

        Debug.Log("SurpriseAttack");
    }

    public void GetAway()
    {
        // 주변 플레이어를 뒤로 밀어냄
        Debug.Log("GetAway");
    }

    public void TripleAttack()
    {
        Debug.Log("TrippleAtack");
        // 3회 공격 후 전방으로 검기 생성
    }
}
