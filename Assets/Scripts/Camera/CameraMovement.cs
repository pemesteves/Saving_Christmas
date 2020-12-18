using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private float minX = -15f, maxX = 15f; //Test values -> must be changed according to the level

    private float cameraYPos, cameraZPos;
    private float halfScreenWidth;

    private void Start()
    {
        cameraYPos = transform.position.y;
        cameraZPos = transform.position.z;

        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(Vector2.one);
        halfScreenWidth = edgeVector.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x - halfScreenWidth > minX && player.transform.position.x + halfScreenWidth < maxX)
        {
            transform.position = new Vector3(player.transform.position.x, cameraYPos, cameraZPos);
        }
    }
}
