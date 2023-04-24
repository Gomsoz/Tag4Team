using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_Meteor : HeroSkill
{
    [SerializeField]
    private GameObject effectPrefab;

    [SerializeField]
    private GameObject meteorPrefab;

    private float meteorHeight = 10f;
    private float meteorDropTime = 2f;
    private float meteorRadius = 2f;

    private int damageBase = 150;
    private float damageFactor = 1f;

    public override void EndCasting()
    {
        base.EndCasting();

        StartCoroutine(DropMeteor());
    }

    private IEnumerator DropMeteor()
    {
        GameObject meteor = Instantiate(meteorPrefab, new Vector3(LocatedlPosition.x, LocatedlPosition.y + meteorHeight), Quaternion.identity);

        float leftTime = meteorDropTime;
        while (leftTime >= 0)
        {
            float height = Mathf.Lerp(0, meteorHeight, leftTime / meteorDropTime);
            meteor.transform.position = new Vector3(LocatedlPosition.x, LocatedlPosition.y + height);
            leftTime -= Time.deltaTime;
            yield return null;
        }

        meteor.SetActive(false);
        meteor.transform.parent = transform;
        Instantiate(effectPrefab, LocatedlPosition, Quaternion.identity);

        Collider2D[] hitCharacters = Physics2D.OverlapCircleAll(LocatedlPosition, meteorRadius);

        foreach (var item in hitCharacters)
        {
            if (item.CompareTag(Utils_Tag.Hero) || item.CompareTag(Utils_Tag.Player))
                continue;

            CharacterBehavior target = item.GetComponent<CharacterBehavior>();
            target.Damaged(skillOwner, ConvertDamage(damageBase, damageFactor));
        }

        EndSkill();
    }
}
