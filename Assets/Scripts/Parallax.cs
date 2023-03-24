using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] 
    private Transform cameraTransform;

    [SerializeField] 
    private float moveSpeed;

    private void Update()
    {
        transform.Translate(-1 * moveSpeed * Time.deltaTime, 0f, 0f);

        if (cameraTransform.position.x >= transform.position.x + 39.6f)
        {
            transform.position = new Vector2(cameraTransform.position.x + 39.6f, transform.position.y);
        }
    }
}
