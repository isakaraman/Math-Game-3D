using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler,  IPointerDownHandler
{
    private Image backgroundimage;
    private Image joystickimage;
    private Vector3 inputVector;

    void Start()
    {
        backgroundimage = GetComponent<Image>();
        joystickimage = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundimage.rectTransform
            ,ped.position
            ,ped.pressEventCamera
            ,out pos))
        {
            pos.x = (pos.x / backgroundimage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / backgroundimage.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
            inputVector=(inputVector.magnitude>1.0f)?inputVector.normalized:inputVector;

            //Move Joystick Ýmage

            joystickimage.rectTransform.anchoredPosition =
                new Vector3(inputVector.x * (backgroundimage.rectTransform.sizeDelta.x /3f)
                , inputVector.z * (backgroundimage.rectTransform.sizeDelta.y / 3f));
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickimage.rectTransform.anchoredPosition = Vector3.zero;
    }
    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }
    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }
}
