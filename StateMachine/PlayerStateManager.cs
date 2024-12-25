using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerStateManager : BaseStateManager<PlayerStateManager.PLAYER_STATES>
{
    public enum PLAYER_STATES {
        IDLE,
        RUNNING,
        DASHING,
        JUMPING,
        FALLING,
        CLIMBING
    }

    private PlayerStateContext _context;

    [SerializeField] private Player PlayerRef;
    // public PlayerMovementStats PlayerRefMovementStats;

    public void Init(Player newPlayerRef){
        PlayerRef = newPlayerRef;
    }

    void Awake(){
        ValidateReferences();

        _context = new PlayerStateContext(InitialState, PlayerRef, PlayerRef.moveStats);

        InitiateStates();
    }


    private void ValidateReferences(){
        Assert.IsNotNull(PlayerRef, "PlayerRef is not assigned");
    }

    private void InitiateStates(){
        // add states to the dictionary and assign their enum
        States.Add(PLAYER_STATES.IDLE, new PlayerIdleState(_context, PLAYER_STATES.IDLE, this));
        States.Add(PLAYER_STATES.RUNNING, new PlayerRunningState(_context, PLAYER_STATES.RUNNING, this));
        States.Add(PLAYER_STATES.JUMPING, new PlayerJumpingState(_context, PLAYER_STATES.JUMPING, this));
        States.Add(PLAYER_STATES.FALLING, new PlayerFallingState(_context, PLAYER_STATES.FALLING, this));
        States.Add(PLAYER_STATES.DASHING, new PlayerDashingState(_context, PLAYER_STATES.DASHING, this));
        States.Add(PLAYER_STATES.CLIMBING, new PlayerClimbingState(_context, PLAYER_STATES.CLIMBING, this));

        CurrentState = States[InitialState];
    } 
}