/*
	Author: Mark Toledo
	Description: Handles the mechanic to transition between differnt menu states
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YeBoisFramework.StateMachine;
namespace YeBoisFramework.UI
{
	public class MenuStateMachine : BaseStateMachine
	{
		#region Properties and Fields
		private Dictionary<MenuStateName,MenuCanvasGroup> menuDictionary = new Dictionary<MenuStateName, MenuCanvasGroup>();
		private MenuCanvasGroup currentMenuState = null;
		#endregion

		#region  Custom Methods
		protected override void InitializeStates()
		{
			menuDictionary = new Dictionary<MenuStateName, MenuCanvasGroup>();
			MenuCanvasGroup[] menuCanvasCroups = GetComponentsInChildren<MenuCanvasGroup>();
			for(int i = 0; i < menuCanvasCroups.Length; i++)
			{
				menuDictionary.Add(menuCanvasCroups[i].stateName, menuCanvasCroups[i]);
				menuCanvasCroups[i].canvasGroup.alpha = 0.0f;
				menuCanvasCroups[i].gameObject.SetActive(false);
			}
		}

		public override void RefreshStateMachine(int trigger) 
		{
			if(currentMenuState != null) 
			{
				currentMenuState.OnStateExit();
				currentMenuState.canvasGroup.alpha = 0.0f;
				currentMenuState.gameObject.SetActive(false);
			}

			bool sucessful= menuDictionary.TryGetValue((MenuStateName) trigger, out currentMenuState);
			if(sucessful)
			{
				currentMenuState.canvasGroup.alpha = 1.0f;
				currentMenuState.gameObject.SetActive(true);
				currentMenuState.OnStateEnter();
			}
			else
			{
				Debug.LogError("MenuState not found");
			}
		}
		#endregion
	}
}
