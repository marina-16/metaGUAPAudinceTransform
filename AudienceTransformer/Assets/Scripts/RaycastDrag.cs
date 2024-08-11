using UnityEngine;

public class RaycastDrag : MonoBehaviour
{
    // Длина луча
    [SerializeField] private float _rayLength;
    // Текущий объект, который перетаскиваем
    [SerializeField] private Dragable _dragable;
    // Точка, откуда строим луч
    [SerializeField] private Transform _head;

    // Касается ли объект поверхности
    public bool IsGrounded { get; private set; }    

    public Dragable CurrentDragable => _dragable;

    // Взять перетаскиваемый объект
    public void Take(Dragable dragable)
    {
        _dragable = dragable;
        _dragable.Drag();
    }

    // Положить перетаскиваемый объект
    public void Put()
    {
        _dragable.Drop();
        _dragable = null;
    }

    private void Update()
    {
        // Если перетаскиваемый объект - это ничего, то дальше не идем
        if (_dragable == null)
        {
            return;
        }

        // Строим луч от нашей позиции в сторону, куда смотрим, результат столкновения луча
        // с объектом кладем в hit. Если столкновение обнаружено 
        if (Physics.Raycast(_head.position + _head.forward,
            _head.forward, out RaycastHit hit, _rayLength))
        {
            // Перемещаем объект в точку столкновения
            _dragable.transform.position = hit.point;
            // Объект касается поверхности
            IsGrounded = true;
        }
        else // Если столкновение не обнаружено 
        {
            // Перемещаем объект в конец луча
            _dragable.transform.position = _head.position + _head.forward * _rayLength;
            // Объект не касается поверхности
            IsGrounded = false;
        }
    }
}
