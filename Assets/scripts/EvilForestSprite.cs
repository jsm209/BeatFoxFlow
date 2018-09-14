using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilForestSprite : Enemy {

    void Update() {
        if (BeatManager.valid && foundPlayer) {
            Move(step);
        }
        base.Update();
    }

    private void Move(float step) {
        gameObject.GetComponent<Transform>().position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, step);
        
        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(target.GetComponent<Transform>().position.x 
        //                                                            - this.GetComponent<Transform>().position.x, 
        //                                                            target.GetComponent<Transform>().position.y 
        //                                                            - this.GetComponent<Transform>().position.y));
            //rigidbody2D = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, step);
    }
}
