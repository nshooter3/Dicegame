using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySelector : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Animator anim;

    private bool hasTarget = false;
    private bool isActive = false;

    private Color fadedCol, normalCol;
    private const float COLOR_FADE_SPEED = 20f;

    // Start is called before the first frame update
    void Start()
    {
        fadedCol = new Color(1f, 1f, 1f, 0f);
        normalCol = new Color(1f, 1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Enemy enemy;
            if (GameManager.instance.clickManager.CheckForSelectedEnemy(out enemy))
            {
                hasTarget = true;
                Vector2 viewportPosition = Camera.main.WorldToScreenPoint(enemy.transform.position);
                RectTransform rectTransform = GetComponent<RectTransform>();
                rectTransform.position = viewportPosition;
                image.color = Color32.Lerp(image.color, normalCol, COLOR_FADE_SPEED * Time.deltaTime);
            }
            else
            {
                hasTarget = false;
                image.color = Color32.Lerp(image.color, fadedCol, COLOR_FADE_SPEED * Time.deltaTime);
            }
        }
        else
        {
            image.color = Color32.Lerp(image.color, fadedCol, COLOR_FADE_SPEED * Time.deltaTime);
        }
    }

    public void SelectTarget()
    {
        Debug.Log("EnemySelector SelectTarget");
        isActive = false;
        anim.SetTrigger("Select");
    }

    public void SetActive()
    {
        Debug.Log("EnemySelector SetActive");
        image.color = fadedCol;
        isActive = true;
        anim.SetTrigger("Idle");
    }
}
