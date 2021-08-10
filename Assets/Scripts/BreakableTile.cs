using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : MonoBehaviour
{
    private Sprite defaultImage;
    private Sprite flagImage;

    private int id;
    private Sprite image;

    private int x;
    private int y;

    private bool broken;
    private bool flaged;

    SpriteRenderer imageComponent;

    void Start()
    {

        id = -2;
        broken = false;
        flaged = false;
        imageComponent = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Reset()
    {

        id = -2;
        broken = false; 
        flaged = false;
    }

    public void Break()
    {

        imageComponent.sprite = image;
        broken = true;
    }

    public void SetUnderneathImage(Sprite image)
    {

        this.image = image;
    }

    public void SetCurrentImage(Sprite image)
    {

        imageComponent.sprite = image;
    }

    public void SetId(int id)
    {

        this.id = id;
    }

    public int GetId()
    {

        return id;
    }

    public int GetX()
    {

        return x;
    }

    public int GetY()
    {

        return y;
    }

    public bool GetBroken()
    {
        return broken;
    }

    public Sprite GetCurrentImage()
    {

       return imageComponent.sprite;
    }

    public void SetX(int x)
    {

        this.x = x;
    }

    public void SetY(int y)
    {

        this.y = y;
    }

    public bool GetFlaged()
    {
        return flaged;
    }

    public void SetFlaged(bool flaged)
    {
        this.flaged = flaged;
    }
}
