using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class TerminalContainer {

        public string name;
        public Sprite icon;
        public Transform transform;

        public TerminalContainer(string _name, Sprite _icon, Transform _transform) {
            name = _name;
            icon = _icon;
            transform = _transform;
        }

    }
}
