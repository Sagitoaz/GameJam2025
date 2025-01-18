using UnityEngine;

public class BarrelRotation : MonoBehaviour
{
    public Transform Pivot;
    public Transform Barrel;
    public Tower _tower;
    void Update()
    {
        if (_tower != null)
        {
            if (_tower._currentTarget != null)
            {
                Vector2 relative = _tower._currentTarget.transform.position - Pivot.position;
                float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
                Vector3 newRotation = new Vector3(0, 0, angle);
                Pivot.localRotation = Quaternion.Euler(newRotation);
                
            }
        }
    }
}
