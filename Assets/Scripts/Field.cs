using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Field : MonoBehaviour, IPointerClickHandler
{
    public StateOfField State
    {
        get { return state; }
        set { ChangeFieldState(value); }
    }
    public enum StateOfField
    {
        EMPTY = 0,
        CIRCLE = 1,
        CROSS = 2

    }
    public Image FieldRepresentation;

    private Sprite crossSprite, circleSprite, emptySprite;
    private StateOfField state;

    public void Awake()
    {
        crossSprite = Resources.Load<Sprite>("crossSprite");
        circleSprite = Resources.Load<Sprite>("circleSprite");
        emptySprite = Resources.Load<Sprite>("emptySprite");
    }

    private void ChangeFieldState(StateOfField newState)
    {
        this.state = newState;
        switch (newState)
        {
            case Field.StateOfField.EMPTY:
                FieldRepresentation.sprite = this.emptySprite;
                break;

            case Field.StateOfField.CIRCLE:
                FieldRepresentation.sprite = this.circleSprite;
                FieldRepresentation.GetComponent<Image>().color = Color.red;
                break;

            case Field.StateOfField.CROSS:
                FieldRepresentation.sprite = this.crossSprite;
                FieldRepresentation.GetComponent<Image>().color = Color.green;
                break;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.FieldWasClicked(this);
    }
}
