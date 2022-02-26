using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PlayerController _playerController;

    public void SetReferences()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _playerController.SetDirectionMove(Vector3.up);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        _playerController.SetDirectionMove(Vector3.down);
    }
}
