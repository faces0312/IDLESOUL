using UnityEngine;

[System.Serializable]
public abstract class UIBase<M,V,C> : MonoBehaviour
    where M : UIModel, new()
    where V : IUIBase
    where C : UIController, new()
{ 
    protected M model;
    [SerializeField] protected V[] views;
    protected C controller;

    public virtual void Start()
    {
        //Model(Data) 초기화
        model = new M();

        //컨트롤러  초기화 및 View 등록
        controller = new C();
        for (int i = 0; i < views.Length; i++)
        {
            controller.Initialize(views[i], model);
        }

        //UI매니저에 UI 등록
        UIManager.Instance.RegisterController(typeof(C).ToString(), controller);    
    }
}

