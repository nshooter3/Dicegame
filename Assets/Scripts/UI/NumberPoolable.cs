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

    public void PlayerHit(Vector2 position)
    {
        RandomlyOffsetPosition(position);
        anim.SetTrigger("PlayerHit");
        StartCoroutine(AvailableCoroutine());
    }

    private void RandomlyOffsetPosition(Vector2 position)
    {
        position.x += Random.Range(-10f, 10f);
        position.y += Random.Range(-10f, 10f);
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
