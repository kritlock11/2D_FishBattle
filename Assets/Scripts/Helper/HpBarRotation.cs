using UnityEngine;

public class HpBarRotation : MonoBehaviour
{
    RectTransform Canvas;
    Transform Parent;
    void Start()
    {
        Canvas = GetComponent<RectTransform>();
        Parent = transform.parent. GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Parent.localScale.x == -1)
        {

        Canvas.rotation = Quaternion.Euler(0,0,-180);
        }
        else
        {
            Canvas.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
