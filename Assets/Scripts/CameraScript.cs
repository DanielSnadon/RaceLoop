using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float zoom = 12.0f;
    public enum spectating {
        player,
        enemy,
        map
    }
    public spectating lookingAt;
    public GameObject player;
    public GameObject enemy;
    public GameObject map;

    void LateUpdate()
    {
        switch (lookingAt)
        {
            case (spectating.player):
                transform.position = player.transform.position + Vector3.up * zoom;
                break;
            case (spectating.enemy):
                transform.position = enemy.transform.position + Vector3.up * zoom;
                break;
            case (spectating.map):
                transform.position = map.transform.position + Vector3.up * zoom;
                break;
        }
    }
}
