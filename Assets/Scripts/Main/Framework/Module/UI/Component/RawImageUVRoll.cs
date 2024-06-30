using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class RawImageUVRoll : MonoBehaviour
{
    private RawImage rawImg;

    public float speed_x = 0.1f;
    public float speed_y = 0.1f;

    public void Start()
    {
        rawImg = GetComponent<RawImage>();
    }

    public void Update()
    {
        if (rawImg == null)
            return;
        float x = rawImg.uvRect.x;
        float y = rawImg.uvRect.y;
        float width = rawImg.uvRect.width;
        float height = rawImg.uvRect.height;
        x -= speed_x * Time.deltaTime;
        y -= speed_y * Time.deltaTime;
        rawImg.uvRect = new Rect(x, y, width, height);
    }
}