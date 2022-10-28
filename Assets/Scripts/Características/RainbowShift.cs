using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowShift : MonoBehaviour
{
    private SpriteRenderer spr;
    private float red;
    private float green;
    private float blue;
    public float colorInterval;
    private bool whiteToBlack;
    public float transparency;
    Color baseColor;

    void Start()
    {
        red = 1.0f;
        blue = 1.0f;
        green = 1.0f;
        whiteToBlack = true;
        spr=GetComponent<SpriteRenderer>();
        baseColor = new Color(red, green, blue, transparency);
        spr.color = baseColor;
    }

    void Update()
    {
        if (whiteToBlack)
        {
            if (blue > 0.0f)
                decreaseBlue();

            else if (green > 0.0f)
                decreaseGreen();

            else if (red > 0.0f)
                decreaseRed();

            else
                whiteToBlack = false;
        }

        if (!whiteToBlack)
        {
            if (blue < 1.0f)
                increaseBlue();

            else if (green < 1.0f)
                increaseGreen();

            else if (red < 1.0f)
                increaseRed();

            else
                whiteToBlack = true;
        }

        spr.color = new Color(red, green, blue, transparency);

    }


    void decreaseRed()
    {
        red -= colorInterval;
    }

    void decreaseGreen()
    {
        green -= colorInterval;
    }

    void decreaseBlue()
    {
        blue -= colorInterval;
    }

    void increaseRed()
    {
        red += colorInterval;
    }

    void increaseGreen()
    {
        green += colorInterval;
    }

    void increaseBlue()
    {
        blue += colorInterval;
    }


}
