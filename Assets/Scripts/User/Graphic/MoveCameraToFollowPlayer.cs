using System.Collections;
using UnityEngine;

public class MoveCameraToFollowPlayer : MonoBehaviour {
    [SerializeField] private GameObject player;
    void Update () {
        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}