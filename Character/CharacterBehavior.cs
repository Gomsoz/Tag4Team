using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public enum CharacterType
{
    Hero,
    Mob,
}

public enum ImprovementAbilityStat
{
    FixedHp,
    PercentageHp,
    FixedMp,
    PercentageMp,
    FixedDamage,
    PercentageDamage,
    FixedDef,
    PercentageDef,
    FixedAs,
    PercentageAs,
    FixedMs,
    PercentageMs,
    Barrier,
}

public enum CharacterExtraStat
{
    AmplificationDamage,
    ReductionDamage,
}

public enum CharacterState
{
    Normal,
    Invincibility,
    Provoked,
    Reflection,
    Berserk,
}

public class CharacterBehavior : MonoBehaviour
{
    public Sprite CharacterImage { get; protected set; }
    protected CharacterType m_characterType;
    protected Dictionary<string, BuffOnPlaying> CharacterBuffs = new();
    protected Dictionary<string, Coroutine> buffTimes = new();

    public Action<int> FlipPublisher = null;
    public Action AttackPublisher = null;
    public Action<CharacterBehavior, int> DamagedPublisher = null;
    public Action<int> HealingPublisher = null;
    public Action DeathPublisher = null;
    public Action<BuffOnPlaying> EndBuffPublisher = null;

    public bool IsInvincibility { get; private set; }
    public bool IsReflection { get; private set; }
    private string reflectionBuffName;
    private float reflectionRatio;
    public bool IsProvked { get; private set; }
    protected CharacterBehavior provocativeCharacter;
    public CharacterBehavior ProvocativeCharacter { get => provocativeCharacter; }

    public bool IsBerserk { get; private set; }
    public bool IsDeath { get; private set; }

    public bool isAttackCooldown = false;

    public bool IsAttackable
    {
        get
        {
            return !(IsProvked || isAttackCooldown);
        }
    }

    public bool IsUsableSkill
    {
        get
        {
            return !IsProvked;
        }
    }

    public bool IsMovable { get; private set; }

    public Animator Anim { get; protected set; }
    [SerializeField]
    protected CharacterAnimationEvent CharacterAnimationEvent;

    protected Vector3 attackRange_angle1 = Vector3.zero;
    protected Vector3 attackRange_angle2;
    protected Collider2D[] CollidersWithinAttackRange;

    public int Hp { get; protected set; }
    public int MaxHp { get; protected set; }
    public int Mp { get; protected set; }
    public int MaxMp { get; protected set; }
    public int Damage { get; protected set; }
    public int Defense { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public float MoveSpeed { get; protected set; }

    protected int flipValue = 1;
    public int FlipValue { get => flipValue; }

    [SerializeField]
    protected Text_Damage txt_damage;

    private void Start()
    {
        SetAnimationEvent();
        CharacterInit();
    }

    public virtual void CharacterInit()
    {
        // Anim = transform.GetComponentInChildren<Animator>();
        
    }

    public virtual void Attack()
    {
        if (IsAttackable == false)
            return;

        AttackPublisher?.Invoke();
        StartCoroutine(AttakCooldown());
    }

    public Collider2D[] GetOrNullCharacterInAttackRange()
    {
        Vector3 angle1 = transform.position + new Vector3(attackRange_angle1.x * flipValue, attackRange_angle1.y);// new Vector3(-2 * flipValue, 0.8f);
        Vector3 angle2 = transform.position + new Vector3(attackRange_angle2.x * flipValue, attackRange_angle2.y);
        CollidersWithinAttackRange = Physics2D.OverlapAreaAll(angle1, angle2);

        if (CollidersWithinAttackRange.Length == 0)
            return null;

        return CollidersWithinAttackRange;
    }

    protected IEnumerator AttakCooldown()
    {
        isAttackCooldown = true;
        float leftTime = AttackSpeed;
        while (leftTime >= 0)
        {
            leftTime -= Time.deltaTime;
            yield return null;
        }
        isAttackCooldown = false;
    }

    public virtual void Damaged(CharacterBehavior Attacker, int value)
    {
        if (IsInvincibility)
        {
            DamagedPublisher?.Invoke(Attacker, value);
            return;
        }

        if (IsReflection)
        {
            Attacker.Damaged(this, (int)(value * reflectionRatio));
            return;
        }

        Hp -= value;
        DamagedPublisher?.Invoke(Attacker, value);
        txt_damage.SetDamage(value);

        if (Hp < 0)
            Death();            
    }

    public virtual void Death()
    {
        Anim?.SetBool("Death", true);
        DeathPublisher?.Invoke();   
    }

    protected void SetAnimationEvent()
    {
        CharacterAnimationEvent.callback_death += EndDeathAnim;
        CharacterAnimationEvent.callback_normalAttack += EndNormalAttackAnim;
        CharacterAnimationEvent.callback_dodge += EndDodgeAnim;
    }

    public virtual void EndDeathAnim()
    {
        IsDeath = true; 
    }

    public virtual void EndNormalAttackAnim()
    {
        
    }

    public virtual void EndDodgeAnim()
    {
        
    }

    public virtual void Healing(CharacterBehavior provider, int value)
    {
        Hp = Hp + value >= MaxHp ? MaxHp : Hp + value;

        HealingPublisher?.Invoke(value);
    }

    public virtual void ManaRecovery()
    {
        
    }

    public virtual void AdjustStatData(ImprovementAbilityStat stat, float value)
    {

    }

    public virtual void AdjustExtraStatData(CharacterExtraStat stat, float value)
    {

    }

    public void SetCharacterState(CharacterState state, bool isOn)
    {
        switch (state)
        {
            case CharacterState.Invincibility:
                IsInvincibility = isOn;
                break;
            case CharacterState.Reflection:
                IsReflection = isOn;
                break;
            case CharacterState.Provoked:
                IsProvked = isOn;
                if (IsProvked == false)
                    provocativeCharacter = null;
                break;
            case CharacterState.Berserk:
                IsBerserk = isOn;
                if (IsBerserk)
                    SetBerserk();
                else
                    ResetBerserk();              
                break;
        }
    }

    public virtual void SetBerserk()
    {

    }

    public virtual void ResetBerserk()
    {

    }

    public void SetProvocativeUnit(CharacterBehavior unit)
    {
        provocativeCharacter = unit;
    }

    public void AddBuff_Reflection(BuffOnPlaying newBuff, float ratio)
    {
        if (IsReflection == true)
        {
            if (ratio < reflectionRatio)
                return;

            BuffOnPlaying buff;
            CharacterBuffs.TryGetValue(reflectionBuffName, out buff);
            StopBuffOnPlaying(buff);
            reflectionBuffName = newBuff.BuffName;

        }

        AddBuffOnPlaying(newBuff);
        reflectionRatio = ratio;
        reflectionBuffName = newBuff.BuffName;
    }

    //public void AddBuff_Provoked(BuffOnPlaying newBuff, CharacterBehavior provocativeUnit)
    //{
    //    AddBuffOnPlaying(newBuff);
    //}

    public virtual void AddBuffOnPlaying(BuffOnPlaying buff)
    {
        if (CharacterBuffs.ContainsKey(buff.BuffName))
            return;

        EndBuffPublisher += EndBuffPublisher;
        buffTimes.Add(buff.BuffName, StartCoroutine(BuffTimer(buff))); 
    }

    private IEnumerator BuffTimer(BuffOnPlaying buff)
    {
        CharacterBuffs.Add(buff.BuffName, buff);
        buff.Apply();
        yield return new WaitForSeconds(buff.BuffDuration);
        buff.Revert(this);
        CharacterBuffs.Remove(buff.BuffName);
        buffTimes.Remove(buff.BuffName);
    }

    public virtual void StopBuffOnPlaying(BuffOnPlaying buff)
    {
        if (CharacterBuffs.ContainsKey(buff.BuffName) == false)
            return;

        StopCoroutine(buffTimes[buff.BuffName]);
        buff.Revert(this);
        CharacterBuffs.Remove(buff.BuffName);
        buffTimes.Remove(buff.BuffName);
    }

    public virtual void Flip(int _flipValue)
    {
        flipValue = _flipValue;

        FlipPublisher?.Invoke(flipValue);
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(flipValue, scale.y, scale.z);
    }
}
