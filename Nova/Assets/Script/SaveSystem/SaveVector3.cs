using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class SaveVector3
{
    public float A { get; set; }
    public float B { get; set; }
    public float C { get; set; }

    public SaveVector3(float a, float b, float c)
    {
        A = a;
        B = b;
        C = c;
    }
}
