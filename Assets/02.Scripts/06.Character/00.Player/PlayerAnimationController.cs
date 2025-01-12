using System;
using Spine;
using Spine.Unity;
using UnityEngine;


public class PlayerAnimationController : MonoBehaviour
{
    #region Inspector
    // [SpineAnimation] attribute allows an Inspector dropdown of Spine animation names coming form SkeletonAnimation.
    [SpineAnimation]
    public string idleAnimationName;

    [SpineAnimation]
    public string runAnimationName;

    [SpineAnimation]
    public string MeleeAttackAnimationName;

    [SpineAnimation]
    public string ShotAttackAnimationName;

    [SpineAnimation]
    public string VictoryAnimationName;

    [SpineAnimation]
    public string DeathAnimationName;

    //[Header("Transitions")]
    //[SpineAnimation]
    //public string idleTurnAnimationName;

    //[SpineAnimation]
    //public string runToIdleAnimationName;

    //public float runWalkDuration = 1.5f;
    #endregion

    [SerializeField] private GameObject StunDebuffAniamtion; //스턴 디버프 상태이상 애니메이션 

    private SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    public void Initialize()
    {
        //spineanimation 초기화
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }
    
    public void StunAnimationStart()
    {
        StunDebuffAniamtion.gameObject.SetActive(true);
    }

    public void StunAnimationEnd()
    {
        StunDebuffAniamtion.gameObject.SetActive(false);
    }
}
