using System.Collections;
using TMPro;
using UnityEngine;

public class NumberPoolable : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI numText;
    [SerializeField]
    private Animator anim;

    public bool available = true;

    public void SetNumText(string str)
    {
        numText.text = str;
    }

    public void SmallHit(Vector2 position)
    {
        RandomlyOffsetPosition(position);
        anim.SetTrigger("SmallHit");
        StartCoroutine(AvailableCoroutine());
    }

    public void Hit(Vector2 position)
    {
        RandomlyOffsetPosition(position);
        anim.SetTrigger("Hit");
        StartCoroutine(AvailableCoroutine());
    }

    public void BigHit(Vector2 position)
    {
        RandomlyOffsetPosition(position);
        anim.SetTrigger("BigHit");
        StartCoroutine(AvailableCoroutine());
    }

    public void AttackBuff(Vector2 position)
    {
        RandomlyOffsetPosition(position);
        anim.SetTrigger("AttackBuff");
        StartCoroutine(AvailableCoroutine());
    }

    public void DefenseBuff(Vector2 position)
    {
        RandomlyOffsetPosition(position);
        anim.SetTrigger("DefenseBuff");
        StartCoroutine(AvailableCoroutine());
    }

    public void PlayerHit(Vector2 position)
    {
        RandomlyOffsetPosition(position, true);
        anim.SetTrigger("PlayerHit");
        StartCoroutine(AvailableCoroutine());
    }

    public void PlayerAttackBuff(Vector2 position)
    {
        RandomlyOffsetPosition(position, true);
        anim.SetTrigger("PlayerAttackBuff");
        StartCoroutine(AvailableCoroutine());
    }

    public void PlayerDefenseBuff(Vector2 position)
    {
        RandomlyOffsetPosition(position, true);
        anim.SetTrigger("PlayerDefenseBuff");
        StartCoroutine(AvailableCoroutine());
    }

    private void RandomlyOffsetPosition(Vector2 position, bool isPlayer = false)
    {
        int offset = 20;
        if (isPlayer)
        {
            offset = 50;
        }
        position.x += Random.Range(-offset, offset);
        position.y += Random.Range(-offset, offset);
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.position = position;
        Debug.Log("set number pos to " + position);
    }

    private IEnumerator AvailableCoroutine()
    {
        available = false;
        yield return new WaitForSeconds(2f);
        available = true;
    }

}
