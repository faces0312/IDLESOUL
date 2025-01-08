using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DialogManager : Singleton<DialogManager>
{
    public Tutorial Tutorial;
    public ConversationUI ConversationUI;

    public bool NewStart;
    private bool isDead;

    protected override void Awake()
    {
        base.Awake();
        EventManager.Instance.Subscribe<AchieveEvent>(Enums.Channel.Achievement, IfDead);
    }

    public void Init()
    {
        if(NewStart == true)
        {
            Tutorial.CoStart();
            ConversationUI.StartConversation(1);
        }
    }

    public void StartConversation(int cycle)
    {
        ConversationUI.StartConversation(cycle);
    }
    
    public void IfDead(AchieveEvent arg)
    {
        if(arg.Action == Enums.ActionType.Player && arg.Type == Enums.AchievementType.Kill && isDead == false)
        {
            isDead = true;
            StartConversation(2);
        }
    }
}