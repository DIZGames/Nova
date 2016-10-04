using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script {
    public interface ITest {

        bool Power {
            get;
            set;
        }

        GameObject gameObject1{
            get;
        }

        IUI iUI{
            get;
        }

        void Ping();

    }
}
