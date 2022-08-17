using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpinningDie spinningDie = this.GetComponentInChildren<SpinningDie>();
        DialogueBox box = this.GetComponentInChildren<DialogueBox>();
        box.AddButtonClickListener((delegate (Quaternion spin) {
            spinningDie.QueueSpin(spin);
        }));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
