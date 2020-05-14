using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    Camera cam;
    [SerializeField] [Range(-20, 20)]
    float CameraOffset;
    [SerializeField]
    float CameraDistance;
    [SerializeField] [Range(0, 20)]
    float CameraSpeedFast;
    [SerializeField]
    [Range(0, 20)]
    float CameraSpeedSlow;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerTooCloseToEdge())
        {
            transform.position += Vector3ToPlanar(Vector3ToPlanar(PlayerLocationPlusOffset()) - (Vector3ToPlanar(transform.position))) * Time.deltaTime * CameraSpeedFast;
        }
        else
        {
            transform.position += Vector3ToPlanar(Vector3ToPlanar(PlayerLocationPlusOffset()) - (Vector3ToPlanar(transform.position))) * Time.deltaTime * CameraSpeedSlow;

        }

    }

    Vector3 PlanarCameraTargetMiddle()
    {
        Vector3 temp = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane));
        return Vector3ToPlanar(temp);
    }

    bool PlayerTooCloseToEdge()
    {
        Vector3 playerLocOnScreen = cam.WorldToScreenPoint(player.transform.position);

        return (playerLocOnScreen.x < CameraDistance || playerLocOnScreen.x > Screen.width - CameraDistance || playerLocOnScreen.y < CameraDistance || playerLocOnScreen.y > Screen.height - CameraDistance);
    }

    Vector3 Vector3ToPlanar(Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }

    Vector3 PlayerLocationPlusOffset()
    {
        return new Vector3(player.transform.position.x + CameraOffset, player.transform.position.y, player.transform.position.z + CameraOffset);
    }

}
