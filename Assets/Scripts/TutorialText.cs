using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    public bool isActive;

    private Color fadedCol, normalCol;
    private const float COLOR_FADE_SPEED = 10f;

    // Start is called before the first frame update
    void Start()
    {
        fadedCol = text.color;
        fadedCol.a = 0f;
        normalCol = text.color;
        normalCol.a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            text.color = Color32.Lerp(text.color, normalCol, COLOR_FADE_SPEED * Time.deltaTime);
        }
        else
        {
            text.color = Color32.Lerp(text.color, fadedCol, COLOR_FADE_SPEED * Time.deltaTime);
        }
    }
}
