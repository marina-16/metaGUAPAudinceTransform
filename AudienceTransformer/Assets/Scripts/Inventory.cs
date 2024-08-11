using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Dragable[] _dragableList;

    private int _currentDragableIndex = 0;

    public Dragable CurrentDragable => _dragableList[_currentDragableIndex];

    public void SetDragable(int index)
    {
        _currentDragableIndex = index;
    }
}
