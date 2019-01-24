using UnityEngine;

public class CameraInput : MonoBehaviour
{
	public float MainSpeed = 100.0f; //regular speed
	public float ShiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
	public float MaxShift = 1000.0f; //Maximum speed when holdin gshift
	public float CamSens = 0.25f; //How sensitive it with mouse
	private Vector3 m_LastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
	private float m_TotalRun = 1.0f;

	private void Update()
	{
		m_LastMouse = Input.mousePosition - m_LastMouse;
		m_LastMouse = new Vector3(-m_LastMouse.y * CamSens, m_LastMouse.x * CamSens, 0);
		m_LastMouse = new Vector3(transform.eulerAngles.x + m_LastMouse.x, transform.eulerAngles.y + m_LastMouse.y, 0);
		transform.eulerAngles = m_LastMouse;
		m_LastMouse = Input.mousePosition;
		//Mouse  camera angle done.  

		//Keyboard commands
		Vector3 p = GetBaseInput();
		if (Input.GetKey(KeyCode.LeftShift))
		{
			m_TotalRun += Time.deltaTime;
			p = p * m_TotalRun * ShiftAdd;
			p.x = Mathf.Clamp(p.x, -MaxShift, MaxShift);
			p.y = Mathf.Clamp(p.y, -MaxShift, MaxShift);
			p.z = Mathf.Clamp(p.z, -MaxShift, MaxShift);
		}
		else
		{
			m_TotalRun = Mathf.Clamp(m_TotalRun * 0.5f, 1f, 1000f);
			p = p * MainSpeed;
		}

		p = p * Time.deltaTime;
		Vector3 newPosition = transform.position;
		if (Input.GetKey(KeyCode.Space))
		{ //If player wants to move on X and Z axis only
			transform.Translate(p);
			newPosition.x = transform.position.x;
			newPosition.z = transform.position.z;
			transform.position = newPosition;
		}
		else
		{
			transform.Translate(p);
		}

	}

	private static Vector3 GetBaseInput()
	{ //returns the basic values, if it's 0 than it's not active.
		Vector3 vel = new Vector3();
		if (Input.GetKey(KeyCode.W))
		{
			vel += new Vector3(0, 0, 1);
		}
		if (Input.GetKey(KeyCode.S))
		{
			vel += new Vector3(0, 0, -1);
		}
		if (Input.GetKey(KeyCode.A))
		{
			vel += new Vector3(-1, 0, 0);
		}
		if (Input.GetKey(KeyCode.D))
		{
			vel += new Vector3(1, 0, 0);
		}
		return vel;
	}
}
