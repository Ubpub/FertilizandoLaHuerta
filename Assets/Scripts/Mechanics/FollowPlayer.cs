using Unity.Cinemachine;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
    [SerializeField] private CinemachineCamera cinemachineCamera;

    public void Start()
    {
        Transform player = GameObject.Find("Zanahorio").transform;

        if (player == null)
        {
            Debug.LogWarning("No se encontró a zanahorio");
            return;
        }

        cinemachineCamera.Follow = player;
    }
}
