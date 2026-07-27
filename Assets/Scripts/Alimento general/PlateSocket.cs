using UnityEngine;

public class PlateSocket : MonoBehaviour
{
    public TestManager testManager;
    
    private void OnTriggerEnter(Collider other)
    {
        PortionInfo info = other.GetComponent<PortionInfo>();

        if (info != null)
        {
            info.ShowInfo();

            if (testManager != null)
                testManager.PortionPlaced(info);
        }
    }
}