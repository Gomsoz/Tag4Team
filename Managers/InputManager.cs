using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None,
    Up,
    Right,
    Down,
    Left,
}

public enum ControlState
{
    Normal,
    TagHero,
    SelectHero,
}

public class InputManager : MonoBehaviour
{
    // 키 변경 필요
    public Action<Direction> DirectionKeyPublisher = null;
    public Action DicisionKeyPublisher = null;

    public bool isDash = false;

    private bool isLeftArrow = false;
    private bool isRightArrow = false;
    private bool isUpArrow = false;
    private bool isDownArrow = false;

    private bool chkAwayFromArrowKey
    {
        get
        {
            return !(isLeftArrow || isRightArrow || isUpArrow || isDownArrow);
        }
    }

    private Direction verticalArrow = Direction.None;
    private Direction horizontalArrow = Direction.None;

    public ControlState ContorlState = ControlState.Normal;

    private bool waitDoublePress = false;

    private Direction preDirection;
    private Direction dashDirection = Direction.None;

    private void Update()
    {
        if (ContorlState.Equals(ControlState.Normal))
        {
            if (Input.GetKeyUp(KeyCode.F1))
            {
                PlayerController.Instance.TagHero(0);
            }

            if (Input.GetKeyUp(KeyCode.F2))
            {
                PlayerController.Instance.TagHero(1);
            }

            if (Input.GetKeyUp(KeyCode.F3))
            {
                PlayerController.Instance.TagHero(2);
            }

            if (Input.GetKeyUp(KeyCode.F4))
            {
                PlayerController.Instance.TagHero(3);
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                PlayerController.Instance.UseHeroSkill(0);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                PlayerController.Instance.UseHeroSkill(1);
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                PlayerController.Instance.UseHeroSkill(2);
            }

            if (Input.GetKeyUp(KeyCode.R))
            {
                PlayerController.Instance.UseHeroSkill(3);
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                PlayerController.Instance.Attack();
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (verticalArrow.Equals(Direction.Down) == false)
                DirectionKeyPublisher?.Invoke(Direction.Up);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            verticalArrow = Direction.Up;
            isUpArrow = true;

            if (waitDoublePress == false)
            {
                StartCoroutine(ChkDoublePressKeyDown(Direction.Up));
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isUpArrow = false;

            if (isDash)
            {
                if (chkAwayFromArrowKey)
                {
                    isDash = false;
                    PlayerController.Instance.SetDash(false);
                }
            }

            if (verticalArrow.Equals(Direction.Up))
            {
                if (isDownArrow)
                    verticalArrow = Direction.Down;
                else
                    verticalArrow = Direction.None;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (verticalArrow.Equals(Direction.Up) == false)
                DirectionKeyPublisher?.Invoke(Direction.Down);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            verticalArrow = Direction.Down;
            isDownArrow = true;

            if (waitDoublePress == false)
            {
                StartCoroutine(ChkDoublePressKeyDown(Direction.Down));
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isDownArrow = false;

            if (isDash)
            {
                if (chkAwayFromArrowKey)
                {
                    isDash = false;
                    PlayerController.Instance.SetDash(false);
                }
            }

            if (verticalArrow.Equals(Direction.Down))
            {
                if (isUpArrow)
                    verticalArrow = Direction.Up;
                else
                    verticalArrow = Direction.None;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (horizontalArrow.Equals(Direction.Right) == false)
                DirectionKeyPublisher?.Invoke(Direction.Left);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            horizontalArrow = Direction.Left;
            isLeftArrow = true;

            if (waitDoublePress == false)
            {
                StartCoroutine(ChkDoublePressKeyDown(Direction.Left));
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isLeftArrow = false;

            if (isDash)
            {
                if (chkAwayFromArrowKey)
                {
                    isDash = false;
                    PlayerController.Instance.SetDash(false);
                }
            }

            if (horizontalArrow.Equals(Direction.Left))
            {
                if (isRightArrow)
                    horizontalArrow = Direction.Right;
                else
                    horizontalArrow = Direction.None;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (horizontalArrow.Equals(Direction.Left) == false)
                DirectionKeyPublisher?.Invoke(Direction.Right);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            horizontalArrow = Direction.Right;
            isRightArrow = true;

            if (waitDoublePress == false)
            {
                StartCoroutine(ChkDoublePressKeyDown(Direction.Right));
            }
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isRightArrow = false;

            if (isDash)
            {
                if (chkAwayFromArrowKey)
                {
                    isDash = false;
                    PlayerController.Instance.SetDash(false);
                }
            }

            if (horizontalArrow.Equals(Direction.Right))
            {
                if (isLeftArrow)
                    horizontalArrow = Direction.Left;
                else
                    horizontalArrow = Direction.None;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            DicisionKeyPublisher?.Invoke();
            PlayerController.Instance.Dodge(horizontalArrow);
        }
    }

    private IEnumerator ChkDoublePressKeyDown(Direction direction)
    {
        waitDoublePress = true;
        float chkTime = 0.2f;
        float leftTime = 0;
        while (leftTime <= chkTime)
        {
            yield return null;

            if (direction.Equals(Direction.Left))
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    waitDoublePress = false;
                    isDash = true;
                    PlayerController.Instance.SetDash(isDash);
                    break;
                }
            }
            else if (direction.Equals(Direction.Right))
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    waitDoublePress = false;
                    isDash = true;
                    PlayerController.Instance.SetDash(isDash);
                    break;
                }
            }
            else if (direction.Equals(Direction.Up))
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    waitDoublePress = false;
                    isDash = true;
                    PlayerController.Instance.SetDash(isDash);
                    break;
                }
            }
            else if (direction.Equals(Direction.Down))
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    waitDoublePress = false;
                    isDash = true;
                    PlayerController.Instance.SetDash(isDash);
                    break;
                }
            }

            leftTime += Time.deltaTime;  
        }

        waitDoublePress = false;
    }
}
