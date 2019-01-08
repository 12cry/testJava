﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using Newtonsoft.Json;
using testCC.Assets.script;
using testCC.Assets.script.ctrl;
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
			this.run ();
		}

		public void run () {
			print ("---run");
			if (g.over) {
				print ("---over");
				return;
			}
			g.computeCurrentCards ();
			g.showCurrentCards ();

		}
		public void reset () {
			print ("---reset");
			g.over = false;
			g.init ();
		}

		public void test1 () {
			print ("---test1");
		}

	}
}