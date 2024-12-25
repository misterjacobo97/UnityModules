
public class PlayerStateContext {
    private Player _playerRef;
    private PlayerMovementStats _playerMovementStats;
    private PlayerStateManager.PLAYER_STATES _prevState;
    public bool playerIsFastFalling = false;

    public void SetPrevState(PlayerStateManager.PLAYER_STATES state) {
        _prevState = state;
    }

    public PlayerStateContext(PlayerStateManager.PLAYER_STATES prevState, Player player, PlayerMovementStats playerMovementStats){
        _prevState = prevState;
        _playerRef = player;
        _playerMovementStats = playerMovementStats;
    }

    // read-only properties
    public Player Player => _playerRef;
    public PlayerStateManager.PLAYER_STATES PrevState => _prevState;
    public PlayerMovementStats PlayerMovementStats => _playerMovementStats;
}