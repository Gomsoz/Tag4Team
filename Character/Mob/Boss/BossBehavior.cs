using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField]
    protected MobBehavior behavior;

    [SerializeField]
    protected List<MobSkill> mobSkills = new();

    protected int bossLevel = 0;

    private void Start()
    {
        InitBoss();
    }

    protected virtual void InitBoss()
    {
        behavior.UpdateMobLevel += UpdateBossLevel;

        foreach (var item in mobSkills)
        {
            item.SkillInit(behavior);
        }
    }

    public virtual void UpdateBossLevel(int level)
    {
        bossLevel = level;
    }
}
