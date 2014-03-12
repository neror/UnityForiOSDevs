using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MartianCraft.Common
{
	public class CameraFacingBillboard : MonoBehaviour
	{
		[SerializeField] private Camera targetCamera;
		public Camera TargetCamera
		{
			get
			{
				if(targetCamera == null) {
					targetCamera = Camera.main;
				}
				return targetCamera;
			}
			set
			{
				mCameraTransform = null;
				targetCamera = value;
			}
		}

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

		private Transform mCameraTransform;
		private Transform cameraTransform
		{
			get
			{
				if(mCameraTransform == null && TargetCamera != null) {
					mCameraTransform = TargetCamera.transform;
				}
				return mCameraTransform;
			}
		}

		#endregion

		void LateUpdate()
		{
			FaceCamera();
		}

		[ContextMenu("Align To Face Camera")]
		void FaceCamera()
		{
			cachedTransform.LookAt(cachedTransform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
		}

#if UNITY_EDITOR
		void OnValidate()
		{
			TargetCamera = targetCamera;
			FaceCamera();
		}
#endif
	}
}