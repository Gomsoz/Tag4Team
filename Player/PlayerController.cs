using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private int[] fixedMoveX = new int[5] { 0, 0, 1, 0, -1 };
    private int[] fixedMoveY = new int[5] { 0, 1, 0, -1, 0 };
    private float moveSpeed = 0.005f;
    private float dashSpeed = 1;
    private float dodgeTime = 0.3f;
    private float dodgeDistance = 2f;

    [SerializeField]
    private List<HeroBehavior> playerHeroes = new List<HeroBehavior>();
    private HeroBehavior curHero;
    public HeroBehavior CurHero { get { return curHero; } }
    public int CurHeroIdx { get; private set; }

    private List<HeroBehavior> castingHeroes = new List<HeroBehavior>();
    private HeroBehavior postHero;

    [SerializeField]
    private GameObject prefab_Hero;
    [field:SerializeField]
    public Transform HeroParent { get; private set; }
    [field: SerializeField]
    public Transform CastingHolder { get; private set; }

    [SerializeField]
    private SelectLocation locationSelecter;

    [SerializeField]
    private AnimationCurve animCurve;

    public int FlipValue { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Managers.Instance.Input.DirectionKeyPublisher += MovePlayer;
    }
    
    /// <summary>
    /// 등록된 영웅을 가져온다.
    /// </summary>
    public HeroBehavior GetHero(int heroIdx)
    {
        return playerHeroes[heroIdx];
    }
    
    /// <summary>
    /// 게임 시작(Game Scene 진입)시 영웅을 생성한다.
    /// </summary>
    public void CreateHero(HeroInfo data)
    {
        HeroBehavior hero = Instantiate(prefab_Hero, HeroParent).GetComponent<HeroBehavior>();

        hero.HeroInit(playerHeroes.Count, data);
        playerHeroes.Add(hero);
    }

    /// <summary>
    /// 현재 조작중인 영웅에 공격을 명령한다.
    /// </summary>
    public void Attack()
    {
        curHero.Attack();
    }

    public void SetDash(bool isOn)
    {
        dashSpeed = isOn ? 2 : 1;
    }

    /// <summary>
    /// 현재 조작중인 영웅을 움직이도록 한다.
    /// </summary>
    private void MovePlayer(Direction direction)
    {
        if (curHero == null)
            return;

        if (curHero.HeroState.Equals(HeroSkillState.SelectTargeting) || curHero.HeroState.Equals(HeroSkillState.SelectLocation) || curHero.HeroState.Equals(HeroSkillState.Continuously))
            return;

        int moveX = fixedMoveX[(int)direction];
        int moveY = fixedMoveY[(int)direction];

        if (direction.Equals(Direction.Left))
        {
            FlipValue = 1;
            curHero.Flip(FlipValue);
        }
        else if (direction.Equals(Direction.Right))
        {
            FlipValue = -1;
            curHero.Flip(FlipValue);
        }

        transform.Translate(new Vector3(moveX, moveY) * moveSpeed * dashSpeed * Time.deltaTime);
    }

    public void Dodge(Direction dir)
    {
        if (CurHero.IsUseDodge == false)
            return;

        if (dir.Equals(Direction.None))
            return;

        StartCoroutine(StartDodge(dir));
    }

    private IEnumerator StartDodge(Direction dir)
    {
        CurHero.IsUseDodge = false;
        curHero.Anim.SetTrigger("Dodge");
        Vector3 startPosition = transform.position;
        float leftTime = 0;
        while (leftTime < dodgeTime)
        {
            float value  = animCurve.Evaluate(leftTime / dodgeTime);
            float movePosition = Mathf.Lerp(0, dodgeDistance, value);

            transform.position =
                startPosition + new Vector3(movePosition * fixedMoveX[(int)dir], 0);

            leftTime += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// 현재 조작중인 영웅과 키를 입력한 영웅을 교체한다.
    /// </summary>
    public void TagHero(int heroIdx)
    {
        HeroBehavior getHero = playerHeroes[heroIdx];
        CurHeroIdx = heroIdx;
        if (getHero.isTagCooldown)
        {
            Debug.Log("영웅 교체에 시간이 필요합니다.");
            return;
        }

        if(getHero.HeroState.Equals(HeroSkillState.Normal) == false)
        {
            Debug.Log("영웅을 변경할 수 없는 상태입니다");
            return;
        }

        if (getHero.IsDeath)
            return;

        postHero = getHero;

        curHero?.TagOut();
        postHero.TagIn();
        Managers.Instance.UI.Panel_Control.SetHeroBehavior(postHero);

        curHero = postHero;
        moveSpeed = curHero.MoveSpeed;
    }

    /// <summary>
    /// 현재 조작중인 영웅의 상태를 변경한다.
    /// </summary>
    public void ChangeHeroState(HeroBehavior hero, HeroSkillState state)
    {
        switch (state)
        {
            case HeroSkillState.Normal:
                hero.transform.parent = HeroParent;
                //hero.transform.position = Vector3.zero;
                break;
            case HeroSkillState.Casting:
            case HeroSkillState.Continuously:
            case HeroSkillState.SelectTargeting:
                hero.transform.parent = CastingHolder;
                break;
        }
    }

    /// <summary>
    /// 위치를 지정하는 스킬을 사용 시 위치를 선택하는 기능을 실행한다.
    /// </summary>
    public void StartLocationSelecter(Action<Vector3> callback)
    {
        locationSelecter.StartLocationSelecter(callback);
    }

    /// <summary>
    /// 현재 조작중인 영웅의 스킬을 키 입력에 따라 실행시킨다.
    /// </summary>
    public void UseHeroSkill(int skillNum)
    {
        curHero?.UseSkill(skillNum);
    }

    public void UpdateControllerPosition()
    {
        Vector3 newPosition = curHero.transform.position;
        curHero.transform.localPosition = Vector3.zero;
        transform.position = newPosition;
    }

    public void SaveHeroData()
    {
        foreach(var item in playerHeroes)
        {
            item.SaveHeroStat();
        }

        HeroDataManager.Instance.SaveHeroDatas();
    }
}
