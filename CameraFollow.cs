using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Controller2D target;
    public float verticalOffset;
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    public Vector2 focusAreaSize;

    GameObject player;

    FocusArea focusArea;

    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    bool lookAheadStopped;

    float timer = 0;
    bool timerReached = false;

    public Vector3 playerStartPosition;

    void Start()
    {
        transform.position = playerStartPosition;
        player = GameObject.Find("Character");
        focusArea = new FocusArea(target.collide.bounds, focusAreaSize);
    }

    void LateUpdate()
    {
        if (!timerReached)
        {
            timer += Time.deltaTime;
        }
            
        if (!timerReached && timer > 1)
        {
            timerReached = true;
        }

        if (timerReached)
        {
            if (!player.GetComponent<Player>().died)
            {
                focusArea.Update(target.collide.bounds);

                Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffset;

                if (focusArea.velocity.x != 0)
                {
                    lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
                    if (Mathf.Sign(target.playerInput.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInput.x != 0)
                    {
                        lookAheadStopped = false;
                        targetLookAheadX = lookAheadDstX * lookAheadDirX;
                    }
                    else
                    {
                        if (!lookAheadStopped)
                        {
                            lookAheadStopped = true;
                            targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
                        }
                    }
                }

                currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

                focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
                focusPosition += Vector2.right * currentLookAheadX;

                transform.position = (Vector3)focusPosition + Vector3.forward * -10;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .5f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
    }

    struct FocusArea
    {
        public Vector2 centre;
        public Vector2 velocity;
        public float left, right;
        public float top, bottom;

        public FocusArea (Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update (Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
}
