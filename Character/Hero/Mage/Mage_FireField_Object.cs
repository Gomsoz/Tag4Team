using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_FireField_Object : MonoBehaviour
{
    private float ticDuration;
    private int ticCount;
    private float fieldRadius;

    private Action<CharacterBehavior> hitCallback;

    public void Init(float _ticDuration, int _ticCount, float _fieldRadius, Action<CharacterBehavior> callback)
    {
        ticDuration = _ticDuration;
        ticCount = _ticCount;
        fieldRadius = _fieldRadius;
        hitCallback = callback;
    }

    public void StartLanding()
    {
        StartCoroutine(LandingField());
    }

    private IEnumerator LandingField()
    {
        int count = 0;
        float tic = ticDuration;

        while (count < ticCount)
        {
            if (tic >= ticDuration)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, fieldRadius);

                foreach (var item in hits)
                {
                    if (item.CompareTag(Utils_Tag.Hero) || item.CompareTag(Utils_Tag.Player))
                        continue;

                    CharacterBehavior target = item.GetComponent<CharacterBehavior>();
                    if (target != null)
                        hitCallback(target);
                }

                tic = 0;
                count++;
            }

            tic += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
