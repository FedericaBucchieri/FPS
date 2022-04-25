using Unity.FPS.Gameplay;
using UnityEngine;

public class ObjectiveCheckpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ObjectiveMultipleReachPointsManager>().completeObjectivePoint(this.transform, other);
    }
}
