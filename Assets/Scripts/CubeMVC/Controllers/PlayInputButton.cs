using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayInputButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Button _button;

    private System.Action _onBeginDrag;
    private System.Action<Vector2> _onDragEvent;
    private System.Action _onEndEvent;

    private bool _isDrag;

    public void SetubButton(Action OnClick, Action<Vector2> OnButtonDown, Action OnButtonUp)
    {
        _onBeginDrag = OnClick;
        _onDragEvent = OnButtonDown;
        _onEndEvent = OnButtonUp;

        //_button.onClick.AddListener(OnFastClick);
    }

    private void OnFastClick()
    {
        if (!_isDrag)
        {
            _onBeginDrag?.Invoke();

            _onEndEvent?.Invoke();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _onBeginDrag?.Invoke();

        _isDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _onDragEvent?.Invoke(eventData.position);

        _isDrag = true;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        _onEndEvent?.Invoke();

        _isDrag = false;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
