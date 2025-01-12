using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontManager : MonoBehaviour
{
    public Font pixelFont;

    // Start is called before the first frame update
    void Start()
    {
        pixelFont.material.mainTexture.filterMode = FilterMode.Point;
    }
}
