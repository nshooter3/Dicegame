using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject front, tickDown;
    [SerializeField]
    private TextMeshPro textMesh;

    private float initWidth;
    private float curScale, tickdownScale;

    private bool tickingDown = false;

    private float tickdownTimer;
    private const float TICKDOWN_TIME = 1f;
    private const float TICKDOWN_SPEED = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        initWidth = front.transform.localScale.x;
        curScale = 1f;
        tickdownScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (tickingDown)
        {
            if (tickdownTimer > 0)
            {
                tickdownTimer -= Time.deltaTime;
            }
            else if (tickdownScale > curScale)
            {
                tickdownScale = Mathf.Max(curScale, tickdownScale - TICKDOWN_SPEED * Time.deltaTime);
                SetWidth(tickDown.transform, tickdownScale);
            }
            else
            {
                tickingDown = false;
            }
        }
    }

    public void SetHealth(int curHealth, int maxHealth, bool tookDamage = true)
    {
        Debug.Log("CUR HEALTH " + curHealth + ", MAX HEALTH " + maxHealth);
        curScale = (curHealth / (float)maxHealth);
        Debug.Log("CurScale " + curScale);
        SetWidth(front.transform, curScale);
        textMesh.text = curHealth + "/" + maxHealth;
        if (tookDamage && !tickingDown)
        {
            tickingDown = true;
            tickdownTimer = TICKDOWN_TIME;
        }
    }

    private void SetWidth(Transform trans, float scale)
    {
        Vector3 temp = trans.localScale;
        temp.x = scale * initWidth;
        trans.localScale = temp;
    }
}
