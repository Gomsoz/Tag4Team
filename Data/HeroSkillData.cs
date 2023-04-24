using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkillData
{
    public Dictionary<string, SkillData> skilldatas = new();

    public SkillData GetSkillData(string skillID)
    {
        return skilldatas[skillID];
    }
    // TODO
    // 스킬 데이터 입력 부분
}
