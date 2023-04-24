using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_FireBall_FireBall : MonoBehaviour
{
    private Action<CharacterBehavior> collisionCallback;

    [SerializeField]
    private GameObject destroyEffect;

    public void Init(Vector3 goalPosition, float moveSpeed, Action<CharacterBehavior> callback)
    {
        collisionCallback = callback;

        StartCoroutine(MoveFireBall(goalPosition, moveSpeed));
    }

    private IEnumerator MoveFireBall(Vector3 goalPosition, float moveSpeed)
    {
        float leftDistance = (goalPosition - transform.position).magnitude;
        while (leftDistance >= 0)
        {
            if(leftDistance < 0.0001f)
            {
                transform.position = goalPosition;
                break;  
            }

            transform.position = Vector3.MoveTowards(transform.position, goalPosition, moveSpeed * Time.deltaTime);
            leftDistance = (goalPosition - transform.position).magnitude;

            yield return null;
        }

        destroyEffect.SetActive(true);
        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Utils_Tag.Mob) == false)
            return;

        CharacterBehavior target = collision.GetComponent<CharacterBehavior>();
        collisionCallback(target);

        destroyEffect.SetActive(true);
        Destroy(gameObject);
    }
}
