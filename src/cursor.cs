using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursor : MonoBehaviour
{
    public float sensitivity = 1f;
    private RectTransform canvasRect;
    private RawImage cursorraw;

    private void Start()
    {
        cursorraw = GetComponent<RawImage>();
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        cursorraw.color = utils.HexToRGBVector3(PlayerPrefs.GetString("cursorcolor", "#ffffff"));
    }

    private void Update()
    {
        if (!globalvars.paused && !globalvars.dead)
        {
            if (Cursor.visible)
            {
                Cursor.visible = false;
            }
        }
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        
        if (mouseX != 0 || mouseY != 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Vector3 move = new (mouseX * sensitivity, mouseY * sensitivity, 0f);
            transform.Translate(move);
        
            Vector3 clampedPosition = transform.localPosition;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -canvasRect.rect.width / 2f, canvasRect.rect.width / 2f);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, -canvasRect.rect.height / 2f, canvasRect.rect.height / 2f);
            transform.localPosition = clampedPosition;
        }
        else if (!globalvars.paused || !globalvars.dead)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
