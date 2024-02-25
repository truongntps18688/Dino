using UnityEngine;

public class WeaponMovement : MonoBehaviour
{

Vector3 mousePos;
Vector3 weaponPos;
float angle;

void Update()
	{
		mousePos = Input.mousePosition;
		mousePos.z = 5.23f; //The distance between the camera and object
		weaponPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - weaponPos.x;
		mousePos.y = mousePos.y - weaponPos.y;
		angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		if(angle < -90 || angle > 90)
        {
			transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
        }
        else
        {
			transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        }
	}

    private void OnGUI()
    {
		//GUI.Label(new Rect(25, 25, 200, 40), "Angle Between Objects" + angle);
		GUI.Label(new Rect(0, 0, 100, 100), "FPS" + (int)(1.0f / Time.smoothDeltaTime));
	}

}
