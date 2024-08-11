using UnityEngine;

public class PlayerIO : MonoBehaviour
{
    // Скорость вращения объекта
    [SerializeField] private float _scrollSpeed = 1;
    // Меню паузы (выбор объекта)
    [SerializeField] private GameObject _pauseMenu;

    // Инвентарь объектов
    [SerializeField] private Inventory _inventory;
    // Перетаскиватель объектов
    [SerializeField] private Carrier _carrier;
    // Разместитель новых объектов
    [SerializeField] private Instancer _instancer;

    // Флаг паузы
    private bool _isPause = false;
    // Состояние аватара
    private PlayerState _state = PlayerState.Carring;

    private void Update()
    {
        _pauseMenu.SetActive(_isPause);

        Cursor.lockState = _isPause ?
            CursorLockMode.None :
            CursorLockMode.Locked;

        Cursor.visible = _isPause;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPause = !_isPause;
        }

        if (_isPause)
        {
            return;
        }

        // Если нажата правая кнопка мыши
        if (Input.GetMouseButtonDown(1))
        {
            switch (_state)
            {
                case PlayerState.Carring:
                    _carrier.TryRemove();
                    break;
                case PlayerState.Placing:
                    _instancer.Cancel();
                    _state = PlayerState.Carring;
                    break;
            }
        }

        // Если нажата левая кнопка мыши
        if (Input.GetMouseButtonDown(0))
        {
            switch (_state)
            {
                case PlayerState.Carring:
                    if (_carrier.IsDraging)
                    {
                        _carrier.TryDrop();

                    }
                    else
                    {
                        _carrier.TryDrag();
                    }
                    break;
                case PlayerState.Placing:
                    _instancer.Apply();
                    _state = PlayerState.Carring;
                    break;
            }

        }

        _carrier.TryRotate(_scrollSpeed * Input.mouseScrollDelta.y);
    }

    // Выбор объекта
    public void SelectObject(int index)
    {
        // Если состояние переноса объекта и мы переносим объект, то отмена
        if (_state == PlayerState.Carring && _carrier.IsDraging)
        {
            return;
        }

        _inventory.SetDragable(index);
        _instancer.Attach(_inventory.CurrentDragable);

        _state = PlayerState.Placing;
    }
}
