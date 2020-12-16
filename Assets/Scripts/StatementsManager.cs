using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatementsManager : MonoBehaviour
{
    public static StatementsManager Instance;

    public GameObject statementGameObject;
    public UnityEngine.UI.Text statementMessage;
    public UnityEngine.UI.Text statementButtonText;
    public UnityEngine.UI.Button statementButton;

    public void Awake()
    {
        if (StatementsManager.Instance == null)
            StatementsManager.Instance = this;
        else
            Destroy(this);
    }

    public void ShowStatement(string message)
    {
        this.statementMessage.text = message;
        this.statementGameObject.SetActive(true);
    }

    public void ShowStatement(string message, string buttonText, UnityEngine.Events.UnityAction methodCalledWhenButtonBeenClicked)
    {
        this.statementMessage.text = message;
        this.statementButtonText.text = buttonText;
        this.statementButton.onClick.AddListener(methodCalledWhenButtonBeenClicked);
        this.statementGameObject.SetActive(true);
    }
}
