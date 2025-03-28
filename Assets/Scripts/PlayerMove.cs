using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _oldMousePositionX;
    [SerializeField] private float _eulerY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { // чтобы не было рывков в начале кадра
            _oldMousePositionX = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 newPos = transform.position + _speed * Time.deltaTime * transform.forward;
            newPos.x = Mathf.Clamp(newPos.x, -2.5f, 2.5f);   // ограничение перемещения по x
            transform.position = newPos;

            float deltaX = Input.mousePosition.x - _oldMousePositionX;
            _oldMousePositionX = Input.mousePosition.x;

            _eulerY += deltaX;
            _eulerY = Mathf.Clamp(_eulerY, -70, 70);  // ограничения угла поворота, чтобы назад не вертелся
            transform.eulerAngles = new Vector3(0, _eulerY, 0);

        }
    }
}