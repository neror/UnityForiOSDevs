using UnityEngine;
using System.Collections;

namespace CubesAndStuff
{
	public class ThrowSphere : MonoBehaviour
	{
		public float throwingPower = 5f;
		
		private bool isHolding = false;
		private Color startingColor;
		private Plane dragPlane;

		#region Cached Component Properties

		private Transform mCachedTransform;
		private Transform cachedTransform
		{
			get
			{
				if(mCachedTransform == null) {
					mCachedTransform = GetComponent<Transform>();
				}
				return mCachedTransform;
			}
		}

		private Camera mCachedMainCamera;
		private Camera cachedMainCamera
		{
			get
			{
				if(mCachedMainCamera == null) {
					mCachedMainCamera = Camera.main;
				}
				return mCachedMainCamera;
			}
		}

		private Rigidbody mCachedRigidbody;
		private Rigidbody cachedRigidbody
		{
			get
			{
				if(mCachedRigidbody == null) {
					mCachedRigidbody = GetComponent<Rigidbody>();
				}
				return mCachedRigidbody;
			}
		}

		private Renderer mCachedRenderer;
		private Renderer cachedRenderer
		{
			get
			{
				if(mCachedRenderer == null) {
					mCachedRenderer = GetComponent<Renderer>();
				}
				return mCachedRenderer;
			}
		}
		
		#endregion
		
		void Start()
		{
			startingColor = cachedRenderer.material.color;
			cachedRigidbody.isKinematic = true;
		}

		void OnMouseDown()
		{
			isHolding = true;
			cachedRigidbody.isKinematic = true;
			dragPlane = new Plane(Vector3.back, cachedTransform.position);
		}
		
		void OnMouseDrag()
		{
			Vector3 mousePos = Input.mousePosition;
			Ray touchRay = cachedMainCamera.ScreenPointToRay(mousePos);
			float distance = 0f;
			if(dragPlane.Raycast(touchRay, out distance)) {
				Vector3 nextPos = touchRay.GetPoint(distance);
				cachedTransform.position = nextPos;
			}
			StartCoroutine(AnimateColor());
		}

		void OnMouseUp()
		{
			isHolding = false;
			cachedRigidbody.isKinematic = false;
			cachedRigidbody.AddForce(Vector3.forward * throwingPower, ForceMode.Impulse);
		}
		
		IEnumerator AnimateColor()
		{
			Color original = cachedRenderer.material.color;
			bool fadingToWhite = true;
			float time = 0f;
			
			while(isHolding) {
				time = Mathf.Clamp01(time + Time.deltaTime);

				if(fadingToWhite) {
					cachedRenderer.material.color = Color.Lerp(original, Color.white, time);
					if(time == 1f) {
						fadingToWhite = false;
						time = 0f;
					}
				} else {
					cachedRenderer.material.color = Color.Lerp(Color.white, original, time);
					if(time == 1f) {
						fadingToWhite = true;
						time = 0f;
					}
				}
				yield return null;
			}
			cachedRenderer.material.color = original;
		}
		
		void OnCollisionEnter(Collision collision)
		{
			if(collision.collider.gameObject.name.Equals("Cube")) {
				cachedRenderer.material.color = Color.green;
			}
		}
		
		void OnGameReset()
		{
			cachedRigidbody.isKinematic = true;
			cachedRenderer.material.color = startingColor;
		}
	}
}
