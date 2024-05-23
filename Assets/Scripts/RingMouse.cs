using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class RingMouse : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Vector2 moveDir;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = 0;
        float mouseY = 0;

        /*
        if(Mouse.current != null)
        {
            mouseX = Mouse.current.delta.ReadValue().x;
            mouseY = Mouse.current.delta.ReadValue().y;
        }

        if (Gamepad.current != null)
        {
            mouseX = Gamepad.current.rightStick.ReadValue().x;
            mouseY = Gamepad.current.rightStick.ReadValue().y;
        }

        //float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");
        */
        if (Touchscreen.current.touches.Count == 0)
            return;

        if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[0].touchId.ReadValue()))
        {
            if (Touchscreen.current.touches.Count > 1 && Touchscreen.current.touches[1].isInProgress)
            {
                if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[1].touchId.ReadValue()))
                    return;

                Vector2 touchDeltaPosition = Touchscreen.current.touches[1].delta.ReadValue();
                mouseX = touchDeltaPosition.x;
                mouseY = touchDeltaPosition.y;
            }
        }
        else
        {
            if (Touchscreen.current.touches.Count > 0 && Touchscreen.current.touches[0].isInProgress)
            {
                if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[0].touchId.ReadValue()))
                    return;

                Vector2 touchDeltaPosition = Touchscreen.current.touches[0].delta.ReadValue();
                mouseX = touchDeltaPosition.x;
                mouseY = touchDeltaPosition.y;
            }

        }

        mouseX *= mouseSensitivity;
        mouseY *= mouseSensitivity;

        moveDir=new Vector2(mouseX,mouseY);
    }
}
