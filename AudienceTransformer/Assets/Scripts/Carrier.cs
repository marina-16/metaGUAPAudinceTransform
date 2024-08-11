using UnityEngine;

public class Carrier : MonoBehaviour
{
    // Точка, откуда смотрим
    [SerializeField] private Transform _head;
    // Класс-перетаскиватель объектов
    [SerializeField] private RaycastDrag _raycastDrag;
    // Длина луча
    [SerializeField] private float _rayLength = 5f;

    // Флаг перетаскивания, true, если текущий объект не null
    public bool IsDraging => _raycastDrag.CurrentDragable != null;

    // Вращение объекта, если он не null
    public void TryRotate(float angle)
    {
        if (_raycastDrag.CurrentDragable == null)
        {
            return;
        }
        _raycastDrag.CurrentDragable.Rotate(angle);
    }

    // Удаление объекта, если он не null
    public void TryRemove()
    {
        if (_raycastDrag.CurrentDragable == null)
        {
            return;
        }

        var dragable = _raycastDrag.CurrentDragable;

        _raycastDrag.Put();

        Destroy(dragable.gameObject);
    }

    // Попытка начать перетаскивание объекта, на который мы смотрим
    public void TryDrag()
    {
        if (_raycastDrag.CurrentDragable != null)
        {
            return;
        }

        if (Physics.Raycast(_head.position + _head.forward,
            _head.forward, out RaycastHit hit, _rayLength))
        {
            if (hit.collider.transform.root.gameObject.TryGetComponent<Dragable>(out var dragable))
            {
                // Если все ок, взяли объект
                _raycastDrag.Take(dragable);
            }
        }
    }

    // Попытка разместить объект
    public void TryDrop()
    {
        if (_raycastDrag.CurrentDragable == null 
            || !_raycastDrag.IsGrounded 
            || _raycastDrag.CurrentDragable.Collision)
        {
            return;
        }
        // Если все ок, размещаем
        _raycastDrag.Put();
    }
}
