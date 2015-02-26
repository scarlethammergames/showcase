using UnityEngine;
using System.Collections;

public class MainMenu2 : MonoBehaviour {

	private Animator _animator;
	private CanvasGroup _canvasGroup;

	public bool IsOpen {
		get { return _animator.GetBool ("IsOpen"); }
		set { _animator.SetBool ("IsOpen", value); }
	}

	public void Awake() {
		_animator = GetComponent<Animator> ();
		_canvasGroup = GetComponent<CanvasGroup> ();

		//Moves menu to center of the screen when game is playing
		var rect = GetComponent<RectTransform> ();
		rect.offsetMax = rect.offsetMin = new Vector2 (0, 0);
	}
	public void Update() {

		//Enable our menu canvas group if animator is open
		if (!_animator.GetCurrentAnimatorStateInfo (0).IsName ("Open")) {
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
		} else {
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
		}

	}
}
