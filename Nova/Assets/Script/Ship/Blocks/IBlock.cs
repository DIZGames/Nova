
public interface IBlock{

    BlockPosition Position { get; }
    bool CreatesNewShip { get; } // if true ShipPartPosiion should be set to Center
}
