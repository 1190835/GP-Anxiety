using UnityEngine;
using UnityEngine.EventSystems;

// Represents a fixed touch field for detecting touch input
public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Distance of touch movement

    public Vector2 TouchDist;

    // Previous position of touch

    public Vector2 PointerOld;

    // Pointer ID for touch input
    
    protected int PointerId;

    // Indicates if the touch is pressed
    
    public bool Pressed;

    // Update is called once per frame
    void Update()
    {
        // Update touch distance if pressed
        if (Pressed)
        {
            Debug.Log(PointerId);
            Debug.Log(Input.touches.Length);
            Debug.Log(Input.touchCount);
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            // Reset touch distance
            TouchDist = new Vector2();
        }
    }

    // Called when a pointer is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down"+eventData.pointerId);
        // Set pressed to true and update pointer information
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
    }

    // Called when a pointer is released
    public void OnPointerUp(PointerEventData eventData)
    {
        // Set pressed to false
        Pressed = false;
    }
}
