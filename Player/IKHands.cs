using System.Collections;
using UnityEngine;

public class IKHands : MonoBehaviour
{
    public Transform leftHandObj;
    public Transform attachLeft;

    Animator animator;
    float leftHandPositionWeight;
    float leftHandRotationWeight;

    void Awake()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(BlendIK());
    }

    void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandPositionWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandRotationWeight);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, attachLeft.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, attachLeft.rotation);
    }

    IEnumerator BlendIK()
    {
        yield return new WaitForSeconds(0.5f);
        float t = 0f;
        float blendTo = 1;
        float blendFrom = 0;

        while (t < 1)
        {
            t += Time.deltaTime;
            leftHandPositionWeight = Mathf.Lerp(blendFrom, blendTo, t);
            leftHandRotationWeight = Mathf.Lerp(blendFrom, blendTo, t);
            yield return null;
        }

        yield break;
    }
}