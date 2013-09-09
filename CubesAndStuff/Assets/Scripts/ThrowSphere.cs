using UnityEngine;
using System.Collections;

public class ThrowSphere : MonoBehaviour {
	public float throwingPower = 5f;
	
	private bool isHolding = false;
	private Color startingColor;

	void Start()
	{
		startingColor = renderer.material.color;
		rigidbody.isKinematic = true;
	}
	
	void OnMouseDown()
	{
		isHolding = true;
		rigidbody.isKinematic = true;
	}
	
	void OnMouseDrag()
	{
		float sphereDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = sphereDistance;
		Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
		newPos.z = transform.position.z;
		transform.position = newPos;
		StartCoroutine(AnimateColor());
	}

	void OnMouseUp()
	{
		isHolding = false;
		rigidbody.isKinematic = false;
		rigidbody.AddForce(Vector3.forward * throwingPower, ForceMode.Impulse);
	}
	
	IEnumerator AnimateColor()
	{
		Color original = renderer.material.color;
		bool fadingToWhite = true;
		float time = 0f;
		
		while(isHolding) {
			time = Mathf.Clamp01(time + Time.deltaTime);

			if(fadingToWhite) {
				renderer.material.color = Color.Lerp(original, Color.white, time);
				if(time == 1f) {
					fadingToWhite = false;
					time = 0f;
				}
			} else {
				renderer.material.color = Color.Lerp(Color.white, original, time);
				if(time == 1f) {
					fadingToWhite = true;
					time = 0f;
				}
			}
			yield return null;
		}
		renderer.material.color = original;
	}
	
//	void Update()
//	{
//		if(Input.GetMouseButtonDown(0)) {
//			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit;
//			if(Physics.Raycast(mouseRay, out hit)) {
//				if(hit.rigidbody == rigidbody) {
//					isHolding = true;
//					return;
//				}
//			}
//		}
//		
//		if(Input.GetMouseButton(0) && isHolding) {
//			float sphereDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
//			Vector3 mousePos = Input.mousePosition;
//			mousePos.z = sphereDistance;
//			Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
//			newPos.z = transform.position.z;
//			transform.position = newPos;
//			return;
//		}
		
//		if(Input.GetMouseButtonUp(0) && isHolding) {
//			isHolding = false;
//			rigidbody.isKinematic = false;
//			rigidbody.AddForce(Vector3.forward * throwingPower, ForceMode.Impulse);
//		}
//	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.gameObject.name.Equals("Cube")) {
			renderer.material.color = Color.green;
		}
	}
	
	void OnReset()
	{
		rigidbody.isKinematic = true;
		renderer.material.color = startingColor;
	}
}
