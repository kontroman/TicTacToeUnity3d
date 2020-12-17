using UnityEngine;

public class GreenFigure : Figure
{
    private void Start()
    {
        SetColor();
    }

    public override void SetColor()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

}
