using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private RawImage _Image;
    [SerializeField] private float _x, _y;

    // Update is called once per frame
    void Update()
    {
        _Image.uvRect = new Rect(_Image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _Image.uvRect.size);
    }
}
