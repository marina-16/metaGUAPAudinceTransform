using UnityEngine;

public class Instancer : MonoBehaviour
{
    // Размещаемый объект
    [SerializeField] private Dragable _dragable;
    // Класс-перетаскиватель объектов
    [SerializeField] private RaycastDrag _raycastDrag;

    // Вытащить объект на сцену
    public void Attach(Dragable dragable)
    {
        _dragable = Instantiate(dragable);
        _raycastDrag.Take(_dragable);
    }

    // Применить размещение объекта
    public void Apply()
    {
        _raycastDrag.Put();

        _dragable = null;
    }

    // Отменить размещение объекта
    public void Cancel()
    {
        _raycastDrag.Put();

        Destroy(_dragable.gameObject);

        _dragable = null;
    }
}
