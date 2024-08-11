using UnityEngine;

public class RaycastDrag : MonoBehaviour
{
    // ����� ����
    [SerializeField] private float _rayLength;
    // ������� ������, ������� �������������
    [SerializeField] private Dragable _dragable;
    // �����, ������ ������ ���
    [SerializeField] private Transform _head;

    // �������� �� ������ �����������
    public bool IsGrounded { get; private set; }    

    public Dragable CurrentDragable => _dragable;

    // ����� ��������������� ������
    public void Take(Dragable dragable)
    {
        _dragable = dragable;
        _dragable.Drag();
    }

    // �������� ��������������� ������
    public void Put()
    {
        _dragable.Drop();
        _dragable = null;
    }

    private void Update()
    {
        // ���� ��������������� ������ - ��� ������, �� ������ �� ����
        if (_dragable == null)
        {
            return;
        }

        // ������ ��� �� ����� ������� � �������, ���� �������, ��������� ������������ ����
        // � �������� ������ � hit. ���� ������������ ���������� 
        if (Physics.Raycast(_head.position + _head.forward,
            _head.forward, out RaycastHit hit, _rayLength))
        {
            // ���������� ������ � ����� ������������
            _dragable.transform.position = hit.point;
            // ������ �������� �����������
            IsGrounded = true;
        }
        else // ���� ������������ �� ���������� 
        {
            // ���������� ������ � ����� ����
            _dragable.transform.position = _head.position + _head.forward * _rayLength;
            // ������ �� �������� �����������
            IsGrounded = false;
        }
    }
}
