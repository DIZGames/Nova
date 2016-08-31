using System;
using System.Collections.Generic;

[Serializable]
class SaveFile
{
    public List<SaveGameObject> topLevelObjects = new List<SaveGameObject>();
    public Dictionary<string, string> values = new Dictionary<string, string>();

    public Dictionary<SaveGameObject, SaveGameObject> childObjects = new Dictionary<SaveGameObject, SaveGameObject>();

    [Serializable]
    public class SaveGameObject{
        public List<SaveComponent> components = new List<SaveComponent>();

        public string name;
        public string prefabPath;
        public int layer;
        public string tag;
        public bool isStatic;
        public bool isActive;
    }

    [Serializable]
    public class SaveComponent
    {
        public Dictionary<string, string> values = new Dictionary<string, string>();
    }
}
