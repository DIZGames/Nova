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

  public static Offset<FlatGameObject> CreateFlatGameObject(FlatBufferBuilder builder,
      StringOffset name = default(StringOffset),
      StringOffset prefabPath = default(StringOffset),
      StringOffset tag = default(StringOffset),
      int layer = 0,
      bool isStatic = false,
      bool isActive = false) {
    builder.StartObject(6);
    FlatGameObject.AddLayer(builder, layer);
    FlatGameObject.AddTag(builder, tag);
    FlatGameObject.AddPrefabPath(builder, prefabPath);
    FlatGameObject.AddName(builder, name);
    FlatGameObject.AddIsActive(builder, isActive);
    FlatGameObject.AddIsStatic(builder, isStatic);
    return FlatGameObject.EndFlatGameObject(builder);
  }

  public static void StartFlatGameObject(FlatBufferBuilder builder) { builder.StartObject(6); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(0, nameOffset.Value, 0); }
  public static void AddPrefabPath(FlatBufferBuilder builder, StringOffset prefabPathOffset) { builder.AddOffset(1, prefabPathOffset.Value, 0); }
  public static void AddTag(FlatBufferBuilder builder, StringOffset tagOffset) { builder.AddOffset(2, tagOffset.Value, 0); }
  public static void AddLayer(FlatBufferBuilder builder, int layer) { builder.AddInt(3, layer, 0); }
  public static void AddIsStatic(FlatBufferBuilder builder, bool isStatic) { builder.AddBool(4, isStatic, false); }
  public static void AddIsActive(FlatBufferBuilder builder, bool isActive) { builder.AddBool(5, isActive, false); }
  public static Offset<FlatGameObject> EndFlatGameObject(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    builder.Required(o, 4);  // name
    builder.Required(o, 6);  // prefabPath
    builder.Required(o, 8);  // tag
    return new Offset<FlatGameObject>(o);
  }
};


}
