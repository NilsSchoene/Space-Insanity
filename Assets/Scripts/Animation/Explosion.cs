using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator animator;
    private float clipLength;

    void Awake()
    {
        animator = GetComponent<Animator>();
        AnimatorClipInfo[] animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        clipLength = animatorClipInfo[0].clip.length;
    }

    void Start()
    {
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(clipLength);
        Destroy(this.gameObject);
    }
}
