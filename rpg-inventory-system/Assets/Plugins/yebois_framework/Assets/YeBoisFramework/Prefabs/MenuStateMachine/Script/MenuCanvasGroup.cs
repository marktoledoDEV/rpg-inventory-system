/*
	Author: Mark Toledo
	Description: MenuCanvasGroup handles the specific of each individual menu
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YeBoisFramework.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class MenuCanvasGroup : MonoBehaviour 
	{
		#region Properties and Fields
		public MenuStateName stateName;
		#endregion

		#region Components
		public CanvasGroup canvasGroup;
		#endregion

		#region Unity Methods
		private void Awake()
		{
			canvasGroup = GetComponent<CanvasGroup>();
		}

		private void Update()
		{
			OnStateUpdate();
		}
		#endregion

		#region Custom Methods
		public virtual void OnStateEnter() { }
		protected virtual void OnStateUpdate() { }
		public virtual void OnStateExit() { }
		#endregion
	}
}
