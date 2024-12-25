using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerState<EState> : BaseState<PlayerStateManager.PLAYER_STATES>
{
    protected PlayerStateContext Context;
    protected PlayerStateManager psm;

    protected BasePlayerState(PlayerStateContext context, PlayerStateManager.PLAYER_STATES stateKey, PlayerStateManager _psm) : base(stateKey) {
        Context = context;
        psm = _psm;
    }



}
