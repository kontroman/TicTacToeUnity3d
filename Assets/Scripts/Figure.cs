using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour, IColor
{

    public Color figureColor;

    public virtual Color GetColor()
    {
        return figureColor;
    }

    public virtual void SetColor()
    { }

    public virtual void PlaceFigure(Vector3 fieldCoordinates)
    {
        Instantiate(gameObject, fieldCoordinates, Quaternion.identity);
    }

}
