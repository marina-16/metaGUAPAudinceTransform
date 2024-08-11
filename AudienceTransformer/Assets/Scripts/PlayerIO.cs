using UnityEngine;

public class PlayerIO : MonoBehaviour
{
    // �������� �������� �������
    [SerializeField] private float _scrollSpeed = 1;
    // ���� ����� (����� �������)
    [SerializeField] private GameObject _pauseMenu;

    // ��������� ��������
    [SerializeField] private Inventory _inventory;
    // ��������������� ��������
    [SerializeField] private Carrier _carrier;
    // ������������ ����� ��������
    [SerializeField] private Instancer _instancer;

    // ���� �����
    private bool _isPause = false;
    // ��������� �������
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

        // ���� ������ ������ ������ ����
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

        // ���� ������ ����� ������ ����
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

    // ����� �������
    public void SelectObject(int index)
    {
        // ���� ��������� �������� ������� � �� ��������� ������, �� ������
        if (_state == PlayerState.Carring && _carrier.IsDraging)
        {
            return;
        }

        _inventory.SetDragable(index);
        _instancer.Attach(_inventory.CurrentDragable);

        _state = PlayerState.Placing;
    }
}
