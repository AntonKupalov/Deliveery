using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] 
    private RopeController _ropeController;
    [SerializeField] 
    private BoxController _box;
    [SerializeField]
    private Transform _finishPoint;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Rope"))
        {
            _ropeController.Stop();
            _box.DropDown(_finishPoint.position);
        }
    }
}