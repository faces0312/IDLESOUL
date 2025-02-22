﻿using UnityEngine;

public class AnimatorHashData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string attackParameterName = "Attack";
    [SerializeField] private string grabbedParameterName = "Grabbed";
    [SerializeField] private string attackSpeedParameterName = "AttackSpeed";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string skillParameterName = "Skill";
    [SerializeField] private string dieParameterName = "Die";


    public int GrabbedParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int AttackSpeedParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int SkillParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }


    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        GrabbedParameterHash = Animator.StringToHash(grabbedParameterName);
        AttackSpeedParameterHash = Animator.StringToHash(attackSpeedParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        SkillParameterHash = Animator.StringToHash(skillParameterName);
        DieParameterHash = Animator.StringToHash(dieParameterName);
    }
}