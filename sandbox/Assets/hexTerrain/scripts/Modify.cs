using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hexwork
{
    public class Modify : MonoBehaviour
    {

        Vector2 rot;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100))
                {
                    Terrain.SetBloc(hit, new HexBlocAir());
                }
            }

            rot = new Vector2(
                rot.x + Input.GetAxis("Mouse X") * 3,
                rot.y + Input.GetAxis("Mouse Y") * 3);

            transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rot.y, Vector3.left);

            transform.position += transform.forward * 2 * Input.GetAxis("Vertical");
            transform.position += transform.right * 2 * Input.GetAxis("Horizontal");
        }
    }
}

