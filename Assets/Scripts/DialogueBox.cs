using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{

    UnityEngine.UI.Button button;

    List<System.Action<Quaternion>> buttonClickListeners = new List<System.Action<Quaternion>>();

    TMPro.TMP_Dropdown dropdown;

    internal class OptionDataWithId<T>: TMPro.TMP_Dropdown.OptionData {
        public T payload;

        public OptionDataWithId(T payload, string text) : base(text) { this.payload = payload; }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.button = this.GetComponentInChildren<UnityEngine.UI.Button>();
        this.button.onClick.AddListener(
            (delegate {
                TMPro.TMP_Dropdown.OptionData genericOption = this.dropdown.options[this.dropdown.value];
                if (!(genericOption is OptionDataWithId<Quaternion>)) {
                    return;
                }
                OptionDataWithId<Quaternion> option = (OptionDataWithId<Quaternion>) this.dropdown.options[this.dropdown.value];
                foreach (var action in this.buttonClickListeners) {
                    action(option.payload);
                }
            })
        );

        this.dropdown = this.GetComponentInChildren<TMPro.TMP_Dropdown>();
        this.dropdown.AddOptions(
            new List<TMPro.TMP_Dropdown.OptionData> {
                new OptionDataWithId<Quaternion>(Quaternion.FromToRotation(Vector3.back,Vector3.up), "Uppercut"),
                new OptionDataWithId<Quaternion>(Quaternion.FromToRotation(Vector3.back,Vector3.left), "Righthook"),
                new OptionDataWithId<Quaternion>(Quaternion.FromToRotation(Vector3.back,Vector3.right), "Lefthook")
            }
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddButtonClickListener(System.Action<Quaternion> action) {
        this.buttonClickListeners.Add(action);
    }
}
