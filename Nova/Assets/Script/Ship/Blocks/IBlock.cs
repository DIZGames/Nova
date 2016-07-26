
public interface IBlock{

    BlockPosition position { get; }
    bool createsNewShip { get; } // if true ShipPartPosiion should be set to Center
}
