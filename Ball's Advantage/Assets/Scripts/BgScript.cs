using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScript : MonoBehaviour
{
    // Start is called before the first frame update
    Camera bgCamera;
    SpriteRenderer spriteRenderer;
    float ratioX;
    float ratioY;
    int picInlength;
    int picInWidth;
    float cameraHeight;
    Vector2 cameraSize;
    Vector2 spriteSize;
    Vector2 scale;
    RectTransform rect;

    void Start()
    {
        bgCamera = GameObject.Find("BgCamera").GetComponent<Camera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        scale = transform.localScale;
        rect = GetComponent<RectTransform>();
        makeBackground();
    }

    void makeBackground()
    {
        cameraHeight = bgCamera.orthographicSize * 2;
        cameraSize = new Vector2(bgCamera.aspect * cameraHeight, cameraHeight);
        spriteSize = spriteRenderer.sprite.bounds.size;
        ratioX = cameraSize.x / spriteSize.x;
        ratioY = cameraSize.y / spriteSize.y;
        picInlength = (int)ratioX + 1;
        picInWidth = (int)ratioY + 1;
        if (cameraSize.x >= cameraSize.y)
        {
            scale *= ratioX;
        }
        else
        {
            scale *= ratioY;
        }
        transform.position = Vector2.zero;
        transform.localScale = scale;
        Vector3 setPos = new Vector3(rect.position.x, rect.position.y, 0);
        rect.localPosition = setPos;
    }

    void OnMouseDrag()
    {
        
    }
}
