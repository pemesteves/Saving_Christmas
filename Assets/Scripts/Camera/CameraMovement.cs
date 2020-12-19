using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private float minX = -14f, maxX = 118.5f;

    [SerializeField]
    private float minY = 4.5f, maxY = 9f; 

    private float cameraZPos;
    private float halfScreenWidth;

    private void Start()
    {
        cameraZPos = transform.position.z;

        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(Vector2.one);
        halfScreenWidth = edgeVector.x;
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (player.transform.position.x - halfScreenWidth > minX && player.transform.position.x + halfScreenWidth < maxX)
        {
            x = player.transform.position.x;
        }

        if(player.transform.position.y > minY && player.transform.position.y < maxY)
        {
            y = player.transform.position.y;
        }
           
        transform.position = new Vector3(x, y, cameraZPos);
    }
}
