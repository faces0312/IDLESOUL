using System;
using Spine;
using Spine.Unity;
using UnityEngine;


public class PlayerAnimationController : MonoBehaviour
{
    #region Inspector
    // [SpineAnimation] attribute allows an Inspector dropdown of Spine animation names coming form SkeletonAnimation.
    [SpineAnimation]
    public string runAnimationName;

    [SpineAnimation]
    public string idleAnimationName;

    [SpineAnimation]
    public string AttackAnimationName;

    [Header("Transitions")]
    [SpineAnimation]
    public string idleTurnAnimationName;

    [SpineAnimation]
    public string runToIdleAnimationName;

    public float runWalkDuration = 1.5f;
    #endregion

    #region

    #endregion

    private SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;

    private void Awake()
    {
        //spineanimation √ ±‚»≠
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
      
    }
}
