using System;

public interface IStateHandler<TState> where TState : Enum
{
    event Action<TState> StateChanged;
}