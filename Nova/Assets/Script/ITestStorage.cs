using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script {
    public interface ITestStorage : ITest{

        bool StorageSwitch {
            get;
            set;
        }


    }
}
