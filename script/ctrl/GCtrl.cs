﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using Newtonsoft.Json;
using testCC.Assets.script;
using testCC.Assets.script.ctrl;
using testJava.script.constant;
using testJava.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script {
	public class GCtrl : MonoBehaviour {

		public Camera mainCamera;
		public int rowCardLimitNum = 5;
		public int removeCardNum = 2;
		public G g;
		public UICtrl uICtrl;
		public int srcCardWidth = 130;
		public int srcCardHeight = 162;
		public int srcCardWidthGap = 7;

		void Start () {
			Debug.Log ("start---");
			g = new G ();
			g.ctrl = this;
			g.init ();
			U.g = g;

			uICtrl.init ();

			play ();
		}

		public void play () {
			g.play ();
		}
		public void exit () {
			Debug.Log ("exit---");
			Application.Quit ();
		}
	}
}