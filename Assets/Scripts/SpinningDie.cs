
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningDie : MonoBehaviour
{

    private Rigidbody rigidBodyComponent;

    private Quaternion? desiredOrientation = null;

    private TMPro.TextMeshProUGUI text;

    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        this.rigidBodyComponent = this.GetComponentInChildren<Rigidbody>();
        this.text = this.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        this.changeHealth(3);
    }

    // Update is called once per frame
    void Update()
    {
        // Listen for inputs for rotating cube
        ((System.Action)( delegate {
            if (this.desiredOrientation.HasValue) {
                return;
            }
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
                this.QueueSpin(Quaternion.FromToRotation(
                    perspectiveSpin.Item1,
                    perspectiveSpin.Item2
                ));
            }
            return;
        }))();
    }

    void FixedUpdate() {
        // Smoothly rotate cube if in rotating mode
        if (this.desiredOrientation.HasValue) {
            if (Quaternion.Angle(rigidBodyComponent.transform.localRotation, this.desiredOrientation.Value) < 0.01f) {
                rigidBodyComponent.transform.localRotation = this.desiredOrientation.Value;
                this.desiredOrientation = null;
            } else {
                rigidBodyComponent.transform.localRotation = Quaternion.RotateTowards(rigidBodyComponent.transform.localRotation, this.desiredOrientation.Value, Time.fixedDeltaTime * 90 * 4);
            }
        }
    }

    public void QueueSpin(Quaternion perspectiveSpin) {
        this.changeHealth(this.health - 1);
        this.desiredOrientation = rigidBodyComponent.transform.localRotation * Quaternion.FromToRotation(
            Quaternion.Inverse(rigidBodyComponent.transform.localRotation) * Vector3.forward, 
            Quaternion.Inverse(rigidBodyComponent.transform.localRotation) * (perspectiveSpin* Vector3.forward)
        );
    }

    void changeHealth(int newHealth) {
        this.health = newHealth;
        this.text.SetText($"HP: {this.health}");
    }
}
