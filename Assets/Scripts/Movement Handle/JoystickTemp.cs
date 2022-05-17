using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class JoystickTemp : MonoBehaviour
{
    [SerializeField] private RectTransform _stick;
    [SerializeField] private RectTransform _backGround;
    [SerializeField] private float _radius = 100f;

    private int _currentFingerId = -1;
    private Vector2 _oldValue;

    public Vector2 Value { get; private set; }
    public Vector2 Delta { get; private set; }
    public Vector2 OldValue => _oldValue;

   // public event UnityAction Pressed;
   // public event UnityAction Released;


    private void OnValidate()
    {
        _backGround.sizeDelta = new Vector2(_radius, _radius) * 2f;
    }

    private void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if(Input.GetTouch(i).fingerId == _currentFingerId)
            {
                _stick.position = Input.GetTouch(i).position;
                Value = ((_stick.position - _backGround.position) / _radius).normalized;
                Delta = Value - _oldValue;
                _oldValue = Value;
            }
        }
    }

    public void Down(PointerEventData eventData)
    {
        _stick.gameObject.SetActive(true);
        _stick.position = eventData.position;
        _backGround.gameObject.SetActive(true);
        _backGround.position = eventData.position;
        _currentFingerId = eventData.pointerId;
        _oldValue = Vector2.zero;

      //  Pressed?.Invoke();
    }

    public void Up(PointerEventData eventData)
    {
        _stick.gameObject.SetActive(false);
        _backGround.gameObject.SetActive(false);
        _currentFingerId = -1;
        Value = Vector2.zero;
        Delta = Vector2.zero;

       // Released?.Invoke();
    }
}
