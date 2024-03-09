using Assets.Scripts.G3;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Grid grid;

    void Start()
    {
        grid = GetComponentInParent<Grid>();
    }

    public void Fire(Vector3 position)
    {
        Debug.Log("Fire!");
        Vector3 dir = new()
        {
            x = -Mathf.Sin(Mathf.Deg2Rad * transform.localRotation.eulerAngles.z),
            y = Mathf.Cos(Mathf.Deg2Rad * transform.localRotation.eulerAngles.z)
        };
        if (Checkhit(position, dir))
        {
            Debug.Log("Target Hit!");
            grid.ReduceTarget();
        } 
        else
            Debug.Log("Target Missed!");
    }

    private bool Checkhit(Vector3 position, Vector3 direction)
    {
        position += direction;

        if (!grid.CheckBounds(position)) return false;

        var item = grid.ItemAt(new Vector3((int)position.x, (int)position.y, 0));
        if (item != null)
        {
            if (item.type == GridItem.ItemType.Target)
            {
                item.ActiveObject.SetActive(false);
                return true;
            }
            else if (item.type == GridItem.ItemType.Blocker) return false;
        }

        return Checkhit(position, direction);
    }
}
