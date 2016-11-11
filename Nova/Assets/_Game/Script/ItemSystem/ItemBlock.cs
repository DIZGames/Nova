
using Assets.Script.Ship;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.ItemSystem {
    public class ItemBlock : ItemBase{

        public int maxHitPoints;
        public int Energy;
        public int Oxygen;

        public int currentHitPoints;

        [Header("BlockBuilding")]
        public int buildLevel;
        public bool createsNewShip;
        public BlockPosition position;
        //public AttachableRay attachedRay; 
        public List<AttachableRay> attachableRaysForward;
        public List<AttachableRay> attachableRaysUp;
        public List<AttachableRay> attachableRaysDown;
        public List<AttachableRay> attachableRaysRight;
        public List<AttachableRay> attachableRaysLeft;

        public override ItemBase Clone() {
            return Instantiate(this) as ItemBlock;
        }
    }
}
