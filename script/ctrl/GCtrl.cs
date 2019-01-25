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

		public CardCtrl cardCtrlPrefab;
		public G g;
		void Start () {
			g = new G ();
			g.ctrl = this;
			g.init ();

			U.g = g;
			play ();
		}

		public void play () {
			g.play ();
		}

	}
}