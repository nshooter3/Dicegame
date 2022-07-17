using UnityEngine;

public class AnimationStartTimeRandomizer : MonoBehaviour
{
    [SerializeField]
    private string animName;

    // Start is called before the first frame update
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        float randomStartTimeNormalized = Random.Range(0f, 1f);
        anim?.Play(animName, 0, randomStartTimeNormalized);
    }
}
