using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    public InputField inputField;
    public float speed = 5f;
    public GameObject nullTargetMessage;

    private Transform target;

    private void Start() {
        inputField.onEndEdit.AddListener(SetTarget);
    }


    private void SetTarget(string input) {
        GameObject gameObject = GameObject.Find(input);
        if (gameObject != null) {
            target = gameObject.transform;
            nullTargetMessage.SetActive(false);
        } else {
            target = null;
            nullTargetMessage.SetActive(true);
        }
    }

    private void LateUpdate() {
        if (target != null) {
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
