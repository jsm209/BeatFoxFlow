using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSlime : Enemy {

    public Transform groundDetection;
    public Transform wallDetection;

	// Update is called once per frame
	void Update () {
        
        if (BeatManager.valid) {
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f);
            RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.00001f);
            Move(step, groundInfo, wallInfo);
        }
        base.Update();
    }

    private void Move(float step, RaycastHit2D groundInfo, RaycastHit2D wallInfo) {
        gameObject.transform.Translate(Vector2.right * step);

        
        if (!groundInfo.collider) { // || wallInfo.collider
            if (facingRight) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                facingRight = false;
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                facingRight = true;
            }
            

            //Vector2 oldDirection = transform.position;
            //gameObject.transform.Translate(-oldDirection);
        }
    }
}
