using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MobState
{
    Stop,
    Normal,
    Move,
    Attack,
    Action,
}

public class MobBehavior : CharacterBehavior
{
    private MobState state = MobState.Normal;
    public MobState State { get { return state; } }

    private MobInfo thisMobInfo;

    private PlayerController target;
    protected PlayerController Target { get { return target; } }
    protected CharacterBehavior targetHero;

    private string MobName;
    [SerializeField]
    private List<MobSkill> mobSkills = new();

    protected Action ActionInProgress;

    #region Pattern

    protected bool isRestTime = false;
    public bool IsRestTime { get { return isRestTime; } }

    private Coroutine moveCoroutine = null;
    protected float nextBehaviorTerm = 0.5f;
    public float NextBehaviorTerm { get { return nextBehaviorTerm; } }

    private float restDuration = 1f;

    private List<MobSkill> usableSkills = new();
    public List<MobSkill> UsableSkills { get => usableSkills; }
    public bool ReadyToUseSkill
    {
        get
        {
            return usableSkills.Count > 0 ? true : false;
        }
    }

    protected float distance;
    public float Distance
    {
        get { return distance; }
        set
        {
            distance = value;
            ChangeDistancePublisher?.Invoke(value);
        }
    }

    public Action<float> ChangeDistancePublisher = null;

    #endregion

    private void Update()
    {
        if (target != null)
        {
            Distance = (target.transform.position - transform.position).magnitude;
        }
    }

    public void MobInit(MobInfo newMobInfo)
    {
        thisMobInfo = newMobInfo;
        CharacterStatData statData = newMobInfo.Stat;

        newMobInfo.UpdateImprovementAbilityStatData();

        Hp = statData.Hp;
        MaxHp = statData.MaxHp;
        Mp = statData.Mp;
        MaxMp = statData.MaxMp;
        Damage = statData.Dmg;
        Defense = statData.Def;
        AttackSpeed = statData.AttackSpeed;
        MoveSpeed = statData.MoveSpeed;
        attackRange_angle1 = new Vector3(statData.AttackRange_Angle1_x, statData.AttackRange_Angle1_y, 0);
        attackRange_angle2 = new Vector3(statData.AttackRange_Angle2_x, statData.AttackRange_Angle2_y, 0);

        MobName = newMobInfo.MobData.MobName;
        

        foreach (var item in mobSkills)
        {
            item.SkillInit(this);
        }

        Anim = transform.GetComponentInChildren<Animator>();

        string IdleImagePath = $"Mob/{MobName}/Sprites/CharacterImage_{MobName}";
        CharacterImage = Resources.Load<Sprite>(IdleImagePath);

        Managers.Instance.UI.Panel_MobState.SetMob(this);
        target = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void StartMove()
    {
        if (target == null)
            return;

        if (moveCoroutine == null)
            moveCoroutine = StartCoroutine(Move());
    }

    protected IEnumerator Move()
    {
        int flipValue = (Target.transform.position.x - transform.position.x) >= 0 ? -1 : 1;
        Flip(flipValue);

        Anim.SetBool("Run", true);
        while (State.Equals(MobState.Normal))
        {
            if (IsProvked)
                break;

            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        Anim.SetBool("Run", false);
        moveCoroutine = null;
    }

    public void StartProvokedMove()
    {
        if (moveCoroutine == null)
            moveCoroutine = StartCoroutine(ProvokedMove());
    }

    protected IEnumerator ProvokedMove()
    {
        int flipValue = (ProvocativeCharacter.transform.position.x - transform.position.x) >= 0 ? -1 : 1;
        Flip(flipValue);

        Anim.SetBool("Run", true);
        while (IsProvked)
        {
            transform.position = Vector3.MoveTowards(transform.position, ProvocativeCharacter.transform.position, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        Anim.SetBool("Run", false);
        moveCoroutine = null;
    }

    public override void Attack()
    {
        base.Attack();

        if (GetOrNullCharacterInAttackRange() == null)
        {
            state = MobState.Normal;
            return;
        }

        foreach (var item in CollidersWithinAttackRange)
        {
            if (item.CompareTag(Utils_Tag.Mob))
                continue;

            CharacterBehavior target = item.GetComponent<HeroBehavior>();
            target?.Damaged(this, Damage);
        }

        Anim.SetBool("NormalAttack", false);
        state = MobState.Normal;
    }

    public override void Damaged(CharacterBehavior Attacker, int value)
    {
        base.Damaged(Attacker, value);
        Debug.Log("DAMGED");
        Managers.Instance.UI.Panel_MobState.UpdateMobHp(this);
    }

    public override void Death()
    {
        base.Death();

        StopAllCoroutines();
    }

    public override void EndDeathAnim()
    {
        base.EndDeathAnim();

        GameManager.Instance.Victory();
        Destroy(gameObject);
    }

    public override void EndNormalAttackAnim()
    {
        Attack();
    }

    public void NomalAttack()
    {
        Collider2D[] hits = GetOrNullCharacterInAttackRange();

        foreach (var item in hits)
        {
            if (item.CompareTag(Utils_Tag.Player))
            {
                state = MobState.Attack;
                Anim.SetBool("NormalAttack", true);
                break;
            }
        }
    }

    public virtual void UseSkill()
    {
        state = MobState.Action;

        int idx = UnityEngine.Random.Range(0, usableSkills.Count);
        usableSkills[idx].UseSkill(EndSkill);
    }

    public void EndSkill()
    {
        if (state.Equals(MobState.Attack))
            return;

        Rest();

        state = MobState.Normal;
    }

    public void Rest()
    {
        if (isRestTime == false)
        {
            isRestTime = true;
            StartCoroutine(RestTimer());
        }
    }

    private IEnumerator RestTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(restDuration);
            break;
        }

        isRestTime = false;
    }

    public virtual void AddSkill(MobSkill skill)
    {
        usableSkills.Add(skill);
    }

    public virtual void RemoveSkill(MobSkill skill)
    {
        usableSkills.Remove(skill);
    }

    public void StartSkillCooldown(SkillCooldownInfo info)
    {
        StartCoroutine(SkillCooldown(info));
    }

    private IEnumerator SkillCooldown(SkillCooldownInfo info)
    {
        info.isCooldown = true;
        float leftTime = info.totalCooldown;
        while (leftTime >= 0)
        {
            leftTime -= Time.deltaTime;
            yield return null;
        }

        info.isCooldown = false;
    }

    public override void Flip(int _flipValue)
    {
        base.Flip(_flipValue);

        Vector3 scale = txt_damage.transform.localScale;
        txt_damage.transform.localScale = new Vector3(flipValue, scale.y, scale.z);
    }
}
