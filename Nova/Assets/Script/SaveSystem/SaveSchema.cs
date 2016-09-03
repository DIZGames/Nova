// automatically generated, do not modify

namespace Nova
{

using FlatBuffers;

public sealed class FlatSaveData : Table {
  public static FlatSaveData GetRootAsFlatSaveData(ByteBuffer _bb) { return GetRootAsFlatSaveData(_bb, new FlatSaveData()); }
  public static FlatSaveData GetRootAsFlatSaveData(ByteBuffer _bb, FlatSaveData obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public FlatSaveData __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public FlatGameObject GetGameObjects(int j) { return GetGameObjects(new FlatGameObject(), j); }
  public FlatGameObject GetGameObjects(FlatGameObject obj, int j) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int GameObjectsLength { get { int o = __offset(4); return o != 0 ? __vector_len(o) : 0; } }

  public static Offset<FlatSaveData> CreateFlatSaveData(FlatBufferBuilder builder,
      VectorOffset gameObjects = default(VectorOffset)) {
    builder.StartObject(1);
    FlatSaveData.AddGameObjects(builder, gameObjects);
    return FlatSaveData.EndFlatSaveData(builder);
  }

  public static void StartFlatSaveData(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddGameObjects(FlatBufferBuilder builder, VectorOffset gameObjectsOffset) { builder.AddOffset(0, gameObjectsOffset.Value, 0); }
  public static VectorOffset CreateGameObjectsVector(FlatBufferBuilder builder, Offset<FlatGameObject>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartGameObjectsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<FlatSaveData> EndFlatSaveData(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<FlatSaveData>(o);
  }
  public static void FinishFlatSaveDataBuffer(FlatBufferBuilder builder, Offset<FlatSaveData> offset) { builder.Finish(offset.Value); }
};

public sealed class FlatGameObject : Table {
  public static FlatGameObject GetRootAsFlatGameObject(ByteBuffer _bb) { return GetRootAsFlatGameObject(_bb, new FlatGameObject()); }
  public static FlatGameObject GetRootAsFlatGameObject(ByteBuffer _bb, FlatGameObject obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public FlatGameObject __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Name { get { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; } }
  public string PrefabPath { get { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; } }
  public string Tag { get { int o = __offset(8); return o != 0 ? __string(o + bb_pos) : null; } }
  public int Layer { get { int o = __offset(10); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public bool IsStatic { get { int o = __offset(12); return o != 0 ? 0!=bb.Get(o + bb_pos) : (bool)false; } }
  public bool IsActive { get { int o = __offset(14); return o != 0 ? 0!=bb.Get(o + bb_pos) : (bool)false; } }
  public FlatGameObject GetChildren(int j) { return GetChildren(new FlatGameObject(), j); }
  public FlatGameObject GetChildren(FlatGameObject obj, int j) { int o = __offset(16); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int ChildrenLength { get { int o = __offset(16); return o != 0 ? __vector_len(o) : 0; } }
  public FlatComponent GetComponents(int j) { return GetComponents(new FlatComponent(), j); }
  public FlatComponent GetComponents(FlatComponent obj, int j) { int o = __offset(18); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int ComponentsLength { get { int o = __offset(18); return o != 0 ? __vector_len(o) : 0; } }

  public static Offset<FlatGameObject> CreateFlatGameObject(FlatBufferBuilder builder,
      StringOffset name = default(StringOffset),
      StringOffset prefabPath = default(StringOffset),
      StringOffset tag = default(StringOffset),
      int layer = 0,
      bool isStatic = false,
      bool isActive = false,
      VectorOffset children = default(VectorOffset),
      VectorOffset components = default(VectorOffset)) {
    builder.StartObject(8);
    FlatGameObject.AddComponents(builder, components);
    FlatGameObject.AddChildren(builder, children);
    FlatGameObject.AddLayer(builder, layer);
    FlatGameObject.AddTag(builder, tag);
    FlatGameObject.AddPrefabPath(builder, prefabPath);
    FlatGameObject.AddName(builder, name);
    FlatGameObject.AddIsActive(builder, isActive);
    FlatGameObject.AddIsStatic(builder, isStatic);
    return FlatGameObject.EndFlatGameObject(builder);
  }

  public static void StartFlatGameObject(FlatBufferBuilder builder) { builder.StartObject(8); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(0, nameOffset.Value, 0); }
  public static void AddPrefabPath(FlatBufferBuilder builder, StringOffset prefabPathOffset) { builder.AddOffset(1, prefabPathOffset.Value, 0); }
  public static void AddTag(FlatBufferBuilder builder, StringOffset tagOffset) { builder.AddOffset(2, tagOffset.Value, 0); }
  public static void AddLayer(FlatBufferBuilder builder, int layer) { builder.AddInt(3, layer, 0); }
  public static void AddIsStatic(FlatBufferBuilder builder, bool isStatic) { builder.AddBool(4, isStatic, false); }
  public static void AddIsActive(FlatBufferBuilder builder, bool isActive) { builder.AddBool(5, isActive, false); }
  public static void AddChildren(FlatBufferBuilder builder, VectorOffset childrenOffset) { builder.AddOffset(6, childrenOffset.Value, 0); }
  public static VectorOffset CreateChildrenVector(FlatBufferBuilder builder, Offset<FlatGameObject>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartChildrenVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddComponents(FlatBufferBuilder builder, VectorOffset componentsOffset) { builder.AddOffset(7, componentsOffset.Value, 0); }
  public static VectorOffset CreateComponentsVector(FlatBufferBuilder builder, Offset<FlatComponent>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartComponentsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<FlatGameObject> EndFlatGameObject(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    builder.Required(o, 4);  // name
    builder.Required(o, 6);  // prefabPath
    builder.Required(o, 8);  // tag
    return new Offset<FlatGameObject>(o);
  }
};

public sealed class ValuePair : Table {
  public static ValuePair GetRootAsValuePair(ByteBuffer _bb) { return GetRootAsValuePair(_bb, new ValuePair()); }
  public static ValuePair GetRootAsValuePair(ByteBuffer _bb, ValuePair obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public ValuePair __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Name { get { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; } }
  public string Value { get { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; } }

  public static Offset<ValuePair> CreateValuePair(FlatBufferBuilder builder,
      StringOffset name = default(StringOffset),
      StringOffset value = default(StringOffset)) {
    builder.StartObject(2);
    ValuePair.AddValue(builder, value);
    ValuePair.AddName(builder, name);
    return ValuePair.EndValuePair(builder);
  }

  public static void StartValuePair(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(0, nameOffset.Value, 0); }
  public static void AddValue(FlatBufferBuilder builder, StringOffset valueOffset) { builder.AddOffset(1, valueOffset.Value, 0); }
  public static Offset<ValuePair> EndValuePair(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    builder.Required(o, 4);  // name
    return new Offset<ValuePair>(o);
  }
};

public sealed class FlatComponent : Table {
  public static FlatComponent GetRootAsFlatComponent(ByteBuffer _bb) { return GetRootAsFlatComponent(_bb, new FlatComponent()); }
  public static FlatComponent GetRootAsFlatComponent(ByteBuffer _bb, FlatComponent obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public FlatComponent __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Type { get { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; } }
  public ValuePair GetValues(int j) { return GetValues(new ValuePair(), j); }
  public ValuePair GetValues(ValuePair obj, int j) { int o = __offset(6); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int ValuesLength { get { int o = __offset(6); return o != 0 ? __vector_len(o) : 0; } }

  public static Offset<FlatComponent> CreateFlatComponent(FlatBufferBuilder builder,
      StringOffset type = default(StringOffset),
      VectorOffset values = default(VectorOffset)) {
    builder.StartObject(2);
    FlatComponent.AddValues(builder, values);
    FlatComponent.AddType(builder, type);
    return FlatComponent.EndFlatComponent(builder);
  }

  public static void StartFlatComponent(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddType(FlatBufferBuilder builder, StringOffset typeOffset) { builder.AddOffset(0, typeOffset.Value, 0); }
  public static void AddValues(FlatBufferBuilder builder, VectorOffset valuesOffset) { builder.AddOffset(1, valuesOffset.Value, 0); }
  public static VectorOffset CreateValuesVector(FlatBufferBuilder builder, Offset<ValuePair>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartValuesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<FlatComponent> EndFlatComponent(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<FlatComponent>(o);
  }
};


}
