using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SpawnerInput : MonoBehaviour
{
    public ObjectSpawner spawner;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor;

    void Update()
    {
        if (rayInteractor.TryGetHitInfo(out Vector3 hitPos, out Vector3 hitNormal, out int _, out bool isValid) && isValid)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // reemplaza con tu input preferido
            {
                spawner.SpawnObject(hitPos, hitNormal);
            }
        }
    }
}
