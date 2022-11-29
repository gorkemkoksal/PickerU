using UnityEngine;

public class Controls : MonoBehaviour
{
    Touch touch;
    public Vector3 HorizontalVector { get; private set; }
    void Update()
    {
        VerticalMovement();
    }
    private void VerticalMovement()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Stationary)
            {
                HorizontalVector = Vector3.zero;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (touch.deltaPosition.x > 0 && transform.position.x < 4.5f)
                {
                    HorizontalVector = Vector3.right;
                }
                else if (touch.deltaPosition.x < 0 && transform.position.x>-1.5f)
                {
                    HorizontalVector = Vector3.left;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                HorizontalVector = Vector3.zero;
            }
        }

    }
}
