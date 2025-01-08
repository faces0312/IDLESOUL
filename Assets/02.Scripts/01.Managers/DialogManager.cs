using System.Collections;

public class DialogManager : Singleton<DialogManager>
{
    public Tutorial Tutorial;
    public ConversationUI ConversationUI;

    public bool NewStart;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        if(NewStart == true)
        {
            Tutorial.CoStart();
            ConversationUI.StartConversation(1);
        }
    }
}