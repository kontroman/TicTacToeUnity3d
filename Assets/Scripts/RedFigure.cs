using UnityEngine;

public class RedFigure : Figure
{
    private void Start()
    {
        SetColor();
    }

    public override void SetColor()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

}
