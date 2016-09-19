
using Assets.Script.Ship;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.ItemSystem {
    public class ItemBlock : ItemBase{

        public BlockPosition position;
        public bool createsNewShip; 
        public int maxHitPoints;
        public int Energy;
        public int Oxygen;

        public int currentHitPoints;

        [Header("BlockBuilding")]
        public int buildLevel;
        public AttachableRay attachedRay; 
        public List<AttachableRay> attachableRaysForward;
        public List<AttachableRay> attachableRaysUp;
        public List<AttachableRay> attachableRaysDown;
        public List<AttachableRay> attachableRaysRight;
        public List<AttachableRay> attachableRaysLeft;

        public override ItemBase Clone() {
            return Instantiate(this) as ItemBlock;
        }

        //public bool IsCenter()
        //{
        //    return position == BlockPosition.CENTER || position == BlockPosition.CENTER_BOTTOM
        //        || position == BlockPosition.CENTER_MIDDLE || position == BlockPosition.CENTER_TOP;
        //}

        //public bool IsBetween()
        //{
        //    return position == BlockPosition.BETWEEN || position == BlockPosition.BETWEEN_FLOOR
        //        || position == BlockPosition.BETWEEN_MIDDLE || position == BlockPosition.BETWEEN_TOP;
        //}


    }

}
