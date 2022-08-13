
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningDie : MonoBehaviour
{

    private Rigidbody rigidBodyComponent;

    private Quaternion? desiredOrientation = null;

    // Start is called before the first frame update
    void Start()
    {
        this.rigidBodyComponent = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.desiredOrientation.HasValue) {
            System.Tuple<Vector3, Vector3> perspectiveSpin = null;
            if(Input.GetKeyDown(KeyCode.DownArrow)) {
                perspectiveSpin = System.Tuple.Create(Vector3.back, Vector3.down);
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                perspectiveSpin = System.Tuple.Create(Vector3.back, Vector3.up);
            } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                perspectiveSpin = System.Tuple.Create(Vector3.back, Vector3.left);
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                perspectiveSpin = System.Tuple.Create(Vector3.back, Vector3.right);
            }
            if (perspectiveSpin != null) {
                this.desiredOrientation = this.transform.rotation * Quaternion.FromToRotation(
                    Quaternion.Inverse(this.transform.rotation) * perspectiveSpin.Item1, 
                    Quaternion.Inverse(this.transform.rotation) * perspectiveSpin.Item2
                );
            }
        }
    }

    void FixedUpdate() {
        if (this.desiredOrientation.HasValue) {
            if (Quaternion.Angle(this.transform.rotation, this.desiredOrientation.Value) < 0.01f) {
                this.transform.rotation = this.desiredOrientation.Value;
                this.desiredOrientation = null;
            } else {
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, this.desiredOrientation.Value, Time.fixedDeltaTime * 90 * 4);
            }
        }
    }
}
