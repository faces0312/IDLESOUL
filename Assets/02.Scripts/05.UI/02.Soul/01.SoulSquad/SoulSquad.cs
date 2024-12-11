using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSquad : MonoBehaviour
{
    [SerializeField] private SoulSquadView soulSquadView;
    private SoulSquadController soulSquadController;
    private SoulSquadModel soulSquadModel;

    private string uiKey;

    private void Start()
    {
        uiKey = "SoulSquad";

        if (soulSquadController == null)
        {
            soulSquadModel = new SoulSquadModel();
            soulSquadController = new SoulSquadController();
            soulSquadController.Initialize(soulSquadView, soulSquadModel);
            UIManager.Instance.RegisterController(uiKey, soulSquadController);
        }
        gameObject.SetActive(false);
    }
}
