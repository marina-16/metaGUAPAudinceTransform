using UnityEngine;

public class Dragable : MonoBehaviour
{
    // Флаг столкновения 
    public bool Collision {  get; private set; }

    // Начать перетаскивание
    public void Drag()
    {
        // Меняем на слой, игнорирующий лучи
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        // Меняем всем дочерним объектам слой
        foreach(Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }

    public void Drop()
    {
        // Меняем на стандартный слой
        gameObject.layer = LayerMask.NameToLayer("Default");

        // Меняем всем дочерним объектам слой
        foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    // Поворот объекта
    public void Rotate(float angle)
    {
        transform.localEulerAngles += Vector3.up * angle;
    }

    // Если триггера касаются
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Dragable>(out var _))
        {
            Collision = true;
        }
    }

    // Если триггера не касаются
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Dragable>(out var _))
        {
            Collision = false;
        }
    }
}
